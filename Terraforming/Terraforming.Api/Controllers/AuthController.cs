using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Terraforming.Api.Authorization;
using Terraforming.Api.Database;
using Terraforming.Api.Models;

namespace Terraforming.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private MsDataContext _db;
        private readonly IAuthenticationProvider _auth;
        public AuthController(MsDataContext db, IAuthenticationProvider auth)
        {
            _db = db;
            _auth = auth;
        }

        [HttpPost]
        public IActionResult Register([FromBody]User user)
        {
            try
            {
                //check for email duplicates
                var emailExists = _db.Users.FirstOrDefault(x => x.Email == user.Email);
                var usernameExists = _db.Users.FirstOrDefault(x => x.Nickname == user.Nickname);
                if (emailExists != null)
                {
                    return BadRequest("Email already exists");
                }
                if (usernameExists != null)
                {
                    return BadRequest("Username already exists");
                }
                //insert new user
                user.Email = user.Email.ToLower();
                user.IsActive = false;
                user.EmailConfirmed = false;
                user.PasswordHash = AuthManager.HashPassword(user.Password);
                user.VerificationToken = string.Empty;
                user.Updated = DateTime.UtcNow;
                //To do initiate email validation
                var result = _db.Users.Add(user).Entity;
                _db.SaveChanges();
                result.PasswordHash = null;
                return Ok(result);
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
                    var user = _db.Users.Where(x => x.Email == model.Username).FirstOrDefault();
                    if (user != null)
                    {
                        if (!user.IsActive)
                        {
                            return BadRequest("Your Account is inactive. Contanct with the administrator for activation");
                        }
                        if (AuthManager.VerifyHashedPassword(user.PasswordHash, model.Password))
                        {
                            var userToken = _auth.CreateToken(user);
                            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(userToken) });
                        }
                        else
                        {
                            return BadRequest("Wrong Password or Username");
                        }
                    }
                    else
                    {
                        return BadRequest("Wrong Password or Username");
                    }
                }
                else
                {
                    return BadRequest("Wrong Password or Username");
                }

            }
            catch (Exception exc)
            {
                return BadRequest("Error while proccessing the data");
            }
        }

        [HttpPost("external")]
        public IActionResult GoogleOrFacebook([FromBody]User user)
        {
            return Ok();
            //try
            //{
            //    //check for email duplicates
            //    var exists = _db.Users.Where(x => x.Email.ToLower() == user.Email.ToLower()).FirstOrDefault();

            //    if (exists == null)
            //    {
            //        user.Email = user.Email.ToLower();
            //        user.EmailConfirmed = true;
            //        user.ExternaLogin = true;
            //        user.Updated = DateTime.UtcNow;
            //        user.IsActive = true;
            //        user.PasswordHash = string.Empty;
            //        user.VerificationToken = string.Empty;
            //        var result = _db.Users.Add(user);
            //        _db.SaveChanges();
            //        var userToken = _auth.CreateToken(result.Entity);
            //        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(userToken) });
            //    }
            //    else
            //    {
            //        var userToken = _auth.CreateToken(exists);
            //        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(userToken) });
            //    }
            //}
            //catch (Exception exc)
            //{
            //    return BadRequest(exc.Message);
            //}
        }
    }
}
