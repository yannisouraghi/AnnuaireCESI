﻿using ProjetAnnuaire.ViewModel;
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
    public class RechercherPageViewModel : ViewModelBase
    {

        public ICommand SearchCommand { get; set; }
        private readonly IDialogMessageService _dialogService;

        public RechercherPageViewModel()
        {
            SearchCommand = new RelayCommand(async () => await SearchAsync(), () => CanExecuteSearchCommand());
            _dialogService = new DialogMessageService();

        }

        private bool CanExecuteSearchCommand()
        {
            return true;
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


        private string _txtSearchByName;
        public string SearchByName
        {
            get { return _txtSearchByName; }
            set
            {
                _txtSearchByName = value;
                OnPropertyChanged(nameof(SearchByName));
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

        public async Task SearchAsync()
        {
            try
            {
                var httpClient = new HttpClient();
                var baseUri = "https://localhost:7053";
                var uri = $"{baseUri}/api/Employee/Search";
                var queryParams = new List<string>();

                if (!string.IsNullOrEmpty(SearchByName))
                    queryParams.Add($"lastName={Uri.EscapeDataString(SearchByName)}");

                if (SelectedSite != null)
                    queryParams.Add($"site={Uri.EscapeDataString(SelectedSite.City)}");

                if (SelectedService != null)
                    queryParams.Add($"service={Uri.EscapeDataString(SelectedService.Service)}");

                if (queryParams.Count > 0)
                    uri += "?" + string.Join("&", queryParams);

                var response = await httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResult = await response.Content.ReadAsStringAsync();
                    var employees = JsonConvert.DeserializeObject<List<Employees>>(jsonResult);

                    EmployeesList = new ObservableCollection<Employees>(employees);
                }
                else
                {
                    _dialogService.ShowDialogMessage($"Erreur lors de la requête API : {response.StatusCode} - {response.ReasonPhrase}", "Error");
                }
            }
            catch (Exception ex)
            {
<<<<<<< HEAD
                _dialogService.ShowDialogMessage($"Une erreur s'est produite : {ex.Message}", "Error");
            }
=======
                // Gérer les erreurs
            }        
>>>>>>> 9e3bf55aea6b57f8065e11f7c3fc593104006200
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
                _dialogService.ShowDialogMessage($"Une erreur s'est produite : {ex.Message}", "Error");
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
                _dialogService.ShowDialogMessage($"Une erreur s'est produite : {ex.Message}", "Error");
            }
        }
    }
}
