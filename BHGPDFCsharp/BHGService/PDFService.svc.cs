using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using System.Data;
using BHGPDFCsharp;


namespace BHGService
{
    // NOTE: If you change the class name "Service1" here, you must also update the reference to "Service1" in Web.config and in the associated .svc file.
    public class PDFService : IBHGService
    {
        public DataSet GetForms1846_BAL(string strStartDate, string strEndDate)
        {
            return (new Form1846_BAL().Forms_BAL(strStartDate, strEndDate));
        }

        public DataSet GetForms1624_BAL(string strStartDate, string strEndDate)
        {
            return (new Form1624_BAL().Forms_BAL(strStartDate, strEndDate));
        }

        /*
        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }*/

        #region IBHGService Members

        DataSet IBHGService.GetForms1846_BAL(string strStartDate, string strEndDate)
        {
            throw new NotImplementedException();
        }

        DataSet IBHGService.GetForms1624_BAL(string strStartDate, string strEndDate)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
