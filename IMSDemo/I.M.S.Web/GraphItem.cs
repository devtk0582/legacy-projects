using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace I.M.S.Web
{
    public class GraphItem
    {

        private int? m_id;
        private string m_name;
        private int m_count;

        public int? ItemID
        {
            get { return m_id; }
            set { m_id = value; }
        }

        public string ItemName
        {
            get { return m_name; }
            set { m_name = value; }
        }


        public int Qty
        {
            get { return m_count; }
            set { m_count = value; }
        }
    }
}