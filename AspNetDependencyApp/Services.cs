namespace AspNetDependencyApp
{
    public class DateService
    {
        public string GetDate() => DateTime.Now.ToShortDateString();
    }

    public static class DateServiceExtensions
    {
        public static void AddDateService(this IServiceCollection services)
        {
            services.AddTransient<DateService>();
        }
    }





    public interface ITimeService
    {
        string GetTime();
    }

    public class TimeShortService : ITimeService
    {
        public string GetTime() => DateTime.Now.ToShortTimeString();
    }

    public class TimeLongService : ITimeService
    {
        public string GetTime() => DateTime.Now.ToLongTimeString();
    }
}
