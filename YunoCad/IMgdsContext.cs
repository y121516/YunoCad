using Informatix.MGDS;

namespace YunaComputer.YunoCad;

public interface IMgdsContext : IGlobalContext
{
    private class MgdsContext : IMgdsContext { }
    private static readonly IMgdsContext instance = new MgdsContext();
    static new IMgdsContext Instance => instance;

    void CreateManFile() => Cad.CreateMANFile();

    void DocResynch() => Cad.DocResynch();

    void Echo(string echoStr = "") => Cad.Echo(echoStr);

    void Exit(Save drawing = Save.DoNotSave, Save preferences = Save.DoNotSave)
        => Cad.Exit(drawing, preferences);

    void HandleDocument(Action<IDocumentContext> action)
    {
        try
        {
            // Called only to test if an active document exists
            _ = Cad.DocGetViewType();
        }
        catch (Cad.CadException ex) when (ex.ErrorOccurred(AppErrorType.MGDS, AppError.RequiresDocument))
        {
            throw new DocumentHandleException("An active document is required for this operation", ex);
        }
        action(IDocumentContext.Instance);
    }

    void Open(string fileName, string formatOptions)
    {
        // Bug: This is a critical bug in MicroGDS 11.3. 
        // If an empty string ("") is passed to the fileName parameter in Cad.Open, 
        // MicroGDS crashes, leaving the application hanging in communication. 
        // Workaround:
        // We prevent an empty string from being passed to the fileName parameter in Cad.Open.
        if (fileName == "")
        {
            // Intentionally provoke the Cad.CadException with AppError.InvalidParameter[1000]
            // that should originally be thrown
            Cad.Open(null, null);
        }
        // If the formatOptions parameter (an XML string) in Cad.Open is incorrect,
        // it can throw a Cad.CadException with AppError.Because[1143].
        Cad.Open(fileName, formatOptions);
    }

    void Open(string fileName, Informatix.MGDS.ImportExport.Options options)
    {
        Open(fileName, options.Xml());
    }

    void ScreenUpdateMode(ScreenUpdate updateMode)
        => Cad.ScreenUpdateMode(updateMode);
}
