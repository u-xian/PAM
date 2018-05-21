using PAM.Helpers;
using PAM.Models;
using PAM.Repositories;
using System;

namespace PAM.Validators
{
    public class Validator
    {
        public string validate(SessionModel session)
        {
            BlackListRepository bl = new BlackListRepository();
            WhiteListRepository wl = new WhiteListRepository();

            if (!Checkers.IsPhoneNumber(session.sender))
                return "Invalid phone number";

            if (!Checkers.IsPhoneNumber(session.receiver))
                return "Invalid phone number";


            if (Checkers.checkingInputs(session.sender))
                return Properties.Settings.Default.MESSAGE_INVALID_INPUT.Replace("%PARAM%", "MSISDN");

            if (Checkers.checkingInputs(session.receiver))
                return Properties.Settings.Default.MESSAGE_INVALID_INPUT.Replace("%PARAM%", "receiver");

            if (Checkers.checkingInputs(session.amount))
                return Properties.Settings.Default.MESSAGE_INVALID_INPUT.Replace("%PARAM%", "amount");

            if (Checkers.checkingInputs(session.pin))
                return Properties.Settings.Default.MESSAGE_INVALID_INPUT.Replace("%PARAM%", "pin");


            // Only validate blaclist 
            if (bl.CheckExists(session.sender))
                return Properties.Settings.Default.BLACK_LIST_MESSAGE;


            if (!wl.CheckExists(session.sender, 1))
                return Properties.Settings.Default.TC_SA_LIST_MESSAGE;


            if (!wl.CheckExists(session.receiver, 2))
                return Properties.Settings.Default.DL_TL_LIST_MESSAGE.Replace("%RECEIVER%", session.receiver);


            if (Convert.ToDecimal(session.amount) < Convert.ToDecimal(Properties.Settings.Default.PARAM_MINIMUM_AMOUNT))
                return Properties.Settings.Default.MESSAGE_MINIMUM_ALLOWED_AMOUNT.Replace("%AMOUNT%", Properties.Settings.Default.PARAM_MINIMUM_AMOUNT);

            return "ok";
        }
    }
}