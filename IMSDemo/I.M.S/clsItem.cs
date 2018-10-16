using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace I.M.S
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
