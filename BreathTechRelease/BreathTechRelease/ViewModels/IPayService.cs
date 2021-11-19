using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BreathTechRelease.ViewModels
{
    public interface IPayService
    {

        string clientToken { get; set; }
        double total_amount { get; set; }

        event EventHandler<string> OnTokenizationSuccessful;

        event EventHandler<string> OnTokenizationError;

        bool CanPay { get; }

        Task<bool> InitializeAsync(string paymentClientToken);

        //For Card payment
        Task<string> TokenizeCard(string panNumber = "4111111111111111", string expirationMonth = "12", string expirationYear = "2018", string cvv = null);

        //for gpay/apppay
        Task<string> TokenizePlatform(double totalPrice, string merchantId);

        //For paypal payment
        Task<string> TokenizePayPal();

        //for DropUI
        event EventHandler<DropUIResult> OnDropUISuccessful;
        event EventHandler<string> OnDropUIError;
        Task<DropUIResult> ShowDropUI(double totalPrice, string merchantId, int requestCode = 1234);

    }
}
