using AutoMapper;
using PublicBiddingRegistrationMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingRegistrationMicroservice.Data
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IMapper mapper;
        private readonly ApplicationContext context;

        public PaymentRepository(IMapper mapper, ApplicationContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public PaymentConfirmation CreatePayment(PaymentForApplication payment)
        {
            var createdEntity = context.PaymentForApplications.Add(payment);
            return mapper.Map<PaymentConfirmation>(createdEntity.Entity);
        }

        public void DeletePayment(Guid paymentId)
        {
            context.PaymentForApplications.Remove(GetPaymentsById(paymentId));
        }

        public List<PaymentForApplication> GetPayments()
        {
            return context.PaymentForApplications.ToList();
        }

        public PaymentForApplication GetPaymentsById(Guid paymentId)
        {
            return context.PaymentForApplications.FirstOrDefault(e => e.PaymentId.Equals(paymentId));
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public void UpdatePayment(PaymentForApplication payment)
        {
            //implementacija ne ide jer baza obavlja to
        }
    }
}
