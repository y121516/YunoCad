using Informatix.MGDS;
using System.ComponentModel;

namespace YunaComputer.YunoCad;

public interface IDrawingWindowContext : IDocumentContext
{
    private class DrawingWindowContext : IDrawingWindowContext { }
    private static readonly IDrawingWindowContext instance = new DrawingWindowContext();
    static new IDrawingWindowContext Instance => instance;

    void CreateLayer(string layerName, string aliasName = "") =>
        Cad.CreateLayer(layerName, aliasName);

    FormWindowState WindowState
    {
        set
        {
            var arrangement = value switch
            {
                FormWindowState.Normal => Arrange.Restore,
                FormWindowState.Minimized => Arrange.Minimise,
                FormWindowState.Maximized => Arrange.Maximise,
                _ => throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(FormWindowState)),
            };
            Cad.WindowArrange(arrangement);
        }
    }
}
