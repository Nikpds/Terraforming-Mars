using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Terraforming.Api.Database;
using Terraforming.Api.Models;
using Terraforming.Api.ModelViews;
using Terraforming.Api.Services;

namespace Terraforming.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class TeamController : ControllerBase
    {
        private MsDataContext _db;
        public TeamController(MsDataContext db)
        {
            _db = db;
        }

        [HttpPost]
        public IActionResult CreateTeam([FromBody] Team team)
        {
            try
            {
                var exists = _db.Teams.FirstOrDefault(x => x.Title.ToLowerInvariant() == team.Title.ToLowerInvariant());
                if (exists != null)
                {
                    return BadRequest("There is already a Team with name " + team.Title + ". Please change the name and try again");
                }
                team.OwnerId = User.GetUserId();
                team.Created = DateTime.UtcNow;
                team.Updated = DateTime.UtcNow;
                _db.Teams.Add(team);
                _db.SaveChanges();
                var result = _db.Teams.AsNoTracking().Include(i => i.Owner).FirstOrDefault(x => x.Id == team.Id);
                return Ok(result);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateTeam([FromBody] Team team)
        {
            try
            {
                var original = _db.Teams.AsNoTracking().Include(i => i.Owner).FirstOrDefault(x => x.Id == team.Id);
                if (original == null)
                {

                    return BadRequest("Team wan't found");
                }
                original.Color = team.Color;
                original.Updated = DateTime.UtcNow;
                original.Icon = team.Icon;
                original.Title = team.Title;
                _db.Teams.Update(original);
                _db.SaveChanges();
                return Ok(original);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTeam(string id)
        {
            try
            {
                var original = _db.Teams.Find(id);
                if (original == null)
                {
                    return BadRequest("We didn't find the team you are trying to delete");
                }
                _db.Teams.Remove(original);
                _db.SaveChanges();
                return Ok(true);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetTeam(string id)
        {
            try
            {
                var original = _db.Teams.Find(id);
                if (original == null)
                {
                    return BadRequest("We didn't find the team you are trying to view");
                }

                if (original.OwnerId != User.GetUserId())
                {
                    return BadRequest("");
                }
                return Ok(original);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }

        [HttpGet("created/teams")]
        public IActionResult GetCreatedTeams()
        {
            try
            {
                var result = _db.Teams.Include(i => i.Owner)
                    .Where(x => x.OwnerId == User.GetUserId())
                    .ToList();

                return Ok(result);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }

        [HttpGet("members/pending/invites/{id}")]
        public IActionResult GetMembersAndInvites(string id)
        {
            try
            {
                var invitations = from obj in _db.Invitations
                    .Include(x => x.User)
                    .Where(x => x.TeamId == id)
                                  select new InvitationDto
                                  {
                                      Fullname = string.Join(" ", obj.User.Firstname, obj.User.Lastname),
                                      ActionDate = obj.ActionDate,
                                      Created = obj.Created,
                                      IsMember = obj.InivtationStatus == InvitationStatus.Accepted,
                                      Status = obj.InivtationStatus
                                  };
                var result = invitations.OrderBy(x => x.IsMember).ToList();
                return Ok(result);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }
    }

}
