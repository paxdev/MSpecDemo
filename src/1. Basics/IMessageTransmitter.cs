namespace MSpecDemo._1._Basics
{
    public interface IMessageTransmitter
    {
        void Transmit(string message);

        bool IsPortActive(int portNumber);

        string Status { get; }

        string LastMessageSent { get; set; }


    }
}