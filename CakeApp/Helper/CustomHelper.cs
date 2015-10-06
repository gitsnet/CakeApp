using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CakeApp.Helper
{
    public static class CustomHelper
    {
        public static IHtmlString CustomValidationSummary(this HtmlHelper helper, string validationMessage = "")
        {
            string retVal = "";
            if (helper.ViewData.ModelState.IsValid)
                return new HtmlString("");
            retVal += "<div class=\"alert alert-danger alert-dismissible\" role=\"alert\">";
            retVal += "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button>";
            retVal += " <strong>";
            if (!String.IsNullOrEmpty(validationMessage))
                retVal += helper.Encode(validationMessage);
            retVal += "</strong>";

            foreach (var key in helper.ViewData.ModelState.Keys)
            {
                foreach (var err in helper.ViewData.ModelState[key].Errors)
                    retVal += "&nbsp; &nbsp;<span>" + helper.Encode(err.ErrorMessage) + "</span>";
            }
            retVal += "</div>";
            //retVal += "<script>setTimeout($(function(){$('.alert').hide('slow')},5000));</script>";


            return new HtmlString(retVal);
        }
        public static IHtmlString CustomMassage(this HtmlHelper helper, string message = "", string alertType = "")
        {
            string retVal = "";
            if (helper.ViewData.ModelState.IsValid)
                return new HtmlString("");
            retVal += "<div class=\"alert alert-" + alertType + " alert-dismissible\" role=\"alert\">";
            retVal += "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button>";
            retVal += " <strong>";
            if (!String.IsNullOrEmpty(message))
                retVal += helper.Encode(message);
            retVal += "</strong>";


            retVal += "&nbsp; &nbsp;<span>" + helper.Encode(message) + "</span>";

            retVal += "</div>";
            retVal += "<script>setTimeout($(function(){$('.alert').hide('slow')},5000)</script>";


            return new HtmlString(retVal);
        }
    }
}