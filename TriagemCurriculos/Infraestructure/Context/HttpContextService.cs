namespace TriagemCurriculos.Infraestructure
{
    public class HttpContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextService(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        public IHeaderDictionary? GetHeaderDictionary() => _httpContextAccessor.HttpContext?.Request.Headers;

    }
}
