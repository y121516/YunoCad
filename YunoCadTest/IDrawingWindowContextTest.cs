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
}
