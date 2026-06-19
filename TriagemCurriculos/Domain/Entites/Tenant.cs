using TriagemCurriculos.Domain.Enums;

namespace TriagemCurriculos.Domain.Entites
{
    

    public class Tenant
    {
        public string Id { get; private set; } 
        public string CompanyName { get; private set; }
        public PlanTier PlanTier { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public virtual ICollection<JobPosition> JobPositions { get; private set; } = new List<JobPosition>();
        public virtual ICollection<Candidate> Candidates { get; private set; } = new List<Candidate>();

        private Tenant() { }
        public Tenant(string id, string companyName, PlanTier planTier = PlanTier.Free)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("O ID do Tenant é obrigatório.");
            if (string.IsNullOrWhiteSpace(companyName)) throw new ArgumentException("O nome da empresa é obrigatório.");

            Id = id.ToLower().Trim();
            CompanyName = companyName;
            PlanTier = planTier;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdatePlan(PlanTier newTier)
        {
            PlanTier = newTier;
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public void Activate()
        {
            IsActive = true;
        }
    }
}
