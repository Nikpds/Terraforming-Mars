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

    [Route("api/[controller]")]
    public class TeamController : ControllerBase
    {
        private MsDataContext _db;
        public TeamController(MsDataContext db)
        {
            _db = db;
        }

        [Authorize(Policy = "GameMaster")]
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
                var teamUser = new TeamUser();
                _db.Teams.Add(team);
                teamUser.TeamId = team.Id;
                teamUser.UserId = team.OwnerId;
                _db.TeamUsers.Add(teamUser);
                _db.SaveChanges();
                var result = _db.Teams.AsNoTracking().Include(i => i.Owner).FirstOrDefault(x => x.Id == team.Id);
                return Ok(result);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }

        [Authorize(Policy = "GameMaster")]
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

        [Authorize(Policy = "GameMaster")]
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
                var tusers = _db.TeamUsers.Where(x => x.TeamId == id).ToList();
                var invitations = _db.Invitations.Where(x => x.TeamId == id).ToList();
                _db.TeamUsers.RemoveRange(tusers);
                _db.Invitations.RemoveRange(invitations);
                _db.Teams.Remove(original);
                _db.SaveChanges();
                return Ok(true);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }

        [Authorize]
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

        [Authorize(Policy = "GameMaster")]
        [HttpGet("created/teams")]
        public IActionResult GetCreatedTeams()
        {
            try
            {
                var result = (from obj in _db.Teams.Include(i => i.Owner)
                              .Include(i => i.TeamUsers)
                              .Where(x => x.OwnerId == User.GetUserId())
                              select new Team
                              {
                                  Color = obj.Color,
                                  Icon = obj.Icon,
                                  Title = obj.Title,
                                  Created = obj.Created,
                                  Members = obj.TeamUsers.Count(),
                                  Id = obj.Id,
                                  Owner = obj.Owner
                              }).ToList();

                return Ok(result);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }

        [Authorize(Policy = "GameMaster")]
        [HttpGet("teams/game")]
        public IActionResult GetMyTeamsForGame()
        {
            try
            {
                var result = (from obj in _db.Teams.Where(x => x.OwnerId == User.GetUserId())
                              select new Team { Title = obj.Title, Id = obj.Id }).ToList();

                return Ok(result);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }

        [Authorize(Policy = "GameMaster")]
        [HttpGet("members/pending/invites/{id}")]
        public IActionResult GetMembersAndInvites(string id)
        {
            try
            {
                var team = _db.Teams.AsNoTracking().Include(i => i.Owner).FirstOrDefault(x => x.Id == id);
                var invitations = (from obj in _db.Invitations
                    .Include(x => x.User)
                    .Where(x => x.TeamId == id)
                                   select new InvitationDto
                                   {
                                       InvitationsId = obj.Id,
                                       Fullname = string.Join(" ", obj.User.Firstname, obj.User.Lastname),
                                       ActionDate = obj.ActionDate,
                                       Created = obj.Created,
                                       IsMember = obj.InivtationStatus == InvitationStatus.Accepted,
                                       Status = obj.InivtationStatus
                                   }).ToList();
                var ownerInitation = new InvitationDto()
                {
                    Fullname = string.Join(" ", team.Owner.Firstname, team.Owner.Lastname),
                    ActionDate = team.Created,
                    Created = team.Created,
                    IsMember = true,
                    Status = InvitationStatus.Accepted
                };
                invitations.Add(ownerInitation);
                var result = invitations.OrderBy(x => x.IsMember);
                return Ok(result);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }
    }

}
