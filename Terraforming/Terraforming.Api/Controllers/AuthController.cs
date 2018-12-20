using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Terraforming.Api.Authorization;
using Terraforming.Api.Database;
using Terraforming.Api.Models;

namespace Terraforming.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private DataContext _db;
        private readonly IAuthenticationProvider _auth;
        public AuthController(DataContext db, IAuthenticationProvider auth)
        {
            _db = db;
            _auth = auth;
        }

        [HttpGet]
        public IActionResult Test()
        {
            return Ok(DateTime.Now);
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
                user.VerificationToken = string.Empty;

                //To do initiate email validation
                user = await _db.Users.Insert(user);
                user.PasswordHash = null;
                return Ok(user);
            }
            catch (Exception exc)
            {
                return this.BadRequest(exc.Message);
            }
        }

        [HttpPost("token")]
        public IActionResult Token([FromBody] LoginViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = _db.Users.Get(x => x.Email == model.Username).FirstOrDefault();
                    if (user != null)
                    {
                        if (AuthManager.VerifyHashedPassword(user.PasswordHash, model.Password))
                        {
                            var userToken = _auth.CreateToken(user);
                            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(userToken) });
                        }
                        else
                        {
                            return BadRequest("Λάθος όνομα χρήστη ή κωδικός");
                        }
                    }
                    else
                    {
                        return BadRequest("Λάθος όνομα χρήστη ή κωδικός");
                    }
                }
                else
                {
                    return BadRequest("Λάθος όνομα χρήστη ή κωδικός");
                }

            }
            catch (Exception exc)
            {
                return BadRequest("Σφάλμα στην επιβεβαίωση στοιχείων");
            }
        }
    }
}
