using Freelancers.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Security.Cryptography.Xml;
using System.Text;

namespace Freelancers.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

        }

        //Get
        public async Task<IActionResult> IndexAsync()
        {
            List<Users> response = new List<Users>();
            HttpClient client = new HttpClient();
            //you need to change your port number here 
            string url = Convert.ToString("http://108.181.168.224/demo/api/developers");
            client.BaseAddress = new Uri(url);

            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage eventresponseMessage = await client.GetAsync(url);

            if (eventresponseMessage.IsSuccessStatusCode)
            {
                string responseData = eventresponseMessage.Content.ReadAsStringAsync().Result;
                if (responseData != null)
                {
                   
                    response = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Users>>(responseData);
                  
                }


                TempData["AlertMessage"] = "User Added Successfully";
                return View(response);
            }
            return View();
        }

        //Get
        public IActionResult Delete()
        {
            return View();
        }
        //Post
        public IActionResult Update()
        {
            return View();
        }

       [HttpPost]
        public async Task<IActionResult> AddUserAsync(IFormCollection collection)
        {
            if (collection != null)
            {
                Users response = new Users();
                AddUserRequest addUserRequest = new AddUserRequest();
                AddUserRequest request = new AddUserRequest();
                request.Username = collection["txtUsername"].ToString();
                request.Mail = collection["Mail"].ToString();
                request.Phonenumber = collection["Phonenumber"].ToString();
                request.SkillSets = collection["SkillSets"].ToString();
                request.Hobby = collection["Hobby"].ToString();
                string json = JsonConvert.SerializeObject(request);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
//                Dictionary<string, string> values = new Dictionary<string, string>
//{
//    { "Username", collection["txtUsername"].ToString() },
//    { "Mail", collection["txtEmail"].ToString() },
//    { "Phonenumber", collection["txtPhone"].ToString() },
//    { "SkillSets",  collection["txtSkillset"].ToString() },
//    { "Hobby", collection["txtHobby"].ToString() }

//};


                //FormUrlEncodedContent content = new FormUrlEncodedContent(values);
                HttpClient client = new HttpClient();
                //you need to change your port number here 
                string url = Convert.ToString("http://localhost:5150/api/developers");
                client.BaseAddress = new Uri(url);

                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage eventresponseMessage = await client.PostAsync(url, content);

                if (eventresponseMessage.IsSuccessStatusCode)
                {
                    string responseData = eventresponseMessage.Content.ReadAsStringAsync().Result;
                    if (responseData != null)
                    {
                        addUserRequest = JsonConvert.DeserializeObject<AddUserRequest>(responseData);
                    }


                    TempData["AlertMessage"] = "User Added Successfully";
                    return RedirectToAction("IndexAsync");
                }
                else
                {
                    TempData["AlertMessage"] = "Fail to add user";
                    return View(null);
                }
            }

            else
            {
                TempData["AlertMessage"] = "Please Fill Details";
                return View(null);

            }
           
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}