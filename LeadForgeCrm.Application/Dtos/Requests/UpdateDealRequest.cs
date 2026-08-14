using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadForgeCrm.Application.Dtos.Requests
{
    public class UpdateDealRequest
    {
        public string? Title { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? ExpectedCloseDate { get; set; }
        public int? Probability { get; set; }
        public string? Description { get; set; }
    }
}
