using LeadForgeCrm.Domain.Enums;

namespace LeadForgeCrm.Api.Contracts
{

    //It prevents invalid lead statuses from even entering your system.

    public record UpdateLeadStatusRequest
    {
        public LeadStatus Status { get; init; }
    }
}


//Swagger dropdown
//Invalid values auto-rejected
//No "contacteddd" bugs
//Frontend knows allowed states



//this fails automatically
//{
//    "status": "RandomStatus"
//}