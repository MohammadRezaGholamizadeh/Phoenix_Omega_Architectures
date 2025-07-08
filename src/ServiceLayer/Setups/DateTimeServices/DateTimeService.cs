namespace ServiceLayer.Setups.DateTimeServices
{
    public interface DateTimeService
    {
        DateTime Now { get; }
        DateTime Today { get; }
    }
}
