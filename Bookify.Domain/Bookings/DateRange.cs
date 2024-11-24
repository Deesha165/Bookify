using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Domain.Bookings
{
    public record DateRange
    {

        private DateRange() { }

         public DateOnly Start {  get; init; }
        public DateOnly End { get; init; }

        public int LengthInDays => Start.Day-End.Day;

        public static DateRange Create(DateOnly start,DateOnly end)
        {
            if(start>end)
            {
                throw new ApplicationException("End Date precedes start date");
            }
            return new DateRange()
            {
                Start = start,
                End = end
            };

        }

    }
}
