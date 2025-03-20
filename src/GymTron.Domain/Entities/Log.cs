namespace GymTron.Domain.Entities;

public class Log : Entity<int>
{


    public string Message { get; private set; } = string.Empty;


    private Log(string message)
        : base(0)
    {
        Message = message;
        Status.Create();
    }


    public static Log New(string message)
    {
        return new Log(message);
    }
}
