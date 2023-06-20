using Informatix.MGDS;
using InvalidEnumArgumentException = System.ComponentModel.InvalidEnumArgumentException;

namespace YunaComputer.YunoCadTest;

abstract public class MgdsCadTestBase
{
    public enum Context
    {
        Global,
        Mgds,
        Document,
        DrawingWindow,
    }

    /// <summary>
    /// Tests in a state without communication, under global context.
    /// </summary>
    /// <param name="action">Action to test under global context</param>
    void GlobalContextTest(Action action)
    {
        action();
    }

    /// <summary>
    /// Tests in a state with communication with MicroGDS, under MicroGDS context.
    /// </summary>
    /// <param name="action">Action to test under MicroGDS context</param>
    void MgdsContextTest(Action action)
    {
        using var _ = new TransientConversation();
        action();
    }

    /// <summary>
    /// Tests in a state where the current document is open but no view is present,
    /// under document context.
    /// </summary>
    /// <param name="action">Action to test under document context</param>
    void DocumentContextTest(Action action)
    {
        MgdsContextTest(() =>
        {
            Cad.CreateFile();
            Cad.CloseView();
            action();
        });
    }

    /// <summary>
    /// Tests in a state where the current drawing window is open,
    /// under drawing window context.
    /// </summary>
    /// <param name="action">Action to test under drawing window context</param>
    void DrawingWindowContextTest(Action action)
    {
        DocumentContextTest(() =>
        {
            Cad.CreateMANFile();
            action();
        });
    }

    /// <summary>
    /// Tests whether MicroGDS .NET API (Cad.*) works correctly in a specific context.
    /// </summary>
    /// <param name="context">The context under which Cad.* works correctly</param>
    /// <param name="action">The action to test that includes Cad.*</param>
    /// <exception cref="InvalidEnumArgumentException"></exception>
    protected void ContextTest(Context context, Action action)
    {
        var (failTest, test) = context switch
        {
            Context.Global => ((Action<Action>?)null, (Action<Action>)GlobalContextTest),
            Context.Mgds => (GlobalContextTest, MgdsContextTest),
            Context.Document => (MgdsContextTest, DocumentContextTest),
            Context.DrawingWindow => (DocumentContextTest, DrawingWindowContextTest),
            _ => throw new InvalidEnumArgumentException(nameof(context), (int)context, typeof(Context))
        };
        test(action);
        if (failTest is null) return;
        Assert.ThrowsException<Cad.CadException>(() =>
        {
            failTest(action);
        });
    }

    /// <summary>
    /// Tests whether a Cad.CadException is thrown.
    /// </summary>
    /// <param name="appError">The expected AppError.</param>
    /// <param name="action">The action to perform that should throw the exception.</param>
    protected void ThrowsCadException(AppError appError, Action action)
    {
        try
        {
            action();
        }
        catch (Cad.CadException ex)
        {
            if (ex.ErrorOccurred(AppErrorType.MGDS, appError)) return;
            Assert.Fail($"A CadException was thrown, but the value of AppError was different. Expected: {appError} ({(int)appError}).");
        }
        Assert.Fail("No CadException was thrown.");
    }

    /// <summary>
    /// Asserts that a function will throw a Cad.CadException indicating a
    /// CadLink application error.
    /// </summary>
    /// <param name="action">The action expected to throw the exception.</param>
    protected void ThrowsCadLinkAppErrorException(Action action)
    {
        try
        {
            action();
        }
        catch (Cad.CadException ex)
        {
            // If the exception has the expected message indicating a CadLink
            // application error, return early.
            if (ex.Message == "CadLinkアプリケーションエラー") return;
        }
        Assert.Fail("A Cad.CadException indicating a CadLink application error was not thrown.");
    }

    protected void ThrowsCadLinkArraySizeErrorException(Action action)
    {
        try
        {
            action();
        }
        catch (Cad.CadException ex)
        {
            // If the exception has the expected message indicating a CadLink
            // application error, return early.
            if (ex.Message == "CadLink 配列サイズエラー (sessionIDArray)") return;
        }
        Assert.Fail("A Cad.CadException indicating a CadLink array size error was not thrown.");
    }
}
