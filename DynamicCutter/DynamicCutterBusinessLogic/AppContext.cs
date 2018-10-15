using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicCutterBusinessLogic
{
    public class AppContext
    {
        private static AppContext current;

        public static AppContext Current
        {
            get { return current; }
        }

        private AppContext() { }

        public static void Login(DC_User user)
        {
            current = new AppContext()
            {
                User = user
            };
        }

        public static void Logout()
        {
            current = null;
        }

        public DC_User User
        {
            get;
            private set;
        }
    }
}
