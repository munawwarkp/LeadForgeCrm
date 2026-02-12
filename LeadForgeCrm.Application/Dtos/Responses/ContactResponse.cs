using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadForgeCrm.Application.Dtos.Responses
{
    public record ContactResponse(
        int Id,
        string FirstName,
        string LastName,
        int? CompanyId,
        string Email,
        string Phone
        );

}
