using System.ComponentModel.DataAnnotations;

namespace TriagemCurriculos.Domain.DTOs
{
    public class CandidateRequestDTO
    {
        [Required]
        public long JobPositionId { get; set; }
        
        [Required]
        public int StatusTypeId { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string CandidateName { get; set; }
        
        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string CandidateEmail { get; set; }
        
        [MaxLength(255)]
        public string? ResumeFileUrl { get; set; }
    }
}
