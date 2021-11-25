using NServiceBus;

public class PinGeneratedMessage : IMessage
{
    public string Pin { get; set; }
}