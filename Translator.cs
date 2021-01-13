using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

using System.Text.RegularExpressions;


namespace GraphicsControlTest
{
    class Translator
    {

        public static ArrayList packetList = new ArrayList();
        public static ArrayList temp = new ArrayList();
        public static ArrayList errorList = new ArrayList();
        
        public static ArrayList packetProp(string Packet, string HeaderName, string Func, string Comments)
        {
            temp.Clear();
            temp.Add(Packet);
            temp.Add(HeaderName);
            temp.Add(Func);
            temp.Add(Comments);
            return (temp.Clone() as ArrayList);

        }

        public static ArrayList errorProp(string Error, string ErrorName, string Comments)
        {
            temp.Clear();
            temp.Add(Error);
            temp.Add(ErrorName);
            temp.Add(Comments);
            return (temp.Clone() as ArrayList);

        }

        public static string TranslatePacket(string inputString)
        {
            if (packetList.Count == 0) InitData();
                    string last_4_digits = inputString.Substring(4, 4);
                    for (int i = 0; i < packetList.Count; i++)
                    {
                        if ((packetList[i] as ArrayList)[0].ToString() == last_4_digits)
                            return "Packet: " + inputString + " - " + (packetList[i] as ArrayList)[1].ToString() + (packetList[i] as ArrayList)[2].ToString() +
                                   (packetList[i] as ArrayList)[3].ToString();
                    }

                    return "Packet: " + inputString+" - No information found";
            
        }

        public static string TranslateError(string inputString)
        {
            if (errorList.Count == 0) InitData();
            for (int i = 0; i < errorList.Count; i++)
            {
                if ((errorList[i] as ArrayList)[0].ToString() == inputString)
                    return "Error Code: -" + inputString + " - " + (errorList[i] as ArrayList)[1].ToString() + (errorList[i] as ArrayList)[2].ToString();
            }

            return "Error Code: -" + inputString + " - No information found";

        }


        public static string TranslatePackets(string inputString)
        {
            if (packetList.Count == 0) InitData();
            if (inputString.Contains("packet"))
            {
                Match packet = System.Text.RegularExpressions.Regex.Match(inputString, @"packet..?.?(?<packet>\d{8}[h])");
                if (packet.Groups["packet"].Value == "")
                    return inputString;
                else
                {
                    string packet_string = packet.Groups["packet"].ToString();
                    string last_4_digits = packet_string.Substring(4, 4);
                    bool packet_translated = false;
                    for (int i = 0; i < packetList.Count; i++)
                    {
                        if ((packetList[i] as ArrayList)[0].ToString() == last_4_digits)


                            return inputString.Replace(packet_string, packet_string + " (" + (packetList[i] as ArrayList)[1].ToString() + ")");


                    }
                    return inputString;

                }
            }
            return inputString;


            //packet=00000000
            //packet - 00000000h
            //packet 00000000h
            //
            //

        }

