#nullable enable

using System;

namespace WinFormsApp1
{
    public class StationsData
    {
        public string? id_stacji { get; set; }
        public string? stacja { get; set; }
        public string? rzeka { get; set; }
        public string? wojewodztwo { get; set; }
        public string? stan_wody { get; set; }
        public string? stan_wody_data_pomiaru { get; set; }
        public string? temperatura_wody { get; set; }
        public string? temperatura_wody_data_pomiaru { get; set; }
        public string? zjawisko_lodowe { get; set; }
        public string? zjawisko_lodowe_data_pomiaru { get; set; }
        public string? zjawisko_zarastania { get; set; }
        public string? zjawisko_zarastania_data_pomiaru { get; set; }
    }
}