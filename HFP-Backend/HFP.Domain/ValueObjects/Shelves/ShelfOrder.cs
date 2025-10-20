using HFP.Domain.ValueObjects.Base;
using HFP.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HFP.Domain.ValueObjects.Shelves
{
    public class ShelfOrder : ValueObject
    {
        public int Value { get; }

        private ShelfOrder(int value)
        {
            if (value < 1)
                throw new BusinessException("ترتیب نمیتواند 0 یا منفی باشد.");

            Value = value;
        }
        public static ShelfOrder Create(int shelfOrder) => new ShelfOrder(shelfOrder);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator int(ShelfOrder shelfOrder)
            => shelfOrder.Value;

        public static implicit operator ShelfOrder(int shelfOrder)
            => new(shelfOrder);
    }
}
