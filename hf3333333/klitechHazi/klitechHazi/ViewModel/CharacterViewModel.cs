using klitechHazi.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace klitechHazi.ViewModel
{
    /// <summary>
    /// ViewModel osztály, amely kezeli a karakter részletek megjelenítését és adatok lekérését.
    /// </summary>
    internal class CharacterViewModel : INotifyPropertyChanged
    {
        private IceAndFireApi api;
        private Character selectedCharacter;
        private List<string> titles;
        private List<string> aliases;
        private List<string> allegiances;
        private List<string> books;
        private List<string> povBooks;
        private List<string> tvSeries;
        private List<string> playedBy;

        /// <summary>
        /// Kiválasztott karakter tulajdonsága.
        /// </summary>
        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set { selectedCharacter = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Címek tulajdonsága.
        /// </summary>
        public List<string> Titles
        {
            get { return titles; }
            set { titles = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Aliasok tulajdonsága.
        /// </summary>
        public List<string> Aliases
        {
            get { return aliases; }
            set { aliases = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Hűbérúri kötelezettségek tulajdonsága.
        /// </summary>
        public List<string> Allegiances
        {
            get { return allegiances; }
            set { allegiances = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Könyvek tulajdonsága.
        /// </summary>
        public List<string> Books
        {
            get { return books; }
            set { books = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// POV könyvek tulajdonsága.
        /// </summary>
        public List<string> PovBooks
        {
            get { return povBooks; }
            set { povBooks = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// TV sorozatok tulajdonsága.
        /// </summary>
        public List<string> TvSeries
        {
            get { return tvSeries; }
            set { tvSeries = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Eljátszotta tulajdonsága.
        /// </summary>
        public List<string> PlayedBy
        {
            get { return playedBy; }
            set { playedBy = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// A karakter részleteinek betöltése a megadott név alapján.
        /// </summary>
        /// <param name="characterName">A karakter neve.</param>
        public async Task LoadCharacterDetails(string characterName)
        {
            api = new IceAndFireApi();
            try
            {
                SelectedCharacter = await api.GetCharacterAsync2(characterName);
            }
            catch (Exception ex)
            {
                // A GetCharacterAsync2 dobott kivétel, próbáljuk meg a GetCharacterAsync-t használni
                try
                {
                    SelectedCharacter = await api.GetCharacterAsync(characterName);
                }
                catch (Exception innerEx)
                {
                    // Mindkét függvény dobott kivételt, kezeljük a hibát itt
                    // Itt a két kivétel objektum (ex és innerEx) rendelkezésre áll, amelyeket
                    // az alkalmazásban megfelelően kezelhetsz vagy jelenthetsz.
                    // Például:
                    Console.WriteLine($"Hiba történt a karakter betöltése közben: {ex.Message}");
                    Console.WriteLine($"Hiba részletei: {ex.StackTrace}");
                    return;
                }
            }

            if (SelectedCharacter != null)
            {
                Titles = SelectedCharacter.Titles;
                Aliases = SelectedCharacter.Aliases;
            }
        }


        /// <summary>
        /// A karakterek neveinek lekérése a megadott URL-ek alapján.
        /// </summary>
        /// <param name="characterUrls">A karakterek URL-jei.</param>
        /// <returns>A karakterek neveinek listája.</returns>
        public async Task<List<string>> GetCharacterNames(List<string> characterUrls)
        {
            List<string> characterNames = new List<string>();

            foreach (string url in characterUrls)
            {
                Character character = await api.GetCharacterAsync(url);
                if (character != null)
                {
                    characterNames.Add(character.Name);
                }
            }

            return characterNames;
        }

        /// <summary>
        /// A könyvek neveinek lekérése a megadott URL-ek alapján.
        /// </summary>
        /// <param name="bookUrls">A könyvek URL-jei.</param>
        /// <returns>A könyvek neveinek listája.</returns>
        public async Task<List<string>> GetBookNames(List<string> bookUrls)
        {
            List<string> bookNames = new List<string>();

            foreach (string url in bookUrls)
            {
                Book book = await api.GetBookAsync(url);
                if (book != null)
                {
                    bookNames.Add(book.Name);
                }
            }

            return bookNames;
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
