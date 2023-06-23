using Informatix.MGDS;
using static Informatix.MGDS.AppError;
using static YunaComputer.YunoCadTest.MgdsCadTestBase.Context;

namespace YunaComputer.YunoCadTest;

[TestClass]
public class MgdsCadTest : MgdsCadTestBase
{
    void CloseFile()
    {
        Cad.CloseFile(Save.DoNotSave);
    }

    [TestMethod]
    public void CloseFileTest()
    {
        ContextTest(Document, CloseFile);
    }

    void CopySelection()
    {
        Cad.CopySelection();
    }

    [TestMethod]
    public void CopySelectionTest()
    {
        ContextTest(DrawingWindow, CopySelection);
    }

    void CreateLayer()
    {
        Cad.CreateLayer("newLayer", "");
        Cad.CreateLayer("new Layer", null);
        ThrowsCadException(InvalidParameter, () =>
        {
            Cad.CreateLayer(null, "");
        });
        ThrowsCadException(PhaseCreateFailed, () =>
        {
            Cad.CreateLayer("", "");
        });
        ThrowsCadException(PhaseCreateFailed, () =>
        {
            Cad.CreateLayer("*", "");
        });
    }

    [TestMethod]
    public void CreateLayerTest()
    {
        ContextTest(DrawingWindow, CreateLayer);
    }

    void CreateMANFile()
    {
        Cad.CreateMANFile();
    }

    [TestMethod]
    public void CreateMANFileTest()
    {
        ContextTest(Mgds, CreateMANFile);
    }

    void DeleteSelection()
    {
        Cad.DeleteSelection();
    }

    [TestMethod]
    public void DeleteSelectionTest()
    {
        ContextTest(ElementsSelected, DeleteSelection);
    }

    void DocGetViewType()
    {
        Cad.DocGetViewType();
    }

    [TestMethod]
    public void DocGetViewTypeTest()
    {
        ContextTest(Document, DocGetViewType);
    }

    void DocResynch()
    {
        Cad.DocResynch();
    }

    [TestMethod]
    public void DocResynchTest()
    {
        ContextTest(Mgds, DocResynch);
    }

    void DrawExtent()
    {
        Cad.DrawExtent();
    }

    [TestMethod]
    public void DrawExtentTest()
    {
        ContextTest(DrawingWindow, DrawExtent);
    }

    void Echo()
    {
        Cad.Echo("");
        Cad.Echo("hello, world");
        Cad.Echo("A\rB\nC\r\nD\tE");

        ThrowsCadException(InvalidParameter, () =>
        {
            Cad.Echo(null);
        });
    }

    [TestMethod]
    public void EchoTest()
    {
        ContextTest(Mgds, Echo);
    }

    void Exit()
    {
        Cad.Exit(Save.DoNotSave, Save.DoNotSave);
    }

    [TestMethod]
    public void ExitTest()
    {
        // Since Cad.Exit terminates MicroGDS in this test,
        // an exception is thrown when ContextTest tries to terminate MicroGDS during cleanup.
        // This behavior is expected.
        try
        {
            ContextTest(Mgds, Exit);
        }
        catch (Cad.CadException ex)
        {
            if (ex.ErrorOccurred(AppErrorType.MGDS, NoConversation))
            {
                return;
            }
            throw;
        }
    }

    void ExpandView()
    {
        Cad.ExpandView();
    }

    [TestMethod]
    public void ExpandViewTest()
    {
        ContextTest(DrawingWindow, ExpandView);
    }

    void ExpandViewIsActive()
    {
        _ = Cad.ExpandViewIsActive();
    }

    [TestMethod]
    public void ExpandViewIsActiveTest()
    {
        ContextTest(DrawingWindow, ExpandViewIsActive);
    }

    void GetNumSelObj()
    {
        _ = Cad.GetNumSelObj();
    }

    [TestMethod]
    public void GetNumSelObjTest()
    {
        ContextTest(Document, GetNumSelObj);
    }

    void GetSelectMode()
    {
        _ = Cad.GetSelectMode();
    }

    [TestMethod]
    public void GetSelectModeTest()
    {
        ContextTest(Document, GetSelectMode);
    }

    void GetSessionCount()
    {
        _ = Cad.GetSessionCount();
    }

