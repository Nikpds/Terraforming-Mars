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
        public IActionResult Register([FromBody]User user)
        {
            try
            {
                //check for email duplicates
                var mailfilter = Builders<User>.Filter.Eq(x => x.Email, user.Email.ToLower());
                var mailcursor = _db.Users.Collection.Find(mailfilter);
                var mailExists = mailcursor.Any();
                if (mailExists)
                {
                    return BadRequest("Email already exists");
                }
                //insert new user
                user.Email = user.Email.ToLower();
                user.EmailConfirmed = false;
                user.PasswordHash = AuthManager.HashPassword(user.Password);
                user.VerificationToken = string.Empty;

                //To do initiate email validation
                user = _db.Users.Insert(user);
                user.PasswordHash = null;
                return Ok(user);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
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

        [HttpPost("external")]
        public IActionResult GoogleOrFacebook([FromBody]User user)
        {
            try
            {
                //check for email duplicates
                var exists = _db.Users.Get(x => x.Email.ToLower() == user.Email.ToLower()).FirstOrDefault();

                if (exists == null)
                {
                    user.Email = user.Email.ToLower();
                    user.EmailConfirmed = true;
                    user.ExternaLogin = true;
                    user.IsActive = true;
                    user.PasswordHash = string.Empty;
                    user.VerificationToken = string.Empty;
                    user = _db.Users.Insert(user);
                    var userToken = _auth.CreateToken(user);
                    return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(userToken) });
                }
                else
                {
                    var userToken = _auth.CreateToken(exists);
                    return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(userToken) });
                }
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }
    }
}
