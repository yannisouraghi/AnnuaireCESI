using Newtonsoft.Json;
using ProjetAnnuaire.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ProjetAnnuaire.ViewModel;
using System.Windows.Input;

namespace ProjetAnnuaire.View
{
    /// <summary>
    /// Logique d'interaction pour AddService.xaml
    /// </summary>
    public partial class Delete : Window
    {
        public static string UserInput { get; private set; }
        public static int Id { get; private set; }
        public UpdateViewModel ViewModel { get; set; }


        public Delete(string prompt)
        {
            InitializeComponent();
            ViewModel = new UpdateViewModel();
            DataContext = ViewModel;

            if (prompt == "Supprimer un service")
            {
                InitializeServicesComboBoxAsync();
                ServiceComboBox.Visibility = Visibility.Visible;
                LabelContent.Text = "Supprimer un service";
            }
            else if (prompt == "Supprimer un site")
            {
                InitializeSitesComboBoxAsync();
                SiteComboBox.Visibility = Visibility.Visible;
                LabelContent.Text = "Supprimer un site";
            }
        }
        private async Task InitializeServicesComboBoxAsync()
        {
            await ViewModel.InitializeServicesAsync();
        }

        private async Task InitializeSitesComboBoxAsync()
        {
            await ViewModel.InitializeSitesAsync();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (LabelContent.Text == "Supprimer un service" && ServiceComboBox.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un service");
                return;
            }
            else if (LabelContent.Text == "Supprimer un site" && SiteComboBox.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un site");
                return;
            }
            if (LabelContent.Text == "Supprimer un service")
            {
                var ServiceId = (ServiceComboBox.SelectedItem as Services)?.ServiceId ?? 0;
                Id = ServiceId;
            }
            else if (LabelContent.Text == "Supprimer un site")
            {
                var SiteId = (SiteComboBox.SelectedItem as Sites)?.SiteId ?? 0;
                Id = SiteId;
            }
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
