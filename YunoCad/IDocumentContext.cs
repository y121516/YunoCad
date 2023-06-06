using Informatix.MGDS;

namespace YunaComputer.YunoCad;

public interface IDocumentContext : IMgdsContext
{
    private class DocumentContext : IDocumentContext { }
    private static readonly IDocumentContext instance = new DocumentContext();
    static new IDocumentContext Instance => instance;

    void SetCursorFromFile(string fileName) => Cad.SetCursorFromFile(fileName);

    DocViewType ViewType => Cad.DocGetViewType();
}