    [TestMethod]
    public void GetSessionCountTest()
    {
        ContextTest(Global, GetSessionCount);
    }

    void GetSessionIDs()
    {
        Assert.ThrowsException<NullReferenceException>(() =>
        {
            Cad.GetSessionIDs(null, 0);
        });
        {
            var ids = new int[0];
            var maxIds = 0;
            var count = Cad.GetSessionIDs(ids, maxIds);
            Assert.AreEqual(0, count);
        }
        ThrowsCadLinkArraySizeErrorException(() =>
        {
            var ids = new int[0];
            var maxIds = 1;
            Cad.GetSessionIDs(ids, maxIds);
        });
        {
            var ids = new int[1] { 0x0000BEEF };
            var maxIds = 0;
            var count = Cad.GetSessionIDs(ids, maxIds);
            Assert.AreEqual(0, count);
            Assert.AreEqual(0x0000BEEF, ids[0]);
        }
        {
            var ids = new int[1] { 0x0000BEEF };
            var maxIds = 1;
            var count = Cad.GetSessionIDs(ids, maxIds);
            Assert.AreEqual(0, count);
            Assert.AreEqual(0x0000BEEF, ids[0]);
        }
    }

    [TestMethod]
    public void GetSessionIDsTest()
    {
        ContextTest(Global, GetSessionIDs);
    }

    void GetSetEditColour()
    {
        Cad.GetSetEditColour(out string _);
    }

    [TestMethod]
    public void GetSetEditColourTest()
    {
        ContextTest(Document, GetSetEditColour);
    }

    void GetSetEditLineStyle()
    {
        Cad.GetSetEditLineStyle(out string _);
    }

    [TestMethod]
    public void GetSetEditLineStyleTest()
    {
        ContextTest(Document, GetSetEditLineStyle);
    }

    void GetSetEditMaterial()
    {
        Cad.GetSetEditMaterial(out string _);
    }

    [TestMethod]
    public void GetSetEditMaterialTest()
    {
        ContextTest(Document, GetSetEditMaterial);
    }

    void GetSetEditObj()
    {
        Cad.GetSetEditObj(out string _);
    }

    [TestMethod]
    public void GetSetEditObjTest()
    {
        ContextTest(Document, GetSetEditObj);
    }

    void GetSetEditText()
    {
        Cad.GetSetEditText(out string _);
    }

    [TestMethod]
    public void GetSetEditTextTest()
    {
        ContextTest(Document, GetSetEditText);
    }

    void Open()
    {
        const string fileName = @"C:\Program Files\Informatix\MicroGDS 11.3\Sample Drawings\Sdmf1.man";
        var optionXml = new Informatix.MGDS.Import.MAN.Options().Xml();
        Cad.Open(fileName, optionXml);

        ThrowsCadException(InvalidParameter, () =>
        {
            Cad.Open(null, optionXml);
        });
#if false
        // This test is not run due to a critical bug in MicroGDS 11.3.
        // Bug details:
        // When an empty string ("") is passed as the fileName argument to Cad.Open,
        // after a dialog message stating "無題のファイル に正しくないパスが含まれています。",
        // a Microsoft Visual C++ Runtime Library error is displayed,
        // and MicroGDS crashes while still in communication.
        Cad.Open("", optionXml);
#endif
        ThrowsCadException(new[] { FileError, FileReadError }, () =>
        {
            Cad.Open("file", optionXml);
        });
        ThrowsCadException(new[] { FileError, FileReadError }, () =>
        {
            Cad.Open("con", optionXml);
        });
        ThrowsCadException(InvalidParameter, () =>
        {
            Cad.Open(fileName, null);
        });
        ThrowsCadException(Because, () =>
        {
            Cad.Open(fileName, "");
        });
    }

    [TestMethod]
    public void OpenTest()
    {
        ContextTest(Mgds, Open);
    }

    void SaveAs()
    {
        var tempManFile = Path.Combine(Path.GetTempPath(), $"{nameof(SaveAs)}Test.man");
        Cad.SaveAs(tempManFile);
        ThrowsCadException(InvalidParameter, () =>
        {
            Cad.SaveAs(null);
        });
        ThrowsCadException(new[] { FileWriteError, Because }, () =>
        {
            Cad.SaveAs("");
        });
    }

