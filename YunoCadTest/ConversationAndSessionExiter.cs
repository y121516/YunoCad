using Informatix.MGDS;

namespace YunaComputer.YunoCadTest;

/// <summary>
/// Manages a conversation with MicroGDS, and only terminates the session when the instance is disposed.
/// </summary>
class ConversationAndSessionExiter : IDisposable
{
    readonly Conversation conversation;

    /// <summary>
    /// Initializes a new instance of the ConversationAndSessionExiter class and starts a conversation with MicroGDS.
    /// </summary>
    /// <param name="conversation">The conversation to manage.</param>
    /// <param name="conversationStartTimeoutMs">The maximum duration to wait for the conversation to start, in milliseconds.</param>
    /// <param name="sessionId">The session ID to start the conversation with.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="conversation"/> is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown when a conversation has already started.</exception>
    public ConversationAndSessionExiter(
        Conversation conversation,
        int conversationStartTimeoutMs = 5 * 1000,
        int sessionId = Conversation.AnySession)
    {
        this.conversation = conversation ?? throw new ArgumentNullException(nameof(conversation));
        if (conversation.Started) throw new InvalidOperationException("A conversation has already started.");
        conversation.Start(sessionId, conversationStartTimeoutMs);
    }

    /// <summary>
    /// Ends the conversation and its session upon disposal.
    /// </summary>
    public void Dispose()
    {
        Cad.Exit(Save.DoNotSave, Save.DoNotSave);
        conversation.Stop();
    }
}
