using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ElectricalProspectingProfiling.Tools
{
    static public class MessageShow
    {
        public static void Information(string info) => MessageBox.Show(info,"Информация!",MessageBoxButton.OK,MessageBoxImage.Information);
    }
}
