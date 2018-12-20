using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore;
using System.Threading.Tasks;
using Terraforming.Api.Authorization;
using Terraforming.Api.Database;
using Terraforming.Api.Models;

namespace Terraforming.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private DataContext _db;
        public UserController(DataContext db)
        {
            _db = db;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody]User user)
        {
            try
            {
                //check for email duplicates
                var mailfilter = Builders<User>.Filter.Eq(x => x.Email, user.Email.ToLower());
                var mailcursor = await _db.Users.Collection.FindAsync(mailfilter);
                var mailExists = await mailcursor.AnyAsync();
                if (mailExists)
                {
                    return this.BadRequest("Email already exists");
                }

                //insert new user
                user.Email = user.Email.ToLower();
                user.EmailConfirmed = false;
                user.PasswordHash = AuthManager.HashPassword(user.Password);
                user.VerificationToken = (Guid.NewGuid()).ToString();

                //To do initiate email validation


                user = await _db.Users.Insert(user);

                return this.Ok(user);
            }
            catch (Exception exc)
            {
                return this.BadRequest(exc.Message);
            }
        }
    }
}
