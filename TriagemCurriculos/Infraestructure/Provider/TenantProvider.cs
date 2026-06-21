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
                // Tenta pegar do Token JWT primeiro
                if (context.User?.Identity?.IsAuthenticated == true)
                {
                    var tenantClaim = context.User.FindFirst("tenant_id")?.Value;
                    if (!string.IsNullOrEmpty(tenantClaim))
                    {
                        return tenantClaim;
                    }
                }

                // Fallback para o Header (usado no cadastro de tenant/usuário, ou rotas públicas)
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
