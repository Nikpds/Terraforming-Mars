using System;
using Terraforming.Api.Models;

namespace Terraforming.Api.ModelViews
{
    public class InvitationDto
    {
        public string InvitationsId { get; set; }
        public string Fullname { get; set; }
        public InvitationStatus Status { get; set; }
        public DateTime ActionDate { get; set; }
        public DateTime Created { get; set; }
        public bool IsMember { get; set; }

        public InvitationDto() { }
        public InvitationDto(Invitation inv)
        {
            InvitationsId = inv.Id;
            Fullname = string.Join(" ", inv.User.Lastname, inv.User.Firstname);
            Status = inv.InivtationStatus;
            ActionDate = inv.ActionDate;
            Created = inv.Created;
            IsMember = inv.InivtationStatus == InvitationStatus.Accepted;
        }

    }
    public class InvitationViewDto
    {
        public string Id { get; set; }

        public string OwnerName { get; set; }
        public string TeamId { get; set; }
        public string Title { get; set; }
        public string Color { get; set; }
        public string Icon { get; set; }

        public string Comments { get; set; }
        public InvitationStatus Status { get; set; }
        public DateTime ActionDate { get; set; }
        public DateTime Created { get; set; }

        public bool IsMember { get; set; }


    }
}

