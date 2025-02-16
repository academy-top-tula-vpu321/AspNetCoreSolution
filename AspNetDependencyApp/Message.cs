namespace AspNetDependencyApp
{
    public interface ILogger
    {
        void Log(string message);
    }
    public class ConsoleLogger : ILogger
    {
        ITimeService timeService;

        public ConsoleLogger(ITimeService timeService)
        {
            this.timeService = timeService;
        }

        public void Log(string message)
        {
            Console.WriteLine($"Message[{timeService.GetTime()}]: {message}");
        }
    }
    public class Message
    {
        ILogger logger;
        public string Text { get; set; }
        public Message(ILogger logger)
        {
            this.logger = logger;
        }

        public void ShowMessage() => logger.Log(Text);
    }
}
