using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BackendCourse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SyncController : ControllerBase
    {
        [HttpGet("sync")]
        public IActionResult GetSync()
        {

            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();

            Thread.Sleep(1000);
            Console.WriteLine("conexión a base de datos terminada.");


            Thread.Sleep(1000);
            Console.WriteLine("envío de email terminado.");

            Console.WriteLine("todo ha terminado.");

            stopwatch.Stop();

            return Ok(stopwatch.Elapsed);
        }

        [HttpGet("async")]
        public async Task<IActionResult> GetAsync()
        {
            Stopwatch watch = Stopwatch.StartNew();
            watch.Start();

            var task1 = new Task<int>(() =>
            {

                Thread.Sleep(1000);
                Console.WriteLine("conexión a base de datos terminada.");
                return 1;

            });

            var task2 = new Task<int>(() =>
            {

                Thread.Sleep(1000);
                Console.WriteLine("Enviando emails...");
                return 2;

            });

            task1.Start();
            task2.Start();

            Console.WriteLine("otra tarea.");

            var resultTask1 = await task1;
            var resultTask2 = await task2;

            Console.WriteLine("todo ha terminado");
            watch.Stop();

            return Ok(resultTask1+" "+ resultTask2 + " " + watch.Elapsed);

        }
    }
}
