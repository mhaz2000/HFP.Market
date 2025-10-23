using HFP.Domain.Consts;
using HFP.Domain.ValueObjects.Discount;
using HFP.Domain.ValueObjects.Users;
using HFP.Shared.Abstractions.Domain;
using HFP.Shared.Abstractions.Exceptions;

namespace HFP.Domain.Entities
{

    public class Discount : AggregateRoot<Guid>
    {
        public DiscountName Name { get; private set; }
        public DiscountCode Code { get; private set; }
        public DiscountPercentage Percentage { get; private set; }
        public DiscountMaxAmount? MaxAmount { get; private set; }
        public DiscountDate Date { get; private set; }
        public DiscountUsageLimitPerUser UsageLimitPerUser { get; private set; }
        public DiscountType Type { get; private set; }


        private readonly List<DiscountBuyer> _buyers = new List<DiscountBuyer>();
        public IReadOnlyCollection<DiscountBuyer> Buyers => _buyers.AsReadOnly();


        public Discount()
        {
            
        }
        public Discount(DiscountName name, DiscountCode code, DiscountDate date, DiscountMaxAmount? maxAmount, DiscountPercentage percentage,
            DiscountUsageLimitPerUser usageLimitPerUser, DiscountType type)
        {
            Code = code;
            Percentage = percentage;
            Date = date;
            MaxAmount = maxAmount;
            UsageLimitPerUser = usageLimitPerUser;
            Type = type;
            Name = name;
        }


        internal void Update(DiscountName name, DiscountCode code, DiscountDate date, DiscountMaxAmount? maxAmount, DiscountPercentage percentage,
            DiscountUsageLimitPerUser usageLimitPerUser, DiscountType type)
        {
            Code = code;
            Percentage = percentage;
            Date = date;
            MaxAmount = maxAmount;
            UsageLimitPerUser = usageLimitPerUser;
            Type = type;
            Name = name;
        }

        public void AddUser(DiscountBuyer discountBuyer)
        {
            if (Buyers.Any(t => t.Buyer == discountBuyer.Buyer))
                throw new BusinessException("کد تخفیف قبلا برای این کاربر ثبت شده است.");

            _buyers.Add(discountBuyer);
        }
    }
}
