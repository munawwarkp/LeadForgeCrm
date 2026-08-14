using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadForgeCrm.Domain.Entities.CrmCore
{
    public class Address
    {
        public int Id { get; set; }
        public string? Country { get; set; } = null!;
        public string ApartmentName { get; set; } = null!;
        public string StreetAddress { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;  
        public string PostalCode { get; set; } = null!;

        public int? ContactId { get; set; }
        public Contact? Contact { get; set; } = null!;

        public int? LeadId { get; set; }
        public Lead? Lead { get; set; } = null!;

        public Deal? Deal { get; set; } = null!;
        public int? DealId { get; set; }
    }
}
