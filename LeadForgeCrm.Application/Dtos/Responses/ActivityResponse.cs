using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Enums;

namespace LeadForgeCrm.Application.Dtos.Responses
{
    public class ActivityResponse
    {
        public int Id { get; set; } 
        public ActivityType Type { get; set; }
        public string Description { get; set; } = null!;
        public DateTime ActivityDate { get; set; }

        public int UserId { get; set; } 
        public DateTime CreatedAt { get; set; }

        public int EntityId { get; set; }   
        public ActivityEntityType EntityType { get; set; }
    }
}
