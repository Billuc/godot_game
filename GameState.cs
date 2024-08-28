using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Godot;

namespace FYBF
{
    public class GameState
    {
        private static readonly char[] ALL_VOWELS = { 'a', 'e', 'i', 'o', 'u', 'y' };
        private static GameState _instance = null;

        private Place[] AllPlaces { get; set; }
        private Connection[] AllConnections { get; set; }

        private string CurrentPlace { get; set; } = String.Empty;
        private string BestCharacterReached { get; set; } = String.Empty;
        private bool IsNewBestCharacter { get; set; } = false;
        private string SelectedTransportationType { get; set; } = String.Empty;
        private string PreviousSelectedTransportationType { get; set; } = String.Empty;

        private GameState()
        {
            byte[] jsonPlaces = FileAccess.GetFileAsBytes("res://places.json");
            AllPlaces = JsonSerializer.Deserialize<Place[]>(jsonPlaces);

            byte[] jsonConnections = FileAccess.GetFileAsBytes("res://connections.json");
            AllConnections = JsonSerializer.Deserialize<Connection[]>(jsonConnections);
        }

        public static GameState GetInstance()
        {
            GameState._instance ??= new GameState();
            return GameState._instance;
        }

        public string GetCurrentPlaceName()
        {
            Place currentPlaceObj = AllPlaces.First(p => p.Name == CurrentPlace);
            return currentPlaceObj.Name;
        }

        public (string Character, string Interaction) GetCurrentCharacter()
        {
            Place currentPlaceObj = AllPlaces.First(p => p.Name == CurrentPlace);
            return (currentPlaceObj.Character, currentPlaceObj.Interaction);
        }

        public string[] GetCurrentTransportationTypes()
        {
            return AllConnections.Where(c => c.From == CurrentPlace).Select(c => c.Type).Distinct().ToArray();
        }

        public string[] GetCurrentIndications()
        {
            return AllConnections.Where(c => c.From == CurrentPlace && c.Type == SelectedTransportationType).Select(c => c.Indication).Distinct().ToArray();
        }

        public void SetCurrentPlace(string newPlace)
        {
            Place newPlaceObj = AllPlaces.First(p => p.Name == newPlace);
            bool bestPlaceReached = !AllPlaces.TakeWhile(p => p.Character != BestCharacterReached).Any(p => p.Name == newPlace);

            IsNewBestCharacter = bestPlaceReached && newPlaceObj.Character != "" && newPlaceObj.Character != BestCharacterReached;
            if (IsNewBestCharacter)
            {
                BestCharacterReached = newPlaceObj.Character;
            }

            CurrentPlace = newPlaceObj.Name;
            PreviousSelectedTransportationType = SelectedTransportationType;
            SelectedTransportationType = String.Empty;
        }

        public void SetTransportationType(string newTransportationType)
        {
            PreviousSelectedTransportationType = SelectedTransportationType;
            SelectedTransportationType = newTransportationType;
        }

        public string FormatTransportation(string transportationType)
        {
            string lowercaseTransporation = transportationType.ToLower();
            bool startsWithAVowel = ALL_VOWELS.Any(v => v == lowercaseTransporation[0]);
            bool isCurrentTransportation = transportationType == PreviousSelectedTransportationType;

            string action = isCurrentTransportation ? "Rester dans " : "Prendre ";
            string article = startsWithAVowel ? "l'" : "le ";

            return action + article + lowercaseTransporation;
        }
    }

    class Place
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("character")]
        public string Character { get; set; } = String.Empty;

        [JsonPropertyName("interaction")]
        public string Interaction { get; set; } = String.Empty;
    }

    class Connection
    {
        [JsonPropertyName("from")]
        public string From { get; set; }

        [JsonPropertyName("to")]
        public string To { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("indication")]
        public string Indication { get; set; }
    }
}