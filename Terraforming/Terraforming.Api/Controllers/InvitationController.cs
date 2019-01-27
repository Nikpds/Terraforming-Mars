using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Terraforming.Api.Database;
using Terraforming.Api.Models;
using Terraforming.Api.ModelViews;
using Terraforming.Api.Services;

namespace Terraforming.Api.Controllers
{
    [Route("api/[controller]")]
    public class InvitationController : ControllerBase
    {
        private MsDataContext _db;
        public InvitationController(MsDataContext db)
        {
            _db = db;
        }

        [Authorize]
        [HttpGet("invites")]
        public IActionResult GetMyInvitations()
        {
            try
            {
                var result = _db.Invitations.Where(x => x.UserId == User.GetUserId()).ToList();

                return Ok(result);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }

        [Authorize(Policy = "GameMaster")]
        [HttpPost("resend/invite/{id}")]
        public IActionResult ReSendInvite(string id)
        {
            try
            {
                var invitation = _db.Invitations.AsNoTracking().Include(i => i.User).SingleOrDefault(x => x.Id == id);
                if (invitation == null)
                {
                    return BadRequest("Invitation wasn't found!");
                }
                invitation.InivtationStatus = InvitationStatus.Pending;
                invitation.Created = DateTime.UtcNow;
                invitation.Updated = DateTime.UtcNow;
                _db.Invitations.Update(invitation);
                _db.SaveChanges();

                return Ok(new InvitationDto(invitation));
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }

        [Authorize(Policy = "GameMaster")]
        [HttpPost("invites/{userTo}/{teamId}/{comments}")]
        public IActionResult SendInvite(string userTo, string teamId, string comments)
        {
            try
            {
                var exists = _db.Invitations.Include(i => i.User).SingleOrDefault(x => x.TeamId == teamId && x.UserId == userTo);
                if (exists != null)
                {
                    return BadRequest("You have already sent invitation to " + exists.User.Lastname);
                }
                var team = _db.Teams.Find(teamId);
                if (team == null)
                {
                    return BadRequest("Team wasn't found!");
                }
                var invite = new Invitation();
                invite.InivtationStatus = InvitationStatus.Pending;
                invite.OwnerId = User.GetUserId();
                invite.TeamId = teamId;
                invite.UserId = userTo;
                invite.Comments = comments;
                invite.TeamTitle = team.Title;
                invite.Updated = DateTime.UtcNow;
                invite.Created = DateTime.UtcNow;
                _db.Invitations.Add(invite);
                _db.SaveChanges();
                var original = _db.Invitations.AsNoTracking().Include(i => i.User)
                    .FirstOrDefault(x => x.Id == invite.Id);
                return Ok(new InvitationDto(original));
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }

        /// <summary>
        /// Accepts or Declines Team Invatations. If accepted TeamUsers is created
        /// which is the many to many relation from users and teams;
        /// if declined we only change invitation status
        /// </summary>
        /// <param name="status"></param>
        /// <param name="invitation"></param>
        /// <returns>Invitation</returns>
        [Authorize]
        [HttpPost("reply/{status}/{invitation}")]
        public IActionResult ReplyToInvite(int status, string invitation)
        {
            try
            {
                var original = _db.Invitations.Find(invitation);

                if (original == null)
                {
                    return BadRequest("The invitation wasn't found");
                }

                if (original.InivtationStatus != InvitationStatus.Pending)
                {
                    return BadRequest("The invitation has is no longer active");
                }

                original.InivtationStatus = (InvitationStatus)status;
                original.ActionDate = DateTime.UtcNow;
                _db.Invitations.Update(original);

                if (original.InivtationStatus == InvitationStatus.Accepted)
                {
                    var team = new TeamUser();
                    team.TeamId = original.TeamId;
                    team.UserId = original.UserId;
                    _db.TeamUsers.Add(team);
                }
                _db.SaveChanges();

                return Ok(original.InivtationStatus == InvitationStatus.Accepted ? true : false);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }

    }
}
