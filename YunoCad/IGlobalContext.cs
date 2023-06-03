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

    int GetSessionCount() => Cad.GetSessionCount();

    IEnumerable<int> GetSessionIDs() => GetSessionIDs(GetSessionCount());
    IEnumerable<int> GetSessionIDs(int maxSessionIDs)
        => GetSessionIDsValidArraySize(new int[maxSessionIDs], maxSessionIDs);
    IEnumerable<int> GetSessionIDs(int[] sessionIDs)
        => GetSessionIDsValidArraySize(sessionIDs, sessionIDs.Length);
    IEnumerable<int> GetSessionIDs(int[] sessionIDs, int maxSessionIDs)
    {
        if (sessionIDs.Length < maxSessionIDs) maxSessionIDs = sessionIDs.Length;
        return GetSessionIDsValidArraySize(sessionIDs, maxSessionIDs);
    }
    private static IEnumerable<int> GetSessionIDsValidArraySize(int[] sessionIDs, int maxSessionIDs)
        => sessionIDs.Take(Cad.GetSessionIDs(sessionIDs, maxSessionIDs));
}
