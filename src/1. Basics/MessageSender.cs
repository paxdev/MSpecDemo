namespace MSpecDemo._1._Basics
{
    public class MessageSender
    {
        public bool Send(string message) => 
            !string.IsNullOrWhiteSpace(message);
    }
}