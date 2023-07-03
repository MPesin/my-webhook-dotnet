namespace MyWebhook.Subscriptions.Models;

public abstract class ListenerAddress
{
    protected ListenerAddress(Uri address)
    {
        Address = address;
    }

    public Uri Address { get; }

    public override bool Equals(object? obj)
    {
        var equal = false;
        if (obj is ListenerAddress address)
        {
            equal = Equals(address);
        }

        return equal;
    }

    private bool Equals(ListenerAddress address)
    {
        return Address == address.Address;
    }

    public override int GetHashCode()
    {
        return Address.GetHashCode();
    }
}