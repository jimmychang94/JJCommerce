using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthorizeNet.Api.Controllers.Bases;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Contracts;
using AuthorizeNet.Api.Contracts.V1;
using Microsoft.Extensions.Configuration;

namespace JandJCommerce.Models
{
    public class ChargeCreditCard
    {

        public IConfiguration Configuration { get; }

       public ChargeCreditCard(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public ANetApiResponse RunCard()
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


            //Temp credit card to use for tests
            var creditCard = new creditCardType
            {
                cardNumber = "4111111111111111",
                expirationDate = "0827",
                cardCode = "123"
            };


            //address to refactor
            var billingAddress = new customerAddressType
            {
                firstName = "John",
                lastName = "Doe",
                address = "123 my st",
                city = "town",
                zip = "98119"
            };

            //Add line Items orders //


            //standard api call to recieve response 
            var paymentType = new paymentType { Item = creditCard };

            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authCaptureContinueTransaction.ToString(),

                amount = 99.99M,
                payment = paymentType,
                billTo = billingAddress,
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
