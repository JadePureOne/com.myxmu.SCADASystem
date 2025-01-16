using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace com.myxmu.SCADASystem.UserControls
{
    /// <summary>
    /// MsgBox.xaml 的交互逻辑
    /// </summary>
    public partial class MsgBox : UserControl
    {
        public MsgBox()
        {
            InitializeComponent();
        }

        public MsgBox(string content)
        {
            InitializeComponent();
            HeaderTitle.Text = content;
        }

        public MsgBox(string content, MessageBoxButton button = MessageBoxButton.OK)
        {
            InitializeComponent();
            HeaderTitle.Text = content;

            if (button == MessageBoxButton.YesNo)
            {
                stackPanelYesOrNo.Visibility = Visibility.Visible;
            }
            else
            {
                stackPanelOk.Visibility = Visibility;
            }
        }
    }
}
