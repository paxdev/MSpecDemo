namespace MSpecDemo._1._Basics
{
    public class MessageSender
    {
        public bool Send(string message)
        {
            return !string.IsNullOrWhiteSpace(message);
        }
    }
}