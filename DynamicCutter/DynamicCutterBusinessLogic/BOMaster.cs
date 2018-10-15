using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicCutterBusinessLogic
{
    public class BOMaster
    {
        private DynamicCutterAppDataClassesDataContext context;

        public BOMaster(string connectionString)
        {
            context = new DynamicCutterAppDataClassesDataContext(connectionString);
        }

        public GetUserMasterDataResult GetUserMasterData(int userId, string ip)
        {
            return context.GetUserMasterData(userId, ip).SingleOrDefault();
        }

        public List<string> GetDefectCodes()
        {
            return (from defect in context.DC_DefectMasters
                    select defect.DefectCode).ToList();
        }

        public List<DC_ActionMaster> GetActions()
        {
            return (from action in context.DC_ActionMasters
                    select action).ToList();
        }

        public string GetActionDescriptionByID(int actionId)
        {
            return (from action in context.DC_ActionMasters
                    where action.ActionID == actionId
                    select action.ActionDesc).SingleOrDefault();
        }

        public int GetLastestRollCounter()
        {
            var counter = (from ack in context.DC_TransmitLogs
                           where ack.Side == 'A'
                           orderby ack.LogID descending
                           select ack.LogMessage).FirstOrDefault();

            if (!string.IsNullOrEmpty(counter))
                return int.Parse(counter.Substring(1, 2));
            else
                return 0;
        }
    }
}
