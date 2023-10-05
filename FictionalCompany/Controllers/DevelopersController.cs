using FictionalCompany.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FictionalCompany.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DevelopersController : Controller
    {

        private readonly UserContext _userContext;

        public DevelopersController(UserContext userContext)
        {
            _userContext = userContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _userContext.Users.ToListAsync());
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetUser([FromRoute] int id, UpdateUserRequest updateUserRequest)
        {
            var user = await _userContext.Users.FindAsync(id);
            if(user == null)
            {

                return NotFound();
            }
            return Ok(user);

        }
        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserRequest addUserRequest)
        {
            if (addUserRequest != null)
            {
                var user = new Users()
                {

                    Username = addUserRequest.Username,
                    Mail = addUserRequest.Mail,
                    Phonenumber = addUserRequest.Phonenumber,
                    SkillSets = addUserRequest.SkillSets,
                    Hobby = addUserRequest.Hobby
                };
                await _userContext.Users.AddAsync(user);
                await _userContext.SaveChangesAsync();

                return Ok(addUserRequest);
            }
            else
            { return BadRequest(); }
            
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id, UpdateUserRequest updateUserRequest)
        {
            var user = _userContext.Users.Find(id);
            if (user != null)
            {
                user.Username = updateUserRequest.Username;
                user.Mail = updateUserRequest.Mail;
                user.Phonenumber = updateUserRequest.Phonenumber;
                user.SkillSets = updateUserRequest.SkillSets;
                user.Hobby = updateUserRequest.Hobby;
                await _userContext.SaveChangesAsync();
                return Ok(user);
            }
            return NotFound();
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async  Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            var user = await _userContext.Users.FindAsync(id);
            if (user != null)
            {
                _userContext.Remove(user);
                await _userContext.SaveChangesAsync();
                return Ok(user);
            }
            return NotFound();

        }
    }
}
