using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AdonisUI.Controls;

namespace ProjetAnnuaire._Services
{
    public interface IDialogMessageService
    {
        void ShowDialogMessage(string message, string ErrorType);
    }

    public class DialogMessageService : IDialogMessageService
    {
        public void ShowDialogMessage(string message, string ErrorType)
        {
            var messageBox = new MessageBoxModel
            {
                Text = message,
                Caption = "Info",
                Icon = AdonisUI.Controls.MessageBoxImage.Information,
                Buttons = MessageBoxButtons.OkCancel()
            };
            if (ErrorType == "Error")
            {
                messageBox = new MessageBoxModel
                {
                    Text = message,
                    Caption = "Info",
                    Icon = AdonisUI.Controls.MessageBoxImage.Error,
                    Buttons = MessageBoxButtons.OkCancel()
                };
            }
            else if (ErrorType == "Warning")
            {
                messageBox = new MessageBoxModel
                {
                    Text = message,
                    Caption = "Info",
                    Icon = AdonisUI.Controls.MessageBoxImage.Warning,
                    Buttons = MessageBoxButtons.OkCancel()
                };
            }
            else if (ErrorType == "Info")
            {
                messageBox = new MessageBoxModel
                {
                    Text = message,
                    Caption = "Info",
                    Icon = AdonisUI.Controls.MessageBoxImage.Information,
                    Buttons = MessageBoxButtons.OkCancel()
                };
            }
            AdonisUI.Controls.MessageBox.Show(messageBox);
        }

    }

}