    [TestMethod]
    public void SaveAsTest()
    {
        ContextTest(DrawingWindow, SaveAs);
    }

    void SaveView()
    {
        Cad.SaveView();
    }

    [TestMethod]
    public void SaveViewTest()
    {
        ContextTest(DrawingWindow, SaveView);
    }

    void ScreenUpdateMode()
    {
        Cad.ScreenUpdateMode(ScreenUpdate.Interactive);
        Cad.ScreenUpdateMode(ScreenUpdate.Bulk);
    }

    [TestMethod]
    public void ScreenUpdateModeTest()
    {
        ContextTest(Mgds, ScreenUpdateMode);
    }

    void SelectAll()
    {
        Cad.SelectAll();
    }

    [TestMethod]
    public void SelectAllTest()
    {
        ContextTest(DrawingWindow, SelectAll);
    }

    void SelectObject()
    {
        Cad.SelectObject();
    }

    [TestMethod]
    public void SelectObjectTest()
    {
        ContextTest(Document, SelectObject);
    }

    void SelectPrim()
    {
        Cad.SelectPrim();
    }

    [TestMethod]
    public void SelectPrimTest()
    {
        ContextTest(Document, SelectPrim);
    }

    void SetCursorFromFile()
    {
        Cad.SetCursorFromFile(null);
        Cad.SetCursorFromFile("");

        ThrowsCadException(FileReadError, () =>
        {
            Cad.SetCursorFromFile("INVALID PATH");
        });
    }

    [TestMethod]
    public void SetCursorFromFileTest()
    {
        ContextTest(Document, SetCursorFromFile);
    }

    void SetEditColour()
    {
        Cad.SetEditColour("0"); // "By Phase"
        Cad.SetEditColour("Not 0"); // "Not By Phase"
        Cad.SetEditColour("1"); // "1"
        Cad.SetEditColour("Not 1"); // "Not 1"
        Cad.SetEditColour("32/64/96"); // "32/64/96/255"
        Cad.SetEditColour("Not 32/64/96"); // "Not 32/64/96/255"
        Cad.SetEditColour("32/64/96/128"); // "32/64/96/128"
        Cad.SetEditColour("Not 32/64/96/128"); // "Not 32/64/96/128"
        Cad.SetEditColour("All"); // "All"
        Cad.SetEditColour("By Phase"); // "By Phase"
        Cad.SetEditColour("Not All"); // Due to MicroGDS's specification, "Not All" reverts to "All".
        Cad.SetEditColour("Not By Phase"); // Bug: It becomes "By Phase" instead of "Not By Phase".

        ThrowsCadException(InvalidParameter, () =>
        {
            Cad.SetEditColour(null);
        });
        ThrowsCadException(InvalidParameter, () =>
        {
            Cad.SetEditColour("");
        });
        ThrowsCadException(InvalidParameter, () =>
        {
            Cad.SetEditColour("hoge");
        });
    }

    [TestMethod]
    public void SetEditColourTest()
    {
        ContextTest(Document, SetEditColour);
    }

    void SetEditLineStyle()
    {
        Cad.SetEditLineStyle("DEFAULT"); // "DEFAULT"
        Cad.SetEditLineStyle("Not DEFAULT"); // "Not DEFAULT"
        Cad.SetEditLineStyle("not"); // "not"
        Cad.SetEditLineStyle("not "); // "Not "
        Cad.SetEditLineStyle(null); // ""
        Cad.SetEditLineStyle("All"); // "All"
        Cad.SetEditLineStyle("Not ALL"); // "Not All"
        Cad.SetEditLineStyle(""); // ""
        Cad.SetEditLineStyle("Undefined"); // "Undefined"
        Cad.SetEditLineStyle("Not Undefined"); // "Not Undefined"
        Cad.SetEditLineStyle("*"); // "All"
        Cad.SetEditLineStyle("Not *"); // "Not All"
        Cad.SetEditLineStyle("DE*U?T"); // DE*U?T
        Cad.SetEditLineStyle("Not DE*U?T"); // "Not DE*U?T"
    }

    [TestMethod]
    public void SetEditLineStyleTest()
    {
        ContextTest(Document, SetEditLineStyle);
    }

