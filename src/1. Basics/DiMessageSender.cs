namespace MSpecDemo._1._Basics
{
    public class DiMessageSender
    {
        readonly IMessageTransmitter messageTransmitter;

        public DiMessageSender(IMessageTransmitter messageTransmitter)
        {
            this.messageTransmitter = messageTransmitter;
        }

        public void SendMessage(string testMessage) => 
            messageTransmitter.Transmit(testMessage);
    }
}