using Informatix.MGDS;

namespace YunaComputer.YunoCadTest;

/// <summary>
/// Manages a temporary conversation with MicroGDS, starting a new session and exiting it when the instance is disposed.
/// </summary>
class TransientConversation : IDisposable
{
    Conversation conversation;

    /// <summary>
    /// Initializes a new instance of the TransientConversation class, starts a new MicroGDS session and a conversation with it.
    /// </summary>
    /// <param name="conversationStartTimeoutMs">The maximum duration to wait for the conversation to start, in milliseconds.</param>
    /// <param name="fileType">The type of file to start the session with.</param>
    /// <param name="mgdsStartTimeoutMs">The maximum duration to wait for the MicroGDS session to start, in milliseconds.</param>
    public TransientConversation(
        int conversationStartTimeoutMs = 5 * 1000,
        StartFileType fileType = StartFileType.MAN,
        int mgdsStartTimeoutMs = 30 * 1000)
    {
        int id = Cad.StartMicroGDS(fileType, mgdsStartTimeoutMs);
        conversation = new Conversation();
        conversation.Start(id, conversationStartTimeoutMs);
    }

    /// <summary>
    /// Ends the conversation and its session upon disposal.
    /// </summary>
    public void Dispose()
    {
        Cad.Exit(Save.DoNotSave, Save.DoNotSave);
        conversation.Dispose();
    }
}
