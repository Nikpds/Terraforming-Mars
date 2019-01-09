using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Terraforming.Api.Models;

namespace Terraforming.Api.ModelViews
{
    public class InvitationDto
    {
        public string Fullname { get; set; }
        public InvitationStatus Status { get; set; }
        public DateTime ActionDate { get; set; }
        public DateTime Created { get; set; }
        public bool IsMember { get; set; }

    }
}

