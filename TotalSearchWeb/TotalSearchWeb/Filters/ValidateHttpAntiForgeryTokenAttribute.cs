using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;

using TotalSearchWeb.Models;

namespace TotalSearchWeb.Filters
{
    public class ValidateHttpAntiForgeryTokenAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            HttpRequestMessage request = actionContext.ControllerContext.Request;

            try
            {
                if (IsAjaxRequest(request))
                {
                    ValidateRequestHeader(request);
                }
                else
                {
                    AntiForgery.Validate();
                }
            }
            catch (HttpAntiForgeryException e)
            {
                actionContext.Response = request.CreateErrorResponse(HttpStatusCode.Forbidden, e);
            }
        }

        private bool IsAjaxRequest(HttpRequestMessage request)
        {
            IEnumerable<string> xRequestedWithHeaders;
            if (request.Headers.TryGetValues("X-Requested-With", out xRequestedWithHeaders))
            {
                string headerValue = xRequestedWithHeaders.FirstOrDefault();
                if (!String.IsNullOrEmpty(headerValue))
                {
                    return String.Equals(headerValue, "XMLHttpRequest", StringComparison.OrdinalIgnoreCase);
                }
            }

            return false;
        }

        private void ValidateRequestHeader(HttpRequestMessage request)
        {
            string cookieToken = String.Empty;
            string formToken = String.Empty;

            IEnumerable<string> tokenHeaders;
            if (request.Headers.TryGetValues("RequestVerificationToken", out tokenHeaders))
            {
                string tokenValue = tokenHeaders.FirstOrDefault();
                if (!String.IsNullOrEmpty(tokenValue))
                {
                    string[] tokens = tokenValue.Split(':');
                    if (tokens.Length == 2)
                    {
                        cookieToken = tokens[0].Trim();
                        formToken = tokens[1].Trim();
                    }
                }
            }

            AntiForgery.Validate(cookieToken, formToken);
        }
    }



    public class ValidateAddLocationPasswordAttribute : AuthorizationFilterAttribute
    {

        private string CalculateSHA256Hash(string input, string password)
        {
            // Encode the input string into a byte array.
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] inputPasswordBytes = Encoding.UTF8.GetBytes(password);

            byte[] newBytes = inputBytes.Concat(inputPasswordBytes).ToArray();
            // Create an instance of the SHA256 algorithm class
            // and use it to calculate the hash.
            SHA256Managed sha256 = new SHA256Managed();

            byte[] outputBytes = sha256.ComputeHash(newBytes);
            // Convert the outputed hash to a string and return it.
            return Convert.ToBase64String(outputBytes);
        }


        public override void OnAuthorization(HttpActionContext actionContext)
        {
            HttpRequestMessage request = actionContext.ControllerContext.Request;

            IEnumerable<string> tokenHeaders;
            if (request.Headers.TryGetValues("AddLocationPassword", out tokenHeaders))
            {
                string tokenValue = tokenHeaders.FirstOrDefault();
                if (!string.IsNullOrEmpty(tokenValue))
                {
                    using (var DataLayer = new DataLayerDataContext())
                    {
                        Access password = DataLayer.Accesses.FirstOrDefault(item => item.name == "AddLocationPassword");

                        if (password != null)
                        {
                            if (tokenValue != CalculateSHA256Hash(request.Content.ReadAsStringAsync().Result, password.value))
                            {
                                actionContext.Response = request.CreateErrorResponse(HttpStatusCode.Forbidden, "Password invalide");
                            }
                        }
                        else
                        {
                            actionContext.Response = request.CreateErrorResponse(HttpStatusCode.Forbidden, "Pas de Password en base");
                        }
                    }
                }
                else
                {
                    actionContext.Response = request.CreateErrorResponse(HttpStatusCode.Forbidden, "Password vide");
                }
            }
            else
            {
                actionContext.Response = request.CreateErrorResponse(HttpStatusCode.Forbidden,"Pas de Password");
            }
        }
    }
}