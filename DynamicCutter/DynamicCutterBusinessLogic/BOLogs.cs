using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicCutterBusinessLogic
{
    public class BOTransmitLogs
    {
        private DynamicCutterAppDataClassesDataContext context;

        public BOTransmitLogs(string connectionString)
        {
            context = new DynamicCutterAppDataClassesDataContext(connectionString);
        }

        public void SaveTransmitLog(int rollID, string command, char side, string flagRxTx)
        {
            DC_TransmitLog newTransmitLog = new DC_TransmitLog()
            {
                RollID = rollID,
                LogTime = DateTime.Now,
                LogMessage = command,
                Side = side,
                RxTxFlag = flagRxTx
            };

            context.DC_TransmitLogs.InsertOnSubmit(newTransmitLog);
            context.SubmitChanges();
        }
    }

    public class BOExamRollLogs
    {
        private DynamicCutterAppDataClassesDataContext context;

        public BOExamRollLogs(string connectionString)
        {
            context = new DynamicCutterAppDataClassesDataContext(connectionString);
        }

        public int SaveExamRollLog(int rollID, string meterNum, string side, string type)
        {
            DC_ExamRollLog newExamRollLog = new DC_ExamRollLog()
            {
                RollID = rollID,
                LMR = meterNum,
                Side = side,
                ActionID = 1,
                Type = type,
                Transmitted = false
            };

            context.DC_ExamRollLogs.InsertOnSubmit(newExamRollLog);
            context.SubmitChanges();

            return newExamRollLog.LogID;
        }

        public void UpdateExamRollLog(int logID, string meterNum, string grade, string defectCode, string spot, string side, string shade, string action)
        {
            var examRollLog = (from log in context.DC_ExamRollLogs
                               where log.LogID == logID
                               select log).SingleOrDefault();

            if (examRollLog != null)
            {
                examRollLog.LMR = meterNum;
                examRollLog.Grade = int.Parse(grade);
                examRollLog.DefectCode = defectCode;
                examRollLog.Spot = int.Parse(spot);
                examRollLog.Side = side;
                examRollLog.Shade = int.Parse(shade);
                examRollLog.ActionID = int.Parse(action);
                context.SubmitChanges();
            }
        }

        public ExamRollLogView GetExamRollLogByID(int logID)
        {
            return (from roll in context.DC_ExamRollLogs
                    join action in context.DC_ActionMasters on roll.ActionID equals action.ActionID
                    where roll.LogID == logID
                    select new ExamRollLogView()
                    {
                       LogID = roll.LogID,
                       RollID = roll.RollID,
                       LMR = roll.LMR,
                       Grade = roll.Grade,
                       Spot = roll.Spot,
                       DefectCode = roll.DefectCode,
                       Side = roll.Side,
                       Shade = roll.Shade,
                       ActionID = roll.ActionID,
                       ActionDesc = action.ActionDesc,
                       Type = roll.Type,
                       Transmitted = roll.Transmitted
                    }).SingleOrDefault();
        }

        public void DeleteExamRollLogByLogID(int logID)
        {
            context.DeleteExamRollLog(logID);
        }

        public void DeleteExamRollLogsbyRollID(int rollID)
        {
            context.AbortRoll(rollID);
        }

    }

    public class ExamRollLogEventArgs : System.EventArgs
    {
        public RollCommandType CommandType { get; set; }

        public ExamRollLogEventArgs(RollCommandType type)
        {
            CommandType = type;
        }
    }

    public class BOErrorLogs
    {
        private DynamicCutterAppDataClassesDataContext context;

        public BOErrorLogs(string connectionString)
        {
            context = new DynamicCutterAppDataClassesDataContext(connectionString);
        }

        public int LogError(Exception ex)
        {
            DC_ErrorLog newError = new DC_ErrorLog()
            {
                ErrorMessage = ex.Message,
                StackTrace = ex.StackTrace,
                ErrorTime = DateTime.Now
            };

            context.DC_ErrorLogs.InsertOnSubmit(newError);
            context.SubmitChanges();
            return newError.ErrorID;
        }
    }
}
