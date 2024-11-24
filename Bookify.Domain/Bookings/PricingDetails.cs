using Bookify.Domain.shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Domain.Bookings
{
    public record PricingDetails
   (Money PriceForPeriod,Money ClesningFee,
    Money AmenitiesUpCharge,Money TotalPrice);
}
