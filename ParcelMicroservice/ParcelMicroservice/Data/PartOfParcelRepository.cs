using ParcelMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelMicroservice.Data
{
    public class PartOfParcelRepository : IPartOfParcelRepository
    {
        public static List<PartOfParcelModel> PartOfParcels { get; set; } = new List<PartOfParcelModel>();
        public PartOfParcelRepository()
        {
            FillData();
        }

        private void FillData()
        {
            PartOfParcels.AddRange(new List<PartOfParcelModel> {
                new PartOfParcelModel
                {
                    ParcelID = Guid.Parse("a910ad63-b699-4c1a-84df-ea918f1dc82a"),
                    PartOfParcelID = Guid.Parse("67c31d49-b189-4de5-a6e2-b9b5557047a9"),
                    SurfaceAreaPOP = 1000,
                    ClassID = Guid.Parse("61847780-396a-42a7-8e04-941e0d4eddf9"),
                    ClassLandLabel = "I"
                },
                new PartOfParcelModel
                {
                    ParcelID = Guid.Parse("a910ad63-b699-4c1a-84df-ea918f1dc82a"),
                    PartOfParcelID = Guid.Parse("17321524-7822-4daa-8134-a2ec4bed98e0"),
                    SurfaceAreaPOP = 500,
                    ClassID = Guid.Parse("89e2bdc2-7153-463a-8c9f-37bfec240431"),
                    ClassLandLabel = "II"
                }
            });
        }

        public List<PartOfParcelModel> GetPartOfParcels()
        {
            var list = (from p in PartOfParcels
                        select p);
            var bar = list.GroupBy(x => x.ParcelID).Select(x => x.First()).ToList();
            return bar;

        }

        public PartOfParcelModel GetPartOfParcelById(Guid parcelID, Guid partOfParcelID)
        {
            return PartOfParcels.FirstOrDefault(e => e.ParcelID == parcelID && e.PartOfParcelID == partOfParcelID);
        }

        public PartOfParcelConfirmation CreatePartOfParcel(PartOfParcelModel partOfParcel)
        {
            partOfParcel.ParcelID = Guid.NewGuid(); //generise se kljuc nove parcele
            PartOfParcels.Add(partOfParcel); //dodaje se nova parcela u listu parcela
            PartOfParcelModel model = GetPartOfParcelById(partOfParcel.ParcelID, partOfParcel.PartOfParcelID); //instancira se parcela preko metode GetParcelById

            return new PartOfParcelConfirmation //vraca se model potvrde 
            {
                ParcelID = model.ParcelID,
                PartOfParcelID = model.PartOfParcelID,
                SurfaceAreaPOP = model.SurfaceAreaPOP,
                ClassLandLabel = model.ClassLandLabel
            };
        }

        public PartOfParcelConfirmation UpdatePartOfParcel(PartOfParcelModel partOfParcel)
        {
            PartOfParcelModel model = GetPartOfParcelById(partOfParcel.ParcelID, partOfParcel.PartOfParcelID); //instancira se parcela preko metode GetParcelById

            model.ParcelID = partOfParcel.ParcelID;
            model.PartOfParcelID = partOfParcel.PartOfParcelID;
            model.SurfaceAreaPOP = partOfParcel.SurfaceAreaPOP;
            model.ClassID = partOfParcel.ClassID;
            model.ClassLandLabel = partOfParcel.ClassLandLabel;

            return new PartOfParcelConfirmation
            {
                ParcelID = model.ParcelID,
                PartOfParcelID = model.PartOfParcelID,
                SurfaceAreaPOP = model.SurfaceAreaPOP,
                ClassLandLabel = model.ClassLandLabel
            };
        }

        public void DeletePartOfParcel(Guid parcelID, Guid partOfParcelID)
        {
            PartOfParcels.Remove(PartOfParcels.FirstOrDefault(e => e.ParcelID == parcelID && e.PartOfParcelID == partOfParcelID));
        }
    }
}
