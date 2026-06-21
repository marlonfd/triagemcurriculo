namespace TriagemCurriculos.Domain.DTOs
{
    public class CandidateResponseDTO
    {
        public long Id { get; set; }
        public string TenantId { get; set; }
        public long JobPositionId { get; set; }
        public int StatusTypeId { get; set; }
        public string CandidateName { get; set; }
        public string CandidateEmail { get; set; }
        
        public IReadOnlyCollection<string> ExtractedSkills { get; set; }
        public int? AiMatchScore { get; set; }
        public string? AiAnalysisSummary { get; set; }
        
        public string? ResumeFileUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
