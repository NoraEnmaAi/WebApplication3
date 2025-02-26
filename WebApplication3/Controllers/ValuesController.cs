using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.IO.Compression;
using Microsoft.AspNetCore.Hosting;


namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };


        public ValuesController()
        {

        }

        [HttpGet("testing")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpGet("write/{content}")]
        public IActionResult Write(string content)
        {
            var wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var filePath = Path.Combine(wwwRootPath, "fileName.txt");
            System.IO.File.AppendAllText(filePath, "\n"+content);

            return Ok("Done edit by"+ " "+content);
        }

        [HttpGet("download")]
        public IActionResult Download()
        {

            var wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var zipFileName = "wwwroot_backup.zip";
            var zipFilePath = Path.Combine(Directory.GetCurrentDirectory(), zipFileName);

            // Delete old zip file if exists
            if (System.IO.File.Exists(zipFilePath))
            {
                System.IO.File.Delete(zipFilePath);
            }

            // Create a new ZIP file from wwwroot
            ZipFile.CreateFromDirectory(wwwRootPath, zipFilePath);

            // Read the ZIP file bytes
            var fileBytes = System.IO.File.ReadAllBytes(zipFilePath);

            // Return the ZIP file as a downloadable response
            return File(fileBytes, "application/zip", zipFileName);
        }


    }
}
