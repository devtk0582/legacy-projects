using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicCutterBusinessLogic
{
    public class BORolls
    {
        private DynamicCutterAppDataClassesDataContext context;

        public BORolls(string connectionString)
        {
            context = new DynamicCutterAppDataClassesDataContext(connectionString);
        }

        public GetRollInfoResult GetRollInfo()
        {
            var rollInfo = context.GetRollInfo().SingleOrDefault();

            if (rollInfo == null)
            {
                rollInfo = new GetRollInfoResult();
                rollInfo.CurrentRoll = "";
                rollInfo.NextRoll = "";
            }

            return rollInfo;
        }

        public bool UpdateRollInfoByID(int rollID, string rollNumber)
        {
            var roll = (from r in context.DC_RollMasters
                        where r.RollID == rollID
                        select r).SingleOrDefault();

            if (roll != null)
            {
                roll.RollNumber = rollNumber;
                context.SubmitChanges();
                return true;
            }
            else
                return false;
        }

        public ChangeRollResult ChangeRoll(int currentRollID, int nextRollID)
        {
            return context.ChangeRoll(currentRollID, nextRollID).SingleOrDefault();
        }
    }

    public enum RollCommandType
    {
        None, OperatorInfo, DropClamps, EndOfRoll, NewRoll, EnableDisable, Others
    }

    public class ExamRollLogView
    {
        public int LogID { get; set; }

        public int RollID { get; set; }

        public string LMR { get; set; }

        public int? Grade { get; set; }

        public int? Spot { get; set; }

        public string DefectCode { get; set; }

        public string Side { get; set; }

        public int? Shade { get; set; }

        public int? ActionID { get; set; }

        public string ActionDesc { get; set; }

        public string Type { get; set; }

        public bool? Transmitted { get; set; }
    }
}
