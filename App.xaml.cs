using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ProjetAnnuaire._Services;
using ProjetAnnuaire.ViewModel;

namespace ProjetAnnuaire
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // Définissez une propriété pour accéder à votre conteneur d'injection de dépendances
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Créez votre conteneur d'injection de dépendances ici
            var serviceCollection = new ServiceCollection();

            // Ajoutez vos services ici
            serviceCollection.AddSingleton<IDialogMessageService, DialogMessageService>();
            serviceCollection.AddTransient<AjouterPageViewModel>(); // Ajoutez cette ligne

            // Construisez votre conteneur d'injection de dépendances ici
            ServiceProvider = serviceCollection.BuildServiceProvider();

        }
    }
}
