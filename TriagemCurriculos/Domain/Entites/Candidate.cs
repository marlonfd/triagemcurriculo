using RecrutamentoIA.Domain.Common;
using System.Text.Json;

namespace TriagemCurriculos.Domain.Entites
{
    public class Candidate:ITenantEntity
    {
        public int Id { get; private set; }
        public string TenantId { get; private set; }
        public long JobPositionId { get; private set; }
        public int StatusTypeId { get; private set; }
        public string CandidateName { get; private set; }
        public string CandidateEmail { get; private set; }

        public string ExtractedSkillsJson { get; private set; }

        public int? AiMatchScore { get; private set; }
        public string AiAnalysisSummary { get; private set; }
        public string ResumeFileUrl { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public virtual Tenant Tenant { get; private set; }
        public virtual JobPosition JobPosition { get; private set; }
        public virtual SystemType StatusType { get; private set; }

        public IReadOnlyCollection<string> ExtractedSkills =>
            string.IsNullOrEmpty(ExtractedSkillsJson)
                ? Array.Empty<string>()
                : JsonSerializer.Deserialize<List<string>>(ExtractedSkillsJson) ?? new List<string>();

        private Candidate() { }

        public Candidate(string tenantId, long jobPositionId, int statusTypeId, string candidateName, string candidateEmail, string resumeFileUrl)
        {
            TenantId = tenantId;
            JobPositionId = jobPositionId;
            StatusTypeId = statusTypeId;
            CandidateName = candidateName ?? throw new ArgumentNullException(nameof(candidateName));
            CandidateEmail = candidateEmail ?? throw new ArgumentNullException(nameof(candidateEmail));
            ResumeFileUrl = resumeFileUrl;
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdateAiAnalysis(int score, List<string> skills, string summary)
        {
            if (score < 0 || score > 100)
                throw new ArgumentOutOfRangeException(nameof(score), "O score da IA deve ser entre 0 e 100.");

            AiMatchScore = score;
            AiAnalysisSummary = summary;
            ExtractedSkillsJson = JsonSerializer.Serialize(skills);
        }

        public void UpdateStatus(int newStatusTypeId)
        {
            StatusTypeId = newStatusTypeId;
        }
    }
}
