using System;
using System.Diagnostics;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using ProjetAnnuaire._Services;
using ProjetAnnuaire.Model;
using ProjetAnnuaire.ViewModel;

namespace ProjetAnnuaire
{
    public partial class FicheEmployee : Page
    {
        // Propriété pour le ViewModel
        public FicheEmployeeViewModel ViewModel { get; set; }
        public bool isAdminView { get; set; }

        public FicheEmployee(FicheEmployeeViewModel viewModel, bool isAdmin)
        {
            InitializeComponent();
            ViewModel = viewModel;
            isAdminView = isAdmin;
            ViewModel.IsAdmin = isAdmin;
            DataContext = ViewModel;
            InitializeServicesComboBoxAsync();
            InitializeSitesComboBoxAsync();
        }

        private async Task InitializeServicesComboBoxAsync()
        {
            await ViewModel.InitializeServicesAsync();
        }

        private async Task InitializeSitesComboBoxAsync()
        {
            await ViewModel.InitializeSitesAsync();
        }

        public void Save_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxService.SelectedItem == null || ComboBoxCity.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un service et un site");
                return;
            }
            var ServiceId = (ComboBoxService.SelectedItem as Services)?.ServiceId ?? 0;
            var SiteId = (ComboBoxCity.SelectedItem as Sites)?.SiteId ?? 0;
            ViewModel.UpdateEmployee(TextBoxLastName.Text, TextBoxFirstName.Text,TextBoxDepartment.Text, TextBoxJobTitle.Text, TextBoxJobDescription.Text, TextBoxPhoneNumber.Text, TextBoxMobilePhone.Text, TextBoxEmail.Text, ServiceId, SiteId);
            UnloadModifyMenu();
        }

        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            if (isAdminView)
            {
                TextBoxLastName.IsReadOnly = false;
                TextBoxFirstName.IsReadOnly = false;
                TextBoxJobTitle.IsReadOnly = false;
                TextBoxJobDescription.IsReadOnly = false;
                TextBoxPhoneNumber.IsReadOnly = false;
                TextBoxMobilePhone.IsReadOnly = false;
                TextBoxEmail.IsReadOnly = false;
                TextBoxDepartment.IsReadOnly = false;
                Save.Visibility = Visibility.Visible;
                Cancel.Visibility = Visibility.Visible;
                TextBoxService.Visibility = Visibility.Collapsed;
                TextBoxCity.Visibility = Visibility.Collapsed;
                ComboBoxService.Visibility = Visibility.Visible;
                ComboBoxService.SelectedItem = TextBoxService.Text;
                ComboBoxCity.Visibility = Visibility.Visible;
                ComboBoxCity.SelectedItem = TextBoxCity.Text;
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            UnloadModifyMenu();
        }

        public void UnloadModifyMenu()
        {
            TextBoxLastName.IsReadOnly = true;
            TextBoxFirstName.IsReadOnly = true;
            TextBoxJobTitle.IsReadOnly = true;
            TextBoxJobDescription.IsReadOnly = true;
            TextBoxPhoneNumber.IsReadOnly = true;
            TextBoxMobilePhone.IsReadOnly = true;
            TextBoxEmail.IsReadOnly = true;
            TextBoxService.IsReadOnly = true;
            TextBoxCity.IsReadOnly = true;
            TextBoxDepartment.IsReadOnly = true;
            Save.Visibility = Visibility.Collapsed;
            Cancel.Visibility = Visibility.Collapsed;
            TextBoxService.Visibility = Visibility.Visible;
            TextBoxCity.Visibility = Visibility.Visible;
            ComboBoxService.Visibility = Visibility.Collapsed;
            ComboBoxCity.Visibility = Visibility.Collapsed;
        }

        public void Retour_Click(object sender, RoutedEventArgs e)
        {
            if (Window.GetWindow(this) is MainWindow mainWindow)
            {
                mainWindow.NavigateToRechercherPage();
            }
        }
    }
}
