using PublicBiddingRegistrationMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingRegistrationMicroservice.Data
{
    public interface IPaymentRepository
    {
        List<PaymentForApplication> GetPayments();

        PaymentForApplication GetPaymentsById(Guid paymentId);

        PaymentConfirmation CreatePayment(PaymentForApplication payment);

        PaymentConfirmation UpdatePayment(PaymentForApplication payment);

        void DeletePayment(Guid paymentId);
    }
}
