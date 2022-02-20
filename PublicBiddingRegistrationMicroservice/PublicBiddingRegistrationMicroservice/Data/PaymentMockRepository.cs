using PublicBiddingRegistrationMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingRegistrationMicroservice.Data
{
    public class PaymentMockRepository : IPaymentRepository
    {
        public static List<PaymentForApplication> PaymentForApplications { get; set; } = new List<PaymentForApplication>();

        public PaymentMockRepository()
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
            return PaymentForApplications.ToList();
        }

        public PaymentForApplication GetPaymentsById(Guid paymentId)
        {
            return PaymentForApplications.FirstOrDefault(e => e.PaymentId.Equals(paymentId));
        }

        public PaymentConfirmation CreatePayment(PaymentForApplication payment)
        {
            payment.PaymentId = Guid.NewGuid();
            PaymentForApplications.Add(payment);
            PaymentForApplication pay = GetPaymentsById(payment.PaymentId);

            return new PaymentConfirmation
            {
                PaymentId = pay.PaymentId,
                AccountNumber = pay.AccountNumber,
                ReferenceNumber = pay.ReferenceNumber,
                PurposeOfPayment = pay.PurposeOfPayment,
                DateOfPayment = pay.DateOfPayment,
                PublicBiddingId = pay.PublicBiddingId
            };
        }

        public void UpdatePayment(PaymentForApplication payment)
        {
            var pay = GetPaymentsById(payment.PaymentId);

            pay.AccountNumber = payment.AccountNumber;
            pay.ReferenceNumber = payment.ReferenceNumber;
            pay.PurposeOfPayment = payment.PurposeOfPayment;
            pay.DateOfPayment = payment.DateOfPayment;
            pay.PublicBiddingId = payment.PublicBiddingId;
        }

        public void DeletePayment(Guid paymentId)
        {
            PaymentForApplications.Remove(PaymentForApplications.FirstOrDefault(e => e.PaymentId.Equals(paymentId)));
        }

        public bool SaveChanges()
        {
            return true;
        }
    }
}
