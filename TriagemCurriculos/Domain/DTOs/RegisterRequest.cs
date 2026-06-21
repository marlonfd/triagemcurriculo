namespace TriagemCurriculos.Domain.DTOs
{
    public class RegisterRequest
    {
        public string? TenantId { get; set; }
        public string? CompanyName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleTypeId { get; set; }
    }
}
