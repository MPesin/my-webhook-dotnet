using MyWebhook.Utils;

namespace MyWebhook.Localizations;

public class ListenerLocal
{
    private static readonly ListenerLocal Default = Create(LocalLanguage.Undefined);

    /// <summary>
    /// Create a new <see cref="ListenerLocal"/> from <paramref name="localCode"/>.
    /// If <paramref name="localCode"/> is null or empty, function returns <see cref="ListenerLocal.Default"/>
    /// </summary>
    /// <exception cref="ArgumentException">No such <paramref name="localCode"/> exists</exception>
    public static ListenerLocal Create(string localCode)
    {
        var listener = Default;
        if (!string.IsNullOrEmpty(localCode))
        {
            var code = localCode.ParseEnumFromDescription<LocalLanguage>();
            listener = new ListenerLocal(code);
        }

        return listener;
    }

    public static ListenerLocal Create(LocalLanguage localLanguage) => new (localLanguage);

    private ListenerLocal()
    { }

    private ListenerLocal(LocalLanguage language)
    {
        LocalLanguage = language;
    }

    public LocalLanguage LocalLanguage { get; }

    public override int GetHashCode()
    {
        return LocalLanguage.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        if (obj is ListenerLocal local)
        {
            return Equals(local);
        }

        return false;
    }

    private bool Equals(ListenerLocal listenerLocal)
    {
        return LocalLanguage == listenerLocal.LocalLanguage;
    }
}
