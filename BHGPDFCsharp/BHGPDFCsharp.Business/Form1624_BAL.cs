using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BHGPDFCsharp
{
    public class Form1624_BAL
    {
        public System.Data.DataSet Forms_BAL(string strStartDate, string strEndDate)
        {
            return (new Form1624_DAL().Form1624(strStartDate, strEndDate));
        }
    }
}
