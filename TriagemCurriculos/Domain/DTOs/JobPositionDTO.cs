namespace TriagemCurriculos.Domain.DTOs
{
    public class JobPositionRequestDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }

    }
    public class JobPositionResponseDTO
    {
        public long Id { get; set; }
        public string TenantId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
