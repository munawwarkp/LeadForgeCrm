namespace LeadForgeCrm.Api.Dtos.Requests
{
    public class SignupRequest
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string CompanyName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;

    }
}
