using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml;

namespace PAM.Services
{
    public class MiddleWareApi
    {
        // This repository is for logging the request and responses
        // Get MW CONFIGURATIONS
        public string MW_USERNAME = Properties.Settings.Default.MW_USERNAME;
        public string MW_PASSWORD = Properties.Settings.Default.MW_PASSWORD;
        public string MW_CONSUMER_ID = Properties.Settings.Default.MW_CONSUMER_ID;

        //the URL is on the MiddleWare API
        public string url = null;

        //go find the raw XML
        public string requestXml = null;

        /**
         * Method to send request to MW
         * 
         */
        protected string sendRequest()
        {
            string responseXmlString;

            Dictionary<string, string> unkonwnException = new Dictionary<string, string>();
            // The url has the API NAME,therefore  we can use it to extract the 
            // Api name which is positioned at 39
            // The url  looks like "http://10.138.84.138:8002/osb/services/SendNotification_1_0";

            string apiName = this.url.Substring(39);

            //load the request XML 
            XmlDocument requestXML = new XmlDocument();
            requestXML.LoadXml(this.requestXml);

            //Create the Web request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.url);

            //set the properties
            request.Method = "POST";
            request.ContentType = "text/xml";
            request.Timeout = 30 * 1000;

            //open the pipe?
            Stream request_stream = request.GetRequestStream();

            //write the XML to the open pipe (e.g. stream)
            requestXML.Save(request_stream);

            //CLOSE THE PIPE !!! Very important or next step will time out!!!!
            request_stream.Close();

            // Clean the request xml and remove sensitive information before we log anything 
            // To the database
            this.requestXml = this.removeSensitiveInformation(this.requestXml);

            try
            {
                //get the response from the webservice
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream r_stream = response.GetResponseStream();

                //convert it
                StreamReader response_stream = new StreamReader(r_stream, System.Text.Encoding.GetEncoding("utf-8"));
                responseXmlString = response_stream.ReadToEnd().ToString();

                //clean up!
                response_stream.Close();


                return responseXmlString;

            }
            catch (ProtocolViolationException exception) // Protocol Error
            {
                unkonwnException.Add("code", "500");
                unkonwnException.Add("status", "ERROR");
                unkonwnException.Add("description", exception.Message);

                return exception.Message;
            }
            catch (UriFormatException exception) // URI Format Error
            {
                unkonwnException.Add("code", "500");
                unkonwnException.Add("status", "ERROR");
                unkonwnException.Add("description", exception.Message);


                return exception.Message;
            }
            catch (NotSupportedException exception) // Unknown Protocol Error
            {
                unkonwnException.Add("code", "500");
                unkonwnException.Add("status", "ERROR");
                unkonwnException.Add("description", exception.Message);

                return exception.Message;
            }
            catch (IOException exception) // I/O  Error
            {
                unkonwnException.Add("code", "500");
                unkonwnException.Add("status", "ERROR");
                unkonwnException.Add("description", exception.Message);

                return exception.Message;
            }
            catch (WebException exception) // MiddleWare send us Network Error: error from API
            {

                if (exception.Response != null)
                {
                    responseXmlString = new StreamReader(exception.Response.GetResponseStream()).ReadToEnd();
                }
                else
                {
                    responseXmlString = exception.Message;
                }

                return responseXmlString;
            }
            catch (Exception exception) // We could not determine this error, therefore consider it as a generic
            {

                unkonwnException.Add("code", "500");
                unkonwnException.Add("status", "ERROR");
                unkonwnException.Add("description", exception.Message);

                return exception.Message;
            }
        }

        /**
         * This method cleans the response obtained from MW
         */
        public virtual Dictionary<string, string> cleanResponse(string xmlString)
        {
            // Extract the information we need only
            Dictionary<string, string> response = new Dictionary<string, string>();
            response.Add("code", this.getStringBetweenTags(xmlString, "code"));
            response.Add("status", this.getStringBetweenTags(xmlString, "status"));
            response.Add("description", this.getStringBetweenTags(xmlString, "description"));

            return response;
        }
        /**
         * This method helps to get string between a tag
         */
        protected string getStringBetweenTags(string value, string tagName, int nth = 1)
        {
            int startingPoint;
            if (value.Contains(tagName) && value.Contains(tagName))
            {
                if (nth > 1)
                {
                    startingPoint = IndexOfNth(value, tagName, nth);
                    value = value.Substring(startingPoint);
                }
                else
                {
                    var match = Regex.Match(value, @"<([\w]+)[^>]" + tagName + @">(.|\n)*?<\/([\w]+)[^>]" + tagName + ">");

                    if (match.Success) // If we find a match then return the match
                    {
                        value = match.Value.ToString();

                    }
                }

                startingPoint = IndexOfNth(value, tagName, nth) + tagName.Length + 1; // We add one to make sure > is added 
                value = value.Substring(startingPoint);                               // Remove the left part of the string
                startingPoint = value.IndexOf("</");                                  // Determine the index of where the tag ends
                value = value.Substring(0, startingPoint);                            // Get the extact string between these two tag
                return value;
            }

            return null;
        }
        /**
         * Get index of nth occurrence of char in a string
         */
        private static int IndexOfNth(string str, string c, int n)
        {
            int s = -1;

            for (int i = 0; i < n; i++)
            {
                s = str.IndexOf(c, s + 1);

                if (s == -1) break;
            }

            return s;
        }

        /**
         * Make sure sensitive information are removed from this string
         */
        protected string removeSensitiveInformation(string parameter)
        {
            string TigoCashPin = this.getStringBetweenTags(parameter, "password");
            string MiddleWarePassword = this.getStringBetweenTags(parameter, "Password");

            if (!string.IsNullOrEmpty(parameter))
            {
                // Remove TIGO CASH PIN if any 
                if (!string.IsNullOrEmpty(TigoCashPin) && parameter.Contains(TigoCashPin))
                {
                    parameter = parameter.Replace(TigoCashPin, "HIDDEN_PIN");
                }

                // Remove MIDDELWARE PASSWORD IF any
                if (!string.IsNullOrEmpty(MiddleWarePassword) && parameter.Contains(MiddleWarePassword))
                {
                    parameter = parameter.Replace(MiddleWarePassword, "HIDDEN_MW_PASSWORD");
                }
            }
            return parameter;
        }
    }
}