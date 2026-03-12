using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace AppBindingCommands.ViewModels
{
    internal class UsuarioViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string name = string.Empty;
        private string displayMessage = string.Empty;
        public string Name 
        { 
            get => name;
            set
            { 
                if(name == null)
                {
                    return;
                }

                name = value;
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(DisplayName));
            }   
        }
        public string DisplayMessage
        {
            get => displayMessage;
            set
            {
                if (displayMessage == null)
                {
                    return;
                }
                displayMessage = value;
                OnPropertyChanged(nameof(DisplayMessage));
            }
        }
        public string DisplayName => $"Nome digitado:{Name}";

        public ICommand ShowMessageCommand { get; }

        public void ShowMessage()
        {
            DateTime data = Preferences.Get("dtAtual", DateTime.MinValue);
            DisplayMessage = $"Boa Noite {Name}, Hoje é {data}";
        }

        public UsuarioViewModel()
        {
            ShowMessageCommand = new Command(ShowMessage);
            CountCommand = new Command(async () => await CountCharacters());
            CleanCommand = new Command(async () => await CleanConfirmation());
        }

        public async Task CountCharacters()
        {
            string nameLenght = string.Format("Seu nome tem {0} Letras", name.Length);

            await Application.Current.MainPage.DisplayAlert("Informação", nameLenght, "Ok");
        }

        public ICommand CountCommand { get; }

        public async Task CleanConfirmation()
        {
            if(await Application.Current.MainPage.DisplayAlert("Confirmação", "Confirma limpeza dos dados?","Yes","No"))
            {
                Name = string.Empty;
                DisplayMessage = string.Empty;
                OnPropertyChanged(Name);
                OnPropertyChanged(DisplayMessage);

                await Application.Current.MainPage.DisplayAlert("Informação", "Limpeza realizada com sucesso","OK");
            }
        }

        public ICommand CleanCommand {  get; }
       
    }
}
