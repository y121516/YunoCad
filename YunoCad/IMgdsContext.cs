using Informatix.MGDS;

namespace YunaComputer.YunoCad;

public interface IMgdsContext : IGlobalContext
{
    private class MgdsContext : IMgdsContext { }
    private static readonly IMgdsContext instance = new MgdsContext();
    static new IMgdsContext Instance => instance;

    void Echo(string echoStr = "") => Cad.Echo(echoStr);

    void ScreenUpdateMode(ScreenUpdate updateMode)
        => Cad.ScreenUpdateMode(updateMode);
}
