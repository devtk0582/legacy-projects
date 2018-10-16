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
    public class clsItemDetail
    {
        private string m_name;
        private string m_code;
        private int? m_qty;
        private DateTime? m_date;
        private string m_loc;
        private string m_employee;
        private int? m_fullSize;

        public string ItemName
        {
            get { return m_name; }
            set { m_name = value; }
        }
        public string ItemCode
        {
            get { return m_code; }
            set { m_code = value; }
        }
        public int? Qty
        {
            get { return m_qty; }
            set { m_qty = value; }
        }
        public DateTime? ScanDate
        {
            get { return m_date; }
            set { m_date = value; }
        }
        public string Location
        {
            get { return m_loc; }
            set { m_loc = value; }
        }
        public string Employee
        {
            get { return m_employee; }
            set { m_employee = value; }
        }

        public int? FullSize
        {
            get { return m_fullSize; }
            set { m_fullSize = value; }
        }
    }
}
