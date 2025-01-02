using BackendCourse.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendCourse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase

    {
        private readonly IPeopleService _peopleService;

        public PeopleController(IPeopleService peopleService)
        {
            _peopleService = peopleService;
        }



        [HttpGet("all")]
        public List<People> GetAll() => Repositoty.People;




        [HttpGet("getbyid/{id}")]
        public ActionResult<People> GetbyId( int id)
        {
           var people = Repositoty.People.FirstOrDefault(p => p.Id == id);

            if (people is null)
            {
                return NotFound();
            }
            return Ok(people);

        }




        [HttpGet("search/{search}")]
        public List<People> Search(string search) => Repositoty.People.Where(p => p.Name.Contains(search))
            .ToList();



        //[HttpPost]
        //public IActionResult add(People people) {

        //    if (string.IsNullOrEmpty(people.Name))
        //    {
        //        return NotFound();
        //    }

        //    Repositoty.People.Add(people);
        //    return NoContent();
        
        
        //}

        [HttpPost]
        public IActionResult add(People people)
        {

            if (!_peopleService.Validate(people))
            {
                return BadRequest();
            }

            Repositoty.People.Add(people);
            return NoContent();


        }

    }




    public class Repositoty
    {
        public static List<People> People = new List<People>
        {
            new People()
            {
                Id = 1,Name="edwin", Birthdate=new DateTime(1984,05,18)
            },
            new People()
            {
                Id=2,Name="angelica", Birthdate=new DateTime(1979,05,29)
            },
            new People()
            {
                Id=3,Name="samuel", Birthdate = new DateTime(2018,06,12)
            }
        };



    }



    public class People
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime Birthdate { get; set; }

    }


}
