using AutoMapper;
using BL.DTOs;
using BL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IMapper _mapper;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository, IMapper mapper)
        {
            _subscriptionRepository = subscriptionRepository;
            _mapper = mapper;
        }

        public async Task<List<Subscription>> GetAllSubscriptionsAsync()
        {
            return await _subscriptionRepository.GetAllAsync();
        }

        public async Task<List<Subscription>> CheckoutSubscriptionsAsync(int period)
        {
            return await _subscriptionRepository.CheckoutSubscriptionsAsync(period);
        }

        public async Task<SubscriptionDTO> CreateSubscriptionByUserAsync(SubscriptionDTO subscriptionDto)
        {
            var subscription = _mapper.Map<Subscription>(subscriptionDto);

            return _mapper.Map<SubscriptionDTO>(await _subscriptionRepository.CreateSubscriptionByUserAsync(subscription));
        }

        public async Task<SubscriptionDTO> RemoveSubscriptionByIdAsync(int subscriptionId)
        {
            return _mapper.Map<SubscriptionDTO>(await _subscriptionRepository.RemoveSubscriptionByIdAsync(subscriptionId));
        }
    }
}
