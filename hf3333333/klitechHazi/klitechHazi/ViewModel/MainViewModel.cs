using klitechHazi.Model;
using klitechHazi.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace klitechHazi
{
    public sealed partial class MainPage : Page
    {
        private IceAndFireApi _api;
        private ObservableCollection<Character> _characters;

        public MainPage()
        {
            InitializeComponent();
            _api = new IceAndFireApi();
            _characters = new ObservableCollection<Character>();
            CharacterListView.ItemsSource = _characters;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Az előző oldalról átvett karakter nevének lekérése
            string characterName = e.Parameter as string;

            // Karakter URL-jének lekérése a nevével alapján
            string characterUrl = await _api.GetCharacterUrlAsync(characterName);

            // Karakter részletek lekérése az URL alapján
            Character selectedCharacter = await _api.GetCharacterAsync(characterUrl);

            // Karakter hozzáadása a listához
            _characters.Add(selectedCharacter);
        }

        private async void CharacterButton_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = SearchTextBox.Text;

            try
            {
                // Karakterek lekérése a keresési szöveggel
                List<string> characterUris = await _api.GetCharactersBySearchAsync(searchTerm);

                // Karakterek részleteinek lekérése az URL-ek alapján
                foreach (string characterUri in characterUris)
                {
                    Character character = await _api.GetCharacterAsync(characterUri);
                    _characters.Add(character);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Hiba a karakterek lekérésekor: {ex.Message}");
            }
        }

        private void CharacterListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CharacterListView.SelectedItem != null)
            {
                // Kiválasztott karakter részleteinek megjelenítése
                Character selectedCharacter = CharacterListView.SelectedItem as Character;
                Frame.Navigate(typeof(CharacterDetailsPage), selectedCharacter);
            }
        }
    }
}
