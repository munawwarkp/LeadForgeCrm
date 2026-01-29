using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Enums;

namespace LeadForgeCrm.Application.Dtos.Responses
{
    public record UpdateLeadStatusResponse(
        int LeadId,
        LeadStatus Status);

}
