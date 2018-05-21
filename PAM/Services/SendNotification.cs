using System;
using System.Collections.Generic;

namespace PAM.Services
{
    public class SendNotification : MiddleWareApi
    {
        public string SendSMS(string to = "", string message = "")
        {
            this.url = "";

            this.requestXml = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:v1=\"http://xmlns.tigo.com/SendNotificationRequest/V1\" xmlns:v3=\"http://xmlns.tigo.com/RequestHeader/V3\" xmlns:v2=\"http://xmlns.tigo.com/ParameterType/V2\" xmlns:cor=\"http://soa.mic.co.af/coredata_1\">\r\n   <soapenv:Header xmlns:wsse=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd\">\r\n      <cor:debugFlag>true</cor:debugFlag>\r\n      <wsse:Security>\r\n         <wsse:UsernameToken>\r\n            <wsse:Username>PARAM_MW_USERNAME</wsse:Username>\r\n            <wsse:Password>PARAM_MW_PASSWORD</wsse:Password>\r\n         </wsse:UsernameToken>\r\n      </wsse:Security>\r\n   </soapenv:Header>\r\n   <soapenv:Body>\r\n      <v1:SendNotificationRequest>\r\n         <v3:RequestHeader>\r\n            <v3:GeneralConsumerInformation>\r\n               <!--Optional:-->\r\n               <v3:consumerID>PARAM_MW_CONSUMER_ID</v3:consumerID>\r\n               <!--Optional:-->\r\n               <v3:transactionID>345cyz</v3:transactionID>\r\n               <v3:country>RWA</v3:country>\r\n               <v3:correlationID>1234</v3:correlationID>\r\n            </v3:GeneralConsumerInformation>\r\n         </v3:RequestHeader>\r\n         <v1:RequestBody>\r\n            <v1:channelId>SMS</v1:channelId>\r\n            <v1:customerId>PARAM_MW_RECEIVER_MSISDN</v1:customerId>\r\n            <v1:message>PARAM_MW_MESSAGE</v1:message>\r\n           <!--Optional:-->\r\n            <v1:additionalParameters>\r\n               <v2:ParameterType>\r\n                  <v2:parameterName>smsShortCode</v2:parameterName>\r\n                  <v2:parameterValue>PARAM_MW_SHORTCODE</v2:parameterValue>\r\n               </v2:ParameterType>\r\n            </v1:additionalParameters>\r\n            <!--Optional:-->\r\n            <v1:reasonCode>110</v1:reasonCode>\r\n            <v1:externalTransactionId>1234</v1:externalTransactionId>\r\n            <!--Optional:-->\r\n            <v1:comment>Send Notification</v1:comment>\r\n         </v1:RequestBody>\r\n      </v1:SendNotificationRequest>\r\n   </soapenv:Body>\r\n</soapenv:Envelope>";
            this.requestXml = requestXml.Replace("PARAM_MW_USERNAME", this.MW_USERNAME);
            this.requestXml = requestXml.Replace("PARAM_MW_PASSWORD", this.MW_PASSWORD);
            this.requestXml = requestXml.Replace("PARAM_MW_CONSUMER_ID", this.MW_CONSUMER_ID);
            this.requestXml = requestXml.Replace("PARAM_MW_RECEIVER_MSISDN", to);
            this.requestXml = requestXml.Replace("PARAM_MW_MESSAGE", message);
            this.requestXml = requestXml.Replace("PARAM_MW_SHORTCODE", "SMS_FROM");
            this.requestXml = requestXml.Replace("PARAM_EXTERNAL_TRANSACTION_ID", DateTime.Now.ToString("yyyyMMddHis"));


            Dictionary<string, string> results = this.cleanResponse(this.sendRequest());

            return results["status"];
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
            response.Add("TransactionID", this.getStringBetweenTags(xmlString, "SOATransactionID"));
            return response;
        }
    }
}