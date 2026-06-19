using Microsoft.EntityFrameworkCore;
using RecrutamentoIA.Domain.Common;
using System.Linq.Expressions;
using System.Reflection;
using TriagemCurriculos.Infraestructure.Data;

namespace TriagemCurriculos.Infraestructure
{
    public class MainDbContext : DbContext
    {
        private readonly string _currentTenantId;

        public MainDbContext(DbContextOptions<MainDbContext> options, ITenantProvider tenantProvider) : base(options)
        {
            _currentTenantId = tenantProvider.GetTenantId();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Aplica as configurações normais (tabelas, colunas, índices)
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // 2. MÁGICA GENÉRICA: Varre todas as classes mapeadas no banco
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // Verifica se a classe da entidade implementa a interface ITenantEntity
                if (typeof(ITenantEntity).IsAssignableFrom(entityType.ClrType))
                {
                    // Constrói a expressão de filtro dinamicamente: e => e.TenantId == _currentTenantId
                    var filterExpression = CreateTenantFilterExpression(entityType.ClrType);

                    // Aplica o HasQueryFilter na entidade de forma genérica
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filterExpression);
                }
            }
        }
        // Método auxiliar sênior para construir a expressão LINQ em tempo de execução
        private LambdaExpression CreateTenantFilterExpression(Type entityType)
        {
            // Cria o parâmetro da expressão, ex: (x => ...)
            var parameter = Expression.Parameter(entityType, "x");

            // Cria o acesso à propriedade, ex: (x.TenantId)
            var property = Expression.Property(parameter, nameof(ITenantEntity.TenantId));

            // Cria a referência para a variável local do DbContext, ex: (_currentTenantId)
            var currentTenantIdProperty = Expression.Property(Expression.Constant(this), nameof(_currentTenantId));

            // Cria a comparação de igualdade, ex: (x.TenantId == _currentTenantId)
            var comparison = Expression.Equal(property, currentTenantIdProperty);

            // Transforma tudo em uma Lambda Expression funcional: x => x.TenantId == _currentTenantId
            return Expression.Lambda(comparison, parameter);
        }
    }
}
