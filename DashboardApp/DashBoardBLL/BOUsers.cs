using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DashBoardBLL
{
    public class BOUsers
    {
        DashBoardDataClassesDataContext context  = new DashBoardDataClassesDataContext();

        public User Authenticate(string username, string password)
        {
            var user = (from u in context.Users
                        where u.UserName == username && u.Password == password
                        select u).FirstOrDefault();
            return user;
        }

        public int Register(string username, string password, string firstName, string lastName, string email, string phone)
        {
            return context.DB_Register(firstName, lastName, username, password, email, phone);
        }

        public User GetUserByEmail(string email)
        {
            var user = (from u in context.Users
                        where u.Email == email
                        select u).FirstOrDefault();
            return user;
        }

        public List<StateUser> GetUsersGraph()
        {
            var stateUsers = (from u in context.StateUsers
                         select u).ToList();
            return stateUsers;
        }

        public List<DB_GetStateUsersResult> GetStateUsers(int stateId)
        {
            var stateUsers = (from o in context.DB_GetStateUsers(stateId)
                              select o).ToList();

            return stateUsers;
        }

        public List<UserReport> GetUsersReport(int userId)
        {
            List<UserReport> users = new List<UserReport>();
            if (userId == 1)
            {
                users = (from o in context.Users
                         select new UserReport() { UserID = o.UserID, UserName = o.UserName, FirstName = o.FirstName, 
                             LastName = o.LastName, Email = o.Email, StateName = o.State1.StateCode, 
                             TeamName = o.Team1.TeamName, Active = (bool)o.Active }).ToList();
            }
            else
            {
                users = (from o in context.Users
                         where o.UserID > 1
                         select new UserReport() { UserID = o.UserID, UserName = o.UserName, FirstName = o.FirstName, 
                             LastName = o.LastName, Email = o.Email, StateName = o.State1.StateCode, 
                             TeamName = o.Team1.TeamName, Active = (bool)o.Active }).ToList();
            }

            return users;
        }

        public List<UserReport> GetUsersReport(int userId, string searchValue)
        {
            List<UserReport> users = new List<UserReport>();
            if (userId == 1)
            {
                users = (from o in context.Users
                         where o.UserName.Contains(searchValue) || o.FirstName.Contains(searchValue) || o.LastName.Contains(searchValue) || o.Email.Contains(searchValue)
                         select new UserReport() { UserID = o.UserID, UserName = o.UserName, FirstName = o.FirstName, 
                             LastName = o.LastName, Email = o.Email, StateName = o.State1.StateCode, 
                             TeamName = o.Team1.TeamName, Active = (bool)o.Active }).ToList();
            }
            else
            {
                users = (from o in context.Users
                         where o.UserID > 1 && (o.UserName.Contains(searchValue) || o.FirstName.Contains(searchValue) || o.LastName.Contains(searchValue) || o.Email.Contains(searchValue))
                         select new UserReport() { UserID = o.UserID, UserName = o.UserName, FirstName = o.FirstName, 
                             LastName = o.LastName, Email = o.Email, StateName = o.State1.StateCode, 
                             TeamName = o.Team1.TeamName, Active = (bool)o.Active }).ToList();
            }

            return users;
        }

        public User GetUserByID(int userId)
        {
            var user = (from o in context.Users
                        where o.UserID == userId
                        select o).FirstOrDefault();

            return user;
        }

        public bool SaveUser(string userName, string password, string firstName,
            string lastName, string email, string phone, int stateId, int teamId)
        {
            try
            {
                User newUser = new User();
                newUser.UserName = userName;
                newUser.Password = password;
                newUser.FirstName = firstName;
                newUser.LastName = lastName;
                newUser.Email = email;
                newUser.Phone = phone;
                if(stateId == 0)
                    newUser.State = null;
                else
                    newUser.State = stateId;
                if (teamId == 0)
                    newUser.Team = null;
                else
                    newUser.Team = teamId;
                newUser.Active = true;
                context.Users.InsertOnSubmit(newUser);
                context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateUser(int userId, string userName, string firstName, string lastName,
            string email, string phone, int stateId, int teamId)
        {
            try
            {
                var user = (from o in context.Users
                            where o.UserID == userId
                            select o).FirstOrDefault();
                if (user != null)
                {
                    user.UserName = userName;
                    user.FirstName = firstName;
                    user.LastName = lastName;
                    user.Email = email;
                    user.Phone = phone;

                    if (stateId == 0)
                    {
                        user.State1 = null;
                    }
                    else
                    {
                        user.State1 = (from o in context.States
                                      where o.StateID == stateId
                                      select o).FirstOrDefault();
                    }

                    if (teamId == 0)
                    {
                        user.Team1 = null;
                    }
                    else
                    {
                        user.Team1 = (from o in context.Teams
                                      where o.TeamID == teamId
                                      select o).FirstOrDefault();
                    }
                    context.SubmitChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

    public class UserReport
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string StateName { get; set; }
        public string TeamName { get; set; }
        public bool Active { get; set; }
    }
}
