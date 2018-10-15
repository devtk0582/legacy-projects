using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicCutterBusinessLogic
{
    public class BOUsers
    {
        private DynamicCutterAppDataClassesDataContext context;

        public BOUsers(string connectionString)
        {
            context = new DynamicCutterAppDataClassesDataContext(connectionString);
        }

        public DC_User AuthenticateUser(string logInID, string password)
        {
            return (from u in context.DC_Users
                        where u.LoginId == logInID 
                        && u.Password == Util.EncryptString(password)
                        && u.Status.Value == true
                        select u).SingleOrDefault();
        }

        public void UpdateCurrentUser(string side, int userId)
        {
            var currentUser = (from cu in context.DC_CurrentUsers
                               where cu.Side == side
                               select cu).SingleOrDefault();

            if (currentUser != null)
            {
                if (userId == 0)
                    currentUser.UserID = null;
                else
                    currentUser.UserID = userId;

                context.SubmitChanges();
            }
        }

        public bool HasOtherSideCurrentUser(string side)
        {
            var currentUser = (from cu in context.DC_CurrentUsers
                               where cu.Side != side
                               select cu).SingleOrDefault();

            if (currentUser != null && currentUser.UserID != null && currentUser.UserID.Value > 0)
                return true;
            else
                return false;
        }
    }
}
