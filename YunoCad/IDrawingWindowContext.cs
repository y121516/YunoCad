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

    void DrawExtent() => Cad.DrawExtent();

    bool IsExpandView
    {
        get => Cad.ExpandViewIsActive();
        set
        {
            if (value == Cad.ExpandViewIsActive()) return;
            Cad.ExpandView();
        }
    }

    void SaveAs(string fileName) => Cad.SaveAs(fileName);

    void SaveView() => Cad.SaveView();

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