    void SetEditMaterial()
    {
        Cad.SetEditMaterial("DEFAULT"); // "DEFAULT"
        Cad.SetEditMaterial("Not DEFAULT"); // "Not DEFAULT"
        Cad.SetEditMaterial("not"); // "not"
        Cad.SetEditMaterial("not "); // "Not "
        Cad.SetEditMaterial(null); // ""
        Cad.SetEditMaterial("All"); // "All"
        Cad.SetEditMaterial("Not ALL"); // "Not All"
        Cad.SetEditMaterial(""); // ""
        Cad.SetEditMaterial("Undefined"); // "Undefined"
        Cad.SetEditMaterial("Not Undefined"); // "Not Undefined"
        Cad.SetEditMaterial("*"); // "All"
        Cad.SetEditMaterial("Not *"); // "Not All"
        Cad.SetEditMaterial("DE*U?T"); // DE*U?T
        Cad.SetEditMaterial("Not DE*U?T"); // "Not DE*U?T"
    }

    [TestMethod]
    public void SetEditMaterialTest()
    {
        ContextTest(Document, SetEditMaterial);
    }

    void SetEditObj()
    {
        Cad.SetEditObj(null); // ""
        Cad.SetEditObj(""); // ""
        Cad.SetEditObj("a"); // "a:"
        Cad.SetEditObj("a:"); // "a:"
        Cad.SetEditObj("not a:"); // "NOT a:"
        Cad.SetEditObj("not"); // "NOT **"
        Cad.SetEditObj("not "); // "NOT **"
        Cad.SetEditObj("a:b"); // "a:b"
        Cad.SetEditObj("a:b:"); // "a:b"
        Cad.SetEditObj("a:b:c"); // "a:b:c"
        Cad.SetEditObj("a:b:c:"); // "a:b:c"
        Cad.SetEditObj("a:b:c:d"); // "a:b:c:d"
        Cad.SetEditObj("a:b:c:d:"); // "a:b:c:d"
        Cad.SetEditObj("a:b:c:d:e"); // "a:b:c:d:e"
        Cad.SetEditObj("a:b:c:d:e:"); // "a:b:c:d:e"
    }

    [TestMethod]
    public void SetEditObjTest()
    {
        ContextTest(Document, SetEditObj);
    }

    void SetEditText()
    {
        Cad.SetEditText("DEFAULT"); // "DEFAULT"
        Cad.SetEditText("Not DEFAULT"); // "Not DEFAULT"
        Cad.SetEditText("not"); // "not"
        Cad.SetEditText("not "); // "Not "
        Cad.SetEditText(null); // ""
        Cad.SetEditText("All"); // "All"
        Cad.SetEditText("Not ALL"); // "Not All"
        Cad.SetEditText(""); // ""
        Cad.SetEditText("Undefined"); // "Undefined"
        Cad.SetEditText("Not Undefined"); // "Not Undefined"
        Cad.SetEditText("*"); // "All"
        Cad.SetEditText("Not *"); // "Not All"
        Cad.SetEditText("DE*U?T"); // DE*U?T
        Cad.SetEditText("Not DE*U?T"); // "Not DE*U?T"
    }

    [TestMethod]
    public void SetEditTextTest()
    {
        ContextTest(Document, SetEditText);
    }

