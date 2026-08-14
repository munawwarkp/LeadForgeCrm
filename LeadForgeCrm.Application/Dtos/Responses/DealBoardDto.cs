using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadForgeCrm.Application.Dtos.Responses
{
    public class DealBoardDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ContactName { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public int StageId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty; 

    }
}
