using ServiceLayer.Setups.DateTimeServices;

namespace InfrastructureLayer.Services.DateTimes
{
    public class UtcDateTimeService : DateTimeService
    {
        public DateTime Today => DateTime.Now.Date.ToUniversalTime();

        public DateTime Now => RefineDate(DateTime.Now.ToUniversalTime());

        public DateTime RefineDate(DateTime dateTime)
        {
            var timeTicks = dateTime.TimeOfDay.Ticks;
            var extraDigit = timeTicks % 10;

            return dateTime.Subtract(TimeSpan.FromTicks(extraDigit));
        }
    }
}
