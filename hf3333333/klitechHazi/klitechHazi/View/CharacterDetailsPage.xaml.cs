using System;
using System.Collections.Generic;
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
using klitechHazi.Model;
using System.Threading.Tasks;
using klitechHazi.ViewModel;

namespace klitechHazi.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CharacterDetailsPage : Page
    {
        private CharacterViewModel viewModel;

        public CharacterDetailsPage()
        {
            this.InitializeComponent();
            viewModel = new CharacterViewModel();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Az előző oldalról átvett karakter nevének lekérése
            string characterName = e.Parameter as string;

            // ViewModel használata a karakter részletek lekérdezéséhez és megjelenítéséhez
            await viewModel.LoadCharacterDetails(characterName);

            // Ha a karakter található, beállítjuk DataContext-nek
            if (viewModel.SelectedCharacter != null)
            {
                DataContext = viewModel.SelectedCharacter;

                // Címek ListBox feltöltése
                titlesListBox.ItemsSource = viewModel.Titles;

                // Aliasok ListBox feltöltése
                aliasesListBox.ItemsSource = viewModel.Aliases;

                // Hűbérúri kötelezettségek ListBox feltöltése
                allegiancesListBox.ItemsSource = await viewModel.GetCharacterNames(viewModel.SelectedCharacter.Allegiances);

                // Könyvek ListBox feltöltése
                booksListBox.ItemsSource = await viewModel.GetBookNames(viewModel.SelectedCharacter.Books);

                // POV könyvek ListBox feltöltése
                povBooksListBox.ItemsSource = await viewModel.GetBookNames(viewModel.SelectedCharacter.PovBooks);

                // TV sorozatok ListBox feltöltése
                tvSeriesListBox.ItemsSource = viewModel.TvSeries;

                // Eljátszotta ListBox feltöltése
                playedByListBox.ItemsSource = viewModel.PlayedBy;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Visszatérés a korábbi oldalra
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private void SomeButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
