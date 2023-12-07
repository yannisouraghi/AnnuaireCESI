using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using ProjetAnnuaire.Model;
using Prism.Commands;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using ProjetAnnuaire.View;
using DevExpress.Mvvm;
using System.Reflection.Metadata;
using DevExpress.Mvvm.Xpf;
using ProjetAnnuaire._Services;

namespace ProjetAnnuaire.ViewModel
{
    public class AjouterPageViewModel : ViewModelBase
    {
        private string firstName;
        private string lastName;
        private string department;
        private string email;
        private string phoneNumber;
        private string mobilePhone;
        private string jobTitle;
        private string jobDescription;

        private Services selectedService;
        private ObservableCollection<Services> services;

        private Sites selectedSite;
        private ObservableCollection<Sites> sites;

        private readonly IDialogMessageService _dialogService;

        public AjouterPageViewModel()
        {
            services = new ObservableCollection<Services>();
            sites = new ObservableCollection<Sites>();
            _dialogService = new DialogMessageService();
        }

        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                RaisePropertyChanged("FirstName");
            }
        }

        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                RaisePropertyChanged("LastName");
            }
        }

        public string Department
        {
            get { return department; }
            set
            {
                department = value;
                RaisePropertyChanged("Department");
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                RaisePropertyChanged("Email");
            }
        }

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                phoneNumber = value;
                RaisePropertyChanged("PhoneNumber");
            }
        }

        public string MobilePhone
        {
            get { return mobilePhone; }
            set
            {
                mobilePhone = value;
                RaisePropertyChanged("MobilePhone");
            }
        }

        public string JobTitle
        {
            get { return jobTitle; }
            set
            {
                jobTitle = value;
                RaisePropertyChanged("JobTitle");
            }
        }

        public string JobDescription
        {
            get { return jobDescription; }
            set
            {
                jobDescription = value;
                RaisePropertyChanged("JobDescription");
            }
        }

        public Services SelectedService
        {
            get { return selectedService; }
            set
            {
                selectedService = value;
                RaisePropertyChanged("SelectedService");
            }
        }

        public ObservableCollection<Services> Services
        {
            get { return services; }
            set
            {
                services = value;
                RaisePropertyChanged("Services");
            }
        }

        public Sites SelectedSite
        {
            get { return selectedSite; }
            set
            {
                selectedSite = value;
                RaisePropertyChanged("SelectedSite");
            }
        }

        public ObservableCollection<Sites> Sites
        {
            get { return sites; }
            set
            {
                sites = value;
                RaisePropertyChanged("Sites");
            }
        }

        private string newServiceName;

        public string NewServiceName
        {
            get { return newServiceName; }
            set
            {
                newServiceName = value;
                RaisePropertyChanged(nameof(NewServiceName));
            }
        }

        private string newCityName;
        public string NewCityName
        {
            get { return newCityName; }
            set
            {
                newCityName = value;
                RaisePropertyChanged(nameof(NewCityName));
            }
        }

        private string newSiteName;
        private _Services.DialogMessageService dialogService;

        public string NewSiteName
        {
            get { return newSiteName; }
            set
            {
                newSiteName = value;
                RaisePropertyChanged(nameof(NewSiteName));
            }
        }

        public async void AjouterService_Click()
        {
            try
            {
                Add addService = new Add("Ajouter un service");
                addService.Top = (SystemParameters.FullPrimaryScreenHeight - addService.Height) / 2;
                addService.Left = (SystemParameters.FullPrimaryScreenWidth - addService.Width) / 2;
                addService.ShowDialog();
                NewServiceName = Add.UserInput;

                if (!string.IsNullOrEmpty(NewServiceName))
                {
                    using (HttpClient client = new HttpClient())
                    {
                        string apiUrl = $"https://localhost:7053/api/Service/AddService";
                        Services newService = new Services { Service = NewServiceName };
                        string json = JsonConvert.SerializeObject(newService);
                        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                        HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                        if (response.IsSuccessStatusCode)
                        {
                            _dialogService.ShowDialogMessage("Service ajouté avec succès !" , "Info");
                        }
                        else
                        {
                            _dialogService.ShowDialogMessage($"Erreur lors de la requête API : {response.StatusCode} - {response.ReasonPhrase}", "Error");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _dialogService.ShowDialogMessage("Une erreur s'est produite lors de l'ajout du service.", "Error");

            }

        }


        public async void AjouterSite_Click()
        {
            try
            {
                Add addSite = new Add("Ajouter un site");
                addSite.Top = (SystemParameters.FullPrimaryScreenHeight - addSite.Height) / 2;
                addSite.Left = (SystemParameters.FullPrimaryScreenWidth - addSite.Width) / 2;
                addSite.ShowDialog();
                NewCityName = Add.UserInput;
                if (!string.IsNullOrEmpty(NewCityName)) 
                {
                    using (HttpClient client = new HttpClient())
                    {
                        string apiUrl = $"https://localhost:7053/api/Site/AddSite";
                        Sites newSite = new Sites { City = NewCityName};
                        string json = JsonConvert.SerializeObject(newSite);
                        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                        HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                        if (response.IsSuccessStatusCode)
                        {
                            _dialogService.ShowDialogMessage("Site ajouté avec succès !" , "Info");
                        }
                        else
                        {
                            _dialogService.ShowDialogMessage($"Erreur lors de la requête API : {response.StatusCode} - {response.ReasonPhrase}", "Error");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _dialogService.ShowDialogMessage($"Une erreur s'est produite : {ex.Message}", "Error");
            }
        }

        public async void ModifierSite_Click()
        {
            try
            {
                Update UpdateSite = new Update("Modifier un site");
                UpdateSite.Top = (SystemParameters.FullPrimaryScreenHeight - UpdateSite.Height) / 2;
                UpdateSite.Left = (SystemParameters.FullPrimaryScreenWidth - UpdateSite.Width) / 2;
                UpdateSite.ShowDialog();                   
                NewSiteName = Update.UserInput;
                var Id = Update.Id;
                if (!string.IsNullOrEmpty(NewSiteName))
                {
                    using (HttpClient client = new HttpClient())
                    {
                        string apiUrl = $"https://localhost:7053/api/Site/UpdateSite/" +Id;
                        Sites newSite = new Sites { City = NewSiteName };
                        string json = JsonConvert.SerializeObject(newSite);
                        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                        HttpResponseMessage response = await client.PutAsync(apiUrl, content);

                        if (response.IsSuccessStatusCode)
                        {
                            _dialogService.ShowDialogMessage("Site modifié avec succès !" , "Info");
                        }
                        else
                        {
                            _dialogService.ShowDialogMessage($"Erreur lors de la requête API : {response.StatusCode} - {response.ReasonPhrase}", "Error");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _dialogService.ShowDialogMessage($"Une erreur s'est produite : {ex.Message}" , "Error");
            }
        }
        public async void SupprimerSite_Click()
        {
            try
            {
                Delete DeleteSite = new Delete("Supprimer un site");
                DeleteSite.Top = (SystemParameters.FullPrimaryScreenHeight - DeleteSite.Height) / 2;
                DeleteSite.Left = (SystemParameters.FullPrimaryScreenWidth - DeleteSite.Width) / 2;
                DeleteSite.ShowDialog();
                var Id = Delete.Id;
                if (!string.IsNullOrEmpty(Id.ToString()) && Id != 0 )
                {
                    using (HttpClient client = new HttpClient())
                    {
                        string apiUrl = $"https://localhost:7053/api/Site/DeleteSite/" + Id;

                        HttpResponseMessage response = await client.DeleteAsync(apiUrl);

                        if (response.IsSuccessStatusCode)
                        {
                            _dialogService.ShowDialogMessage("Site supprimé avec succès !" , "Info");
                        }
                        else
                        {
                            // Lire le contenu de la réponse
                            string responseBody = await response.Content.ReadAsStringAsync();
                            _dialogService.ShowDialogMessage($"Une erreur s'est produite lors de la suppression du site : {responseBody}" , "Error");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _dialogService.ShowDialogMessage($"Une erreur s'est produite : {ex.Message}" , "Error");
            }
        }


        public async void ModifierService_Click()
        {
            try
            {
                Update UpdateSite = new Update("Modifier un service");
                UpdateSite.Top = (SystemParameters.FullPrimaryScreenHeight - UpdateSite.Height) / 2;
                UpdateSite.Left = (SystemParameters.FullPrimaryScreenWidth - UpdateSite.Width) / 2;
                UpdateSite.ShowDialog();
                NewServiceName = Update.UserInput;
                var Id = Update.Id;
                if (!string.IsNullOrEmpty(NewServiceName))
                {
                    using (HttpClient client = new HttpClient())
                    {
                        string apiUrl = $"https://localhost:7053/api/Service/UpdateService/" + Id;
                        Services newService = new Services { Service = NewServiceName };
                        string json = JsonConvert.SerializeObject(newService);
                        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                        HttpResponseMessage response = await client.PutAsync(apiUrl, content);

                        if (response.IsSuccessStatusCode)
                        {
                            _dialogService.ShowDialogMessage("Service modifié avec succès !" , "Info");
                        }
                        else
                        {
                            _dialogService.ShowDialogMessage($"Erreur lors de la requête API : {response.StatusCode} - {response.ReasonPhrase}" , "Error");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _dialogService.ShowDialogMessage($"Une erreur s'est produite : {ex.Message}", "Error");
            }

        }
        public async void SupprimerService_Click()
        {
            try
            {
                Delete DeleteService = new Delete("Supprimer un service");
                DeleteService.Top = (SystemParameters.FullPrimaryScreenHeight - DeleteService.Height) / 2;
                DeleteService.Left = (SystemParameters.FullPrimaryScreenWidth - DeleteService.Width) / 2;
                DeleteService.ShowDialog();
                var Id = Delete.Id;
                if (!string.IsNullOrEmpty(Id.ToString()) && Id != 0)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        string apiUrl = $"https://localhost:7053/api/Service/DeleteService/" + Id;

                        HttpResponseMessage response = await client.DeleteAsync(apiUrl);

                        if (response.IsSuccessStatusCode)
                        {
                            _dialogService.ShowDialogMessage("Service supprimé avec succès !", "Info");
                        }
                        else
                        {
                            string responseBody = await response.Content.ReadAsStringAsync();
                            _dialogService.ShowDialogMessage($"Une erreur s'est produite lors de la suppression du service : {responseBody}", "Error");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _dialogService.ShowDialogMessage($"Une erreur s'est produite : {ex.Message}", "Error");
            }
        }
    }
}
