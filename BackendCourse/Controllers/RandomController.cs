using BackendCourse.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendCourse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandomController : ControllerBase
    {
        private readonly IRandomService _randomSingleton;
        private readonly IRandomService _randomScoped;
        private readonly IRandomService _randomTransinet;
        private readonly IRandomService _randomSingleton2;
        private readonly IRandomService _randomScoped2;
        private readonly IRandomService _randomTransinet2;

        public RandomController([FromKeyedServices("randomSingleton")] IRandomService randomSingleton,
                                [FromKeyedServices("randomScoped")]IRandomService randomScoped,
                                [FromKeyedServices("randomTransinet")] IRandomService randomTransinet,
                                [FromKeyedServices("randomSingleton")] IRandomService randomSingleton2,
                                [FromKeyedServices("randomScoped")]IRandomService randomScoped2,
                                [FromKeyedServices("randomTransinet")] IRandomService randomTransinet2)

        { 

            _randomSingleton = randomSingleton;
            _randomScoped = randomScoped;
            _randomTransinet = randomTransinet;
            _randomSingleton2 = randomSingleton2;
            _randomScoped2 = randomScoped2;
            _randomTransinet2 = randomTransinet2;
        }


        [HttpGet]
        public ActionResult<Dictionary<string, int>> Get() 
        {
        
            var result = new Dictionary<string, int>();

            result.Add("singleton1",_randomSingleton.Value);
            result.Add("scoped1",_randomScoped.Value);
            result.Add("transient1",_randomTransinet.Value);
            
            result.Add("singleton2",_randomSingleton2.Value);
            result.Add("scoped2",_randomScoped2.Value);
            result.Add("trasient2",_randomTransinet2.Value);

            return result;
        
        }


    }
}
