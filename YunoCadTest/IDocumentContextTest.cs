using Informatix.MGDS;
using YunaComputer.YunoCad;

namespace YunaComputer.YunoCadTest;

[TestClass]
public class IDocumentContextTest
{
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
