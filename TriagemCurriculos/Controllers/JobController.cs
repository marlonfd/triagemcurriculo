using Microsoft.AspNetCore.Mvc;
using TriagemCurriculos.Domain.Entites;

namespace TriagemCurriculos.Controllers
{    
    [Route("jobs")]
    [ApiController]
    public class JobController : ControllerBase
    {

        [HttpGet()]
        public IEnumerable<JobPosition> GetJobPositions()
        {
            
            return null;
        }
    }
}
