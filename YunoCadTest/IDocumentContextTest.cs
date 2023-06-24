using Informatix.MGDS;
using YunaComputer.YunoCad;

namespace YunaComputer.YunoCadTest;

[TestClass]
public class IDocumentContextTest
{
    [TestMethod]
    public void CloseFileTest()
    {
        var ctx = IGlobalContext.Instance;
        var id = ctx.StartMicroGDS();
        using var c = new Conversation();
        c.Start(mgds =>
        {
            mgds.CreateManFile();
            Cad.CloseView();
            mgds.HandleDocument(document =>
            {
                document.CloseFile();
            });
            mgds.Exit();
        }, id);
    }

    [TestMethod]
    public void DeleteSelectionTest()
    {
        var ctx = IGlobalContext.Instance;
        var id = ctx.StartMicroGDS();
        using var c = new Conversation();
        c.Start(mgds =>
        {
            Cad.CreateMANFile();
            Cad.CreateText("text", new Cad.Vector());
            Cad.SelectAll();
            mgds.HandleDocument(document =>
            {
                document.DeleteSelection();
                Cad.CloseView();
                document.DeleteSelection();
            });
            mgds.Exit();
        }, id);
    }

    [TestMethod]
    public void DocGetViewTypeTest()
    {
        var ctx = IDocumentContext.Instance;
        var id = ctx.StartMicroGDS();
        using var c = new Conversation();
        c.Start(mgds =>
        {
            Cad.CreateMANFile();
            Cad.CloseView();
            Cad.DocGetViewType();
        }, id);
        Cad.Exit(Save.DoNotSave, Save.DoNotSave);
    }

    [TestMethod]
    public void EnumerateWndNamesTest()
    {
        var ctx = IGlobalContext.Instance;
        var id = ctx.StartMicroGDS();
        using var c = new Conversation();
        c.Start(mgds =>
        {
            mgds.CreateManFile();
            Cad.CloseView();
            mgds.HandleDocument(document =>
            {
                foreach (var wndName in document.EnumerateWndNames())
                {
                }
            });
            mgds.Exit();
        }, id);
    }

    [TestMethod]
    public void GetNumSelObjTest()
    {
        var ctx = IGlobalContext.Instance;
        var id = ctx.StartMicroGDS();
        using var c = new Conversation();
        c.Start(mgds =>
        {
            mgds.CreateManFile();
            Cad.CloseView();
            mgds.HandleDocument(document =>
            {
                document.GetNumSelObj();
            });
            mgds.Exit();
        }, id);
    }

    [TestMethod]
    public void HandleDrawingWindowTest()
    {
        var ctx = IGlobalContext.Instance;
        var id = ctx.StartMicroGDS();
        using var c = new Conversation();
        c.Start(mgds =>
        {
            mgds.CreateManFile();
            void TestHandleDrawingWindowThrowsException()
            {
                mgds.HandleDocument(document =>
                {
                    document.HandleDrawingWindow(window =>
                    {
                        throw new Exception("OK");
                    });
                });
            }
            Assert.ThrowsException<Exception>(TestHandleDrawingWindowThrowsException);
            mgds.Exit();
        }, id);
    }

    [TestMethod]
    public void LayoutMdiTest()
    {
        var ctx = IGlobalContext.Instance;
        var id = ctx.StartMicroGDS();
        using var c = new Conversation();
        c.Start(mgds =>
        {
            mgds.CreateManFile();
            mgds.CreateManFile();
            mgds.CreateManFile();
            mgds.CreateManFile();
            Cad.CloseView();
            mgds.HandleDocument(document =>
            {
                document.LayoutMdi(System.Windows.Forms.MdiLayout.Cascade);
                document.LayoutMdi(System.Windows.Forms.MdiLayout.TileHorizontal);
                document.LayoutMdi(System.Windows.Forms.MdiLayout.TileVertical);
                document.LayoutMdi(System.Windows.Forms.MdiLayout.ArrangeIcons);
            });
            mgds.Exit();
        }, id);
    }

    [TestMethod]
    public void SelectionModeTest()
    {
        var ctx = IGlobalContext.Instance;
        var id = ctx.StartMicroGDS();
        using var c = new Conversation();
        c.Start(mgds =>
        {
            mgds.CreateManFile();
            Cad.CloseView();
            mgds.HandleDocument(document =>
            {
                var mode = document.SelectionMode;
                document.SelectionMode = mode;
                document.SelectionMode = SelectionMode.Obj;
                document.SelectionMode = SelectionMode.Prim;
            });
            mgds.Exit();
        }, id);
    }

    [TestMethod]
    public void SetCursorFromFileTest()
    {
        var ctx = IDocumentContext.Instance;
        var id = ctx.StartMicroGDS();
        using var c = new Conversation();
        c.Start(id, 5 * 1000);
        Cad.CreateMANFile();
        Cad.CloseView();
        ctx.SetCursorFromFile("");
        Cad.Exit(Save.DoNotSave, Save.DoNotSave);
    }

    [TestMethod]
    public void SetEditTest()
    {
        var ctx = IGlobalContext.Instance;
        var id = ctx.StartMicroGDS();
        using var c = new Conversation();
        c.Start(mgds =>
        {
            mgds.CreateManFile();
            Cad.CloseView();
            mgds.HandleDocument(document =>
            {
                var colour = document.SetEditColour;
                document.SetEditColour = colour;
                var lineStyle = document.SetEditLineStyle;
                document.SetEditLineStyle = lineStyle;
                var material = document.SetEditMaterial;
                document.SetEditMaterial = material;
                var obj = document.SetEditObj;
                document.SetEditObj = obj;
                var text = document.SetEditText;
                document.SetEditText = text;
            });
            mgds.Exit();
        }, id);
    }
}
