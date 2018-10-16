using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DashBoardBLL
{
    public class BOStates
    {
        DashBoardDataClassesDataContext context  = new DashBoardDataClassesDataContext();

        public List<StatesPopulation> GetStatesGraph()
        {
            var statesPopulation = (from sp in context.StatesPopulations
                                    select sp).ToList();

            return statesPopulation;
        }

        public State GetStateByID(int stateId)
        {
            var state = (from s in context.States
                         where s.StateID == stateId
                         select s).FirstOrDefault();
            return state;
        }

        public List<DB_GetStatePopulationsResult> GetStatePopulations(int stateId)
        {
            var statePopulations = (from o in context.DB_GetStatePopulations(stateId)
                                    select o).ToList();

            return statePopulations;
        }

        public List<StateReport> GetStatesReport()
        {
            var states = (from o in context.States
                          select new StateReport
                          {
                              StateID = o.StateID,
                              StateName = o.StateName,
                              StateCode = o.StateCode,
                              Area = o.Area,
                              Population = o.Population
                          }).ToList();

            return states;
        }

        public List<StateReport> GetStatesReport(string searchValue)
        {
            var states = (from o in context.States
                          where o.StateCode.Contains(searchValue) || o.StateName.Contains(searchValue)
                          select new StateReport
                          {
                              StateID = o.StateID,
                              StateName = o.StateName,
                              StateCode = o.StateCode,
                              Area = o.Area,
                              Population = o.Population
                          }).ToList();

            return states;
        }

        public List<State> GetDDLStates()
        {
            var states = (from o in context.States
                          select o).ToList();

            return states;
        }

        public bool SaveState(string stateName, string stateCode, string area, int population)
        {
            try
            {
                State newState = new State();
                newState.StateName = stateName;
                newState.StateCode = stateCode;
                newState.Area = area.ToCharArray()[0];
                newState.Population = population;
                context.States.InsertOnSubmit(newState);
                context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateState(int stateId, string stateName, string stateCode, string area, int population)
        {
            try
            {
                State state = (from o in context.States
                               where o.StateID == stateId
                               select o).FirstOrDefault();
                if (state != null)
                {
                    state.StateName = stateName;
                    state.StateCode = stateCode;
                    state.Area = area.ToCharArray()[0];
                    state.Population = population;

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

    public class StateReport
    {
        public int StateID { get; set; }
        public string StateName { get; set; }
        public string StateCode { get; set; }
        public char? Area { get; set; }
        public int? Population { get; set; }
    }
}
