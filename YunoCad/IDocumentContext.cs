using Informatix.MGDS;
using System.ComponentModel;

namespace YunaComputer.YunoCad;

public interface IDocumentContext : IMgdsContext
{
    private class DocumentContext : IDocumentContext { }
    private static readonly IDocumentContext instance = new DocumentContext();
    static new IDocumentContext Instance => instance;

    DialogResult CloseFile(Save drawing = Save.DoNotSave) => Cad.CloseFile(drawing);

    void DeleteSelection()
    {
        // If nothing is selected, Cad.DeleteSelection() throws
        // a Cad.CadException with AppError.RequiresSelection[1053].
        // Therefore, if nothing is selected, we bypass the operation.
        if (Cad.GetNumSelObj() == 0) return;
        Cad.DeleteSelection();
    }

    int GetNumSelObj() => Cad.GetNumSelObj();

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

    void LayoutMdi(MdiLayout layout)
    {
        var arrangement = layout switch
        {
            MdiLayout.Cascade => Arrange.Cascade,
            MdiLayout.TileHorizontal => Arrange.TileHoriz,
            MdiLayout.TileVertical => Arrange.TileVert,
            MdiLayout.ArrangeIcons => Arrange.Icons,
            _ => throw new InvalidEnumArgumentException(nameof(layout), (int)layout, typeof(MdiLayout))
        };
        Cad.WindowArrange(arrangement);
    }

    /// <summary>
    /// Gets or sets the selection mode.
    /// When the selection mode changes, the selection state will be cleared. 
    /// However, if the selection mode remains the same, the selection state will not be affected.
    /// </summary>
    Informatix.MGDS.SelectionMode SelectionMode
    {
        get => Cad.GetSelectMode();
        set
        {
            if (value == Cad.GetSelectMode()) return;
            switch (value)
            {
                case Informatix.MGDS.SelectionMode.Obj: Cad.SelectObject(); break;
                case Informatix.MGDS.SelectionMode.Prim: Cad.SelectPrim(); break;
            }
        }
    }

    void SetCursorFromFile(string fileName) => Cad.SetCursorFromFile(fileName);

    string SetEditColour
    {
        get
        {
            Cad.GetSetEditColour(out var value);
            return value;
        }
        set => Cad.SetEditColour(value);
    }

    string SetEditLineStyle
    {
        get
        {
            Cad.GetSetEditLineStyle(out var value);
            return value;
        }
        set => Cad.SetEditLineStyle(value);
    }

    string SetEditMaterial
    {
        get
        {
            Cad.GetSetEditMaterial(out var value);
            return value;
        }
        set => Cad.SetEditMaterial(value);
    }

    string SetEditObj
    {
        get
        {
            Cad.GetSetEditObj(out var value);
            return value;
        }
        set => Cad.SetEditObj(value);
    }

    string SetEditText
    {
        get
        {
            Cad.GetSetEditText(out var value);
            return value;
        }
        set => Cad.SetEditText(value);
    }

    DocViewType ViewType => Cad.DocGetViewType();
}
