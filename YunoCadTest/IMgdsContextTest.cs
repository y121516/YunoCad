using Informatix.MGDS;
using YunaComputer.YunoCad;

namespace YunaComputer.YunoCadTest;

[TestClass]
public class IMgdsContextTest
{
    [TestMethod]
    public void CreateManFileTest()
    {
        var ctx = IMgdsContext.Instance;
        var id = ctx.StartMicroGDS();
        using var c = new Conversation();
        c.Start(id, 5 * 1000);
        ctx.CreateManFile();
        ctx.Exit();
    }

    [TestMethod]
    public void DocResynchTest()
    {
        var ctx = IDocumentContext.Instance;
        var id = ctx.StartMicroGDS();
        using var c = new Conversation();
        c.Start(mgds =>
        {
            mgds.DocResynch();
            mgds.Exit();
        }, id);
    }

    [TestMethod]
    public void EchoTest()
    {
        var ctx = IMgdsContext.Instance;
        var id = ctx.StartMicroGDS();
        using var c = new Conversation();
        c.Start(id, 5 * 1000);
        ctx.Echo("hello, world!");
        Cad.Exit(Save.DoNotSave, Save.DoNotSave);
    }

    [TestMethod]
    public void ExitTest()
    {
        var ctx = IMgdsContext.Instance;
        var id = ctx.StartMicroGDS();
        using var c = new Conversation();
        c.Start(id, 5 * 1000);
        ctx.Exit();
    }

    [TestMethod]
    public void OpenTest()
    {
        var ctx = IMgdsContext.Instance;
        var id = ctx.StartMicroGDS();
        using var c = new Conversation();
        c.Start(mgds =>
        {
            var fileName = @"C:\Program Files\Informatix\MicroGDS 11.3\Sample Drawings\Sdmf1.man";
            var options = new Informatix.MGDS.Import.MAN.Options();
            mgds.Open(fileName, options);
            mgds.Exit();
        }, id);
    }

    [TestMethod]
    public void ScreenUpdateModeTest()
    {
        var mc = IMgdsContext.Instance;
        var id = mc.StartMicroGDS();
        using var c = new Conversation();
        c.Start(id, 5 * 1000);
        mc.ScreenUpdateMode(ScreenUpdate.Interactive);
        mc.ScreenUpdateMode(ScreenUpdate.Bulk);
        Cad.Exit(Save.DoNotSave, Save.DoNotSave);
    }
}
