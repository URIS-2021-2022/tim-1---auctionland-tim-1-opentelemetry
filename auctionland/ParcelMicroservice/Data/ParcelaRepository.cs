using ParcelMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelMicroservice.Data
{
    public class ParcelaRepository : IParcelaRepository
    {
        public static List<ParcelaModel> ParcelModels { get; set; } = new List<ParcelaModel>();

        public ParcelaRepository()
        {
            FillData();
        }

        public void FillData()
        {
            ParcelModels.AddRange(new List<ParcelaModel>
            {
                new ParcelaModel
                {
                    ParcelaID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    PovrsinaParcele = 1077,
                    BrojParcele = "1",
                    BrojListaNepokretnosti = "5432",
                    OblikSvojineStvarnoStanje = ParcelaModel.OblikSvojine[0],
                    KulturaStvarnoStanje = ParcelaModel.Kultura[1],
                    KlasaStvarnoStanje = ParcelaModel.Klasa[4],
                    ObradivostStvarnoStanje = ParcelaModel.Obradivost[1],
                    ZasticenaZonaStvarnoStanje = ParcelaModel.ZasticenaZona[2],
                    OdvodnjavanjeStvarnoStanje = ParcelaModel.Odvodnjavanje[0]
                    new OpstinaModel 
                    {

                    },
                    ListaDelovaParcele

                }
            });
        }

        public ParcelaConfirmation CreateParcel(ParcelaModel parcelModel)
        {
            throw new NotImplementedException();
        }

        public void DeleteParcel(Guid parcelID)
        {
            throw new NotImplementedException();
        }

        public ParcelaModel GetParcelByID(Guid parcelID)
        {
            throw new NotImplementedException();
        }

        public List<ParcelaModel> GetParcels(int parcelSurface = 0, List<DeoParceleModel> parcelParts = null)
        {
            throw new NotImplementedException();
        }

        public ParcelaConfirmation UpdateParcel(ParcelaModel parcelModel)
        {
            throw new NotImplementedException();
        }
    }
}
