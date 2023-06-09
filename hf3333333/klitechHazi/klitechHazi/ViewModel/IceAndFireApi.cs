﻿using klitechHazi.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace klitechHazi
{
    internal class IceAndFireApi
    {
        private const string BaseUrl = "https://www.anapioficeandfire.com/api/";
        private HttpClient _httpClient = new HttpClient();

        public async Task<ObservableCollection<Book>> GetBooksAsync()
        {
            string url = $"{BaseUrl}books";
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                ObservableCollection<Book> books = JsonConvert.DeserializeObject<ObservableCollection<Book>>(json);
                return books;
            }

            return null;
        }

        //Keresesnel vegul ezt hasznalom
        public async Task<Character> GetCharacterAsync2(string characterName)
        {
            string url = $"{BaseUrl}characters?name={characterName}"; // Az adott karakter URL-je

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                List<Character> characters = JsonConvert.DeserializeObject<List<Character>>(json);

                if (characters != null && characters.Count > 0)
                {
                    return characters[0];
                }
            }

            throw new Exception("Karakter nem található."); // Kivétel dobása, ha nem található karakter
        }
        //Ezt hasznalom mindenhol mashol ahol nev alapjan megyek tovabb a detailesView-ra
        public async Task<Character> GetCharacterAsync(string characterName)
        {
            string url = characterName; // Az adott karakter URL-je

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                Character character = JsonConvert.DeserializeObject<Character>(json);

                if (character != null)
                {
                    return character;
                }
            }

            throw new Exception("Karakter nem található."); // Kivétel dobása, ha nem található karakter
        }




        public async Task<List<string>> GetCharactersBySearchAsync(string searchTerm)
        {
            string url = $"{BaseUrl}characters?pageSize=50";
            List<string> characterUris = new List<string>();

            while (!string.IsNullOrEmpty(url))
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    List<Character> characters = JsonConvert.DeserializeObject<List<Character>>(json);

                    if (characters != null)
                    {
                        List<Character> filteredCharacters = characters.Where(c => c.Name.Equals(searchTerm)).ToList();

                        List<string> filteredCharacterUris = filteredCharacters.Select(c => c.Url).ToList();
                        characterUris.AddRange(filteredCharacterUris);

                        // Ellenőrizzük, hogy van-e további oldal az eredményekben
                        IEnumerable<string> linkHeaders = response.Headers.GetValues("Link");
                        url = GetNextPageUrlFromLinkHeaders(linkHeaders);
                    }
                    else
                    {
                        url = null;
                    }
                }
                else
                {
                    url = null;
                }
            }

            if (characterUris.Any())
            {
                return characterUris;
            }

            throw new Exception("Nem sikerült lekérni a karaktereket.");
        }

        //lapozashoz kell, mert egyszerre csak 10 karaktert ker le egy oldalon
        private string GetNextPageUrlFromLinkHeaders(IEnumerable<string> linkHeaders)
        {
            foreach (string linkHeader in linkHeaders)
            {
                string[] links = linkHeader.Split(',');

                foreach (string link in links)
                {
                    string[] parts = link.Split(';');

                    if (parts.Length == 2 && parts[1].Trim() == "rel=\"next\"")
                    {
                        string url = parts[0].Trim();

                        if (url.StartsWith("<") && url.EndsWith(">"))
                        {
                            return url.Substring(1, url.Length - 2);
                        }
                    }
                }
            }

            return null;
        }


        public async Task<List<string>> GetCharactersAsync()
        {
            string url = $"{BaseUrl}characters?pageSize=50";
            List<string> characterNames = new List<string>();

            while (!string.IsNullOrEmpty(url))
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    List<Character> characters = JsonConvert.DeserializeObject<List<Character>>(json);

                    if (characters != null)
                    {
                        List<string> characterNamesOnPage = characters.Select(c => c.Name).ToList();
                        characterNames.AddRange(characterNamesOnPage);

                        // Ellenőrizzük, hogy van-e további oldal az eredményekben
                        IEnumerable<string> linkHeaders = response.Headers.GetValues("Link");
                        url = GetNextPageUrlFromLinkHeaders(linkHeaders);
                    }
                    else
                    {
                        url = null;
                    }
                }
                else
                {
                    url = null;
                }
            }

            if (characterNames.Any())
            {
                return characterNames;
            }

            throw new Exception("Nem sikerült lekérni a karaktereket.");
        }


        //a Hazak api lekereset valositja meg
        public async Task<ObservableCollection<string>> GetHouseNamesAsync()
        {
            string url = $"{BaseUrl}houses?pageSize=50";
            List<string> houseNames = new List<string>();

            while (!string.IsNullOrEmpty(url))
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    List<House> houses = JsonConvert.DeserializeObject<List<House>>(json);

                    if (houses != null)
                    {
                        List<string> namesOnPage = houses.Select(h => h.Name).ToList();
                        houseNames.AddRange(namesOnPage);

                        // Ellenőrizzük, hogy van-e további oldal az eredményekben
                        IEnumerable<string> linkHeaders = response.Headers.GetValues("Link");
                        url = GetNextPageUrlFromLinkHeaders(linkHeaders);
                    }
                    else
                    {
                        url = null;
                    }
                }
                else
                {
                    url = null;
                }
            }

            if (houseNames.Any())
            {
                return new ObservableCollection<string>(houseNames);
            }

            throw new Exception("Nem sikerült lekérni a házakat.");
        }

        //Itt egy karaktert van az ObservableCollection-ben a visszateresnel
        public async Task<ObservableCollection<Character>> GetCharactersAsyncCharacter()
        {
            string url = $"{BaseUrl}characters?pageSize=50";
            ObservableCollection<Character> characters = new ObservableCollection<Character>();

            while (!string.IsNullOrEmpty(url))
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    ObservableCollection<Character> charactersOnPage = JsonConvert.DeserializeObject<ObservableCollection<Character>>(json);

                    if (charactersOnPage != null)
                    {
                        foreach (Character character in charactersOnPage)
                        {
                            characters.Add(character);
                        }

                        // Ellenőrizzük, hogy van-e további oldal az eredményekben
                        IEnumerable<string> linkHeaders = response.Headers.GetValues("Link");
                        url = GetNextPageUrlFromLinkHeaders(linkHeaders);
                    }
                    else
                    {
                        url = null;
                    }
                }
                else
                {
                    url = null;
                }
            }

            if (characters.Any())
            {
                return characters;
            }

            throw new Exception("Nem sikerült lekérni a karaktereket.");
        }



        //a konyvek api kereseit valositja meg
        public async Task<Book> GetBookAsync(string bookUrl)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(bookUrl);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                Book book = JsonConvert.DeserializeObject<Book>(json);
                return book;
            }





            return null;
        }




        //a karakterek url-jet keri le az api-rol
        public async Task<string> GetCharacterUrlAsync(string characterName)
        {
            try
            {
                string url = $"{BaseUrl}characters?name={characterName}";
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    List<Character> characters = JsonConvert.DeserializeObject<List<Character>>(json);

                    if (characters != null && characters.Count > 0)
                    {
                        return characters[0].Url;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Hiba történt a karakter URL lekérdezése során.", ex);
            }

            throw new Exception("Karakter nem található.");
        }


        public async Task<Character> GetCharacterDetailsAsync(string characterUrl)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(characterUrl);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                Character character = JsonConvert.DeserializeObject<Character>(json);
                return character;
            }

            throw new Exception("Karakter nem található.");
        }


    }
}
