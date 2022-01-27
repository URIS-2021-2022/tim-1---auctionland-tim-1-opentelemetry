using PublicBiddingRegistrationMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingRegistrationMicroservice.Data
{
    public class PaymentRepository : IPaymentRepository
    {
        public static List<PaymentForApplication> PaymentForApplications { get; set; } = new List<PaymentForApplication>();

        public PaymentRepository()
        {
            FillData();
        }

        public void FillData()
        {
            PaymentForApplications.AddRange(new List<PaymentForApplication>
            {
                new PaymentForApplication
                {
                    PaymentId = Guid.Parse("044f3de0-a9dd-4c2e-b745-89976a1b2a36"),
                    AccountNumber = 884647615,
                    ReferenceNumber = 748644664,
                    PurposeOfPayment = "Some purpose",
                    DateOfPayment = DateTime.Parse("2020-11-15T09:00:00"),
                    PublicBiddingId = Guid.Parse("9d8bab08-f442-4297-8ab5-ddfe08e335f3")
                },
                new PaymentForApplication
                {
                    PaymentId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    AccountNumber = 88557615,
                    ReferenceNumber = 255644664,
                    PurposeOfPayment = "purpose",
                    DateOfPayment = DateTime.Parse("2020-11-15T09:00:00"),
                    PublicBiddingId = Guid.Parse("21ad52f8-0281-4241-98b0-481566d25e4f")
                }
            });
        }

        public List<PaymentForApplication> GetPayments()
        {
            Console.WriteLine("Svi");
            return PaymentForApplications.ToList();
        }

        public PaymentForApplication GetPaymentsById(Guid paymentId)
        {
            throw new NotImplementedException();
        }

        public PaymentConfirmation CreatePayment(PaymentForApplication payment)
        {
            throw new NotImplementedException();
        }

        public PaymentConfirmation UpdatePayment(PaymentForApplication payment)
        {
            throw new NotImplementedException();
        }

        public void DeletePayment(Guid paymentId)
        {
            throw new NotImplementedException();
        }
    }
}
