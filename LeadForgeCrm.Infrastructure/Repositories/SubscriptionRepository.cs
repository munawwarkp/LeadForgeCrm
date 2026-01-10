using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Entities.SaasCore;
using LeadForgeCrm.Domain.Interfaces;
using LeadForgeCrm.Infrastructure.Data;

namespace LeadForgeCrm.Infrastructure.Repositories
{
    public class SubscriptionRepository:ISubscriptionRepository
    {
        private readonly AppDbContext _context;
        public SubscriptionRepository(AppDbContext appDbContext) 
        { 
            _context = appDbContext;
        }

        public void AddSubscription(Subscription subscription)
        {
            _context.Subscriptions.AddAsync(subscription);
            //await _context.SaveChangesAsync();
        }
    }
}
