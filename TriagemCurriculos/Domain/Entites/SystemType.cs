namespace TriagemCurriculos.Domain.Entites
{
    public class SystemType
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Code { get; set; }
        public string DisplayName { get; set; }
        public bool IsActive { get; set; }

        private SystemType() { }

        public SystemType(string category, string code, string displayName)
        {
            Category = category ?? throw new ArgumentNullException(nameof(category));
            Code = code ?? throw new ArgumentNullException(nameof(code));
            DisplayName = displayName ?? throw new ArgumentNullException(nameof(displayName));
            IsActive = true;
        }

        public void UpdateDisplayName(string newDisplayName)
        {
            if (string.IsNullOrWhiteSpace(newDisplayName)) throw new ArgumentException("Nome de exibição inválido.");
            DisplayName = newDisplayName;
        }
    }
}
