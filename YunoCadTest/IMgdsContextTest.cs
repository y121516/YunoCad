using Informatix.MGDS;
using YunaComputer.YunoCad;

namespace YunaComputer.YunoCadTest;

[TestClass]
public class IMgdsContextTest
{
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
