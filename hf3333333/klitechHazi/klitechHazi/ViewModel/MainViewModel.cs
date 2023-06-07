using klitechHazi.Model;
using klitechHazi.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace klitechHazi.ViewModel
{
    public class MainPageViewModel
    {
        public ObservableCollection<Book> Books { get; set; }
        public ObservableCollection<Character> Characters { get; set; }
        public ObservableCollection<House> Houses { get; set; }
        private bool booksLoaded = false;
        private bool housesLoaded = false;
        private bool charactersLoaded = false;
        private const string SearchTermFileName = "SearchTerm.txt";
        public string SearchTerm { get; set; }

        private Frame frame;

        public MainPageViewModel(Frame frame)
        {
            Books = new ObservableCollection<Book>();
            Characters = new ObservableCollection<Character>();
            Houses = new ObservableCollection<House>();
            this.frame = frame;
        }

        // Könyvek betöltése aszinkron módon
        public async Task LoadBooksAsync()
        {
            if (!booksLoaded)
            {
                IceAndFireApi api = new IceAndFireApi();
                ObservableCollection<Book> books = await api.GetBooksAsync();

                if (books != null)
                {
                    Books.Clear();
                    foreach (var book in books)
                    {
                        Books.Add(book);
                    }
                    booksLoaded = true;
                }
            }
        }

        // Házak betöltése aszinkron módon
        public async Task LoadHousesAsync()
        {
            if (!housesLoaded)
            {
                IceAndFireApi api = new IceAndFireApi();
                ObservableCollection<House> houses = await api.GetHousesAsync();
                if (houses != null)
                {
                    Houses.Clear();
                    foreach (var house in houses)
                    {
                        Houses.Add(house);
                    }
                    housesLoaded = true;
                }
            }
        }

        // Karakterek betöltése aszinkron módon
        public async Task LoadCharactersAsync()
        {
            if (!charactersLoaded)
            {
                IceAndFireApi api = new IceAndFireApi();
                ObservableCollection<Character> characters = await api.GetCharactersAsyncCharacter();
                if (characters != null)
                {
                    Characters.Clear();
                    foreach (var character in characters)
                    {
                        Characters.Add(character);
                    }
                    charactersLoaded = true;
                }
            }
        }

        // Adatok megjelenítése aszinkron módon
        public async Task DisplayDataAsync()
        {
            await LoadBooksAsync();
            await LoadHousesAsync();
            await LoadCharactersAsync();
        }

        // Keresési kifejezés betöltése aszinkron módon
        public async Task<string> LoadSearchTermAsync()
        {
            try
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile file = await localFolder.GetFileAsync(SearchTermFileName);
                return await FileIO.ReadTextAsync(file);
            }
            catch (FileNotFoundException)
            {
                return string.Empty;
            }
        }

        // Keresési kifejezés mentése aszinkron módon
        public async Task SaveSearchTermAsync(string searchTerm)
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile file = await localFolder.CreateFileAsync(SearchTermFileName, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, searchTerm);
        }

        // Keresési kifejezés inicializálása aszinkron módon
        public async Task InitializeSearchTermAsync()
        {
            string searchTerm = await GetSavedSearchTermAsync();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                await PerformSearch(searchTerm);
            }
        }

        // Elmentett keresési kifejezés lekérése aszinkron módon
        public async Task<string> GetSavedSearchTermAsync()
        {
            try
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile file = await localFolder.GetFileAsync(SearchTermFileName);
                return await FileIO.ReadTextAsync(file);
            }
            catch (FileNotFoundException)
            {
                return string.Empty;
            }
        }

        // Keresés végrehajtása aszinkron módon
        public async Task PerformSearch(string searchTerm)
        {
            IceAndFireApi api = new IceAndFireApi();
            string characterUrl = await api.GetCharacterUrlAsync(searchTerm);

            if (!string.IsNullOrEmpty(characterUrl))
            {
                Character selectedCharacter = await api.GetCharacterDetailsAsync(characterUrl);
                if (frame != null)
                {
                    frame.Navigate(typeof(CharacterDetailsPage), selectedCharacter);
                }
            }
            else
            {
                // Kezelés az adott visszajelzésnek vagy üzenetnek a felhasználónak, ha nem találtunk karaktereket.
            }
        }
    }
}
