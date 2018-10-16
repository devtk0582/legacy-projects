using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DashBoardBLL
{
    public class BOTeams
    {
        DashBoardDataClassesDataContext context = new DashBoardDataClassesDataContext();

        public List<TeamChamp> GetTeamChampions()
        {
            var teamChamps = (from tc in context.TeamChamps
                              select tc).ToList();

            return teamChamps;
        }

        public List<DB_GetTeamChampionsResult> GetTeamChampions(int teamId)
        {
            var teamChamps = (from o in context.DB_GetTeamChampions(teamId)
                              select o).ToList();
            return teamChamps;
        }

        public Team GetTeamByID(int teamId)
        {
            var team = (from o in context.Teams
                        where o.TeamID == teamId
                        select o).FirstOrDefault();

            return team;
        }

        public List<TeamReport> GetTeamsReport()
        {
            var teams = (from o in context.Teams
                         select new TeamReport
                         {
                             TeamID = o.TeamID,
                             TeamName = o.TeamName,
                             StateName = o.State.StateCode,
                             Arena = o.Arena,
                             HeadCoach = o.HeadCoach,
                             Champions = o.Champions,
                             Founded = o.Founded
                         }).ToList();
            return teams;
        }

        public List<TeamReport> GetTeamsReport(string searchValue)
        {
            var teams = (from o in context.Teams
                         where o.TeamName.Contains(searchValue) || o.State.StateName.Contains(searchValue) || o.TeamStars.Contains(searchValue)
                         select new TeamReport
                         {
                             TeamID = o.TeamID,
                             TeamName = o.TeamName,
                             StateName = o.State.StateCode,
                             Arena = o.Arena,
                             HeadCoach = o.HeadCoach,
                             Champions = o.Champions,
                             Founded = o.Founded
                         }).ToList();
            return teams;
        }

        public List<Team> GetDDLTeams()
        {
            var teams = (from o in context.Teams
                         select o).ToList();

            return teams;
        }

        public bool SaveTeam(string teamName, int stateId, string teamStars, string arena, string headCoach, int champions, int founded)
        {
            try
            {
                Team newTeam = new Team();
                newTeam.TeamName = teamName;
                if (stateId == 0)
                    newTeam.State = null;
                else
                    newTeam.State = (from o in context.States
                                 where o.StateID == stateId
                                 select o).FirstOrDefault();

                newTeam.TeamStars = teamStars;
                newTeam.Arena = arena;
                newTeam.HeadCoach = headCoach;
                newTeam.Champions = champions;
                newTeam.Founded = founded;
                context.Teams.InsertOnSubmit(newTeam);
                context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateTeam(int teamId, string teamName, int stateId, string teamStars, string arena, string headCoach, int champions, int founded)
        {
            try
            {
                Team team = (from o in context.Teams
                             where o.TeamID == teamId
                             select o).FirstOrDefault();

                if (team != null)
                {
                    team.TeamName = teamName;
                    if (stateId == 0)
                        team.State = null;
                    else
                        team.State = (from o in context.States
                                      where o.StateID == stateId
                                      select o).FirstOrDefault();

                    team.TeamStars = teamStars;
                    team.Arena = arena;
                    team.HeadCoach = headCoach;
                    team.Champions = champions;
                    team.Founded = founded;
                    context.Teams.InsertOnSubmit(team);
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

    public class TeamReport
    {
        public int TeamID { get; set; }
        public string TeamName { get;set;}
        public string StateName {get;set;}
        public string Arena {get;set;}
        public string HeadCoach {get;set;}
        public int? Champions {get;set;}
        public int? Founded {get;set;}
    }
}
