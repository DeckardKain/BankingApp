using BankingApp.DTO.UI; // cheating here a little :)
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using BankingAppBackEnd.Data;
using BankingAppCore.Models.Customers;
using BankingAppBackEnd.Factories;

namespace BankingAppBackEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly CustomerFactory _customerFactory;

        public HomeController(HttpClient httpClient, CustomerFactory customerFactory)
        {
            _httpClient = httpClient;
            _customerFactory = customerFactory;
        }

        [HttpPost("registernewcustomer")]
        public IActionResult HandleNewUserPostRequest([FromBody] JsonElement data)
        {
            try
            {                
                var userDTO = JsonSerializer.Deserialize<UserRegisterDTO>(data.GetRawText());
                if (userDTO == null)
                {
                    return BadRequest("Invalid JSON payload.");
                }
                // Does ModelState contain any errors or validation failures?
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Create new customer

                var newCustomer = _customerFactory.CreateNewCustomerRegistration(userDTO);

                return Ok();


            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message} Stack Trace: {ex.StackTrace}");

                return StatusCode(500, "An error occured while processing the request.");
            }


            
        }


        // GET: HomeController
        public ActionResult Index()
        {
            return View();
        }

        // GET: HomeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
