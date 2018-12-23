using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Terraforming.Api.Database;

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
                //check for email duplicates
                var result = _db.Users.ToList();
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
