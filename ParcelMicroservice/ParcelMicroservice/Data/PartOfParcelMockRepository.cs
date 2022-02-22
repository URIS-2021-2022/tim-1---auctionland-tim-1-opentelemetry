using ParcelMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelMicroservice.Data
{
    public class PartOfParcelMockRepository : IPartOfParcelRepository
    {
        public static List<PartOfParcel> PartOfParcels { get; set; } = new List<PartOfParcel>();
        public PartOfParcelMockRepository()
        {
            FillData();
        }

        private void FillData()
        {
            PartOfParcels.AddRange(new List<PartOfParcel> {
                new PartOfParcel
                {
                    ParcelID = Guid.Parse("866f2352-771f-4405-a9b5-9878b0fbff0f"),
                    PartOfParcelID = Guid.Parse("67c31d49-b189-4de5-a6e2-b9b5557047a9"),
                    SurfaceAreaPOP = 1000,
                    ClassID = Guid.Parse("61847780-396a-42a7-8e04-941e0d4eddf9"),
                    ClassLandLabel = "I"
                },
                new PartOfParcel
                {
                    ParcelID = Guid.Parse("866f2352-771f-4405-a9b5-9878b0fbff0f"),
                    PartOfParcelID = Guid.Parse("17321524-7822-4daa-8134-a2ec4bed98e0"),
                    SurfaceAreaPOP = 500,
                    ClassID = Guid.Parse("89e2bdc2-7153-463a-8c9f-37bfec240431"),
                    ClassLandLabel = "II"
                }
            });
        }

        public List<PartOfParcel> GetPartOfParcels()
        {
            var list = (from p in PartOfParcels
                        select p);
            var bar = list.GroupBy(x => x.PartOfParcelID).Select(x => x.First()).ToList();
            return bar;

        }

        public PartOfParcel GetPartOfParcelById(Guid partOfParcelID)
        {
            return PartOfParcels.FirstOrDefault(e => e.PartOfParcelID == partOfParcelID);
        }

        public PartOfParcelConfirmation CreatePartOfParcel(PartOfParcel partOfParcel)
        {
            partOfParcel.PartOfParcelID = Guid.NewGuid(); //generise se kljuc novog dela parcele
            partOfParcel.ParcelID = Guid.Parse("866f2352-771f-4405-a9b5-9878b0fbff0f"); //testiranje 
            PartOfParcels.Add(partOfParcel); //dodaje se nova parcela u listu parcela
            PartOfParcel model = GetPartOfParcelById(partOfParcel.PartOfParcelID); //instancira se parcela preko metode GetParcelById

            return new PartOfParcelConfirmation //vraca se model potvrde 
            {
                ParcelID = model.ParcelID,
                PartOfParcelID = model.PartOfParcelID,
                SurfaceAreaPOP = model.SurfaceAreaPOP,
                ClassLandLabel = model.ClassLandLabel
            };
        }

        public void UpdatePartOfParcel(PartOfParcel partOfParcel)
        {
            PartOfParcel model = GetPartOfParcelById(partOfParcel.PartOfParcelID); //instancira se parcela preko metode GetParcelById

            model.ParcelID = Guid.Parse("866f2352-771f-4405-a9b5-9878b0fbff0f");
            model.PartOfParcelID = partOfParcel.PartOfParcelID;
            model.SurfaceAreaPOP = partOfParcel.SurfaceAreaPOP;
            model.ClassID = partOfParcel.ClassID;
            model.ClassLandLabel = partOfParcel.ClassLandLabel;
        }

        public void DeletePartOfParcel(Guid partOfParcelID)
        {
            PartOfParcels.Remove(PartOfParcels.FirstOrDefault(e => e.PartOfParcelID == partOfParcelID));
        }

        public bool SaveChanges()
        {
            return true;
        }
    }
}
