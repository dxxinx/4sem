using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FlowerShop.Services;

namespace FlowerShop.ViewModels
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        private string _userName = "Пользователь";
        private string _email = "user@example.com";
        private string _selectedTheme = "Default";
        private string _selectedLanguage = "Ru";

        public event PropertyChangedEventHandler PropertyChanged;

        public string UserName
        {
            get => _userName;
            set { _userName = value; OnPropertyChanged(); }
        }

        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        public ObservableCollection<string> Themes { get; } =
            new() { "Default", "Pink" };

        public string SelectedTheme
        {
            get => _selectedTheme;
            set
            {
                if (_selectedTheme != value)
                {
                    _selectedTheme = value;
                    OnPropertyChanged();
                    ThemeManager.ApplyTheme(value);
                }
            }
        }

        public ObservableCollection<string> Languages { get; } =
            new() { "Ru", "En" };

        public string SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                if (_selectedLanguage != value)
                {
                    _selectedLanguage = value;
                    OnPropertyChanged();
                }
            }
        }

        private void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
