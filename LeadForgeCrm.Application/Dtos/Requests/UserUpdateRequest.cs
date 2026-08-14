using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadForgeCrm.Application.Dtos.Requests
{
    public class UserUpdateRequest
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }

        [Range(1, int.MaxValue)]
        public int RoleId { get; set; }
    }
}
