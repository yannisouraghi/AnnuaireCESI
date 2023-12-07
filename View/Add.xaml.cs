using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjetAnnuaire.View
{
    /// <summary>
    /// Logique d'interaction pour AddService.xaml
    /// </summary>
    public partial class Add : Window
    {
        //Genere ma fenetre pour fonctionner avec le code de ma page AjouterPageViewModel, qui est la page qui gere les actions de la fenetre et qui appel cette fenetre, en focntion du prompt en parametre on affiche dans la fenetre le bon message, gere aussi la fonction ajouter et annuler de la fenetre
        public static string UserInput { get; private set; }


        public Add(string prompt)
        {
            InitializeComponent();

            if (prompt == "Ajouter un service")
            {
                LabelContent.Text = "Ajouter un service";
                UserInput = "";
            }
            else if (prompt == "Ajouter un site")
            {
                
                LabelContent.Text = "Ajouter un site";
                UserInput = "";
                
            }
        }

            private void Add_Click(object sender, RoutedEventArgs e)
            {
                if (ServiceInput.Text == "")
                {
                    MessageBox.Show("Veuillez saisir une valeur valide");
                    return;
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
