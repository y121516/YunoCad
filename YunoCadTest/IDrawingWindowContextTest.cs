using Informatix.MGDS;
using YunaComputer.YunoCad;

namespace YunaComputer.YunoCadTest;

[TestClass]
public class IDrawingWindowContextTest
{
    [TestMethod]
    public void CopySelectionTest()
    {
        var ctx = IGlobalContext.Instance;
        var id = ctx.StartMicroGDS();
        using var c = new Conversation();
        c.Start(mgds =>
        {
            mgds.CreateManFile();
            Cad.CopySelection();
            mgds.Exit();
        }, id);
    }

    [TestMethod]
    public void CreateLayerTest()
    {
        var ctx = IDocumentContext.Instance;
        var id = ctx.StartMicroGDS();
        using var c = new Conversation();
        c.Start(mgds =>
        {
            mgds.CreateManFile();
            Cad.CreateLayer("newLayer", "");
            mgds.Exit();
        }, id);
    }

    [TestMethod]
    public void DrawExtentTest()
    {
        var ctx = IGlobalContext.Instance;
        var id = ctx.StartMicroGDS();
        using var c = new Conversation();
        c.Start(mgds =>
        {
            mgds.CreateManFile();
            mgds.HandleDocument(document =>
            {
                document.HandleDrawingWindow(window =>
                {
                    window.DrawExtent();
                });
            });
            mgds.Exit();
        }, id);
    }

    [TestMethod]
    public void IsExpandViewTest()
    {
        var ctx = IGlobalContext.Instance;
        var id = ctx.StartMicroGDS();
        using var c = new Conversation();
        c.Start(mgds =>
        {
            mgds.CreateManFile();
            mgds.HandleDocument(document =>
            {
                document.HandleDrawingWindow(window =>
                {
                    window.IsExpandView = false;
                    Assert.AreEqual(false, window.IsExpandView);
                    window.IsExpandView = true;
                    Assert.AreEqual(true, window.IsExpandView);
                });
            });
            mgds.Exit();
        }, id);
    }

    [TestMethod]
    public void SaveAsTest()
    {
        var ctx = IGlobalContext.Instance;
        var id = ctx.StartMicroGDS();
        using var c = new Conversation();
        var name = Path.ChangeExtension(nameof(SaveAsTest), ".man");
        var tempFileName = Path.Combine(Path.GetTempPath(), name);
        c.Start(mgds =>
        {
            mgds.CreateManFile();
            mgds.HandleDocument(document =>
            {
                document.HandleDrawingWindow(window =>
                {
                    window.SaveAs(tempFileName);
                });
            });
            mgds.Exit();
        }, id);
        File.Delete(tempFileName);
    }

    [TestMethod]
    public void SaveViewTest()
    {
        var ctx = IGlobalContext.Instance;
        var id = ctx.StartMicroGDS();
        using var c = new Conversation();
        c.Start(mgds =>
        {
            mgds.CreateManFile();
            mgds.HandleDocument(document =>
            {
                document.HandleDrawingWindow(window =>
                {
                    Cad.SaveView();
                });
            });
            mgds.Exit();
        }, id);
    }

    [TestMethod]
    public void SelectAllTest()
    {
        var ctx = IGlobalContext.Instance;
        var id = ctx.StartMicroGDS();
        using var c = new Conversation();
        c.Start(mgds =>
        {
            mgds.CreateManFile();
            mgds.HandleDocument(document =>
            {
                document.HandleDrawingWindow(window =>
                {
                    window.SelectAll();
                });
            });
            mgds.Exit();
        }, id);
    }

    [TestMethod]
    public void WindowStateTest()
    {
        var ctx = IGlobalContext.Instance;
        var id = ctx.StartMicroGDS();
        using var c = new Conversation();
        c.Start(mgds =>
        {
            mgds.CreateManFile();
            mgds.HandleDocument(document =>
            {
                document.HandleDrawingWindow(window =>
                {
                    window.WindowState = System.Windows.Forms.FormWindowState.Normal;
                    window.WindowState = System.Windows.Forms.FormWindowState.Minimized;
                    window.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                });
            });
            mgds.Exit();
        }, id);
    }
}
