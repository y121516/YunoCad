using Informatix.MGDS;

namespace YunaComputer.YunoCad;

public interface IDocumentContext : IMgdsContext
{
    private class DocumentContext : IDocumentContext { }
    private static readonly IDocumentContext instance = new DocumentContext();
    static new IDocumentContext Instance => instance;

    void HandleDrawingWindow(Action<IDrawingWindowContext> action)
    {
        try
        {
            // Called only to test if a drawing window exists
            _ = Cad.GetSetWndDimension();
        }
        catch (Cad.CadException ex) when (ex.ErrorOccurred(AppErrorType.MGDS, AppError.RequiresActiveDrawing))
        {
            throw new DrawingWindowHandleException("A drawing window is required for this operation", ex);
        }
        action(IDrawingWindowContext.Instance);
    }

    void SetCursorFromFile(string fileName) => Cad.SetCursorFromFile(fileName);

    DocViewType ViewType => Cad.DocGetViewType();
}
