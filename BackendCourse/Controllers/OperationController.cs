using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace BackendCourse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        [HttpGet]
        public decimal Get(decimal a, decimal b) { 
        
            return a + b;
        
        }

        [HttpPost]
        public decimal Add( Numbers numbers, 
                            [FromHeader] string host, 
                            [FromHeader (Name ="Content-Length")] string ContentLength,
                            [FromHeader(Name ="X-some")] string some)
        {
            Console.WriteLine(host);
            Console.WriteLine(ContentLength);
            Console.WriteLine(some);
            return numbers.A - numbers.B;

        }

        [HttpPut]
        public decimal Edit(decimal a, decimal b)
        {

            return a * b;

        }


        [HttpDelete]
        public decimal Delete(decimal a, decimal b)
        {

            return a / b;

        }

    }

    public class Numbers
    {
        public decimal A { get; set; }
        public decimal B { get; set; }
    }


}
