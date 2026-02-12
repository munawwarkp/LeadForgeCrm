using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadForgeCrm.Application.Dtos.Responses
{
    public record DealChangeResponse(
        int DealId,
        int PipelineStageId,
        int Order,
        string Title,
        decimal Amount,
        DateTime? ExpectedCloseDate,
        string Status
        );
}
