using ProjetAnnuaire.ViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProjetAnnuaire.Model;
using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using ProjetAnnuaire._Services;

namespace ProjetAnnuaire.ViewModel
{
    public class DeleteViewModel : ViewModelBase
    {
        private readonly IDialogMessageService _dialogService;

        public DeleteViewModel()
        {
            _dialogService = new DialogMessageService();    
        }


        private ObservableCollection<Employees> _employeesList;
        public ObservableCollection<Employees> EmployeesList
        {
            get { return _employeesList; }
            set
            {
                _employeesList = value;
                OnPropertyChanged(nameof(EmployeesList));
            }
        }

        private ObservableCollection<Services> _servicesList;
        public ObservableCollection<Services> ServicesList
        {
            get { return _servicesList; }
            set
            {
                _servicesList = value;
                OnPropertyChanged(nameof(ServicesList));
            }
        }

        private ObservableCollection<Sites> _sitesList;
        public ObservableCollection<Sites> SitesList
        {
            get { return _sitesList; }
            set
            {
                _sitesList = value;
                OnPropertyChanged(nameof(SitesList));
            }
        }

        private Services _selectedService;
        public Services SelectedService
        {
            get { return _selectedService; }
            set
            {
                _selectedService = value;
                OnPropertyChanged(nameof(SelectedService));
            }
        }

        private Sites _selectedSite;
        public Sites SelectedSite
        {
            get { return _selectedSite; }
            set
            {
                _selectedSite = value;
                OnPropertyChanged(nameof(SelectedSite));
            }
        }

        public async Task InitializeServicesAsync()
        {
            try
            {
                var httpClient = new HttpClient();
                var baseUri = "https://localhost:7053";
                var uri = $"{baseUri}/api/Service/Services";

                var response = await httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResult = await response.Content.ReadAsStringAsync();
                    var services = JsonConvert.DeserializeObject<List<Services>>(jsonResult);
                    ServicesList = new ObservableCollection<Services>(services);
                }
                else
                {
                    _dialogService.ShowDialogMessage($"Erreur lors de la requête API : {response.StatusCode} - {response.ReasonPhrase}", "Error");
                }
            }
            catch (Exception ex)
            {
                _dialogService.ShowDialogMessage("Erreur : " + ex.Message, "Error");
            }
        }

        public async Task InitializeSitesAsync()
        {
            try
            {
                var httpClient = new HttpClient();
                var baseUri = "https://localhost:7053";
                var uri = $"{baseUri}/api/Site/Sites";

                var response = await httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResult = await response.Content.ReadAsStringAsync();
                    var sites = JsonConvert.DeserializeObject<List<Sites>>(jsonResult);
                    SitesList = new ObservableCollection<Sites>(sites);
                }
                else
                {
                    _dialogService.ShowDialogMessage($"Erreur lors de la requête API : {response.StatusCode} - {response.ReasonPhrase}", "Error");
                }
            }
            catch (Exception ex)
            {
                _dialogService.ShowDialogMessage("Erreur : " + ex.Message, "Error");
            }
        }
    }
}
