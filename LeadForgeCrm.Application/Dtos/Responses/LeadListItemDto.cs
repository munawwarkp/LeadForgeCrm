using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadForgeCrm.Application.Dtos.Responses
{
    public record LeadListItemDto(
        int Id,
        string Name,
        string Email,
        string Phone,
        string LeadSource,
        string Status,
        string AssignedToName,
        DateTime CreatedAt
        );
    
}
