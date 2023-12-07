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
    public partial class Update : Window
    {
        public static string UserInput { get; private set; }
        public static int Id { get; private set; }
        public UpdateViewModel ViewModel { get; set; }


        public Update(string prompt)
        {
            InitializeComponent();
            ViewModel = new UpdateViewModel();
            DataContext = ViewModel;

            if (prompt == "Modifier un service")
            {
                InitializeServicesComboBoxAsync();
                ServiceComboBox.Visibility = Visibility.Visible;
                LabelContent.Text = "Modifier un service";
                UserInput = "";
            }
            else if (prompt == "Modifier un site")
            {
                InitializeSitesComboBoxAsync();
                SiteComboBox.Visibility = Visibility.Visible;
                LabelContent.Text = "Modifier un site";
                UserInput = "";

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
            if (LabelContent.Text == "Modifier un service" && ServiceComboBox.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un service");
                return;
            }
            else if (LabelContent.Text == "Modifier un site" && SiteComboBox.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un site");
                return;
            }
            if (ServiceInput.Text == "")
            {
                MessageBox.Show("Veuillez saisir une valeur valide");
                return;
            }
            if (LabelContent.Text == "Modifier un service")
            {
                var ServiceId = (ServiceComboBox.SelectedItem as Services)?.ServiceId ?? 0;
                Id = ServiceId;
            }
            else if (LabelContent.Text == "Modifier un site")
            {
                var SiteId = (SiteComboBox.SelectedItem as Sites)?.SiteId ?? 0;
                Id = SiteId;
            }
            UserInput = ServiceInput.Text;
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            UserInput = "";
            this.Close();
        }

    }
}