    void StartMicroGDS()
    {
        const int mgdsStartTimeoutMs = 30 * 1000;

        // Verify that MicroGDS successfully starts with each file format.
        // The function should fail and throw an exception for unsupported file formats.
        // Formats wrapped in ThrowsCadLinkAppErrorException are expected to fail.
        using var c = new Conversation();
        {
            int id = Cad.StartMicroGDS(StartFileType.CPJ, mgdsStartTimeoutMs);
            using var _ = new ConversationAndSessionExiter(c, sessionId: id);
        }
        ThrowsCadLinkAppErrorException(() =>
        {
            int id = Cad.StartMicroGDS(StartFileType.WND, mgdsStartTimeoutMs);
            using var _ = new ConversationAndSessionExiter(c, sessionId: id);
        });
        {
            int id = Cad.StartMicroGDS(StartFileType.MAN, mgdsStartTimeoutMs);
            using var _ = new ConversationAndSessionExiter(c, sessionId: id);
        }
        {
            int id = Cad.StartMicroGDS(StartFileType.MTF, mgdsStartTimeoutMs);
            using var _ = new ConversationAndSessionExiter(c, sessionId: id);
        }
        {
            int id = Cad.StartMicroGDS(StartFileType.STY, mgdsStartTimeoutMs);
            using var _ = new ConversationAndSessionExiter(c, sessionId: id);
        }
        {
            int id = Cad.StartMicroGDS(StartFileType.AIF, mgdsStartTimeoutMs);
            using var _ = new ConversationAndSessionExiter(c, sessionId: id);
        }
        {
            int id = Cad.StartMicroGDS(StartFileType.BIF, mgdsStartTimeoutMs);
            using var _ = new ConversationAndSessionExiter(c, sessionId: id);
        }
        ThrowsCadLinkAppErrorException(() =>
        {
            int id = Cad.StartMicroGDS(StartFileType.DWG, mgdsStartTimeoutMs);
            using var _ = new ConversationAndSessionExiter(c, sessionId: id);
        });
        ThrowsCadLinkAppErrorException(() =>
        {
            int id = Cad.StartMicroGDS(StartFileType.DXF, mgdsStartTimeoutMs);
            using var _ = new ConversationAndSessionExiter(c, sessionId: id);
        });
        {
            int id = Cad.StartMicroGDS(StartFileType.CV7, mgdsStartTimeoutMs);
            using var _ = new ConversationAndSessionExiter(c, sessionId: id);
        }
        ThrowsCadLinkAppErrorException(() =>
        {
            int id = Cad.StartMicroGDS(StartFileType.XML, mgdsStartTimeoutMs);
            using var _ = new ConversationAndSessionExiter(c, sessionId: id);
        });
        // Wait for MicroGDS to completely exit so that we can determine if the
        // session started in the next test.
        while (Cad.GetSessionCount() > 0)
        {
            Thread.Sleep(0);
        }

        // Confirming that if MicroGDS does not start within the specified time, 
        // the function fails and throws an exception due to the timeout setting.
        ThrowsCadLinkAppErrorException(() =>
        {
            // An exception is thrown immediately, but MicroGDS will still start
            int id = Cad.StartMicroGDS(StartFileType.MAN, 0);
            using var _ = new ConversationAndSessionExiter(c, sessionId: id);
        });

        // Wait for the start of MicroGDS from the previous test to complete to
        // start communication.
        int count;
        while (true)
        {
            count = Cad.GetSessionCount();
            if (count == 1) break;
            Thread.Sleep(0);
        }

        // Cleanup: Start communication and terminate MicroGDS.
        var ids = new int[1];
        Cad.GetSessionIDs(ids, 1);
        while (true)
        {
            try
            {
                using var exiter = new ConversationAndSessionExiter(c, sessionId: ids[0]);
                break;
            }
            catch (Cad.CadException ex)
            {
                // If you try to start communication immediately after starting
                // MicroGDS, these errors may occur, so retry until successful.
                if (ex.ErrorOccurred(AppErrorType.MGDS, CommSetupFail) ||
                    ex.ErrorOccurred(AppErrorType.MGDS, NoConversation))
                {
                    // If communication setup fails, try again
                    continue;
                }
                // Throw any unexpected errors
                throw;
            }
        }
    }

    [TestMethod]
    public void StartMicroGDSTest()
    {
        ContextTest(Global, StartMicroGDS);
    }

    void WindowArrange1()
    {
        Cad.WindowArrange(Arrange.Cascade);
        Cad.WindowArrange(Arrange.Tile);
        Cad.WindowArrange(Arrange.Icons);
        Cad.WindowArrange(Arrange.TileHoriz);
        Cad.WindowArrange(Arrange.TileVert);
    }

    [TestMethod]
    public void WindowArrange1Test()
    {
        ContextTest(Document, WindowArrange1);
    }

    void WindowArrange2()
    {
        // The active document changes when Cad.WindowArrange(Arrange.Minimise) is called.
        // Therefore, save the ID of the active document before the call,
        // and reactivate the original document after the call.
        Cad.DocGetCurID(out string docId);
        Cad.WindowArrange(Arrange.Minimise);
        Cad.DocFind(false, docId);
        Cad.WindowArrange(Arrange.Maximise);
        Cad.WindowArrange(Arrange.Restore);
    }

    [TestMethod]
    public void WindowArrange2Test()
    {
        ContextTest(DrawingWindow, WindowArrange2);
    }
}
