using Squirrel;
using StockManagement.Commands;
using StockManagement.Stores;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StockManagement.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        public NavbarViewModel NavbarViewModel { get; }
        private UpdateManager _updateManager;
        public UpdateManager UpdateManager
        {
            get => _updateManager; set => _updateManager = value;
        }
        private string _currentInstalledVersion;
        public string CurrentInstalledVersion
        {
            get => _currentInstalledVersion;
            set
            {
                _currentInstalledVersion = value;
                OnPropertyChanged(nameof(CurrentInstalledVersion));
            }
        }
        private bool _isUpdateAvailable;
        public bool IsUpdateAvailable
        {
            get => _isUpdateAvailable;
            set
            {
                _isUpdateAvailable = value;
                OnPropertyChanged(nameof(IsUpdateAvailable));
            }
        }
        public ICommand CheckForUpdate { get; }
        public ICommand InstallUpdate { get; }
        public SettingsViewModel(NavbarViewModel navbarViewModel)
        {
            NavbarViewModel = navbarViewModel;
            this._isUpdateAvailable = false;
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo version = FileVersionInfo.GetVersionInfo(assembly.Location);
            this._currentInstalledVersion = version.FileVersion.ToString();    
            CheckForUpdate = new CheckForUpdateCommand(this);
            InstallUpdate = new InstallUpdateCommand(this);
        }


        public async Task InitializeTheUpdateManager()
        {
            this._updateManager = await UpdateManager
                .GitHubUpdateManager(@"https://github.com/Aymen-Dev-tech/StockManagement");
        }
    }
}
