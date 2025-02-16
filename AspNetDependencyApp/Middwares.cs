namespace AspNetDependencyApp
{
    public class TimeMessageMiddleware
    {
        RequestDelegate next;

        public TimeMessageMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, IEnumerable<ITimeService> services)
        {
            context.Response.ContentType = "text/html; charset=utf-8";
            string strTime = "";
            foreach (var service in services)
                strTime += service.GetTime() + " ";

            await context.Response.WriteAsync($"<h2>Current time: {strTime}</h2>");
        }
    }
}
