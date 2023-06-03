using Informatix.MGDS;

namespace YunaComputer.YunoCad;

public interface IGlobalContext
{
    private class GlobalContext : IGlobalContext { }
    private static readonly IGlobalContext instance = new GlobalContext();
    static IGlobalContext Instance => instance;

    private const StartFileType defaultFileType = StartFileType.MAN;
    private const int defaultTimeoutMs = 30 * 1000;
    int StartMicroGDS(StartFileType fileType = defaultFileType, int timeoutMs = defaultTimeoutMs)
        => Cad.StartMicroGDS(fileType, timeoutMs);
}
