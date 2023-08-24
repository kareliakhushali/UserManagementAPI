using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserCrudAPI.Data;
using UserCrudAPI.Models;

namespace UserCrudAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UserController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public UserController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _appDbContext.Users.ToListAsync();
            return Ok(employees);
        }
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User userRequest)
        {
            //here we are generating here the id by ourselves
            userRequest.Id = Guid.NewGuid();
            await _appDbContext.Users.AddAsync(userRequest);
            await _appDbContext.SaveChangesAsync();
            return Ok(userRequest);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, User updateUserRequest)
        {
            var user = await _appDbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            user.FirstName = updateUserRequest.FirstName;
            user.LastName = updateUserRequest.LastName;
            user.Email = updateUserRequest.Email;
            user.Gender = updateUserRequest.Gender;
            user.Hobbies = updateUserRequest.Hobbies;
            user.DateOfBirth = updateUserRequest.DateOfBirth;
            user.ImageUpload = updateUserRequest.ImageUpload;

            await _appDbContext.SaveChangesAsync();

            return Ok(user);
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            var user = await _appDbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            _appDbContext.Users.Remove(user);
            await _appDbContext.SaveChangesAsync();
            return Ok(user);

        }



    }
}
