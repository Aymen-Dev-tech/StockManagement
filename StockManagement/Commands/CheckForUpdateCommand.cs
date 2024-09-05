using StockManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Squirrel;
using System.Windows.Forms;
using Aspose.Pdf.Operators;

namespace StockManagement.Commands
{
    public class CheckForUpdateCommand : CommandBase
    {
        private readonly SettingsViewModel _settingsViewModel;

        public CheckForUpdateCommand(SettingsViewModel settingsViewModel)
        {
            _settingsViewModel = settingsViewModel;
        }

        public async override void Execute(object parameter)
        {
            try
            {
                await _settingsViewModel.InitializeTheUpdateManager();
                var updateInfo = await _settingsViewModel.UpdateManager.CheckForUpdate();

                if (updateInfo.ReleasesToApply.Count > 0)
                {
                    _settingsViewModel.IsUpdateAvailable = true;
                }
                else
                {
                    MessageBox.Show("You are running the latest version", "No Updates", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
    }
}
