using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthorizeNet.Api.Controllers.Bases;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Contracts;
using AuthorizeNet.Api.Contracts.V1;
using Microsoft.Extensions.Configuration;
using JandJCommerce.Models.ViewModels;

namespace JandJCommerce.Models
{
    /// <summary>
    /// This is the class that charges the user's credit card
    /// </summary>
    public class ChargeCreditCard
    {

        public IConfiguration Configuration { get; }

       public ChargeCreditCard(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// This is the method that charges the user's credit card.
        /// </summary>
        /// <param name="cvm">The information we use to charge the credit card</param>
        /// <returns>The response from the api</returns>
        public ANetApiResponse RunCard(CheckoutViewModel cvm)
        {

            //make sure it's running in the sandbox environment//
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;


            //define the merchant information (authentication / transaction key)
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = Configuration["Authorize:ApiLoginID"],
                ItemElementName = ItemChoiceType.transactionKey,
                Item = Configuration["Authorize:ApiTransactionKey"]
            };

            long cardNumber = (long)cvm.Card;
            //Temp credit card to use for tests
            var creditCard = new creditCardType
            {
                cardNumber = cardNumber.ToString(),
                expirationDate = "0918",
                cardCode = "123"
            };


            //address to refactor
            var billingAddress = new customerAddressType
            {
                firstName = cvm.FirstName,
                lastName = cvm.LastName,
                address = cvm.Street,
                city = cvm.City,
                zip = "98119"
            };

            //Add line Items orders //
            lineItemType[] MakeLineItems = new lineItemType[cvm.Order.BasketItems.Count];
            for (int i = 0; i < MakeLineItems.Length; i++)
            {
                MakeLineItems[i] = new lineItemType
                {
                    itemId = cvm.Order.BasketItems[i].Product.ID.ToString(),
                    name = cvm.Order.BasketItems[i].Product.Name,
                    quantity = cvm.Order.BasketItems[i].Quantity,
                    unitPrice = cvm.Order.BasketItems[i].Product.Price
                };
            }

            //standard api call to recieve response 
            var paymentType = new paymentType { Item = creditCard };

            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),

                amount = cvm.Order.TotalPrice,
                payment = paymentType,
                billTo = billingAddress,
                lineItems = MakeLineItems
            };

            var request = new createTransactionRequest { transactionRequest = transactionRequest };

            // instantiate new controller that will call service
            var controller = new createTransactionController(request);
            controller.Execute();


            //get response from service (errors contained if any)
            var response = controller.GetApiResponse();

            //validate response

            if (response.messages.resultCode == messageTypeEnum.Ok)
            {
                if (response.transactionResponse != null)
                {
                    Console.WriteLine($"Success, Auth Code : {response.transactionResponse.authCode}");
                }
            }
            else
            {
                Console.WriteLine($"Error : {response.messages.message[0].code} {response.messages.message[0].text}");
                if (response.transactionResponse != null)
                {
                    Console.WriteLine($"Transaction Error : {response.transactionResponse.errors[0].errorCode}  {response.transactionResponse.errors[0].errorText}");
                }
            }
            return response;
        

        }
    
    }
}
