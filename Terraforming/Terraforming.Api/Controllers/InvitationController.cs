using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Terraforming.Api.Database;
using Terraforming.Api.Models;
using Terraforming.Api.Services;

namespace Terraforming.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class InvitationController : ControllerBase
    {
        private MsDataContext _db;
        public InvitationController(MsDataContext db)
        {
            _db = db;
        }

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

        [HttpPost("invites/{userTo}/{teamId}/{comments}")]
        public IActionResult SendInvite(string userTo, string teamId, string comments)
        {
            try
            {
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

                return Ok(invite);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }

        [HttpPost("reply/{status}/{invititation}")]
        public IActionResult ReplyToInvite(int status, string invitation)
        {
            try
            {
                var original = _db.Invitations.Find(invitation);
                original.InivtationStatus = (InvitationStatus)status;
                _db.Invitations.Update(original);
                _db.SaveChanges();
                return Ok(original);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }

    }
}
