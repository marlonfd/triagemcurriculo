using RecrutamentoIA.Domain.Common;

namespace TriagemCurriculos.Domain.Entites
{
    public class JobPosition:ITenantEntity
    {
        public int Id { get; set; }
        public string TenantId { get; private set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

        // Propriedades de Navegação do ORM
        public virtual Tenant Tenant { get; private set; }
        public virtual ICollection<Candidate> Candidates { get; private set; } = new List<Candidate>();

        private JobPosition() { }

        public JobPosition(string tenantId, string title, string description)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("O título da vaga é obrigatório.");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("A descrição da vaga é obrigatória.");

            TenantId = tenantId;
            Title = title;
            Description = description;
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdateDetails(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}
