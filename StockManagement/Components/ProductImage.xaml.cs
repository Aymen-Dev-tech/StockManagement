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
    /// Interaction logic for ProductImage.xaml
    /// </summary>
    public partial class ProductImage : UserControl
    {


        

        public static readonly DependencyProperty ImagePathProperty =
            DependencyProperty.Register("ImagePath", typeof(ImageSource), typeof(ProductImage), new PropertyMetadata(null));


        public ImageSource ImagePath
        {
            get { return (ImageSource)GetValue(ImagePathProperty); }
            set { SetValue(ImagePathProperty, value); }
        }
        public ProductImage()
        {
            InitializeComponent();
        }
    }
}
