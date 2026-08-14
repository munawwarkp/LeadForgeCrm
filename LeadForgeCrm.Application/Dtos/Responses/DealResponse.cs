using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadForgeCrm.Application.Dtos.Responses
{
    public class DealResponse
    {
        public int Id { get; set; }
        public string PipelineStageName { get; set; } = string.Empty;
        public string ?Title { get; set; }
        public decimal Amount { get; set; }
        public decimal ExpectedRevenue { get; set; }
        public DateTime? ExpectedCloseDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Order { get; set; }
        public string? CompanyName { get; set; }
        public string? ContactName { get; set; }
        public int Probability { get; set; }
        public string? Description { get; set; }
    }
}
