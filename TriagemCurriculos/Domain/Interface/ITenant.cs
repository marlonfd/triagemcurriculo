namespace RecrutamentoIA.Domain.Common;

public interface ITenantEntity
{
    string TenantId { get; }
}

public abstract class BaseEntity<TId>;