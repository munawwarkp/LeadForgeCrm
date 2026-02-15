using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadForgeCrm.Application.Dtos.Responses
{
    public record CreateDealResponse(
        string Title,
        decimal Amount,
        DateTime? ClosingDate,
        string Description
        );
}
