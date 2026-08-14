using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadForgeCrm.Application.Dtos.Responses
{
    public class UserResponse
    {
        public string FirstName { get; set; }= string.Empty;
        public string? LastName { get; set; } 
        public string Email { get; set; } = string.Empty;
        public string Role {  get; set; } = string.Empty;   
        public DateTime CreatedAt { get; set; } 
    }
}
