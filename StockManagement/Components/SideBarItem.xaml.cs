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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StockManagement.Components
{
    /// <summary>
    /// Interaction logic for SideBarItem.xaml
    /// </summary>
    public partial class SideBarItem : UserControl
    {


       

        public static readonly DependencyProperty IconPathProperty =
            DependencyProperty.Register("IconPath", typeof(ImageSource), typeof(SideBarItem), new PropertyMetadata(null));

        public ImageSource IconPath
        {
            get { return (ImageSource)GetValue(IconPathProperty); }
            set { SetValue(IconPathProperty, value); }
        }


        public static readonly DependencyProperty TextProperty =
           DependencyProperty.Register("Text", typeof(string), typeof(SideBarItem), new PropertyMetadata(string.Empty));
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

       



        public static readonly DependencyProperty NavigationCommandProperty =
            DependencyProperty.Register("NavigationCommand", typeof(ICommand), typeof(SideBarItem), new PropertyMetadata(null));
        public ICommand NavigationCommand
        {
            get { return (ICommand)GetValue(NavigationCommandProperty); }
            set { SetValue(NavigationCommandProperty, value); }
        }

        public SideBarItem()
        {
            InitializeComponent();
        }
    }
}
