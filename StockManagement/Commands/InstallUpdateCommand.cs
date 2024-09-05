using Aspose.Pdf.Operators;
using Squirrel;
using StockManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StockManagement.Commands
{
    public class InstallUpdateCommand : CommandBase
    {
        private readonly SettingsViewModel _settingsViewModel;

        public InstallUpdateCommand(SettingsViewModel settingsViewModel)
        {
            _settingsViewModel = settingsViewModel;
        }

        public async override void Execute(object parameter)
        {
            try
            {
                await _settingsViewModel.UpdateManager.UpdateApp();
                MessageBox.Show("Update Completed\n Please restart the app", "Update Completed", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update Error", MessageBoxButton.OK,  MessageBoxImage.Error);
            }
        }
    }
}
