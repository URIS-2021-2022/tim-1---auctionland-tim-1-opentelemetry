using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelMicroservice.Models
{
    public class ParcelaModel
    {
        public Guid ParcelaID { get; set; }
        public int PovrsinaParcele { get; set; }
        public string BrojParcele { get; set; }
        public string BrojListaNepokretnosti { get; set; }
        public string OblikSvojineStvarnoStanje { get; set; }
        public string KulturaStvarnoStanje { get; set; }
        public string KlasaStvarnoStanje { get; set; }
        public string ObradivostStvarnoStanje { get; set; }
        public string ZasticenaZonaStvarnoStanje { get; set; }
        public string OdvodnjavanjeStvarnoStanje { get; set; }
        public OpstinaModel Opstina { get; set; }
        public List<DeoParceleModel> ListaDelovaParcele { get; set; }
        public static List<String> Kultura { get; set; } = new List<String> { "Njive", "Vrtovi", "Voćnjaci", "Vinogradi", "Livade", "Pašnjaci", "Šume", "Trstici-močvare" };
        public static List<String> Obradivost { get; set; } = new List<String> { "Obradivo", "Ostalo" };
        public static List<String> ZasticenaZona { get; set; } = new List<String> { "1", "2", "3", "4" };
        public static List<String> OblikSvojine { get; set; } = new List<String> { "Privatna svojina", "Državna svojina RS", "Državna svojina", "Društvena svojina", "Zadružna svojina", "Mešovita svojina", "Drugi oblici" };
        public static List<String> Odvodnjavanje { get; set; } = new List<String> { "Ravnanje zemljišta", "Sezonski kanali", "Odvodnjavanje drenažom", "Otvoreni kanali" };
        public static List<String> Klasa { get; set; } = new List<String> { "I", "II", "III", "IV", "V", "VI", "VII", "VIII" };

    }
}
