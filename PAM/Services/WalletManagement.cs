using System;
using System.Collections.Generic;

namespace PAM.Services
{
    public class WalletManagement : MiddleWareApi
    {

        public Dictionary<string, string> pay(string msisdn, string amount, string pin)
        {
            Dictionary<string, string> results = new Dictionary<string, string>();

            this.url = "";

            this.requestXml = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:v2=\"http://xmlns.tigo.com/MFS/WalletManagementRequest/V2\" xmlns:v3=\"http://xmlns.tigo.com/RequestHeader/V3\" xmlns:v21=\"http://xmlns.tigo.com/ParameterType/V2\" xmlns:cor=\"http://soa.mic.co.af/coredata_1\">\r\n<soapenv:Header xmlns:wsse=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd\">\r\n<cor:debugFlag>true</cor:debugFlag><wsse:Security>\r\n<wsse:UsernameToken>\r\n<wsse:Username>PARAM_MW_USERNAME</wsse:Username>\r\n<wsse:Password>PARAM_MW_PASSWORD</wsse:Password>\r\n</wsse:UsernameToken>\r\n</wsse:Security>\r\n</soapenv:Header>\r\n<soapenv:Body>\r\n<v2:CashoutRequest>\r\n<v3:RequestHeader>\r\n<v3:GeneralConsumerInformation>\r\n<v3:consumerID>PARAM_MW_CONSUMER_ID</v3:consumerID>\r\n<!--Optional:--><v3:transactionID>PARAM_EXTERNAL_TRANSACTION_ID</v3:transactionID>\r\n<v3:country>RWA</v3:country>\r\n<v3:correlationID>111</v3:correlationID>\r\n</v3:GeneralConsumerInformation>\r\n</v3:RequestHeader>\r\n<v2:requestBody>\r\n<v2:sourceWallet>\r\n<!--You have a CHOICE of the next 2 items at this level--><v2:msisdn>PARAM_CHANNEL</v2:msisdn>\r\n</v2:sourceWallet>\r\n<v2:targetWallet>\r\n<!--You have a CHOICE of the next 2 items at this level--><v2:msisdn>PARAM_MFI_MSISDN</v2:msisdn>\r\n   </v2:targetWallet>\r\n<!--Optional:--><v2:password>PARAM_CUSTOMER_PIN</v2:password>\r\n<v2:amount>PARAM_AMOUNT</v2:amount>\r\n<!--Optional:--><v2:additionalParameters>\r\n<!--Zero or more repetitions:--><v21:ParameterType>\r\n<v21:parameterName>ShortCode</v21:parameterName>\r\n<v21:parameterValue>80019</v21:parameterValue>\r\n</v21:ParameterType>\r\n</v2:additionalParameters>\r\n</v2:requestBody>\r\n</v2:CashoutRequest>\r\n</soapenv:Body>\r\n</soapenv:Envelope>";
            this.requestXml = requestXml.Replace("PARAM_MW_USERNAME", this.MW_USERNAME);
            this.requestXml = requestXml.Replace("PARAM_MW_PASSWORD", this.MW_PASSWORD);
            this.requestXml = requestXml.Replace("PARAM_MW_CONSUMER_ID", this.MW_CONSUMER_ID);
            this.requestXml = requestXml.Replace("PARAM_EXTERNAL_TRANSACTION_ID", DateTime.Now.ToString("yyyyMMddHs"));
            this.requestXml = requestXml.Replace("PARAM_CHANNEL", msisdn);
            this.requestXml = requestXml.Replace("PARAM_MFI_MSISDN", "MW_MFI_ACCOUNT");
            this.requestXml = requestXml.Replace("PARAM_CUSTOMER_PIN", pin);
            this.requestXml = requestXml.Replace("PARAM_AMOUNT", amount);

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


        public Dictionary<string, string> rollback(string sender, string amount, string transactionId, string comment)
        {
            Dictionary<string, string> results = new Dictionary<string, string>();

            this.url = "http://10.138.84.227:8001/osb/services/WalletManagement_2_0";

            this.requestXml = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:v2=\"http://xmlns.tigo.com/MFS/WalletManagementRequest/V2\" xmlns:v3=\"http://xmlns.tigo.com/RequestHeader/V3\" xmlns:v21=\"http://xmlns.tigo.com/ParameterType/V2\" xmlns:cor=\"http://soa.mic.co.af/coredata_1\">\r\n   <soapenv:Header xmlns:wsse=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd\">\r\n      <cor:debugFlag>true</cor:debugFlag>\r\n      <wsse:Security>\r\n         <wsse:UsernameToken>\r\n            <wsse:Username>PARAM_MW_USERNAME</wsse:Username>\r\n            <wsse:Password>PARAM_MW_PASSWORD</wsse:Password>\r\n         </wsse:UsernameToken>\r\n      </wsse:Security>\r\n   </soapenv:Header>\r\n   <soapenv:Body>\r\n      <v2:CashinRequest>\r\n         <v3:RequestHeader>\r\n            <v3:GeneralConsumerInformation>\r\n               <v3:consumerID>PARAM_MW_CONSUMER_ID</v3:consumerID>\r\n               <!--Optional:-->\r\n               <v3:transactionID>PARAM_EXTERNAL_TRANSACTION_ID</v3:transactionID>\r\n               <v3:country>RWA</v3:country>\r\n               <v3:correlationID>222</v3:correlationID>\r\n            </v3:GeneralConsumerInformation>\r\n         </v3:RequestHeader>\r\n         <v2:requestBody>\r\n            <v2:agentWallet>\r\n               <!--You have a CHOICE of the next 2 items at this level-->\r\n               <v2:msisdn>PARAM_MFI_MSISDN</v2:msisdn>\r\n            </v2:agentWallet>\r\n            <!--Optional:-->\r\n            <v2:password>PARAM_MFI_PIN</v2:password>\r\n            <v2:subscriberWallet>\r\n               <!--You have a CHOICE of the next 2 items at this level-->\r\n               <v2:msisdn>PARAM_CHANNEL</v2:msisdn>\r\n            </v2:subscriberWallet>\r\n            <v2:amount>PARAM_AMOUNT</v2:amount>\r\n         <v2:additionalParameters>\r\n   <v21:ParameterType>\r\n                <v21:parameterName>Remarks</v21:parameterName>\r\n                <v21:parameterValue>PARAM_REMARK</v21:parameterValue>\r\n            </v21:ParameterType>\r\n            <!--Zero or more repetitions:-->\r\n            <v21:ParameterType>\r\n                <v21:parameterName>ShortCode</v21:parameterName>\r\n                <v21:parameterValue>80064</v21:parameterValue>\r\n            </v21:ParameterType>\r\n        </v2:additionalParameters>\r\n         </v2:requestBody>\r\n      </v2:CashinRequest>\r\n   </soapenv:Body>\r\n</soapenv:Envelope>";
            this.requestXml = requestXml.Replace("PARAM_MW_USERNAME", this.MW_USERNAME);
            this.requestXml = requestXml.Replace("PARAM_MW_PASSWORD", this.MW_PASSWORD);
            this.requestXml = requestXml.Replace("PARAM_MW_CONSUMER_ID", this.MW_CONSUMER_ID);
            this.requestXml = requestXml.Replace("PARAM_CHANNEL", sender);
            this.requestXml = requestXml.Replace("PARAM_MFI_MSISDN", "CASHPARTNER_MSISDN");
            this.requestXml = requestXml.Replace("PARAM_MFI_PIN", "CASHPARATNER_PIN");
            this.requestXml = requestXml.Replace("PARAM_AMOUNT", amount);
            this.requestXml = requestXml.Replace("PARAM_REMARK", comment);
            this.requestXml = requestXml.Replace("PARAM_EXTERNAL_TRANSACTION_ID", transactionId);

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

        /**
          * This method cleans the response obtained from MW
          */
        public override Dictionary<string, string> cleanResponse(string xmlString)
        {
            // Extract the information we need only
            Dictionary<string, string> response = new Dictionary<string, string>();
            response.Add("code", this.getStringBetweenTags(xmlString, "code"));
            response.Add("status", this.getStringBetweenTags(xmlString, "status"));
            response.Add("description", this.getStringBetweenTags(xmlString, "description"));
            response.Add("transactionId", this.getStringBetweenTags(xmlString, "transactionId"));
            return response;
        }
    }
}