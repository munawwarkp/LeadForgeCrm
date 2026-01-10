using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Entities.SaasCore;

namespace LeadForgeCrm.Domain.Interfaces
{
    public interface ISubscriptionRepository
    {
        void AddSubscription(Subscription subscription);
    }
}
