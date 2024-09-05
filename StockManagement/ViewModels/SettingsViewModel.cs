using StockManagement.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        public NavbarViewModel NavbarViewModel { get; }

        public SettingsViewModel(NavbarViewModel navbarViewModel)
        {
            NavbarViewModel = navbarViewModel;
        }
    }
}
