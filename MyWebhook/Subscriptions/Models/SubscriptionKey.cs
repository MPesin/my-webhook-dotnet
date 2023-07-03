namespace MyWebhook.Subscriptions.Models;

public class SubscriptionKey
{
    public SubscriptionKey()
    {
        Key = Guid.NewGuid();
    }
        
    public SubscriptionKey(Guid id)
    {
        Key = id;
    }
        
    public Guid Key { get; }

    public override bool Equals(object obj)
    {
        var isEqual = false;
        if (obj is SubscriptionKey key)
        {
            isEqual = Equals(key);
        }

        return isEqual;
    }

    private bool Equals(SubscriptionKey other)
    {
        return Key.Equals(other.Key);
    }

    public override int GetHashCode()
    {
        return Key.GetHashCode();
    }
}