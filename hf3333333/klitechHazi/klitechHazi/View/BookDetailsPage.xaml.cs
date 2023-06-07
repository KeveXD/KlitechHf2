using klitechHazi.Model;
using klitechHazi.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using klitechHazi.ViewModel;

namespace klitechHazi
{
    public sealed partial class BookDetailsPage : Page
    {
        private BooksViewModel viewModel;

        public BookDetailsPage()
        {
            this.InitializeComponent();
            viewModel = new BooksViewModel();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Az előző oldalról átvett könyv adatok lekérése
            Book selectedBook = (Book)e.Parameter;

            // ViewModel használata a könyv részletek lekérdezéséhez és megjelenítéséhez
            await viewModel.LoadBookDetails(selectedBook);

            // Könyv részletek megjelenítése a felületen
            titleLabel.Text = "Címe: " + viewModel.Title;
            authorLabel.Text = "Írója: " + viewModel.Authors;
            // Egyéb könyv részletek megjelenítése itt

            // Példa más adatok megjelenítésére:
            isbnLabel.Text = "ISBN: " + viewModel.Isbn;
            pagesLabel.Text = "Oldalszám: " + viewModel.NumberOfPages.ToString();
            publisherLabel.Text = "Kiadó: " + viewModel.Publisher;
            countryLabel.Text = "Ország: " + viewModel.Country;
            mediaTypeLabel.Text = "Média típusa: " + viewModel.MediaType;
            releasedLabel.Text = "Megjelenés dátuma: " + viewModel.Released;

            // Karakterek betöltése és megjelenítése
            ObservableCollection<Character> characters = await viewModel.LoadCharacters();

            charactersListBox.ItemsSource = characters.Where(c => viewModel.SelectedBook.Characters.Contains(c.Url));
            povCharactersListBox.ItemsSource = characters.Where(c => viewModel.SelectedBook.PovCharacters.Contains(c.Url));
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void Character_Click(object sender, RoutedEventArgs e)
        {
            Button characterButton = (Button)sender;
            Character selectedCharacter = (Character)characterButton.DataContext;

            // Átirányítás a CharacterReszletek oldalra
            Frame.Navigate(typeof(CharacterDetailsPage), selectedCharacter.Url);
        }
    }
}
