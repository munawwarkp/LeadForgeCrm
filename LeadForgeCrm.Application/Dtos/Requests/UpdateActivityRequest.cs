using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Enums;

namespace LeadForgeCrm.Application.Dtos.Requests
{
    public class UpdateActivityRequest
    {
        public ActivityType Type { get; set; }  
        public string Description { get; set; } = null!;    
        public DateTime ActivityDate{ get; set; }
    }
}
