using RecrutamentoIA.Domain.Common;

namespace TriagemCurriculos.Domain.Entites
{
    public class User : ITenantEntity
    {
        public long Id { get; private set; }
        public string TenantId { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public int RoleTypeId { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public virtual Tenant Tenant { get; private set; }
        public virtual SystemType RoleType { get; private set; }

        private User() { }

        public User(string tenantId, string name, string email, string passwordHash, int roleTypeId)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("O nome é obrigatório.");
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("O email é obrigatório.");
            if (string.IsNullOrWhiteSpace(passwordHash)) throw new ArgumentException("A senha é obrigatória.");

            TenantId = tenantId;
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            RoleTypeId = roleTypeId;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        public void Deactivate() => IsActive = false;
        public void Activate() => IsActive = true;
    }
}
