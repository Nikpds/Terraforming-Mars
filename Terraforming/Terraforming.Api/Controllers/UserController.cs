using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Terraforming.Api.Authorization;
using Terraforming.Api.Database;
using Terraforming.Api.Models;
using Terraforming.Api.ModelViews;
using Terraforming.Api.Services;

namespace Terraforming.Api.Controllers
{

    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private MsDataContext _db;
        public UserController(MsDataContext db)
        {
            _db = db;
        }

        [Authorize(Policy = "Administrator")]
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

        [Authorize(Policy = "GameMaster")]
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

        [Authorize]
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            try
            {
                var userId = User.GetUserId();
                var u = _db.Users.FirstOrDefault(x => x.Id == userId);
                if (u == null)
                {
                    return BadRequest("User wasn't found");
                }
                var profile = new UserProfile(u);
                profile.Teams = _db.TeamUsers.Include(i => i.Team)
                    .Where(x => x.UserId == userId)
                    .Select(s => s.Team).ToList();
                profile.Invitations = (from obj in _db.Invitations.Include(i => i.Owner)
                                      .Include(i => i.Team).Where(x => x.UserId == userId)
                                       select new InvitationViewDto
                                       {
                                           ActionDate = obj.ActionDate,
                                           Color = obj.Team.Color,
                                           Comments = obj.Comments,
                                           Created = obj.Created,
                                           Icon = obj.Team.Icon,
                                           Id = obj.Id,
                                           IsMember = obj.InivtationStatus == InvitationStatus.Accepted,
                                           OwnerName = string.Join(" ", obj.Owner.Lastname, obj.Owner.Firstname),
                                           Status = obj.InivtationStatus,
                                           TeamId = obj.TeamId,
                                           Title = obj.TeamTitle
                                       }).ToList();

                return Ok(profile);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }

        [Authorize(Policy = "GameMaster")]
        [HttpGet("teammates/{teamId}")]
        public IActionResult GetTeamMates(string teamId)
        {
            try
            {
                var userId = User.GetUserId();
                var users = _db.Teams.Include(i => i.TeamUsers)
                    .ThenInclude(u => u.User)
                    .SingleOrDefault(x => x.Id == teamId).TeamUsers.Select(s => s.User);
                return Ok(users);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }

        [Authorize(Policy = "Administrator")]
        [HttpPost("resetpassword/{userId}")]
        public IActionResult ResetUsersPassword(string userId)
        {
            try
            {
                var user = _db.Users.Single(s => s.Id == userId);
                user.PasswordHash = AuthManager.HashPassword("12345");
                _db.Users.Update(user);
                _db.SaveChanges();
                return Ok(true);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }

        [Authorize(Policy = "Administrator")]
        [HttpPost("activate/{userId}/{status}")]
        public IActionResult ActivateDeactiateUser(string userId, bool status)
        {
            try
            {
                var user = _db.Users.Single(s => s.Id == userId);
                if (user == null)
                {
                    return BadRequest("Invalid user");
                }

                user.IsActive = status;
                _db.Users.Update(user);
                _db.SaveChanges();
                return Ok(true);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }

        [Authorize]
        [HttpPost("changepassword/{current}/{newpassword}")]
        public IActionResult ResetUsersPassword(string current,string newpassword)
        {
            try
            {
                var userId = User.GetUserId();
                var user = _db.Users.Single(s => s.Id == userId);
                if (user == null)
                {
                    return BadRequest("User does not exist");
                }
                if (!(AuthManager.VerifyHashedPassword(user.PasswordHash, current)))
                {
                    return BadRequest("Current Password is Incorrect");
                }
                user.PasswordHash = AuthManager.HashPassword(newpassword);
                _db.Users.Update(user);
                _db.SaveChanges();
                return Ok(true);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }

    }
}
