using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Terraforming.Api.Database;
using Terraforming.Api.ModelViews;

namespace Terraforming.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private MsDataContext _db;
        public UserController(MsDataContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var result = _db.Users.ToList();
                result.ForEach(x => x.PasswordHash = null);
                return Ok(result);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }


        [HttpGet("search/{field}")]
        public IActionResult SearcUserForInvitation(string field)
        {
            try
            {
                var f = field.ToLowerInvariant();
                var q = from user in _db.Users.AsNoTracking().Where(x => x.Email.ToLowerInvariant().Contains(f) || x.Nickname.ToLowerInvariant().Contains(f))
                             select new UserSearchView()
                             {
                                 Email = user.Email,
                                 Firstname = user.Firstname,
                                 Lastname = user.Lastname,
                                 Nickname = user.Nickname,
                                 Id = user.Id
                             };
                var result = q.ToList();
                return Ok(result);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }

        [HttpGet("teammates/{teamId}")]
        public IActionResult GetTeamMates(string teamId)
        {
            try
            {               
                var result = _db.Users.Where(x => x.TeamUsers.Any(a => a.TeamId == teamId)).ToList();
                result.ForEach(x => x.PasswordHash = null);
                return Ok(result);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }
    }
}
