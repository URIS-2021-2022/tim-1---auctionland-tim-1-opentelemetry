using ParcelMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelMicroservice.Data
{
    public class ParcelRepository : IParcelRepository
    {
        public static List<Parcel> Parcels { get; set; } = new List<Parcel>();
        public ParcelRepository()
        {
            FillData();
        }

        private static void FillData()
        {
            Parcels.AddRange(new List<Parcel> {
                new Parcel
                {
                    ParcelID = Guid.Parse("866f2352-771f-4405-a9b5-9878b0fbff0f"),
                    SurfaceArea = 6000,
                    NumberOfParcel = "1/2022",
                    RealEstateListNumber = "1 - Prepis",
                    CultureRealStatus = "Njive",
                    ClassRealStatus = "III",
                    WorkabilityActualStatus = "Obradivo",
                    ProtectedZoneActualStatus = "2",
                    FormOfOwnership = "Državna svojina",
                    DrainageActualCondition = "Površinsko odvodnjavanje",
                    MunicipalityID = Guid.Parse("e4902805-41f9-45d7-8b4d-7c85e8f27868"),
                    NameOfTheMunicipality = "Čantavir"
                },
                new Parcel
                {
                    ParcelID = Guid.Parse("628f7390-cb85-4b69-94bd-e3e6c424d725"),
                    SurfaceArea = 3000,
                    NumberOfParcel = "2/2022",
                    RealEstateListNumber = "2 - Prepis",
                    CultureRealStatus = "Pašnjaci",
                    ClassRealStatus = "II",
                    WorkabilityActualStatus = "Ostalo",
                    ProtectedZoneActualStatus = "1",
                    FormOfOwnership = "Društvena svojina",
                    DrainageActualCondition = "Podzemno odvodnjavanje",
                    MunicipalityID = Guid.Parse("aa3f2265-7182-4424-ba83-2eed388ce748"),
                    NameOfTheMunicipality = "Bajmok"
                }
            });
        }

        public List<Parcel> GetParcels(string NumberOfParcel)
        {
            var list = (from p in Parcels
                    where string.IsNullOrEmpty(NumberOfParcel) || p.NumberOfParcel == NumberOfParcel
                    select p);
            var bar = list.GroupBy(x => x.ParcelID).Select(x => x.First()).ToList();
            return bar;

        } 

        public Parcel GetParcelById(Guid parcelID)
        {
            return Parcels.FirstOrDefault(e => e.ParcelID == parcelID);
        }

        public ParcelConfirmation CreateParcel(Parcel parcel)
        {
            parcel.ParcelID = Guid.NewGuid(); //generise se kljuc nove parcele
            Parcels.Add(parcel); //dodaje se nova parcela u listu parcela
            Parcel model = GetParcelById(parcel.ParcelID); //instancira se parcela preko metode GetParcelById

            return new ParcelConfirmation //vraca se model potvrde 
            {
                ParcelID = model.ParcelID,
                SurfaceArea = model.SurfaceArea,
                NumberOfParcel = model.NumberOfParcel,
                NameOfTheMunicipality = model.NameOfTheMunicipality
            };
        }

        public ParcelConfirmation UpdateParcel(Parcel parcel)
        {
            Parcel model = GetParcelById(parcel.ParcelID); //instancira se parcela preko metode GetParcelById

            model.ParcelID = parcel.ParcelID;
            model.SurfaceArea = parcel.SurfaceArea;
            model.NumberOfParcel = parcel.NumberOfParcel;
            model.RealEstateListNumber = parcel.RealEstateListNumber;
            model.CultureRealStatus = parcel.CultureRealStatus;
            model.ClassRealStatus = parcel.ClassRealStatus;
            model.WorkabilityActualStatus = parcel.WorkabilityActualStatus;
            model.ProtectedZoneActualStatus = parcel.ProtectedZoneActualStatus;
            model.FormOfOwnership = parcel.FormOfOwnership;
            model.DrainageActualCondition = parcel.DrainageActualCondition;
            model.MunicipalityID = parcel.MunicipalityID;
            model.NameOfTheMunicipality = parcel.NameOfTheMunicipality;

            return new ParcelConfirmation
            {
                ParcelID = model.ParcelID,
                SurfaceArea = model.SurfaceArea,
                NumberOfParcel = model.NumberOfParcel,
                NameOfTheMunicipality = model.NameOfTheMunicipality
            };
        }

        public void DeleteParcel(Guid parcelID)
        {
            Parcels.Remove(Parcels.FirstOrDefault(e => e.ParcelID == parcelID));
        }
    }
}

