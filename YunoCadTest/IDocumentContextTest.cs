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
}
