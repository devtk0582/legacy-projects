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
    public class Size3of4
    {
        private string m_name;

        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }
        private int m_count;

        public int Count
        {
            get { return m_count; }
            set { m_count = value; }
        }
         public Size3of4(string name, int count)
        {
            Name = name;
            Count = count;
        }

    }
}