        public static void InitData()
        {
            packetList.Clear();
            //packetList.Add(packetProp("0001000e","IA_NET_CURRENTVERSION","","/ skipped one because forgot to bump for 5.1"));
            //packetList.Add(packetProp("00010003","IA_NET_VERSION_DEPARTMENTS","","/ First version where departments are sent in IA_NET_CONNECT"));
            //packetList.Add(packetProp("00010005","IA_NET_VERSION_PRIMESETTING","","/ First version where prime settings are sent in IA_NET_PRIMESETTING"));
            //packetList.Add(packetProp("00010006","IA_NET_VERSION_MSG_VER","","/ First version where message flags, .rc versions are sent"));
            //packetList.Add(packetProp("00010007","IA_NET_VERSION_TREEFIX","","/ First version supporting client/server version exchange, fix for tree insert bug (5529)"));
            //packetList.Add(packetProp("00010008","IA_NET_VERSION_DYNVALUES","","/ First version supporting dynamic values (value set string can now contain "c#\""));
            //packetList.Add(packetProp("00010009","IA_NET_VERSION_4_0 ","","/ Just updated this value for 4.0?"));
            //packetList.Add(packetProp("0001000a","IA_NET_VERSION_CLUSTER","","/ For 5.0. Supports encrypted values and clusters. New IA_NET_CONNECT parameters"));
            //packetList.Add(packetProp("0001000b","IA_NET_VERSION_TASKPREFETCH","","/ For 5.0 SP1, supports IAValueTaskCacheFill, sends parameter upon connecting, new retriggering changes like more data on IA_NET_REQBATCHDEBUG"));
            //packetList.Add(packetProp("0001000d ","IA_NET_VERSION_5_2","","/ DFor 5.2 Full Release.  Supports nodeloc activation, etc."));
            packetList.Add(packetProp("0000", "IA_NET_MSGBASE", "", ""));
            packetList.Add(packetProp("0001", "IA_NET_ECHOSTRING", "", "/ <none>"));
            packetList.Add(packetProp("0002", "IA_NET_REQLIST", "IAListInit()", "/ ialist.c"));
            packetList.Add(packetProp("0003", "IA_NET_REQNODEIDS", "IATreeRequestIDs()", "/ iatree.c"));
            packetList.Add(packetProp("0004", "IA_NET_REQTREEBRANCH", "IATreeNodeLock()", "/ iatree.c"));
            packetList.Add(packetProp("0005", "IA_NET_REQFILE", "IAFileOpen()", "/ iafile.c"));
            packetList.Add(packetProp("0006", "IA_NET_REQTASK", "", "/ <none>"));
            packetList.Add(packetProp("0007", "IA_NET_REQVALUE", "IAValueRequest()", "/ iavalue.c"));
            packetList.Add(packetProp("0008", "IA_NET_SETVALUE", "IATaskFinish()", "/ iatask.c"));
            packetList.Add(packetProp("0009", "IA_NET_CONNECT", "IAConnect()", "/ iaclient.c"));
            packetList.Add(packetProp("000a", "IA_NET_REQPDCLOCK", "IATreeNodePDCLock()/IATreeLockWaitLock()", "/ iatree.c"));
            packetList.Add(packetProp("000b", "IA_NET_REQPDCUNLOCK", "IATreeNodePDCUnlockEx()/IATreeLockWaitUnlock()", "/ iatree.c"));
            packetList.Add(packetProp("000c", "IA_NET_REQPDCSTATUS", "IATreeNodePDCGetStatus()/IATreeLockWaitCheck()/IATreeNodeVerifyLock()", "/ iatree.c"));
            packetList.Add(packetProp("000d", "IA_NET_REQCOMMITPAGES", "IATreeCommitPages()", "/ iatree.c"));
            packetList.Add(packetProp("000e", "IA_NET_REQPDCLOCKPARENTS", "", "/ <none>"));
            packetList.Add(packetProp("000f", "IA_NET_RENAMEFILE", "IAFileRename()", "/ iafile.c"));
            packetList.Add(packetProp("0010", "IA_NET_UNLINKFILE", "IAFileUnlink()", "/ iafile.c"));
            packetList.Add(packetProp("0012", "IA_NET_REQPROCESSNEW", "IAProcessNew()", "/ iabatch.c"));
            packetList.Add(packetProp("0013", "IA_NET_REQPROCESSDELETE", "IAProcessDelete()", "/ iabatch.c"));
            packetList.Add(packetProp("0014", "IA_NET_REQPROCESSCOPY", "", "/ <none>"));
            packetList.Add(packetProp("0015", "IA_NET_REQPROCESSEXPORT", "", "/ <none>"));
            packetList.Add(packetProp("0020", "IA_NET_REQBATCHNEW", "IABatchNew()", "/ iabatch.c"));
            packetList.Add(packetProp("0021", "IA_NET_REQBATCHDELETE", "IABatchDelete()", "/ iabatch.c"));
            packetList.Add(packetProp("0022", "IA_NET_REQBATCHUNLOAD", "IABatchUnload()", "/ iabatch.c"));
            packetList.Add(packetProp("0030", "IA_NET_REQSETMAXTASKS", "IASet(_MAXTASKS/_PREFETCH)", "/ iaclient.c"));
            packetList.Add(packetProp("0032", "IA_NET_LOGIN", "IALogin()", "/ iaclient.c"));
            packetList.Add(packetProp("0033", "IA_NET_LOGOUT", "IALogout()", "/ iaclient.c"));
            packetList.Add(packetProp("0031", "IA_NET_SETTREEUPDATE", "", "/ <none>"));
            packetList.Add(packetProp("0034", "IA_NET_REQTREEOPEN", "IATreeLock() - obsolete", "/ iatreec.c"));
            packetList.Add(packetProp("0035", "IA_NET_REQTREECLOSE", "IATreeUnlock() - obsolete", "/ iatreec.c"));
            packetList.Add(packetProp("0040", "IA_NET_SETSECURITYINIT", "", "/ <none>"));
            packetList.Add(packetProp("0041", "IA_NET_SETSECURITYITEM", "", "/ <none>"));
            packetList.Add(packetProp("0042", "IA_NET_SETSECURITYDONE", "IASecuritySetDone()", "/ iabatch.c"));
            packetList.Add(packetProp("0044", "IA_NET_REQCHECKACCESS", "IASecurityGet()", "/ iabatch.c"));
            packetList.Add(packetProp("0045", "IA_NET_REQSETSERVERDATE", "IAServerDateSet()", "/ iabatch.c"));
            packetList.Add(packetProp("004c", "IA_NET_CHECKPARENTLIST", "IATreePDCLockParents()", "/ iatreeia.c"));
            packetList.Add(packetProp("004d", "IA_NET_FINDSAVEDLOCK", "IATreeFindLastSavedLock()", "/ iatreeia.c"));
            packetList.Add(packetProp("004e", "IA_NET_REMOVESAVEDLOCK", "IATreeRemoveSavedLock()", "/ iatreeia.c"));
            packetList.Add(packetProp("004f", "IA_NET_REQPDCBLOCKER", "IATreeLockWaitUnsave()", "/ iatreeia.c"));
            packetList.Add(packetProp("0050", "IA_NET_REQPDCSAVELOCKS", "IATreeSendSaveLocks()", "/ iatreeia.c"));
            packetList.Add(packetProp("0051", "IA_NET_REQPDCRESTORELOCKS", "IATreePDCLockParents()", "/ iatreeia.c"));
            packetList.Add(packetProp("0052", "IA_NET_REQFIRSTTASKEDCHILD", "IATreeFindFirstTaskedChild()", "/ iatreeia.c"));
            packetList.Add(packetProp("0053", "IA_NET_CLEARTASK", "IATaskClear()", "/ iatask.c"));
            packetList.Add(packetProp("0054", "IA_NET_REQSETTASKBATCH", "IASet(_BATCH)", "/ iaclient.c"));
            packetList.Add(packetProp("0055", "IA_NET_SETMODULEID", "IASet(_MODULEID)", "/ iaclient.c"));
            packetList.Add(packetProp("0056", "IA_NET_SETMODULEFEATURES", "IASet(_MODULEFEATURES)", "/ iaclient.c"));
            packetList.Add(packetProp("0057", "IA_NET_PREPARETASK", "IATaskPrepare()", "/ iatask.c"));
            packetList.Add(packetProp("0058", "IA_NET_ACTIVATEBATCH", "IABatchActivate()", "/ iabatch.c"));
            packetList.Add(packetProp("0059", "IA_NET_DEACTIVATEBATCH", "IABatchDeactivate()", "/ iabatch.c"));
            packetList.Add(packetProp("005a", "IA_NET_RESERVEINSTANCE", "IATaskReserveInstance()", "/ iatask.c"));
            packetList.Add(packetProp("005b", "IA_NET_RELEASEINSTANCE", "IATaskReleaseInstance()", "/ iatask.c"));
            packetList.Add(packetProp("005c", "IA_NET_SETMODULEDEPARTMENTS", "IASet(_DEPARTMENTS)", "/ iaclient.c"));
            packetList.Add(packetProp("005d", "IA_NET_FINDSAVEDLOCKS", "IATreeFindSavedLocks()", "/ iatreeia.c"));
            packetList.Add(packetProp("005e", "IA_NET_REQSETTASKNODES", "IASet(_TASKNODES)", "/ iaclient.c"));
            packetList.Add(packetProp("005f", "IA_NET_FOLDERACTION", "IAProcessFolder...()", "/ iabatch.c"));
            //packetList.Add(packetProp("1","IA_NET_FOLDERACTION_CREATE","IAProcessFolderCreate()","/ iabatch.c"));
            //packetList.Add(packetProp("2","IA_NET_FOLDERACTION_DELETE","IAProcessFolderDelete()","/ iabatch.c"));
            //packetList.Add(packetProp("3","IA_NET_FOLDERACTION_RENAME","IAProcessFolderRename()","/ iabatch.c"));
            packetList.Add(packetProp("0060", "IA_NET_LOGIN_SSPI", "IALogin()", "/ iaclient.c"));
            packetList.Add(packetProp("0061", "IA_NET_DELETEVALUE", "IAValueDelete()", "/ iavalue.c"));
            packetList.Add(packetProp("0062", "IA_NET_REQDISPIDVALUE", "IAValue...()", "/ iavalue.c"));
            packetList.Add(packetProp("0063", "IA_NET_SETDISPIDVALUE", "IAValue...()", "/ iavalue.c"));
            packetList.Add(packetProp("0064", "IA_NET_REQBATCHDEBUG", "IABatchDebugInit(), IABatchDebugEnd()", "/ iabatchd.c"));
            packetList.Add(packetProp("0065", "IA_NET_WRITEFINISHDEBUG", "IABatchDebugProcFinish()", "/ iabatchd.c"));
            packetList.Add(packetProp("0066", "IA_NET_UPDATEVBA", "", "/ Updates the VBA storage with changes in debugging"));
            packetList.Add(packetProp("0067", "IA_NET_REQCRYPTKEY", "Requests Diffie-Hellman public key from server", "/ iacrypt.cpp"));
            packetList.Add(packetProp("0068", "IA_NET_REQCRYPTTEST", "Sends Diffie-Hellman public key to server for test", "/ iacrypt.cpp"));
            packetList.Add(packetProp("0069", "IA_NET_REQPROTECTEDVALUE", "IAValueRequestProtected()", "/ iavalue.c"));
            packetList.Add(packetProp("006A", "IA_NET_SETPROTECTEDVALUE", "IAValueSetProtected()", "/ iavalue.c"));
            packetList.Add(packetProp("006B", "IA_NET_REQREGISTRY", "IAValueGetRegistry()", ""));
            packetList.Add(packetProp("006C", "IA_NET_SETREGISTRY", "IAValueSetRegistry()", ""));
            packetList.Add(packetProp("006D", "IA_NET_DISCONNECTCLIENT", "IADisconnectClient()", ""));
            packetList.Add(packetProp("006E", "IA_NET_WRITEFINISHDEBUGT", "IABatchDebugTreeProcFinish()", "/ iabatchd.c"));
            packetList.Add(packetProp("006F", "IA_NET_WRITEFINISHDEBUGB", "IABatchDebugBatchProcFinish()", "/ iabatchd.c"));
            packetList.Add(packetProp("0070", "IA_NET_RETRIGGERNODE", "IABatchRetriggerNode()", "/ iabatch.c"));
            packetList.Add(packetProp("0071", "IA_NET_CALLNOTIFY", "IABatchCallNotify()", "/ iabatch.c"));
            packetList.Add(packetProp("0072", "IA_NET_SETTASKCACHEFILL", "IAValueTaskCacheFill()", ""));
            packetList.Add(packetProp("0073", "IA_NET_DELETEDEPARTMENT", "IADepartmentDelete", ""));
            packetList.Add(packetProp("0066", "IA_NET_CREATEVALUE", "IAValueCreate()", "/ iavalue.c"));
            packetList.Add(packetProp("0101", "IA_NET_WRITEFILE", "IAFileClose()", "/ iafile.c"));
            packetList.Add(packetProp("0102", "IA_NET_WRITESTATS", "", "/ <none>"));
            packetList.Add(packetProp("0103", "IA_NET_WRITEFINISHTASK", "IATaskFinish()", "/ iatask.c"));
            packetList.Add(packetProp("0106", "IA_NET_MOVENODE", "IATreeNodeMoveEx()", "/ iatree.c"));
            packetList.Add(packetProp("0107", "IA_NET_DELETENODES", "IATreeNodeDelete()", "/ iatree.c"));
            packetList.Add(packetProp("0108", "IA_NET_ADDNEWNODE", "IATreeNodeAdd()", "/ iatree.c"));
            packetList.Add(packetProp("0110", "IA_NET_PINGREPLY", "IAReceiveProc()", "/ iaclient.c"));
            packetList.Add(packetProp("0111", "IA_NET_LOGMESSAGE", "IALogNTMessage()", "/ iaclient.c"));
            packetList.Add(packetProp("0112", "IA_NET_PRIMESETTING", "IASet(_PRIMEMAX...)", "/ iaclient.c"));
            packetList.Add(packetProp("0100", "IA_NET_PRIMESETTING_MAXVALUES", "", "/ tag values also used in iaclient.c, iaclient.h, iaswork.h"));
            packetList.Add(packetProp("0101", "IA_NET_PRIMESETTING_MAXVALUESIZE", "", "/ tag values also used in iaswork.h"));
            packetList.Add(packetProp("0102", "IA_NET_PRIMESETTING_MAXNODES", "", "/ tag values also used in iaswork.h"));
            packetList.Add(packetProp("0103", "IA_NET_PRIMESETTING_MAXFILES", "", "/ tag values also used in iaswork.h"));
            packetList.Add(packetProp("0113", "IA_NET_SENDMESSAGEFLAGS", "IASet(_MESSAGEFLAGS)", "/ iaclient.c"));
            packetList.Add(packetProp("0114", "IA_NET_KILLCLIENT", "IASNetConEnd()", "/ iasnet.c"));
            packetList.Add(packetProp("0115", "IA_NET_REQCOMMITNODE", "IATreeCommitNode()", "/ iatreeia.c"));
            packetList.Add(packetProp("0210", "IA_NET_SENTPING", "", ""));
            packetList.Add(packetProp("0211", "IA_NET_SENTDEBUGROUTINE", "", "/ instance routine debugging"));
            packetList.Add(packetProp("0212", "IA_NET_SENTDEBUGROUTINET", "", "/ tree routine debuggging"));
            packetList.Add(packetProp("0213", "IA_NET_SENTDEBUGROUTINEB", "", "/ batch/process routine debuggging"));
            packetList.Add(packetProp("0214", "IA_NET_TIMEOUTMARKERRESPONSE", "", "/ used when there is a timeout - provides a marker so we know when next message happens"));
            packetList.Add(packetProp("0220", "IA_NET_REQBORROWLICENSE", "", "/ Borrow a license from another server"));
            packetList.Add(packetProp("0221", "IA_NET_REQBORROWEDLICENSE", "", "/ Borrow a license from another server"));
            packetList.Add(packetProp("0222", "IA_NET_RETURNLICENSE", "", "/ Return a borrowed license"));
            packetList.Add(packetProp("0223", "IA_NET_RETURNEDLICENSE", "", "/ Return a borrowed license"));
            packetList.Add(packetProp("0301", "IA_NET_SENTERROR", "", ""));
            packetList.Add(packetProp("0302", "IA_NET_SENTLIST", "", ""));
            packetList.Add(packetProp("0303", "IA_NET_SENTNODEIDS", "", ""));
            packetList.Add(packetProp("0304", "IA_NET_SENTTREEBRANCH", "", ""));
            packetList.Add(packetProp("0305", "IA_NET_SENTFILE", "", ""));
            packetList.Add(packetProp("0306", "IA_NET_SENTTASK", "", ""));
            packetList.Add(packetProp("0307", "IA_NET_SENTVALUE", "", ""));
            packetList.Add(packetProp("0308", "IA_NET_DIDSETVALUE", "", ""));
            packetList.Add(packetProp("0309", "IA_NET_CONNECTRESULT", "", ""));
            packetList.Add(packetProp("030a", "IA_NET_SENTPDCLOCK", "", ""));
            packetList.Add(packetProp("030b", "IA_NET_SENTPDCUNLOCK", "", ""));
            packetList.Add(packetProp("030c", "IA_NET_SENTPDCSTATUS", "", ""));
            packetList.Add(packetProp("0312", "IA_NET_SENTPROCESSNEW", "", ""));
            packetList.Add(packetProp("0313", "IA_NET_SENTPROCESSDELETE", "", ""));
            packetList.Add(packetProp("0320", "IA_NET_SENTBATCHNEW", "", ""));
            packetList.Add(packetProp("0321", "IA_NET_SENTBATCHDELETE", "", ""));
            packetList.Add(packetProp("0322", "IA_NET_SENTBATCHUNLOAD", "", ""));
            packetList.Add(packetProp("0332", "IA_NET_LOGINRESULT", "", ""));
            packetList.Add(packetProp("0333", "IA_NET_LOGOUTRESULT", "", ""));
            packetList.Add(packetProp("0334", "IA_NET_TREEOPENRESULT", "", ""));
            packetList.Add(packetProp("0335", "IA_NET_UPDTADDNEWNODE", "", ""));
            packetList.Add(packetProp("0336", "IA_NET_UPDTMOVENODE", "", ""));
            packetList.Add(packetProp("0337", "IA_NET_UPDTDELETENODES", "", ""));
            packetList.Add(packetProp("0340", "IA_NET_SENTSECURITYHANDLE", "", ""));
            packetList.Add(packetProp("0341", "IA_NET_SENTSECURITYITEM", "", ""));
            packetList.Add(packetProp("0342", "IA_NET_SENTSECURITYDONE", "", ""));
            packetList.Add(packetProp("0344", "IA_NET_CHECKACCESSRESULT", "", ""));
            packetList.Add(packetProp("0345", "IA_NET_SETSERVERDATERESULT", "", ""));
            packetList.Add(packetProp("034c", "IA_NET_CHECKEDPARENTLIST", "", ""));
            packetList.Add(packetProp("034d", "IA_NET_FOUNDSAVEDLOCK", "", ""));
            packetList.Add(packetProp("034e", "IA_NET_REMOVEDSAVEDLOCK", "", ""));
            packetList.Add(packetProp("034f", "IA_NET_SENTPDCBLOCKER", "", ""));
            packetList.Add(packetProp("0350", "IA_NET_SENTPDCSAVELOCKS", "", ""));
            packetList.Add(packetProp("0351", "IA_NET_SENTPDCRESTORELOCKS", "", ""));
            packetList.Add(packetProp("0352", "IA_NET_SENTFIRSTTASKEDCHILD", "", ""));
            packetList.Add(packetProp("0353", "IA_NET_CLEAREDTASK", "", ""));
            packetList.Add(packetProp("0354", "IA_NET_SETTASKBATCHRESULT", "", ""));
            packetList.Add(packetProp("0355", "IA_NET_MODULEIDRESULT", "", ""));
            packetList.Add(packetProp("0356", "IA_NET_MODULEFEATURESRESULT", "", ""));
            packetList.Add(packetProp("0357", "IA_NET_PREPAREDTASK", "", ""));
            packetList.Add(packetProp("0358", "IA_NET_ACTIVATEDBATCH", "", ""));
            packetList.Add(packetProp("0359", "IA_NET_DEACTIVATEDBATCH", "", ""));
            packetList.Add(packetProp("035a", "IA_NET_RESERVEDINSTANCE", "", ""));
            packetList.Add(packetProp("035b", "IA_NET_RELEASEDINSTANCE", "", ""));
            packetList.Add(packetProp("035c", "IA_NET_MODULEDEPTSRESULT", "", ""));
            packetList.Add(packetProp("035e", "IA_NET_SETTASKNODESRESULT", "", ""));
            packetList.Add(packetProp("035f", "IA_NET_FOLDERACTIONRESULT", "", ""));
            packetList.Add(packetProp("0361", "IA_NET_DELETEDVALUE", "", ""));
            packetList.Add(packetProp("0362", "IA_NET_SENTDISPIDVALUE", "", ""));
            packetList.Add(packetProp("0364", "IA_NET_SENTBATCHDEBUG", "", ""));
            packetList.Add(packetProp("0367", "IA_NET_SENTCRYPTKEY", "", ""));
            packetList.Add(packetProp("0368", "IA_NET_DIDCRYPTTEST", "", ""));
            packetList.Add(packetProp("0369", "IA_NET_SENTPROTECTEDVALUE", "", ""));
            packetList.Add(packetProp("036A", "IA_NET_DIDSETPROTECTEDVALUE", "", ""));
            packetList.Add(packetProp("036B", "IA_NET_SENTREGISTRY", "", ""));
            packetList.Add(packetProp("036C", "IA_NET_DIDREGISTRYSET", "", ""));
            packetList.Add(packetProp("036D", "IA_NET_DIDDISCONNECT", "", ""));
            packetList.Add(packetProp("0370", "IA_NET_RETRIGGEREDNODE", "", ""));
            packetList.Add(packetProp("0371", "IA_NET_CALLEDNOTIFY", "", ""));
            packetList.Add(packetProp("0372", "IA_NET_DIDSETTASKCACHEFILL", "IAValueTaskCacheFill()", ""));
            packetList.Add(packetProp("0373", "IA_NET_DIDDELETEDEPARTMENT", "IAValueTaskCacheFill()", ""));
            packetList.Add(packetProp("0401", "IA_NET_WROTEFILE", "", ""));
            packetList.Add(packetProp("0406", "IA_NET_MOVEDNODE", "", ""));
            packetList.Add(packetProp("0407", "IA_NET_DELETEDNODES", "", ""));
            packetList.Add(packetProp("0408", "IA_NET_ADDEDNEWNODE", "", ""));
            packetList.Add(packetProp("0415", "IA_NET_COMMITTEDNODE", "", ""));

            errorList.Add(errorProp("4400", "IA_ERR_NOTREADY", "Scanner is not ready, try again."));
            errorList.Add(errorProp("4401", "IA_ERR_NOPAGE", "No page was found in the feeder."));
            errorList.Add(errorProp("4402", "IA_ERR_PAGES", "Pages were found in the feeder."));
            errorList.Add(errorProp("4403", "IA_ERR_DONTKNOW", "Scanner cannot determine whether there are pages in the feeder or not."));
            errorList.Add(errorProp("4404", "IA_ERR_BADSETUP", "Scanner setup parameters are incorrect."));
            errorList.Add(errorProp("4405", "IA_ERR_NODEV", "Scanner is not responding, check card, power, and cables."));
            errorList.Add(errorProp("4406", "IA_ERR_NOTME", "Driver cannot handle this type of scanner."));
            errorList.Add(errorProp("4407", "IA_ERR_END", "Scanning of the page has completed."));
            errorList.Add(errorProp("4408", "IA_ERR_PARAMERR", "Scanner parameters are incorrect."));
            errorList.Add(errorProp("4409", "IA_ERR_NOTOPEN", "s_open was not called before using the driver."));
            errorList.Add(errorProp("4410", "IA_ERR_TIMEOUT", "Scanner timed out, check paper feeder."));
            errorList.Add(errorProp("4411", "SCAN_NOTFOUND", "Driver file was not found."));
            errorList.Add(errorProp("4412", "SCAN_NOMEM", "Scanner driver out of memory."));
            errorList.Add(errorProp("4413", "IA_ERR_STILLOPEN", "Scanners still open when s_unload was called."));
            errorList.Add(errorProp("4414", "SCAN_TOOMANY", "Too many drivers loaded or too many scanners opened."));
            errorList.Add(errorProp("4415", "IA_ERR_FREADER", "Bad driver file or read error when loading."));
            errorList.Add(errorProp("4421", "IA_ERR_EXEHDR_BAD", "Driver file is not an executable."));
            errorList.Add(errorProp("4423", "IA_ERR_BAD_HANDLE", "Bad scanner handle."));
            errorList.Add(errorProp("4426", "IA_ERR_JAM", "Paper jammed in scanner; clear paper and continue."));
            errorList.Add(errorProp("4427", "IA_ERR_NOBOARD", "Scanner interface board not responding, check installation."));
            errorList.Add(errorProp("4428", "IA_ERR_NOMSDRV", "Cannot open system-level scanner driver; check installation."));
            errorList.Add(errorProp("4429", "IA_ERR_COVER", "Scanner cover is open."));
            errorList.Add(errorProp("4430", "IA_ERR_LAMP", "Bad or fading scanner lamp."));
            errorList.Add(errorProp("4431", "IA_ERR_SCANNER", "Scanner hardware problem, check scanner and paper feeder."));
            errorList.Add(errorProp("4432", "IA_ERR_COMM", "Cannot talk to scanner, check cables and power."));
            errorList.Add(errorProp("4433", "IA_ERR_SOFTWARE", "Scanner control software detected error."));
            errorList.Add(errorProp("4434", "IA_ERR_BADCARD", "Error on scanner interface card, check card."));
            errorList.Add(errorProp("4435", "IA_ERR_TRANSPORT", "Problem with scanner transport mechanism, check scanner."));
            errorList.Add(errorProp("4436", "IA_ERR_OVERHEAT", "Scanner overheated, wait for it to cool, then try again."));
            errorList.Add(errorProp("4437", "IA_ERR_BUSY", "Scanner is busy, operation can not be completed."));
            errorList.Add(errorProp("4438", "IA_ERR_NOTNOW", "Scanner command invalid in current state."));
            errorList.Add(errorProp("4439", "IA_ERR_PROCHALT", "Recognition card internal error; run Scanexec.bat to reinitialize."));
            errorList.Add(errorProp("4440", "IA_ERR_TOOSMALL", "The defined zone was too small for this scanner."));
            errorList.Add(errorProp("4450", "IA_ERR_FLFOPEN", "Cannot open image file list file."));
            errorList.Add(errorProp("4451", "IA_ERR_FPTOOLONG", "Image file path too long."));
            errorList.Add(errorProp("4452", "IA_ERR_FIFOPEN", "Cannot open image file."));
            errorList.Add(errorProp("4453", "IA_ERR_FBADFMT", "Image file is bad, error in format."));
            errorList.Add(errorProp("4454", "IA_ERR_FRDERR", "File read error."));
            errorList.Add(errorProp("4455", "IA_ERR_FWN16", "Image file width not multiple of 16 pixels."));
            errorList.Add(errorProp("4456", "IA_ERR_BADCOMP", "Cannot decompress image file."));
            errorList.Add(errorProp("4457", "IA_ERR_FUNSUPP", "Image file format is not supported"));
            errorList.Add(errorProp("4458", "IA_ERR_FEOF", "Got to end of file before done."));
            errorList.Add(errorProp("4459", "IA_ERR_COLOR", "Color or grayscale image file format not supported."));
            errorList.Add(errorProp("4460", "IA_ERR_TWAIN", "Error loading TWAIN; check Twain.dll."));
            errorList.Add(errorProp("4461", "IA_ERR_TOOMANYTAGS", "Too many tags specified to PixWin. Check count."));
            errorList.Add(errorProp("4462", "IA_ERR_WINDOWRANGE", "Window parameter was out of range in PixWin"));
            errorList.Add(errorProp("4463", "IA_ERR_DIVBYZERO", "Division by zero detected."));
            errorList.Add(errorProp("4500", "IA_ERR_NOMEM", "Not enough memory."));
            errorList.Add(errorProp("4501", "IA_ERR_PARAM", "Bad parameter."));
            errorList.Add(errorProp("4502", "IA_ERR_BADTAG", "Tag not found."));
            errorList.Add(errorProp("4503", "IA_ERR_NOCHANGE", "Tag value not changed."));
            errorList.Add(errorProp("4504", "IA_ERR_NOPIXDFLT", "Cannot load module Pixflt.dll; make sure file is in path."));
            errorList.Add(errorProp("4505", "IA_ERR_ACCESS", "Invalid access rights."));
            errorList.Add(errorProp("4506", "IA_ERR_BADPATH", "Path is bad."));
            errorList.Add(errorProp("4507", "IA_ERR_CLOSE", "Cannot close file."));
            errorList.Add(errorProp("4508", "IA_ERR_CREAT", "Cannot create file."));
            errorList.Add(errorProp("4509", "IA_ERR_DISKFULL", "Disk is full; erase files and try again."));
            errorList.Add(errorProp("4510", "IA_ERR_HANDLE", "Invalid file handle."));
            errorList.Add(errorProp("4511", "IA_ERR_TOOMANY", "Too many files open."));
            errorList.Add(errorProp("4512", "IA_ERR_WRITE", "Cannot write to file."));
            errorList.Add(errorProp("4513", "IA_ERR_BADACCESS", "Invalid access rights."));
            errorList.Add(errorProp("4514", "IA_ERR_NOTFOUND", "File not found."));
            errorList.Add(errorProp("4515", "IA_ERR_OPEN", "Cannot open file."));
            errorList.Add(errorProp("4516", "IA_ERR_BADMAGIC", "Invalid driver or cannot load driver."));
            errorList.Add(errorProp("4517", "IA_ERR_SEEK", "Seek error."));
            errorList.Add(errorProp("4518", "IA_ERR_NOFUNC", "Cannot find function within driver."));
            errorList.Add(errorProp("4519", "IA_ERR_NOTLOADED", "Driver is not loaded."));
            errorList.Add(errorProp("4520", "IA_ERR_ENDSTACK", "End of stack."));
            errorList.Add(errorProp("4521", "IA_ERR_ENDPAGE", "End of page."));
            errorList.Add(errorProp("4522", "IA_ERR_ENDZONE", "End of zone."));
            errorList.Add(errorProp("4523", "IA_ERR_UNKNOWN", "Unknown error."));
            errorList.Add(errorProp("4524", "IA_ERR_UNLINK", "Cannot delete file."));
            errorList.Add(errorProp("4525", "IA_ERR_NOCURSCANNER", "No scanner is currently selected."));
            errorList.Add(errorProp("4526", "IA_ERR_CANCEL", "User cancelled operation."));
            errorList.Add(errorProp("4527", "IA_ERR_WARNING", "Warning"));
            errorList.Add(errorProp("4528", "IA_ERR_ERROR", "Error"));
            errorList.Add(errorProp("4529", "IA_ERR_COPYNEWER1", "The driver is already installed. Copy anyway?"));
            errorList.Add(errorProp("4530", "IA_ERR_COPYNEWER2", "The driver is already installed. Copy anyway?"));
            errorList.Add(errorProp("4531", "IA_ERR_ADDSCANNER", "Add Scanner"));
            errorList.Add(errorProp("4532", "IA_ERR_PUTENV", "Error writing value to configuration file."));
            errorList.Add(errorProp("4533", "IA_ERR_GETENV", "Error reading value from configuration file."));
            errorList.Add(errorProp("4534", "IA_ERR_SAMEDEVICE", "Cannot rename file from one disk to another."));
            errorList.Add(errorProp("4535", "IA_ERR_CREATESEL", "Cannot create protected-mode selector. Check Windows configuration."));
            errorList.Add(errorProp("4536", "IA_ERR_CANTFIND", "Cannot locate SCSI device; check cable and power."));
            errorList.Add(errorProp("4537", "IA_ERR_RESTRICT", "Restricted version of ISIS. Cannot use that driver."));
            errorList.Add(errorProp("4538", "IA_ERR_CANTLOAD", "(Not Ready)"));
            errorList.Add(errorProp("4539", "IA_ERR_DRIVERBUSY", "Driver in use by another program; quit chooser?"));
            errorList.Add(errorProp("4540", "IA_ERR_SYSVERSION", "Wrong system version; requires System 6.0.5 or later."));
            errorList.Add(errorProp("4541", "IA_ERR_NOSHARE", "Share not loaded; quit Windows and run SHARE.EXE."));
            errorList.Add(errorProp("4542", "IA_ERR_LOCKED", "File sharing violation."));
            errorList.Add(errorProp("4543", "IA_ERR_SHAREBUF", "Share buffer to small; increase SHARE.EXE buffer space."));
            errorList.Add(errorProp("4144", "IA_ERR_SHARE", "File locking (SHARE) failed."));
            errorList.Add(errorProp("4545", "IA_ERR_EXISTS", "File already exists."));
            errorList.Add(errorProp("4546", "IA_ERR_READ", "File write error."));
            errorList.Add(errorProp("4547", "IA_ERR_PIXVERSION", "Wrong version of PIXDFLT. Replace all old versions of Pixdflt.dll with updated library."));
            errorList.Add(errorProp("4568", "IA_ERR_IMPROPER_PATH", "File was loaded from non-standard directory."));
            errorList.Add(errorProp("4570", "IA_ERR_NET_NOWINSOCK", "Windows Sockets is not installed or is an incompatible version."));
            errorList.Add(errorProp("4571", "IA_ERR_NET_NETDOWN", "The network is down. Check local configuration."));
            errorList.Add(errorProp("4572", "IA_ERR_NET_INPROGRESS", "There is a blocking network call already in progress. Try again when it is finished."));
            errorList.Add(errorProp("4573", "IA_ERR_NET_NOSOCKET", "No more socket handles are available."));
            errorList.Add(errorProp("4574", "IA_ERR_NET_NOBUFFER", "No buffer space is available for the operation."));
            errorList.Add(errorProp("4575", "IA_ERR_NET_PORTNOTFOUND", "Unable to find port address. Check services file or NIS setup."));
            errorList.Add(errorProp("4576", "IA_ERR_NET_HOSTNOTFOUND", "Unable to resolve address from hostname."));
            errorList.Add(errorProp("4577", "IA_ERR_NET_NOTHREAD", "Unable to allocate a new thread."));
            errorList.Add(errorProp("4578", "IA_ERR_NET_LISTEN", "Error while listening to network."));
            errorList.Add(errorProp("4579", "IA_ERR_NET_ADDRINUSE", "Address of requested port is already in use."));
            errorList.Add(errorProp("4580", "IA_ERR_NET_BADADDR", "Port address is invalid. Check services file or NIS setup."));
            errorList.Add(errorProp("4581", "IA_ERR_NET_CONNREFUSED", "Connection refused by remote host. Make sure server is operational."));
            errorList.Add(errorProp("4582", "IA_ERR_NET_NOSERVER", "Unable to reach network."));
            errorList.Add(errorProp("4583", "IA_ERR_NET_NETUNREACH", "Unable to connect to server. Check if server is operational."));
            errorList.Add(errorProp("4584", "IA_ERR_NET_LOSTCONNECTION", "Connection has been lost. Check remote process."));
            errorList.Add(errorProp("4585", "IA_ERR_NET_NOCONNECTION", "This operation requires a server connection. Please connect and try again."));
            errorList.Add(errorProp("4586", "IA_ERR_NET_ASYNCSELECT", "Error on call to WSAAsyncSelect()."));
            errorList.Add(errorProp("4587", "IA_ERR_PERM_NOACCESS", "Insufficient permissions."));
            errorList.Add(errorProp("4588", "IA_ERR_NET_NEEDMORE", "Only used by the server; means there is more data to be read in. "));
            errorList.Add(errorProp("4598", "IA_ERR_SIMULATE", "PIXDEBUG Simulated Error. (Should not occur.)"));
            errorList.Add(errorProp("4599", "IA_ERR_xxx", "Undefined error message."));
            errorList.Add(errorProp("4605", "IA_ERR_NET_TIMEOUT", "Network transfer timed out."));
            errorList.Add(errorProp("4606", "IA_ERR_NET_NORECOVERY", "Unrecoverable error. Check services file or NIS setup."));
            errorList.Add(errorProp("4607", "PIX_ERR_NOTMULTIPAGE", "Format does not support multi-page."));
            errorList.Add(errorProp("4611", "IA_ERR_NET_ALREADYWAITING", "Connection is already waiting for a synchronous response."));
            errorList.Add(errorProp("5000", "PIXM_NOMEM", "PixTools/View out of memory."));
            errorList.Add(errorProp("5001", "PIXM_BADTAG", "PixTools/View invalid tag."));
            errorList.Add(errorProp("5002", "PIXM_BADINDEX", "PixTools/View bad index."));
            errorList.Add(errorProp("5003", "PIXM_BADVALUE", "PixTools/View invalid value."));
            errorList.Add(errorProp("5004", "PIXM_BADHANDLE", "PixTools/View bad structure."));
            errorList.Add(errorProp("5005", "PIXM_BADCODE", "PixTools/View decompression error."));
            errorList.Add(errorProp("5006", "PIXM_NOTSAME", "PixTools/View not all values equal."));
            errorList.Add(errorProp("5007", "PIXM_DONE", "PixTools/View done."));
            errorList.Add(errorProp("5008", "PIXM_BADFUNC, PIXM_NOGRAY", "PixTools/View function not supported."));
            errorList.Add(errorProp("5009", "PIXM_BADESC", "PixTools/View bad device escape."));
            errorList.Add(errorProp("5010", "PIXM_RESEND_DATA", "PixTools/View need to resend data."));
            errorList.Add(errorProp("5011", "PIXM_JPEGERR", "PixTools/View JPEG error."));
            errorList.Add(errorProp("5012", "PIXM_NODATA", "PixTools/View no data in page."));
            errorList.Add(errorProp("5013", "PIXM_NCOLORPERMS", "PixTools/View no color display permissions."));
            errorList.Add(errorProp("5014", "PIXM_NOGRAYPERMS", "PixTools/View no grayscale display permissions"));
            errorList.Add(errorProp("5015", "PIXM_NOCTIPERMS", "PixTools/View no Cornerstone toolkit permissions."));
            errorList.Add(errorProp("5016", "PIXM_NOPERMS", "PixTools/View insufficient permissions."));
            errorList.Add(errorProp("5017", "PIXM_NOBINARYPEMS", "PixTools/View no binary display permissions."));
            errorList.Add(errorProp("5018", "PIXM_NOWINPRINTPERMS", "PixTools/View no Windows printing permissions."));
            errorList.Add(errorProp("5019", "PIXM_NEEDRECT", "PixTools/View need a window or a destination rectangle."));
            errorList.Add(errorProp("5020", "PIXM_GDIEROR", "PixTools/View GDI returned error."));
            errorList.Add(errorProp("5021", "PIXM_NOJPEGDECOMP", "PixTools/View no JPEG decompression permissions."));
            errorList.Add(errorProp("5022", "PIXM_NOCTIHW", "PixTools/View need Cornerstone hardware for operation."));
            errorList.Add(errorProp("5023", "PIXM_NODIB2DEV", "PixTools/View Cannot put DIB on device."));
            errorList.Add(errorProp("5024", "PIXM_UNSUPP_FMT", "PixTools/View does not support specified color format."));
            errorList.Add(errorProp("5025", "PIXM_CANTDRAW", "PixTools/View no argument to draw on."));
            errorList.Add(errorProp("5026", "PIXM_NOLZWDECMP", "PixTools/View no LZW decompression permissions."));
            errorList.Add(errorProp("5027", "PIXM_CANTROT", "PixTools/View image is too long to rotate."));
            errorList.Add(errorProp("5028", "PIXM_IMAGETOOWIDE", "PixTools/View image wider than 32K in 16bits only."));
            errorList.Add(errorProp("5029", "PIXM_IMAGETOOLONG", "PixTools/View image longer than 32K in 16bits only."));
            errorList.Add(errorProp("5030", "PIXM_NOCOMP", "PixTools/View decompression is not available."));
            errorList.Add(errorProp("5031", "PIXM_NOHIST", "PixTools/View Histogram is not available."));
            errorList.Add(errorProp("5032", "PIXM_DDRAWERR", "PixTools/View Direct Draw error."));
            errorList.Add(errorProp("5033", "PIXM_NOTNOW", "PixTools/View this operation is not available now."));
            errorList.Add(errorProp("5034", "PIXM_NOJPEG2000DECOMP", "PixTools/View no JPEG2000 decompression permissions."));
            errorList.Add(errorProp("5035", "PIXM_UNSUPP_BPS2", "PixTools/View toolkit does not support 2-bit palette."));
            errorList.Add(errorProp("5036", "PIXM_UNSUPP_BPS3", "PixTools/View toolkit does not support 3-bit palette."));
            errorList.Add(errorProp("5037", "PIXM_UNSUPP_BPS5", "PixTools/View toolkit does not support 5-bit palette."));
            errorList.Add(errorProp("5038", "PIXM_UNSUPP_BPS6", "PixTools/View toolkit does not support 6-bit palette."));
            errorList.Add(errorProp("5039", "PIXM_UNSUPP_BPS7", "PixTools/View toolkit does not support 7-bit palette."));
            errorList.Add(errorProp("5040", "PIXM_UNSUPP_TRANSPARENCY", "PixTools/View toolkit does not support transparency masks."));
            errorList.Add(errorProp("5300", "PIXM_JPEGEXPERR", "PixTools/View"));
            errorList.Add(errorProp("5301", "PIXM_JPEGEXPERR_BADSYNC", "PixTools/View"));
            errorList.Add(errorProp("5302", "PIXM_JPEGEXPERR_NONTINIT", "PixTools/View"));
            errorList.Add(errorProp("5303", "PIXM_JPEGEXPERR_ALREADYINIT", "PixTools/View"));
            errorList.Add(errorProp("6000", "IA_ERR_BADSPECIAL", "Bad special string/1."));
            errorList.Add(errorProp("6001", "IA_ERR_BADSPECIAL2", "Bad special string/2."));
            errorList.Add(errorProp("6002", "IA_ERR_BADKEYSTRING", "Bad Key string."));
            errorList.Add(errorProp("6003", "IA_ERR_NEEDTASK", "Error - need task."));
            errorList.Add(errorProp("6004", "IA_ERR_BADLOCK", "Error locking node."));
            errorList.Add(errorProp("6005", "IA_ERR_LOCKWAIT", "Error - waiting for node lock."));
            errorList.Add(errorProp("6006", "IA_ERR_ALREADYLOCKED", "Error - node is already locked."));
            errorList.Add(errorProp("6007", "IA_ERR_NODENOTCACHED", "Node is not in cache."));
            errorList.Add(errorProp("6008", "IA_ERR_UNKNOWNNT", "An unknown NT API error has occurred."));
            errorList.Add(errorProp("6009", "IA_ERR_IP_SOFTWARE", "Image Processing software error."));
            errorList.Add(errorProp("6010", "IA_ERR_IP_NOTAVAILABLE", "Image Processing hardware not available."));
            errorList.Add(errorProp("6011", "IA_ERR_IP_HARDWARE", "Image Processing hardware error."));
            errorList.Add(errorProp("6012", "IA_ERR_INDEX", "Bad batch or process index."));
            errorList.Add(errorProp("6013", "IA_ERR_INDEXFILE", "Error with index file. Please check Batchidx.txt on server."));
            errorList.Add(errorProp("6014", "IA_ERR_PICKVALUE", "Must choose an IA Value."));
            errorList.Add(errorProp("6015", "IA_ERR_SERVERMESSAGE", "Server sent a message; it should be displayed instead of this."));
            errorList.Add(errorProp("6016", "IA_ERR_BADLOGIN or IA_ERR_LOGON_FAILURE", "Could not log in; check user name and password. NT error 1326"));
            errorList.Add(errorProp("6017", "IA_ERR_NOSUCHINST", "There is no instance of this module in the batch."));
            errorList.Add(errorProp("6018", "IA_ERR_SERVERDISKFULL", "The server disk does not have enough space to continue this operation."));
            errorList.Add(errorProp("6019", "IA_ERR_LOCKWAITFORSAVE", "Error: waiting for saved node lock."));
            errorList.Add(errorProp("6020", "IA_ERR_LIC_NONE", "There are no valid licenses for this module."));
            errorList.Add(errorProp("6021", "IA_ERR_LIC_EXPIRED", "This license has expired."));
            errorList.Add(errorProp("6022", "IA_ERR_LIC_MODCONNECTIONS", "Maximum licensed module connections has been exceeded."));
            errorList.Add(errorProp("6023", "IA_ERR_LIC_MODPAGES", "Maximum licensed module pages has been exceeded."));
            errorList.Add(errorProp("6024", "IA_ERR_LIC_SRVCONNECTIONS", "Maximum licensed server connections has been exceeded."));
            errorList.Add(errorProp("6025", "IA_ERR_LIC_SRVPAGES", "Maximum licensed server pages has been exceeded."));
            errorList.Add(errorProp("6026", "IA_ERR_LIC_BADFEATURE", "The use of this feature has not been licensed."));
            errorList.Add(errorProp("6027", "IA_ERR_LIC_BADLICENSE", "This module has not been properly licensed."));
            errorList.Add(errorProp("6028", "IA_ERR_LIC_OTHERMACHINE", "Maximum licensed module connections from different machines has been exceeded."));
            errorList.Add(errorProp("6040", "IA_ERR_ACCESS_READBATCH", "You do not have permission to read this batch or process."));
            errorList.Add(errorProp("6041", "IA_ERR_ACCESS_MODIFYBATCH", "You do not have permission to modify this batch or process."));
            errorList.Add(errorProp("6042", "IA_ERR_ACCESS_DELETEBATCH", "You do not have permission to delete this batch or process."));
            errorList.Add(errorProp("6043", "IA_ERR_ACCESS_RUNMODULE", "You do not have permission to run this module."));
            errorList.Add(errorProp("6044", "IA_ERR_ACCESS_SETSEC", "You do not have permission to change permissions for this object."));
            errorList.Add(errorProp("6045", "IA_ERR_ACCESS_CREATEBATCH", "You do not have permission to create this batch."));
            errorList.Add(errorProp("6046", "IA_ERR_ACCESS_INSTALLPROCESS", "You do not have permission to install this process."));
            errorList.Add(errorProp("6047", "IA_ERR_ACCESS_NTFSONLY", "The InputAccel Server does not support permissions for this object because it is not installed on an NTFS volume."));
            errorList.Add(errorProp("6048", "IA_ERR_ACCESS_UNLOADBATCH", "You do not have permission to unload this batch."));
            errorList.Add(errorProp("6049", "IA_ERR_ACCESS_UNKNOWNACCT", "Permissions could not be set for an unknown account name."));
            errorList.Add(errorProp("6060", "IA_ERR_VERSION_OLDSERVER", "Could not connect to the server - server version is too old for the client."));
            errorList.Add(errorProp("6061", "IA_ERR_VERSION_OLDCLIENT", "Could not connect to the server - client version is too old for the server."));
            errorList.Add(errorProp("6062", "IA_ERR_VERSION_OLDBATCH", "Could not do operation batch or process version is too old."));
            errorList.Add(errorProp("6070", "IA_ERR_OLDLOCALTREE", "Local tree cache is outdated. Subsequent operations may fail."));
            errorList.Add(errorProp("6071", "IA_ERR_BADLOCALTREE", "Operation failed. Update local tree cache and try again."));
            errorList.Add(errorProp("6072", "IA_ERR_BADNODEHANDLE", "Invalid tree node. Local tree cache may be outdated."));
            errorList.Add(errorProp("6080", "IA_ERR_LOGON_ACCTRESTRICTION ", "Logon failure: user account restriction. NT error 1327: ERROR_ACCOUNT_RESTRICTION"));
            errorList.Add(errorProp("6081", "IA_ERR_LOGON_HOURS ", "Logon failure: account logon time restriction violation. NT error 1328: ERROR_INVALID_LOGON_HOURS"));
            errorList.Add(errorProp("6082", "IA_ERR_LOGON_PASSWORDEXPIRED", "Logon failure: the specified account password has expired. NT error 1330: ERROR_PASSWORD_EXPIRED"));
            errorList.Add(errorProp("6083", "IA_ERR_LOGON_ACCTDISABLED", "Logon failure: account currently disabled. NT error 1331: ERROR_ACCOUNT_DISABLED"));
            errorList.Add(errorProp("6084", "IA_ERR_LOGON_BADTYPE", "Logon failure: the user has not been granted the requested logon type. NT error 1385: ERROR_LOGON_TYPE_NOT_GRANTED"));
            errorList.Add(errorProp("6085", "IA_ERR_LOGON_ACCTLOCKEDOUT", "The referenced account is currently locked out and may not be logged on to  NT error 1909: ERROR_ACCOUNT_LOCKED_OUT"));
            errorList.Add(errorProp("6086", "IA_ERR_LOGON_NEGOTIATION", "Logon failure: error during security context negotiation."));
            errorList.Add(errorProp("6100", "IA_ERR_PASTE_BADBATCH", "Cannot paste settings; instances of batch do not match settings."));
            errorList.Add(errorProp("6101", "IA_ERR_PASTE_BADINST", "Cannot paste settings; instance does not match settings."));
            errorList.Add(errorProp("6102", "IA_ERR_PASTE_BADCONTENTS", "Cannot paste settings; settings are invalid."));
            errorList.Add(errorProp("6110", "IA_ERR_EXCEPTION", "Exception error occurred."));
            errorList.Add(errorProp("6111", "IA_ERR_ALREADYRESERVED", "This batch and instance is already reserved."));
            errorList.Add(errorProp("6112", "IA_ERR_NORETRY", "Result code passed to the optional IA Error subroutine in the PCF to control whether tasks never retry (NORETRY), retry n time (default 3) (RETRYSOME), or retry indefinitely (RETRY)."));
            errorList.Add(errorProp("6113", "IA_ERR_RETRYSOME", "Nothing"));
            errorList.Add(errorProp("6114", "IA_ERR_RETRY", "Nothing"));
            errorList.Add(errorProp("6020", "IA_ERR_SCHEMA_BATCH", "Illegal Batch schema."));
            errorList.Add(errorProp("6121", "IA_ERR_SCHEMA_PROCESS", "Illegal Process schema."));
            errorList.Add(errorProp("6130", "IA_ERR_INSUFFICIENTBUFFER", "Insufficient buffer."));
            errorList.Add(errorProp("6140", "IA_ERR_CREATEVBAHOST", "Error creating VBA host object."));
            errorList.Add(errorProp("6141", "IA_ERR_TOOMANYDEBUGGERS", "Only one debugging session per batch is possible."));
            errorList.Add(errorProp("6142", "IA_ERR_NEEDSINGLESERVER", "Operation cannot work in a cluster. "));
            errorList.Add(errorProp("6143", "IA_ERR_DIFFERENTBASES", "Servers in a cluster cannot have different bases. "));
            errorList.Add(errorProp("6144", "IA_ERR_DUPLICATECLUSTER", "Servers in a cluster cannot have the same cluster number."));
            errorList.Add(errorProp("6145", "IA_ERR_NEEDSAMESERVER", "Operation requires objects on the same Server."));
            errorList.Add(errorProp("6146", "IA_ERR_BADCLUSTER", "Inconsistent cluster parameters."));
            errorList.Add(errorProp("6147", "IA_ERR_INCOMPATIBLECLUSTER", "One of the Servers is not part of the cluster."));
            errorList.Add(errorProp("6148", "IA_ERR_CLUSTERNOTSUPPORTED", "This module is not supported in a multi-server environment."));
            errorList.Add(errorProp("6149", "IA_ERR_REQUIRESSINGLESERVER", "This login requires a single server."));
            errorList.Add(errorProp("6150", "IA_ERR_CRYPTINIT", "Error initializing cryptography functions used for encrypted IA values."));
            errorList.Add(errorProp("6151", "IA_ERR_CANTENCRYPT", "Error encrypting value."));
            errorList.Add(errorProp("6152", "IA_ERR_CANTDECRYPT", "Error decrypting encrypted value."));
            errorList.Add(errorProp("6153", "IA_ERR_CRYPTDISABLED", "Encrypted values disabled by registry setting. "));
            errorList.Add(errorProp("6160", "IA_ERR_OBJECTEMPTY", "A COM object handle is not pointing to a valid COM object.  Used by the server and internally by IAComDll. "));
            errorList.Add(errorProp("6161", "IA_ERR_CANTSETCONST", "An attempt was made to set the value of a constant. Setting the value of a constant is not valid."));
            errorList.Add(errorProp("6162", "IA_ERR_OBJECTFREED", "Object was freed before unlocking."));
            errorList.Add(errorProp("6170", "IA_ERR_3RDPARTY_CONNREFUSED", "Connection refused by third-party server."));
            errorList.Add(errorProp("6171", "IA_ERR_3RDPARTY_NOSERVER", "Unable to connect to third-party server."));
            errorList.Add(errorProp("6172", "IA_ERR_3RDPARTY_LOGON_FAILURE", "Could not log into third-party server."));
            errorList.Add(errorProp("6173", "IA_ERR_3RDPARTY_LOSTCONNECTION", "Lost connection with third-party server."));
            errorList.Add(errorProp("6174", "IA_ERR_3RDPARTY_NOCONNECTION", "Operation requires a third-party server connection."));
            errorList.Add(errorProp("6175", "IA_ERR_3RDPARTY_ALREADYCONNECTED", "Already connected to third-party server."));
            errorList.Add(errorProp("6180", "IA_ERR_DEPTINUSE_CONNECTION", "Department is in use by a connection, Cannot delete."));
            errorList.Add(errorProp("6181", "IA_ERR_DEPTINUSE_BATCH", "Department is in use by a batch or process, Cannot delete."));
            errorList.Add(errorProp("6191", "IA_ERR_ACT_NOAMPMATCH", "No matching AMP file found for activation key."));
            errorList.Add(errorProp("6192", "IA_ERR_ACT_NOAMPFILES", "No AMP files found."));
            errorList.Add(errorProp("6193", "IA_ERR_ACT_BADSTRUCTVERSION", "IASActivationSafe struct version mismatch."));
            errorList.Add(errorProp("6194", "IA_ERR_ACT_CAFBADSIG", "CAF signature element bad or missing."));
            errorList.Add(errorProp("6195", "IA_ERR_ACT_CAFMISSING", "CAF file not found."));
            errorList.Add(errorProp("6196", "IA_ERR_ACT_CAFMISSINGELEMENT", "CAF file element not found."));
            errorList.Add(errorProp("6197", "IA_ERR_ACT_CAFELEMENTUNKNOWN", "Unrecognized CAF file element found."));
            errorList.Add(errorProp("6198", "IA_ERR_ACT_NOTAFILEDONGLE", "Dongle is not a file dongle, or file dongle is not the current license mode."));
            errorList.Add(errorProp("6199", "IA_ERR_ACT_ZEROSERIALNUM", "Dongle returning zero serial number."));
            errorList.Add(errorProp("6200", "IA_ERR_ACT_SETACTLOWSCORE", "Activation key set by Administrator application correct, but score too low. "));
            errorList.Add(errorProp("6201", "IA_ERR_ACT_SETACTBADKEY", "Activation key set by Administrator application didn't work even though it contained valid chars and key string check digit calculation passed okay.  The User may have typed key in via Administrator not realizing displayed server profile ID changed (i.e. his machine profile changed due to undock or dock, or...), or user provided wrong server ID number to EMC Captiva. "));
            errorList.Add(errorProp("6202", "IA_ERR_ACT_BADKEYCHECK", "Activation key invalid - check digit test failed."));
            errorList.Add(errorProp("6203", "IA_ERR_ACT_BADKEYCHARS", "Activation key invalid - contains invalid characters."));
            errorList.Add(errorProp("6204", "IA_ERR_ACT_BADKEYLENGTH ", "Activation key invalid - must be 27 digits."));
            errorList.Add(errorProp("6205", "IA_ERR_ACT_CANTDELETEAMPFILE", "Cannot delete one or more .AMP files."));
            errorList.Add(errorProp("6206", "IA_ERR_ACT_CLOCKTAMPER", "Significant clock rollback or forward detected by grace period logic."));
            errorList.Add(errorProp("6207", "IA_ERR_ACT_REGRWFAILED ", "Registry read or write of activation state failed."));
            errorList.Add(errorProp("6208", "IA_ERR_ACT_BADFDSTATE", "Activation state data's FDState corrupt (tamper attempt?)."));
            errorList.Add(errorProp("6209", "IA_ERR_ACT_CAFBADVERSION", "CAF file version not compatible with server."));
            errorList.Add(errorProp("6220", "IA_ERR_MPS_CAFNORULESELEMENT", "CAF file has no MachineProfileRules element."));
            errorList.Add(errorProp("6221", "IA_ERR_MPS_BADXMLSYNTAX ", "General XML syntax error in file - see info log for exact details."));
            errorList.Add(errorProp("6222", "IA_ERR_MPS_CAFFILEATTRMISSING", "Attribute missing in CAF file."));
            errorList.Add(errorProp("6223", "IA_ERR_MPS_CAFFILEATTRUNKNOWN", "Unknown attribute found in CAF file."));
            errorList.Add(errorProp("6224", "IA_ERR_MPS_CAFFILEATTRPRESENT", "CAF file attribute not allowed in this context."));
            errorList.Add(errorProp("6225", "IA_ERR_MPS_XMLFILEMISSING", "XML file not found (internal temp error code)."));
            errorList.Add(errorProp("6230", "IA_ERR_CMP_NOMACHINEPROFILE", "MachineProfile XML element missing in AMP or CMP file."));
            errorList.Add(errorProp("6240", "IA_ERR_SI_MISSINGSYSDIR ", "WMI returned invalid Windows System directory path."));
            errorList.Add(errorProp("6241", "IA_ERR_SI_BADSYSDIRNAME ", "WMI returned invalid Windows System directory path drive letter."));
            errorList.Add(errorProp("6242", "IA_ERR_SI_NODISKPART", "No disk partition found for System Drive."));
            errorList.Add(errorProp("6243", "IA_ERR_SI_BADHWKEYNAME", "Unknown hardware key name requested."));
            errorList.Add(errorProp("6244", "IA_ERR_SI_WMIINSTANCEMISSING", "Requested WMI class instance is missing."));
            errorList.Add(errorProp("6245", "IA_ERR_SI_WMINULLPROPVALUE", "Requested WMI property value is null."));
            errorList.Add(errorProp("7000", "SA_Unknown (also used for general QuickModule errors)", "An unknown error occurred."));
            errorList.Add(errorProp("6812", "IA_ODBCVAL_NOT_CONNECTED", "ODBC validation dll error: Not connected to a data source."));
            errorList.Add(errorProp("6813", "IA_ODBCVAL_CANT_SET_SCROLL_OPTIONS", "ODBC validation dll error: Error setting database connection cursor type."));
            errorList.Add(errorProp("6814", "IA_ODBCVAL_MORE_DATA", "ODBC validation dll error: The last query returned more data than the buffer could hold."));
            errorList.Add(errorProp("6999", "IA_ERR_INTERNAL", "Internal InputAccel Error."));
            errorList.Add(errorProp("7001", "SA_Success", "Success."));
            errorList.Add(errorProp("7002", "SA_SAPProxy_InitFailed", "SAP Proxy could not be instantiated."));
            errorList.Add(errorProp("7003", "SA_SAPLogon_InitFailed", "SAP Logon could not be instantiated."));
            errorList.Add(errorProp("7004", "SA_SAPBAPIStr_InitFailed ", "SAP BAPI structure could not be instantiated."));
            errorList.Add(errorProp("7005", "SA_SAPBAPITbl_InitFailed ", "SAP BAPI table could not be instantiated. "));
            errorList.Add(errorProp("7006", "SA_InvalidArg_ServerName", "The ServerName argument to the function is invalid."));
            errorList.Add(errorProp("7007", "SA_InvalidArg_UserName ", "The UserName argument to the function is invalid."));
            errorList.Add(errorProp("7008", "SA_InvalidArg_Password ", "The Password argument to the function is invalid. "));
            errorList.Add(errorProp("7009", "SA_AlreadyConnected ", "A connection to the SAP Server already exists. Close the connection before establishing another one. "));
            errorList.Add(errorProp("7010", "SA_InitConnExcp ", "An exception was caught during connection initialization. "));
            errorList.Add(errorProp("7011", "SA_NotConnected ", "An external call was made prior to connecting to the server."));
            errorList.Add(errorProp("7012", "SA_InvalidArg_ContRep ", "The ArchiveID argument to the function is invalid."));
            errorList.Add(errorProp("7013", "SA_InvalidArg_DocType ", "The DocumentClass argument to the function is invalid. "));
            errorList.Add(errorProp("7014", "SA_InvalidArg_BarCode ", "The Barcode argument to the function is invalid."));
            errorList.Add(errorProp("7015", "SA_InvalidArg_ContServName", "The ContentServerName argument to the function is invalid. "));
            errorList.Add(errorProp("7016", "SA_InvalidArg_ContServScriptPath", "The ContentServerScriptPath argument to the function is invalid. "));
            errorList.Add(errorProp("7017", "SA_InvalidArg_FilePathToPost ", "The FilePathToPost argument to the function is invalid. "));
            errorList.Add(errorProp("7018", "SA_InvalidArg_UseSecureURL ", "UseSecureURL is true, but the path to the certificate is invalid. "));
            errorList.Add(errorProp("7019", "SA_CouldNotAccessFileInfo ", "Could not get access information on file."));
            errorList.Add(errorProp("7020", "SA_UnableToBuildURL", "Unable to build the URL. This is most likely due to an exception being thrown."));
            errorList.Add(errorProp("7021", "SA_PutDocToContSrvFailed", "Putting the document to the content server failed. "));
            errorList.Add(errorProp("7022", "SA_BAPIArchCaughtAnExp ", "An exception was thrown during the execution of the BAPI Archiving function."));
            errorList.Add(errorProp("7023", "SA_InvalidArg_ContServPort", "The PortNumber argument for the content server is not valid. "));
            errorList.Add(errorProp("7024", "SA_Exception ", "An exception was thrown. Call GetErrorInfo( ) for more details."));
            errorList.Add(errorProp("7025", "SA_SAPRFCTbl_Init Failed ", "The SAP RFC Table could not be instantiated."));
            errorList.Add(errorProp("7026", "SA_BeginRFCArchNotCalled ", "BeginRFCArchExport( ) was not called or was not successful. BeginRFCArchExport( ) must be called before calling this function."));
            errorList.Add(errorProp("7027", "SA_RFCTableNotInit ", "The RFC Table for RFC archiving was not initialized. BeginRFCArchExport( ) must be called successfully first."));
            errorList.Add(errorProp("7028", "SA_RemoveDocFromContSrvFailed ", "Removal of the document from the content server failed. The document must be removed manually."));
            errorList.Add(errorProp("7029", "SA_BAPIArchExpWithRemoveFailure ", "An exception was thrown during the BAPI Archiving function. During recovery, the document could not be removed and now must be removed manually."));
            errorList.Add(errorProp("7030", "SA_InvalidArg_ArchObj", "The DocumentType argument to the function is invalid."));
            errorList.Add(errorProp("7031", "SA_FinArchExpWithRemoveFailure ", "An exception was thrown during the Finish RFC Archiving function. During recovery, the document could not be removed and now must be removed manually."));

        }
    }
}