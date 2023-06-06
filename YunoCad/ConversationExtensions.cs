using Informatix.MGDS;

namespace YunaComputer.YunoCad;

public static class ConversationExtensions
{
    private const int DefaultTimeout = 5 * 1000;

    public static void Start(
        this Conversation c,
        Action<IMgdsContext> action,
        int sessionID = Conversation.AnySession,
        int timeoutMs = DefaultTimeout)
    {
        c.Start(sessionID, timeoutMs);
        action(IMgdsContext.Instance);
    }
}
