using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadForgeCrm.Application.Dtos.Responses
{
    public class TenantDetailsDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Currency { get; set; } = null!;
    }
}
