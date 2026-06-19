using TriagemCurriculos.Infraestructure.Interface;

namespace TriagemCurriculos.Infraestructure.Provider
{
    public class TenantProvider : ITenantProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string? _manualTenantId;
        public TenantProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetTenantId()
        {
            if (!string.IsNullOrEmpty(_manualTenantId))
            {
                return _manualTenantId;
            }
            var context = _httpContextAccessor.HttpContext;
            if (context != null)
            {
                if (context.Request.Headers.TryGetValue("X-Tenant-Id", out var tenantHeader))
                {
                    return tenantHeader.ToString();
                }

            }
            // Para rotas como Login/Register (ou sem autenticação), pode não haver Tenant.
            // Retornamos string vazia para o DbContext conseguir instanciar sem quebrar.
            return string.Empty;
        }
        public void SetTenantId(string tenantId)
        {
            _manualTenantId = tenantId;
        }
    }
}
