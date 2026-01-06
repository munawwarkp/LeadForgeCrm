namespace LeadForgeCrm.Api.Dtos.Requests
{
    public class UpdateTenantRequest
    {
        public string CompanyName { get; set; } 
        public string PhoneNumber { get; set; }
        public string Currency { get; set; } = "INR";

    }
}
