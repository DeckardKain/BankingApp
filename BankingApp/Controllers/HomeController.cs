using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BankingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public HomeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        //
        [HttpPost]
        public async Task<IActionResult> CreateNewUser([FromBody] object data)
        {
            _httpClient.BaseAddress = new Uri("https://localhost:7176/api");

            var response = await _httpClient.PostAsJsonAsync("newuser", data);

            if (response.IsSuccessStatusCode)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        // GET: api/<controll>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controll>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controll>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controll>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controll>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
