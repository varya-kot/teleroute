namespace Teleroute.Send
{
    public interface ISend<Client>
    {
        void Send(Client client);
    }
}
