using Informatix.MGDS;
using YunaComputer.YunoCad;

namespace YunaComputer.YunoCadTest;

[TestClass]
public class IDrawingWindowContextTest
{
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
