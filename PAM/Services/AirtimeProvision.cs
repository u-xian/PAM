using System;
using System.Collections.Generic;

namespace PAM.Services
{
    public class AirtimeProvision : MiddleWareApi
    {
        public Dictionary<string, string> addStock(string msisdn, string amount, string tcnxid)
        {

            Dictionary<string, string> results = new Dictionary<string, string>();

            this.url = "";


            this.requestXml = @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:v1=""http://xmlns.tigo.com/BuyTransferBalanceRequest/V1"" xmlns:v3=""http://xmlns.tigo.com/RequestHeader/V3"" xmlns:v2=""http://xmlns.tigo.com/ParameterType/V2"" xmlns:cor=""http://soa.mic.co.af/coredata_1""><soapenv:Header xmlns:wsse=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd""><cor:debugFlag>true</cor:debugFlag><wsse:Security><wsse:UsernameToken><wsse:Username>PARAM_MW_USERNAME</wsse:Username><wsse:Password>PARAM_MW_PASSWORD</wsse:Password></wsse:UsernameToken></wsse:Security></soapenv:Header><soapenv:Body><v1:BuyTransferBalanceRequest><v3:RequestHeader><v3:GeneralConsumerInformation><v3:consumerID>PARAM_MW_CONSUMER_ID</v3:consumerID><!--Optional:--><v3:transactionID>PARAM_EXTERNAL_TRANSACTION_ID</v3:transactionID><v3:country>RWA</v3:country><v3:correlationID>qwerty</v3:correlationID></v3:GeneralConsumerInformation></v3:RequestHeader><v1:requestBody><v1:action>BUY</v1:action><!--Optional:--><v1:creditMsisdn>PARAM_MSISDN</v1:creditMsisdn><v1:amount>PARAM_AMOUNT</v1:amount><!--Optional:--><v1:transactionId>PARAM_EXTERNAL_TRANSACTION_ID</v1:transactionId><!--Optional:--><v1:institutionId>123</v1:institutionId><!--Optional:--><v1:additionalParameters><!--1 or more repetitions:--><v2:ParameterType><v2:parameterName>consumerType</v2:parameterName><v2:parameterValue>ChannelUser</v2:parameterValue></v2:ParameterType><v2:ParameterType><v2:parameterName>TRFCATEGORY</v2:parameterName><v2:parameterValue>SALE</v2:parameterValue></v2:ParameterType><v2:ParameterType><v2:parameterName>PAYMENTTYPE</v2:parameterName><v2:parameterValue>CASH</v2:parameterValue></v2:ParameterType></v1:additionalParameters></v1:requestBody></v1:BuyTransferBalanceRequest></soapenv:Body></soapenv:Envelope>";
            this.requestXml = this.requestXml.Replace("PARAM_MW_USERNAME", Properties.Settings.Default.MW_USERNAME);
            this.requestXml = this.requestXml.Replace("PARAM_MW_PASSWORD", Properties.Settings.Default.MW_PASSWORD);
            this.requestXml = this.requestXml.Replace("PARAM_MW_CONSUMER_ID", Properties.Settings.Default.MW_CONSUMER_ID);
            this.requestXml = this.requestXml.Replace("PARAM_MSISDN", msisdn);
            this.requestXml = this.requestXml.Replace("PARAM_AMOUNT", amount);
            this.requestXml = this.requestXml.Replace("PARAM_EXTERNAL_TRANSACTION_ID", tcnxid);


            try
            {

                results = this.cleanResponse(this.sendRequest());
                //LogsModel.save("TIGO|O2C", msisdn, this.removeSensitiveInformation(requestquery), responseXmlString);
                return results;

            }
            catch (Exception e)
            {
                results.Add("Exception", e.Message);
                return results;
            }
        }



    }
}