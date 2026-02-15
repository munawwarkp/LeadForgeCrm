using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadForgeCrm.Application.Dtos.Requests
{
    public record DealRequest(
        string DealName,

        int? CompanyId,
        int ContactId,

        int StageId,

        decimal? Amount,
        DateTime ClosingDate,
        string? Description);
}
