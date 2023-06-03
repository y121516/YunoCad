using Informatix.MGDS;
using YunaComputer.YunoCad;

namespace YunaComputer.YunoCadTest;

[TestClass]
public class IGlobalContextTest
{
    [TestMethod]
    public void StartMicroGDSTest()
    {
        var gc = IGlobalContext.Instance;
        try
        {
            var id = gc.StartMicroGDS();
            Assert.AreNotEqual(0, id);
            using var c = new Conversation();
            c.Start(id, 5 * 1000);
            Cad.Exit(Save.DoNotSave, Save.DoNotSave);
        }
        catch (Cad.CadException ex)
        {
            Assert.Fail(ex.Message);
        }
    }

    [TestMethod]
    public void GetSessionCountTest()
    {
        var gc = IGlobalContext.Instance;
        var count1 = gc.GetSessionCount();
        var id = gc.StartMicroGDS();
        var count2 = gc.GetSessionCount();
        Assert.AreEqual(1, count2 - count1);
        using var c = new Conversation();
        c.Start(id, 5 * 1000);
        Cad.Exit(Save.DoNotSave, Save.DoNotSave);
    }
}
