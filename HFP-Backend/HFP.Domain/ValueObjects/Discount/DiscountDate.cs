using HFP.Domain.ValueObjects.Base;
using HFP.Shared.Abstractions.Exceptions;

namespace HFP.Domain.ValueObjects.Discount
{
    public class DiscountDate : ValueObject
    {
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }

        private DiscountDate() { } // Required for EF Core

        public DiscountDate(DateTime start, DateTime end)
        {
            if ((end - start).TotalHours < 1)
                throw new BusinessException("مدت اعتبار کد تخفیف باید بیش از یک ساعت باشد.");

            if(start.Date < DateTime.Now.Date)
                throw new BusinessException("زمان شروع اعتبار کد تحفیف باید جلو تر از تاریخ حال حاضر باشد.");

            StartDate = start;
            EndDate = end;
        }

        public static DiscountDate Create(DateTime start, DateTime end) => new DiscountDate(start, end);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return new { StartDate, EndDate };
        }
    }
}
