using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DevExpress.Internal.WinApi.Windows.UI.Notifications;
using Newtonsoft.Json;
using ProjetAnnuaire._Services;
using ProjetAnnuaire.Model;

namespace ProjetAnnuaire.ViewModel
{
    public class FicheEmployeeViewModel : ViewModelBase
    {
        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                if (lastName != value)
                {
                    lastName = value;
                    OnPropertyChanged(nameof(LastName));
                }
            }
        }

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (firstName != value)
                {
                    firstName = value;
                    OnPropertyChanged(nameof(FirstName));
                }
            }
        }
        private string department;
        public string Department
        {
            get { return department; }
            set
            {
                if (department != value)
                {
                    department = value;
                    OnPropertyChanged(nameof(Department));
                }
            }
        }

        private string jobTitle;
        public string JobTitle
        {
            get { return jobTitle; }
            set
            {
                if (jobTitle != value)
                {
                    jobTitle = value;
                    OnPropertyChanged(nameof(JobTitle));
                }
            }
        }

        private string jobDescription;
        public string JobDescription
        {
            get { return jobDescription; }
            set
            {
                if (jobDescription != value)
                {
                    jobDescription = value;
                    OnPropertyChanged(nameof(JobDescription));
                }
            }
        }

        private string phoneNumber;
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                if (phoneNumber != value)
                {
                    phoneNumber = value;
                    OnPropertyChanged(nameof(PhoneNumber));
                }
            }
        }

        private string mobilePhone;
        public string MobilePhone
        {
            get { return mobilePhone; }
            set
            {
                if (mobilePhone != value)
                {
                    mobilePhone = value;
                    OnPropertyChanged(nameof(MobilePhone));
                }
            }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                if (email != value)
                {
                    email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        private string service;

        public string Service
        {
            get { return service; }
            set
            {
                if (service != value)
                {
                    service = value;
                    OnPropertyChanged(nameof(Service));
                }
            }
        }

        private string city;
        public string City
        {
            get { return city; }
            set
            {
                if (city != value)
                {
                    city = value;
                    OnPropertyChanged(nameof(City));
                }
            }
        }

        private string adress;
        public string Adress
        {
            get { return adress; }
            set
            {
                if (adress != value)
                {
                    adress = value;
                    OnPropertyChanged(nameof(Adress));
                }
            }
        }

        private string zipCode;
        public string ZipCode
        {
            get { return zipCode; }
            set
            {
                if (zipCode != value)
                {
                    zipCode = value;
                    OnPropertyChanged(nameof(ZipCode));
                }
            }
        }

        private string country;
        public string Country
        {
            get { return country; }
            set
            {
                if (country != value)
                {
                    country = value;
                    OnPropertyChanged(nameof(Country));
                }
            }
        }

        private bool isAdmin;
        public bool IsAdmin
        {
            get { return isAdmin; }
            set
            {
                Debug.WriteLine(isAdmin);
                if (isAdmin != value)
                {
                    isAdmin = value;
                    OnPropertyChanged(nameof(IsAdmin));
                }
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

        public ICommand ModifyCommand { get; private set; }
        private readonly IDialogMessageService _dialogService;



        public FicheEmployeeViewModel()
        {
            ModifyCommand = new RelayCommand(UpdateEmployee, CanModifyLastName);
            _dialogService = new DialogMessageService();
        }

        public async void UpdateEmployee(string LastName, string FirstName, string Department, string JobTitle, string JobDescription, string PhoneNumber, string MobilePhone, string Email, int ServiceId, int SiteId)
        {
            try
            {
                CreateEmployeeModel employeeModel = new CreateEmployeeModel
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Department = Department,
                    Email = Email,
                    PhoneNumber = PhoneNumber,
                    MobilePhone = MobilePhone,
                    JobTitle = JobTitle,
                    JobDescription = JobDescription,
                    ServiceId = ServiceId,
                    SiteId = SiteId
                };

                // Convertissez l'objet en JSON
                string jsonEmployee = JsonConvert.SerializeObject(employeeModel);

                // Définissez l'URL de l'API
                string apiUrl = "https://localhost:7053/api/Employee/UpdateEmployee/"+ id;

                // Utilisez HttpClient pour envoyer les données au serveur
                using (HttpClient client = new HttpClient())
                {
                    StringContent content = new StringContent(jsonEmployee, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PutAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        _dialogService.ShowDialogMessage("Employé modifié avec succès !", "Info" );
                    }
                    else
                    {
                        _dialogService.ShowDialogMessage($"Erreur lors de la requête API : {response.StatusCode} - {response.ReasonPhrase}" , "Error");
                    }
                }
            }
            catch (Exception ex)
            {
                _dialogService.ShowDialogMessage($"Une erreur s'est produite : {ex.Message}" , "Error");
            }
        }
        


        private bool CanModifyLastName()
        {
            return isAdmin;
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
                   _dialogService.ShowDialogMessage($"Erreur lors de la requête API : {response.StatusCode} - {response.ReasonPhrase}" , "Error");
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
