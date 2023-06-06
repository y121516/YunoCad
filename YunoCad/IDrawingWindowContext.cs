using Informatix.MGDS;

namespace YunaComputer.YunoCad;

public interface IDrawingWindowContext : IDocumentContext
{
    private class DrawingWindowContext : IDrawingWindowContext { }
    private static readonly IDrawingWindowContext instance = new DrawingWindowContext();
    static new IDrawingWindowContext Instance => instance;

    void CreateLayer(string layerName, string aliasName = "") =>
        Cad.CreateLayer(layerName, aliasName);
}
