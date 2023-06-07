using klitechHazi.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace klitechHazi.ViewModel
{
    internal class BooksViewModel : INotifyPropertyChanged
    {
        private Book selectedBook;
        private string title;
        private string authors;
        private string isbn;
        private int numberOfPages;
        private string publisher;
        private string country;
        private string mediaType;
        private string released;

        public Book SelectedBook
        {
            get { return selectedBook; }
            set { selectedBook = value; OnPropertyChanged(); }
        }

        public string Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged(); }
        }

        public string Authors
        {
            get { return authors; }
            set { authors = value; OnPropertyChanged(); }
        }

        public string Isbn
        {
            get { return isbn; }
            set { isbn = value; OnPropertyChanged(); }
        }

        public int NumberOfPages
        {
            get { return numberOfPages; }
            set { numberOfPages = value; OnPropertyChanged(); }
        }

        public string Publisher
        {
            get { return publisher; }
            set { publisher = value; OnPropertyChanged(); }
        }

        public string Country
        {
            get { return country; }
            set { country = value; OnPropertyChanged(); }
        }

        public string MediaType
        {
            get { return mediaType; }
            set { mediaType = value; OnPropertyChanged(); }
        }

        public string Released
        {
            get { return released; }
            set { released = value; OnPropertyChanged(); }
        }

        public async Task LoadBookDetails(Book book)
        {
            SelectedBook = book;

            // Könyv részletek lekérése és beállítása
            Title = "Címe: " + book.Name;
            Authors = "Írója: " + string.Join(", ", book.Authors);
            // Egyéb könyv részletek beállítása itt

            // Példa más adatok beállítására:
            Isbn = "ISBN: " + book.Isbn;
            NumberOfPages = book.NumberOfPages;
            Publisher = "Kiadó: " + book.Publisher;
            Country = "Ország: " + book.Country;
            MediaType = "Média típusa: " + book.MediaType;
            Released = "Megjelenés dátuma: " + book.Released.ToString();
        }

        public async Task<ObservableCollection<Character>> LoadCharacters()
        {
            IceAndFireApi api = new IceAndFireApi();
            ObservableCollection<Character> characters = await api.GetCharactersAsyncCharacter();
            return characters;
        }

        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
