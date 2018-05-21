using PAM.Models;
using PAM.Repositories;
using PAM.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PAM.Factory
{
    public class Proccess
    {
        public string purchasing(SessionModel session)
        {
            WalletManagement wm = new WalletManagement();
            LogsRepository logs = new LogsRepository();
            TransactionRepository tnx = new TransactionRepository();
            AirtimeProvision ap = new AirtimeProvision();

            string transactionId = DateTime.Now.ToString("yyyyMMddHs");

            string epin_tnx_id = string.Empty;
            Dictionary<string, string> results_epin;
            string message = string.Empty;
            Double comm_fee = 0;
            Double comm_amount = 0;
            Double total_amount = 0;

            string sender_message = string.Empty;
            string receiver_message = string.Empty;

            string rollback_tc_message = string.Empty;
            string rollback_epin_message = string.Empty;

            try
            {

                // Debit the sender
                Dictionary<string, string> results = wm.pay(session.sender, session.amount, session.pin);

                //If the status is not 0  we consider this as failed transactions
                if (!results["status"].Equals("OK"))
                    logs.Save(session.sender, session.receiver, "Trying to charge  the sender on MFS", results["description"]);
                return "Ntago ubashije kurangura kubera ikibazo kiri kuri Tigocash";

                tnx.Save(session.sender, session.receiver, "TC", results["transactionId"], "", double.Parse(session.amount), 0, 0, true);


                //comm_fee = CommissionModel.getCommission();
                comm_fee = 0;
                comm_amount = Math.Ceiling((Double.Parse(session.amount) * comm_fee));
                total_amount = Double.Parse(session.amount) + comm_amount;

                results_epin = ap.addStock(session.receiver, total_amount.ToString(), results["transactionId"]);


                if (!results_epin.ContainsKey("status") || !results_epin["status"].ToLower().Equals("ok"))
                {
                    //Do rollback
                    Dictionary<string, string> rollback_results = wm.rollback(session.sender, session.amount, results["transactionId"], "ROLLBACK TC");

                    tnx.Save(session.sender, session.receiver, "ROLLBACK TC", rollback_results["transactionId"], "", double.Parse(session.amount), 0, 0, true);

                    string msg = string.Join(", ", results_epin.Select(m => m.Key + ":" + m.Value).ToArray());
                    logs.Save(session.sender, session.receiver, "Trying to provision the receiver on PRETUPS", msg);


                    // Send notification
                    rollback_tc_message = "Ntago ubashije kurangura kubera ikibazo kiri kuri Epin usubijwe amafaranga yawe  " + session.amount.ToString() + ",Murakoze";
                    //(new SendNotification()).SendSms(sessionValues.sender, rollback_tc_message);
                    return rollback_tc_message;
                }

                tnx.Save(session.sender, session.receiver, "EPIN", results["transactionId"], "", double.Parse(session.amount), comm_amount, total_amount, true);

                ////// SEND NOTIFICATIONS

                ////Sender

                sender_message = "Amafaranga uranguye angana " + total_amount.ToString() + " yoherejwe neza kuri numero " + session.receiver + ",Murakoze";
                //(new SendNotification()).SendSms(session.sender, sender_message);

                ////Receiver

                receiver_message = "Wakiriye amafaranga angana " + total_amount.ToString() + " avuye kuri numero " + session.sender + ",Murakoze";
                //(new SendNotification()).SendSms(session.receiver, receiver_message);

                return "Waguze neza Epin ukoresheje Tigocash.You bought successfully Epin using Tigocash";

            }
            catch (Exception ex)
            {
                // log and return the error message 
                logs.Save(session.sender, session.receiver, "Trying to provision the receiver on PRETUPS", ex.Message);
                return ex.Message;
            }

        }
    }
}