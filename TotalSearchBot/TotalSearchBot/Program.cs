using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Threading;
using System.Security.Cryptography;
using System.Configuration;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


namespace TotalSearchBot
{
    class Program
    {

        private static string password = TotalSearchBot.Properties.Settings.Default.AddLocationPassword;
        private static string url = TotalSearchBot.Properties.Settings.Default.AddLocationUrl;

        static void Main(string[] args)
        {

            //string adresse = "סמטת המעיין 2, ירושלים";
            //decimal[] Locations = GeoService.GetGeolocation(adresse);

            //if (Locations != null)
            //{
            //    Console.WriteLine("Lattitude : " + Locations[0]);
            //    Console.WriteLine("Longitude : " + Locations[1]);
            //}

            //RobotWinWin(1,3,null);

            //RobotYad2();

            Thread ThreadWinWin1 = new Thread(() => RobotWinWinWebApi(1, 2, "8b563b8886a7e123b6821964549140c9"));

            //Thread ThreadWinWin1 = new Thread(() => RobotWinWin(1, 2, "8b563b8886a7e123b6821964549140c9"));
            //Thread ThreadWinWin2 = new Thread(() => RobotWinWin(3, 4, "8b563b8886a7e123b6821964549140c9"));

            //Thread ThreadYad2_1 = new Thread(() => RobotYad2(1, 2, "1178"));
            //Thread ThreadYad2_2 = new Thread(() => RobotYad2(3, 4, "1178"));


            //ThreadYad2_1.Start();
            //ThreadYad2_2.Start();

            ThreadWinWin1.Start();
            //ThreadWinWin2.Start();


            //ThreadWinWin1.Join();
            //ThreadWinWin2.Join();

        }


        private static string CalculateSHA256Hash(string input, string password)
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

        public static void RobotWinWinWebApi(int? StartPage, int? EndPage, string CityId)
        {


            if (StartPage == null)
            {
                StartPage = 0;
            }
            if (EndPage == null)
            {
                EndPage = 1000;
            }

            if (CityId == null)
            {
                CityId = "";
            }
            else
            {
                CityId = "&search=" + CityId;
            }

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("AddLocationPassword", "");

            

            //using (DataLayerDataContext Db = new DataLayerDataContext())
            //{

                //FirefoxProfile profile = new FirefoxProfile();
                //profile.SetPreference("general.useragent.override", "Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16");
                //IWebDriver driver = new FirefoxDriver(profile);


                //profile.SetPreference("general.useragent.override", "Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16");
                IWebDriver driver = new ChromeDriver();

                //int page = StartPage;

                for (int page = (int)StartPage; page < (EndPage + 1); page++)
                {

                    //Navigate to the site
                    driver.Navigate().GoToUrl("http://www.winwin.co.il/RealEstate/ForRent/Search/SearchResults/RealEstatePage.aspx?PageNumberBottom=" + page.ToString() + "&PageNumberTop=" + page.ToString() + CityId);

                    try
                    {
                        driver.FindElement(By.XPath("//img[@src=\"app_themes/default/images/HP/Error/ErrorMessage.jpg\"]"));
                        break;
                    }
                    catch (NoSuchElementException)
                    {
                        //page++;
                    }


                    ///app_themes/default/images/HP/Error/ErrorMessage.jpg

                    IList<IWebElement> ResultsNodes = driver.FindElements(By.Id("savedAddsTable"));
                    if (ResultsNodes != null && ResultsNodes.Count > 0)
                    {
                        IWebElement TableAdds = ResultsNodes[0];

                        IList<IWebElement> DetailNodes = TableAdds.FindElements(By.CssSelector("tr.paid , tr.TitleData"));
                        if (DetailNodes != null && DetailNodes.Count > 0)
                        {
                            for (int i = 0; i < DetailNodes.Count; i++)
                            {
                                IWebElement node = DetailNodes[i];
                                string id = node.GetAttribute("id");
                                //bool create = false;


                                //direct link : http://www.winwin.co.il/RealEstate/ForRent/Ads/RealEstateAds,4088105.aspx


                                string diraId = Regex.Replace(id, @"[^0-9]+", "").Trim();

                                //Location location = Db.Locations.FirstOrDefault(item => (item.sourceid == diraId &&
                                //                                                            item.sourcename == "winwin"));
                                //if (location == null)
                                //{
                                //    location = new Location();
                                //    create = true;
                                //}
                                Location location = new Location();

                                location.sourceid = diraId;

                                location.sourcename = "winwin";

                                string adresse = "";

                                IWebElement AdresseNode = node.FindElements(By.TagName("td"))[5];
                                if (AdresseNode != null && AdresseNode.Text != null)
                                {
                                    location.adresse = AdresseNode.Text.Trim();
                                    adresse = AdresseNode.Text.Trim();
                                }

                                IWebElement DateNode = node.FindElements(By.TagName("td"))[9];
                                if (DateNode != null && DateNode.Text != null)
                                {
                                    location.date = DateTime.Parse(DateNode.Text.Replace(".", "/") + "/2013");
                                }

                                node.Click();

                                if (id != null)
                                {
                                    IWebElement DetailNode2 = driver.FindElement(By.CssSelector("div#" + id.Replace("Open", "") + "Div"));
                                    if (DetailNode2 != null)
                                    {
                                        //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                                        //IWebElement myDynamicElement = wait.Until<IWebElement>((d) =>
                                        //{
                                        //    return d.FindElement(By.CssSelector("div#" + id.Replace("Open", "") + "Div div.ColContainer"));
                                        //});

                                        WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(10)); // wait for max of 5 seconds
                                        wait2.Until(ExpectedConditions.ElementExists(By.CssSelector("div#" + id.Replace("Open", "") + "Div div.ColContainer")));


                                        IWebElement ImageElement = driver.FindElements(By.CssSelector("div#" + id.Replace("Open", "") + "Div div.ColContainer"))[1];
                                        if (ImageElement != null)
                                        {
                                            IWebElement NodeImage1 = ImageElement.FindElements(By.CssSelector("div.Col"))[0];

                                            if (NodeImage1 != null)
                                            {
                                                string src = NodeImage1.FindElement(By.CssSelector("div.GalleryImgContainer img")).GetAttribute("src");
                                                if (src != null && !src.Contains("NoImage"))
                                                {
                                                    location.urlimage = src;
                                                }

                                            }

                                        }


                                        IWebElement myDynamicElement = driver.FindElement(By.CssSelector("div#" + id.Replace("Open", "") + "Div div.ColContainer"));


                                        IWebElement NodeGeneral1 = myDynamicElement.FindElements(By.CssSelector("div.Col"))[0];

                                        IList<IWebElement> DetailNodes3 = NodeGeneral1.FindElements(By.CssSelector("div.Row , div.RowPrice"));
                                        if (DetailNodes3 != null && DetailNodes3.Count > 0)
                                        {
                                            foreach (IWebElement node1 in DetailNodes3)
                                            {
                                                IWebElement NodeKey = node1.FindElement(By.CssSelector("div.LabelText"));
                                                IWebElement NodeValue = node1.FindElement(By.CssSelector("div.Data , div.PriceData"));

                                                if (NodeKey != null && NodeValue != null)
                                                {
                                                    if (NodeValue.Text != "" && NodeValue.Text.Trim() != "-")
                                                    {

                                                        if (NodeKey.Text.Contains("סוג הנכס"))
                                                        {
                                                            location.typebien = NodeValue.Text.Trim();
                                                        }
                                                        else if (NodeKey.Text.Contains("תת אזור"))
                                                        {
                                                            location.zone = NodeValue.Text.Trim();
                                                        }
                                                        else if (NodeKey.Text.Contains("עיר"))
                                                        {
                                                            location.ville = NodeValue.Text.Trim();

                                                            if (NodeValue.Text.Trim() != "")
                                                            {
                                                                decimal[] Locations = GeoService.GetGeolocation(adresse + ", " + NodeValue.Text.Trim());

                                                                if (Locations != null && Locations.Length >= 2)
                                                                {
                                                                    location.latitude = Locations[0];
                                                                    location.longitude = Locations[1];
                                                                }
                                                            }
                                                        }
                                                        else if (NodeKey.Text.Contains("כניסה"))
                                                        {
                                                            if (NodeValue.Text.Contains("מיידי"))
                                                            {
                                                                location.entreeimmediate = true;
                                                            }
                                                            else if (NodeValue.Text.Contains("גמיש"))
                                                            {
                                                                location.entreeimmediate = true;
                                                            }
                                                            else
                                                            {
                                                                string Knissa = NodeValue.Text.Trim();
                                                                DateTime date = DateTime.Parse(Knissa);
                                                                location.dateentree = date;
                                                            }
                                                        }
                                                        else if (NodeKey.Text.Contains("שכונה"))
                                                        {
                                                            location.quartier = NodeValue.Text.Trim();
                                                        }
                                                        else if (NodeKey.Text.Contains("מס' מרפסות"))
                                                        {
                                                            location.nombrebalcon = int.Parse(NodeValue.Text.Trim());
                                                        }
                                                        else if (NodeKey.Text.Contains("לשותפים"))
                                                        {
                                                            location.plusieursresidents = NodeValue.Text.Contains("לא") ? false : true;
                                                        }
                                                        else if (NodeKey.Text.Contains("חדרים"))
                                                        {
                                                            location.nombrechambre = decimal.Parse(NodeValue.Text.Trim());
                                                        }
                                                        else if (NodeKey.Text.Contains("שטח"))
                                                        {
                                                            string superficie = Regex.Replace(NodeValue.Text, @"[^0-9]+", "").Trim();
                                                            location.superficie = int.Parse(superficie);
                                                        }
                                                        else if (NodeKey.Text.Contains("מחיר"))
                                                        {
                                                            if (NodeValue != null && !NodeValue.Text.Contains("לא צויין"))
                                                            {
                                                                string Price = NodeValue.Text.Trim();
                                                                Price = Regex.Replace(Price, @"[^0-9]+", "");
                                                                location.prix = decimal.Parse(Price);
                                                            }
                                                        }
                                                        else if (NodeKey.Text.Contains("תשלומים"))
                                                        {
                                                            string Tachloumim = NodeValue.Text.Trim();
                                                            location.nombreversement = int.Parse(Tachloumim);
                                                        }
                                                        else if (NodeKey.Text.Contains("קומה"))
                                                        {
                                                            if (NodeValue.Text.Contains("מתוך") && !NodeValue.Text.Contains("קרקע"))
                                                            {
                                                                string etage = NodeValue.Text.Replace("\r\n", " ");
                                                                etage = Regex.Replace(etage, @"[^0-9\. ]+", "").Trim();
                                                                string etagenumero = etage.Substring(0, etage.IndexOf(" ")).Trim();
                                                                string etagetotal = etage.Substring(etage.IndexOf(" ")).Trim();

                                                                location.etage = decimal.Parse(etagenumero);
                                                                location.nombreetage = int.Parse(etagetotal);
                                                            }
                                                            else
                                                            {
                                                                if (NodeValue.Text.Contains("קרקע"))
                                                                {
                                                                    location.etage = 0;
                                                                    if (NodeValue.Text.Contains("מתוך"))
                                                                    {
                                                                        string etagetotal = Regex.Replace(NodeValue.Text, @"[^0-9]+", "").Trim();
                                                                        location.nombreetage = int.Parse(etagetotal);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    location.etage = decimal.Parse(NodeValue.Text.Trim());
                                                                }
                                                            }

                                                        }


                                                    }

                                                    if (NodeKey.Text.Contains("מרפסת"))
                                                    {
                                                        string src = NodeValue.FindElement(By.XPath("img")).GetAttribute("src");

                                                        if (src != null)
                                                        {
                                                            if (src.Contains("BlackV"))
                                                            {
                                                                location.balcon = true;
                                                            }
                                                            else
                                                            {
                                                                location.balcon = false;
                                                            }
                                                        }

                                                    }

                                                    else if (NodeKey.Text.Contains("מזגן"))
                                                    {
                                                        string src = NodeValue.FindElement(By.XPath("img")).GetAttribute("src");

                                                        if (src != null)
                                                        {
                                                            if (src.Contains("BlackV"))
                                                            {
                                                                location.climatisation = true;
                                                            }
                                                            else
                                                            {
                                                                location.climatisation = false;
                                                            }
                                                        }

                                                    }

                                                    else if (NodeKey.Text.Contains("חניה"))
                                                    {
                                                        string src = NodeValue.FindElement(By.XPath("img")).GetAttribute("src");

                                                        if (src != null)
                                                        {
                                                            if (src.Contains("BlackV"))
                                                            {
                                                                location.garage = true;
                                                            }
                                                            else
                                                            {
                                                                location.garage = false;
                                                            }
                                                        }

                                                    }

                                                    else if (NodeKey.Text.Contains("ממ"))
                                                    {
                                                        string src = NodeValue.FindElement(By.XPath("img")).GetAttribute("src");

                                                        if (src != null)
                                                        {
                                                            if (src.Contains("BlackV"))
                                                            {
                                                                location.chambreforte = true;
                                                            }
                                                            else
                                                            {
                                                                location.chambreforte = false;
                                                            }
                                                        }

                                                    }

                                                    else if (NodeKey.Text.Contains("מעלית"))
                                                    {

                                                        string src = NodeValue.FindElement(By.XPath("img")).GetAttribute("src");

                                                        if (src != null)
                                                        {
                                                            if (src.Contains("BlackV"))
                                                            {
                                                                location.ascenseur = true;
                                                            }
                                                            else
                                                            {
                                                                location.ascenseur = false;
                                                            }
                                                        }

                                                    }

                                                    else if (NodeKey.Text.Contains("גישה לנכים"))
                                                    {
                                                        string src = NodeValue.FindElement(By.XPath("img")).GetAttribute("src");

                                                        if (src != null)
                                                        {
                                                            if (src.Contains("BlackV"))
                                                            {
                                                                location.acceshandicape = true;
                                                            }
                                                            else
                                                            {
                                                                location.acceshandicape = false;
                                                            }
                                                        }

                                                    }

                                                    else if (NodeKey.Text.Contains("ריהוט"))
                                                    {
                                                        string src = NodeValue.FindElement(By.XPath("img")).GetAttribute("src");

                                                        if (src != null)
                                                        {
                                                            if (src.Contains("BlackV"))
                                                            {
                                                                location.meublee = true;
                                                            }
                                                            else
                                                            {
                                                                location.meublee = false;
                                                            }
                                                        }

                                                    }

                                                }

                                            }
                                        }




                                        //IWebElement NodeGeneral2 = myDynamicElement.FindElements(By.CssSelector("div.Col"))[0];

                                        //IList<IWebElement> DetailNodes4 = NodeGeneral2.FindElements(By.CssSelector("div.Row"));
                                        //if (DetailNodes4 != null && DetailNodes4.Count > 0)
                                        //{
                                        //    foreach (IWebElement node1 in DetailNodes4)
                                        //    {
                                        //        IWebElement NodeKey = node1.FindElement(By.CssSelector("div.LabelText"));
                                        //        IWebElement NodeValue = node1.FindElement(By.CssSelector("div.Data"));

                                        //        if (NodeKey != null && NodeValue != null)
                                        //        {

                                        //            if (NodeKey.Text.Contains("מרפסת"))
                                        //            {
                                        //                string src = NodeValue.FindElement(By.XPath("img")).GetAttribute("src");

                                        //                if (src != null)
                                        //                {
                                        //                    if (src.Contains("BlackV"))
                                        //                    {
                                        //                        location.balcon = true;
                                        //                    }
                                        //                    else
                                        //                    {
                                        //                        location.balcon = false;
                                        //                    }
                                        //                }

                                        //            }

                                        //            else if (NodeKey.Text.Contains("מזגן"))
                                        //            {
                                        //                string src = NodeValue.FindElement(By.XPath("img")).GetAttribute("src");

                                        //                if (src != null)
                                        //                {
                                        //                    if (src.Contains("BlackV"))
                                        //                    {
                                        //                        location.climatisation = true;
                                        //                    }
                                        //                    else
                                        //                    {
                                        //                        location.climatisation = false;
                                        //                    }
                                        //                }

                                        //            }

                                        //            else if (NodeKey.Text.Contains("חניה"))
                                        //            {
                                        //                string src = NodeValue.FindElement(By.XPath("img")).GetAttribute("src");

                                        //                if (src != null)
                                        //                {
                                        //                    if (src.Contains("BlackV"))
                                        //                    {
                                        //                        location.garage = true;
                                        //                    }
                                        //                    else
                                        //                    {
                                        //                        location.garage = false;
                                        //                    }
                                        //                }

                                        //            }

                                        //            else if (NodeKey.Text.Contains("ממ"))
                                        //            {
                                        //                string src = NodeValue.FindElement(By.XPath("img")).GetAttribute("src");

                                        //                if (src != null)
                                        //                {
                                        //                    if (src.Contains("BlackV"))
                                        //                    {
                                        //                        location.chambreforte = true;
                                        //                    }
                                        //                    else
                                        //                    {
                                        //                        location.chambreforte = false;
                                        //                    }
                                        //                }

                                        //            }

                                        //            else if (NodeKey.Text.Contains("מעלית"))
                                        //            {

                                        //                string src = NodeValue.FindElement(By.XPath("img")).GetAttribute("src");

                                        //                if (src != null)
                                        //                {
                                        //                    if (src.Contains("BlackV"))
                                        //                    {
                                        //                        location.ascenseur = true;
                                        //                    }
                                        //                    else
                                        //                    {
                                        //                        location.ascenseur = false;
                                        //                    }
                                        //                }

                                        //            }

                                        //            else if (NodeKey.Text.Contains("גישה לנכים"))
                                        //            {
                                        //                string src = NodeValue.FindElement(By.XPath("img")).GetAttribute("src");

                                        //                if (src != null)
                                        //                {
                                        //                    if (src.Contains("BlackV"))
                                        //                    {
                                        //                        location.acceshandicape = true;
                                        //                    }
                                        //                    else
                                        //                    {
                                        //                        location.acceshandicape = false;
                                        //                    }
                                        //                }

                                        //            }

                                        //            else if (NodeKey.Text.Contains("ריהוט"))
                                        //            {
                                        //                string src = NodeValue.FindElement(By.XPath("img")).GetAttribute("src");

                                        //                if (src != null)
                                        //                {
                                        //                    if (src.Contains("BlackV"))
                                        //                    {
                                        //                        location.meublee = true;
                                        //                    }
                                        //                    else
                                        //                    {
                                        //                        location.meublee = false;
                                        //                    }
                                        //                }

                                        //            }


                                        //        }



                                        //    }
                                        //}
                                        //IWebElement DetailNode3 = DetailNode2.FindElement(By.XPath("div"));
                                    }

                                }

                                //if (!Db.Locations.Any(item => (item.sourcename == "yad2"
                                //                            && item.typebien == location.typebien
                                //                            && item.ville == location.ville
                                //                            && item.quartier == location.quartier
                                //                            && item.nombrechambre == location.nombrechambre
                                //                            && item.adresse == location.adresse
                                //                            && item.etage == location.etage
                                //                            && (item.superficie == location.superficie || (item.superficie == null && location.superficie == null))
                                //                            && (item.prix == location.prix || (item.prix == null && location.prix == null))
                                //                            && item.entreeimmediate == location.entreeimmediate
                                //                            && ((item.dateentree == location.dateentree) || (item.dateentree == null && location.dateentree == null))
                                //                            && item.garage == location.garage
                                //                            && item.climatisation == location.climatisation
                                //                            && item.chambreforte == location.chambreforte
                                //                            && item.ascenseur == location.ascenseur
                                //                            && item.balcon == location.balcon
                                //                            && item.acceshandicape == location.acceshandicape
                                //                            && item.meublee == location.meublee)))
                                //{


                                //if (create)
                                //{
                                //    Db.Locations.InsertOnSubmit(location);
                                //}
                                //Db.SubmitChanges();

                                //}

                                try
                                {
                                    MediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter();
                                    // Use the JSON formatter to create the content of the request body.
                                    HttpContent content = new ObjectContent<Location>(location, jsonFormatter);


                                    client.DefaultRequestHeaders.Remove("AddLocationPassword");
                                    client.DefaultRequestHeaders.Add("AddLocationPassword", CalculateSHA256Hash(content.ReadAsStringAsync().Result, password));

                                    HttpResponseMessage response = client.PostAsJsonAsync("api/LocationManager", location).Result;
                                    if (!response.IsSuccessStatusCode)
                                    {
                                        Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                                    }
                                }
                                catch (Exception ex)
                                {

                                }
                                



                            }
                        }

                    }
                }

                driver.Close();

                driver.Quit();

            //}
        }


        public static void RobotWinWin(int? StartPage, int? EndPage, string CityId)
        {


            if (StartPage == null)
            {
                StartPage = 0;
            }
            if (EndPage == null)
            {
                EndPage = 1000;
            }

            if (CityId == null)
            {
                CityId = "";
            }
            else
            {
                CityId = "&search=" + CityId;
            }

          
            using (DataLayerDataContext Db = new DataLayerDataContext())
            {

                //FirefoxProfile profile = new FirefoxProfile();
                //profile.SetPreference("general.useragent.override", "Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16");
                //IWebDriver driver = new FirefoxDriver(profile);

                
                //profile.SetPreference("general.useragent.override", "Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16");
                IWebDriver driver = new ChromeDriver();

                //int page = StartPage;

                for (int page = (int)StartPage; page < (EndPage + 1); page++)
                {

                    //Navigate to the site
                    driver.Navigate().GoToUrl("http://www.winwin.co.il/RealEstate/ForRent/Search/SearchResults/RealEstatePage.aspx?PageNumberBottom=" + page.ToString() + "&PageNumberTop=" + page.ToString() + CityId);

                    try
                    {
                        driver.FindElement(By.XPath("//img[@src=\"app_themes/default/images/HP/Error/ErrorMessage.jpg\"]"));
                        break;
                    }
                    catch (NoSuchElementException)
                    {
                        //page++;
                    }


                    ///app_themes/default/images/HP/Error/ErrorMessage.jpg

                    IList<IWebElement> ResultsNodes = driver.FindElements(By.Id("savedAddsTable"));
                    if (ResultsNodes != null && ResultsNodes.Count > 0)
                    {
                        IWebElement TableAdds = ResultsNodes[0];

                        IList<IWebElement> DetailNodes = TableAdds.FindElements(By.CssSelector("tr.paid , tr.TitleData"));
                        if (DetailNodes != null && DetailNodes.Count > 0)
                        {
                            for (int i = 0; i < DetailNodes.Count; i++)
                            {
                                IWebElement node = DetailNodes[i];
                                string id = node.GetAttribute("id");
                                bool create = false;


                                //direct link : http://www.winwin.co.il/RealEstate/ForRent/Ads/RealEstateAds,4088105.aspx


                                string diraId = Regex.Replace(id, @"[^0-9]+", "").Trim();

                                Location location = Db.Locations.FirstOrDefault(item => (item.sourceid == diraId &&
                                                                                            item.sourcename == "winwin"));
                                if (location == null)
                                {
                                   location = new Location();
                                   create = true;
                                }

                                location.sourceid = diraId;

                                location.sourcename = "winwin";

                                string adresse = "";

                                IWebElement AdresseNode = node.FindElements(By.TagName("td"))[5];
                                if (AdresseNode != null && AdresseNode.Text != null)
                                {
                                    location.adresse = AdresseNode.Text.Trim();
                                    adresse = AdresseNode.Text.Trim();
                                }

                                IWebElement DateNode = node.FindElements(By.TagName("td"))[9];
                                if (DateNode != null && DateNode.Text != null)
                                {
                                    location.date = DateTime.Parse(DateNode.Text.Replace(".", "/") + "/2013");
                                }

                                node.Click();

                                if (id != null)
                                {
                                    IWebElement DetailNode2 = driver.FindElement(By.CssSelector("div#" + id.Replace("Open", "") + "Div"));
                                    if (DetailNode2 != null)
                                    {
                                        //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                                        //IWebElement myDynamicElement = wait.Until<IWebElement>((d) =>
                                        //{
                                        //    return d.FindElement(By.CssSelector("div#" + id.Replace("Open", "") + "Div div.ColContainer"));
                                        //});

                                        WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(10)); // wait for max of 5 seconds
                                        wait2.Until(ExpectedConditions.ElementExists(By.CssSelector("div#" + id.Replace("Open", "") + "Div div.ColContainer")));


                                        IWebElement ImageElement = driver.FindElements(By.CssSelector("div#" + id.Replace("Open", "") + "Div div.ColContainer"))[1];
                                        if (ImageElement != null)
                                        {
                                            IWebElement NodeImage1 = ImageElement.FindElements(By.CssSelector("div.Col"))[0];

                                            if (NodeImage1 != null)
                                            {
                                                string src = NodeImage1.FindElement(By.CssSelector("div.GalleryImgContainer img")).GetAttribute("src");
                                                if (src != null && !src.Contains("NoImage"))
                                                {
                                                    location.urlimage = src;
                                                }

                                            }

                                        }


                                        IWebElement myDynamicElement = driver.FindElement(By.CssSelector("div#" + id.Replace("Open", "") + "Div div.ColContainer"));


                                        IWebElement NodeGeneral1 = myDynamicElement.FindElements(By.CssSelector("div.Col"))[0];

                                        IList<IWebElement> DetailNodes3 = NodeGeneral1.FindElements(By.CssSelector("div.Row , div.RowPrice"));
                                        if (DetailNodes3 != null && DetailNodes3.Count > 0)
                                        {
                                            foreach (IWebElement node1 in DetailNodes3)
                                            {
                                                IWebElement NodeKey = node1.FindElement(By.CssSelector("div.LabelText"));
                                                IWebElement NodeValue = node1.FindElement(By.CssSelector("div.Data , div.PriceData"));

                                                if (NodeKey != null && NodeValue != null)
                                                {
                                                    if (NodeValue.Text != "" && NodeValue.Text.Trim() != "-")
                                                    {

                                                        if (NodeKey.Text.Contains("סוג הנכס"))
                                                        {
                                                            location.typebien = NodeValue.Text.Trim();
                                                        }
                                                        else if (NodeKey.Text.Contains("תת אזור"))
                                                        {
                                                            location.zone = NodeValue.Text.Trim();
                                                        }
                                                        else if (NodeKey.Text.Contains("עיר"))
                                                        {
                                                            location.ville = NodeValue.Text.Trim();

                                                            if (NodeValue.Text.Trim() != "")
                                                            {
                                                                decimal[] Locations = GeoService.GetGeolocation(adresse + ", " + NodeValue.Text.Trim());

                                                                if (Locations != null && Locations.Length >= 2)
                                                                {
                                                                    location.latitude = Locations[0];
                                                                    location.longitude = Locations[1];
                                                                }
                                                            }
                                                        }
                                                        else if (NodeKey.Text.Contains("כניסה"))
                                                        {
                                                            if (NodeValue.Text.Contains("מיידי"))
                                                            {
                                                                location.entreeimmediate = true;
                                                            }
                                                            else if (NodeValue.Text.Contains("גמיש"))
                                                            {
                                                                location.entreeimmediate = true;
                                                            }
                                                            else
                                                            {
                                                                string Knissa = NodeValue.Text.Trim();
                                                                DateTime date = DateTime.Parse(Knissa);
                                                                location.dateentree = date;
                                                            }
                                                        }
                                                        else if (NodeKey.Text.Contains("שכונה"))
                                                        {
                                                            location.quartier = NodeValue.Text.Trim();
                                                        }
                                                        else if (NodeKey.Text.Contains("מס' מרפסות"))
                                                        {
                                                            location.nombrebalcon = int.Parse(NodeValue.Text.Trim());
                                                        }
                                                        else if (NodeKey.Text.Contains("לשותפים"))
                                                        {
                                                            location.plusieursresidents = NodeValue.Text.Contains("לא") ? false : true;
                                                        }
                                                        else if (NodeKey.Text.Contains("חדרים"))
                                                        {
                                                            location.nombrechambre = decimal.Parse(NodeValue.Text.Trim());
                                                        }
                                                        else if (NodeKey.Text.Contains("שטח"))
                                                        {
                                                            string superficie = Regex.Replace(NodeValue.Text, @"[^0-9]+", "").Trim();
                                                            location.superficie = int.Parse(superficie);
                                                        }
                                                        else if (NodeKey.Text.Contains("מחיר"))
                                                        {
                                                            if (NodeValue != null && !NodeValue.Text.Contains("לא צויין"))
                                                            {
                                                                string Price = NodeValue.Text.Trim();
                                                                Price = Regex.Replace(Price, @"[^0-9]+", "");
                                                                location.prix = decimal.Parse(Price);
                                                            }
                                                        }
                                                        else if (NodeKey.Text.Contains("תשלומים"))
                                                        {
                                                            string Tachloumim = NodeValue.Text.Trim();
                                                            location.nombreversement = int.Parse(Tachloumim);
                                                        }
                                                        else if (NodeKey.Text.Contains("קומה"))
                                                        {
                                                            if (NodeValue.Text.Contains("מתוך") && !NodeValue.Text.Contains("קרקע"))
                                                            {
                                                                string etage = NodeValue.Text.Replace("\r\n", " ");
                                                                etage = Regex.Replace(etage, @"[^0-9\. ]+", "").Trim();
                                                                string etagenumero = etage.Substring(0, etage.IndexOf(" ")).Trim();
                                                                string etagetotal = etage.Substring(etage.IndexOf(" ")).Trim();

                                                                location.etage = decimal.Parse(etagenumero);
                                                                location.nombreetage = int.Parse(etagetotal);
                                                            }
                                                            else
                                                            {
                                                                if (NodeValue.Text.Contains("קרקע"))
                                                                {
                                                                    location.etage = 0;
                                                                    if (NodeValue.Text.Contains("מתוך"))
                                                                    {
                                                                        string etagetotal = Regex.Replace(NodeValue.Text, @"[^0-9]+", "").Trim();
                                                                        location.nombreetage = int.Parse(etagetotal);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    location.etage = decimal.Parse(NodeValue.Text.Trim());
                                                                }
                                                            }

                                                        }


                                                    }

                                                    if (NodeKey.Text.Contains("מרפסת"))
                                                    {
                                                        string src = NodeValue.FindElement(By.XPath("img")).GetAttribute("src");

                                                        if (src != null)
                                                        {
                                                            if (src.Contains("BlackV"))
                                                            {
                                                                location.balcon = true;
                                                            }
                                                            else
                                                            {
                                                                location.balcon = false;
                                                            }
                                                        }

                                                    }

                                                    else if (NodeKey.Text.Contains("מזגן"))
                                                    {
                                                        string src = NodeValue.FindElement(By.XPath("img")).GetAttribute("src");

                                                        if (src != null)
                                                        {
                                                            if (src.Contains("BlackV"))
                                                            {
                                                                location.climatisation = true;
                                                            }
                                                            else
                                                            {
                                                                location.climatisation = false;
                                                            }
                                                        }

                                                    }

                                                    else if (NodeKey.Text.Contains("חניה"))
                                                    {
                                                        string src = NodeValue.FindElement(By.XPath("img")).GetAttribute("src");

                                                        if (src != null)
                                                        {
                                                            if (src.Contains("BlackV"))
                                                            {
                                                                location.garage = true;
                                                            }
                                                            else
                                                            {
                                                                location.garage = false;
                                                            }
                                                        }

                                                    }

                                                    else if (NodeKey.Text.Contains("ממ"))
                                                    {
                                                        string src = NodeValue.FindElement(By.XPath("img")).GetAttribute("src");

                                                        if (src != null)
                                                        {
                                                            if (src.Contains("BlackV"))
                                                            {
                                                                location.chambreforte = true;
                                                            }
                                                            else
                                                            {
                                                                location.chambreforte = false;
                                                            }
                                                        }

                                                    }

                                                    else if (NodeKey.Text.Contains("מעלית"))
                                                    {

                                                        string src = NodeValue.FindElement(By.XPath("img")).GetAttribute("src");

                                                        if (src != null)
                                                        {
                                                            if (src.Contains("BlackV"))
                                                            {
                                                                location.ascenseur = true;
                                                            }
                                                            else
                                                            {
                                                                location.ascenseur = false;
                                                            }
                                                        }

                                                    }

                                                    else if (NodeKey.Text.Contains("גישה לנכים"))
                                                    {
                                                        string src = NodeValue.FindElement(By.XPath("img")).GetAttribute("src");

                                                        if (src != null)
                                                        {
                                                            if (src.Contains("BlackV"))
                                                            {
                                                                location.acceshandicape = true;
                                                            }
                                                            else
                                                            {
                                                                location.acceshandicape = false;
                                                            }
                                                        }

                                                    }

                                                    else if (NodeKey.Text.Contains("ריהוט"))
                                                    {
                                                        string src = NodeValue.FindElement(By.XPath("img")).GetAttribute("src");

                                                        if (src != null)
                                                        {
                                                            if (src.Contains("BlackV"))
                                                            {
                                                                location.meublee = true;
                                                            }
                                                            else
                                                            {
                                                                location.meublee = false;
                                                            }
                                                        }

                                                    }

                                                }

                                            }
                                        }




                                        //IWebElement NodeGeneral2 = myDynamicElement.FindElements(By.CssSelector("div.Col"))[0];

                                        //IList<IWebElement> DetailNodes4 = NodeGeneral2.FindElements(By.CssSelector("div.Row"));
                                        //if (DetailNodes4 != null && DetailNodes4.Count > 0)
                                        //{
                                        //    foreach (IWebElement node1 in DetailNodes4)
                                        //    {
                                        //        IWebElement NodeKey = node1.FindElement(By.CssSelector("div.LabelText"));
                                        //        IWebElement NodeValue = node1.FindElement(By.CssSelector("div.Data"));

                                        //        if (NodeKey != null && NodeValue != null)
                                        //        {
                                                    
                                        //            if (NodeKey.Text.Contains("מרפסת"))
                                        //            {
                                        //                string src = NodeValue.FindElement(By.XPath("img")).GetAttribute("src");

                                        //                if (src != null)
                                        //                {
                                        //                    if (src.Contains("BlackV"))
                                        //                    {
                                        //                        location.balcon = true;
                                        //                    }
                                        //                    else
                                        //                    {
                                        //                        location.balcon = false;
                                        //                    }
                                        //                }

                                        //            }

                                        //            else if (NodeKey.Text.Contains("מזגן"))
                                        //            {
                                        //                string src = NodeValue.FindElement(By.XPath("img")).GetAttribute("src");

                                        //                if (src != null)
                                        //                {
                                        //                    if (src.Contains("BlackV"))
                                        //                    {
                                        //                        location.climatisation = true;
                                        //                    }
                                        //                    else
                                        //                    {
                                        //                        location.climatisation = false;
                                        //                    }
                                        //                }

                                        //            }

                                        //            else if (NodeKey.Text.Contains("חניה"))
                                        //            {
                                        //                string src = NodeValue.FindElement(By.XPath("img")).GetAttribute("src");
                                                        
                                        //                if (src != null)
                                        //                {
                                        //                    if (src.Contains("BlackV"))
                                        //                    {
                                        //                        location.garage = true;
                                        //                    }
                                        //                    else
                                        //                    {
                                        //                        location.garage = false;
                                        //                    }
                                        //                }

                                        //            }

                                        //            else if (NodeKey.Text.Contains("ממ"))
                                        //            {
                                        //                string src = NodeValue.FindElement(By.XPath("img")).GetAttribute("src");

                                        //                if (src != null)
                                        //                {
                                        //                    if (src.Contains("BlackV"))
                                        //                    {
                                        //                        location.chambreforte = true;
                                        //                    }
                                        //                    else
                                        //                    {
                                        //                        location.chambreforte = false;
                                        //                    }
                                        //                }

                                        //            }

                                        //            else if (NodeKey.Text.Contains("מעלית"))
                                        //            {

                                        //                string src = NodeValue.FindElement(By.XPath("img")).GetAttribute("src");
                                                        
                                        //                if (src != null)
                                        //                {
                                        //                    if (src.Contains("BlackV"))
                                        //                    {
                                        //                        location.ascenseur = true;
                                        //                    }
                                        //                    else
                                        //                    {
                                        //                        location.ascenseur = false;
                                        //                    }
                                        //                }

                                        //            }

                                        //            else if (NodeKey.Text.Contains("גישה לנכים"))
                                        //            {
                                        //                string src = NodeValue.FindElement(By.XPath("img")).GetAttribute("src");

                                        //                if (src != null)
                                        //                {
                                        //                    if (src.Contains("BlackV"))
                                        //                    {
                                        //                        location.acceshandicape = true;
                                        //                    }
                                        //                    else
                                        //                    {
                                        //                        location.acceshandicape = false;
                                        //                    }
                                        //                }

                                        //            }

                                        //            else if (NodeKey.Text.Contains("ריהוט"))
                                        //            {
                                        //                string src = NodeValue.FindElement(By.XPath("img")).GetAttribute("src");

                                        //                if (src != null)
                                        //                {
                                        //                    if (src.Contains("BlackV"))
                                        //                    {
                                        //                        location.meublee = true;
                                        //                    }
                                        //                    else
                                        //                    {
                                        //                        location.meublee = false;
                                        //                    }
                                        //                }

                                        //            }


                                        //        }



                                        //    }
                                        //}
                                        //IWebElement DetailNode3 = DetailNode2.FindElement(By.XPath("div"));
                                    }

                                }

                                if (!Db.Locations.Any(item => (item.sourcename == "yad2"
                                                            && item.typebien == location.typebien
                                                            && item.ville == location.ville
                                                            && item.quartier == location.quartier
                                                            && item.nombrechambre == location.nombrechambre
                                                            && item.adresse == location.adresse
                                                            && item.etage == location.etage
                                                            && (item.superficie == location.superficie || (item.superficie == null && location.superficie == null))
                                                            && (item.prix == location.prix || (item.prix == null && location.prix == null))
                                                            && item.entreeimmediate == location.entreeimmediate
                                                            && ((item.dateentree == location.dateentree) || (item.dateentree == null && location.dateentree == null))
                                                            && item.garage == location.garage
                                                            && item.climatisation == location.climatisation
                                                            && item.chambreforte == location.chambreforte
                                                            && item.ascenseur == location.ascenseur
                                                            && item.balcon == location.balcon
                                                            && item.acceshandicape == location.acceshandicape
                                                            && item.meublee == location.meublee)))
                                {


                                    if (create)
                                    {
                                        Db.Locations.InsertOnSubmit(location);
                                    }
                                    Db.SubmitChanges();

                                }


                            }
                        }

                    }
                }

                driver.Close();

                driver.Quit();

            }
        }


        public static void RobotYad2(int? StartPage, int? EndPage, string CityId)
        {
            ////IWebDriver driver = new FirefoxDriver();

            //FirefoxProfile profile = new FirefoxProfile();
            //profile.SetPreference("general.useragent.override", "Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16");
            //IWebDriver driver = new FirefoxDriver(profile);

            ////Navigate to the site
            //driver.Navigate().GoToUrl("http://www.google.fr");
            //// Find the text input element by its name
            //IWebElement query = driver.FindElement(By.Name("q"));
            //// Enter something to search for
            //query.SendKeys("Selenium");
            //// Now submit the form
            //query.Submit();
            //// Google's search is rendered dynamically with JavaScript.
            //// Wait for the page to load, timeout after 5 seconds
            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            //wait.Until((d) => { return d.Title.StartsWith("Selenium"); });

            ////Check that the Title is what we are expecting
            //Console.WriteLine(driver.Title);

            //driver.Quit();

            //var entities = new DataLayerDataContext();

            if (StartPage == null)
            {
                StartPage = 0;
            }
            if (EndPage == null)
            {
                EndPage = 1000;
            }
            if (CityId == null)
            {
                CityId = "";
            }
            else
            {
                CityId = "&CityID=" + CityId;
            }

            using (DataLayerDataContext Db = new DataLayerDataContext())
            {


                FirefoxProfile profile = new FirefoxProfile();
                profile.SetPreference("general.useragent.override", "Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16");
                IWebDriver driver = new FirefoxDriver(profile);

                //Navigate to the site
                driver.Navigate().GoToUrl("http://m.yad2.co.il");

                //Console.WriteLine(driver.Title);
                // Find the text input element by its name
                //IWebElement query = driver.FindElement(By.Name("q"));
                // Enter something to search for
                //query.SendKeys("Selenium");
                // Now submit the form
                //query.Submit();
                //int page = 1;
                List<string> handles = null;

                for (int page = (int)StartPage; page < (EndPage + 1); page++)
                {

                    int count = 0;
                    handles = driver.WindowHandles.ToList<string>();

                    if (handles != null)
                    {
                        driver.SwitchTo().Window(handles.First());
                    }

                    driver.Navigate().GoToUrl("http://m.yad2.co.il/Nadlan/NadlanRents.php?Page=" + page.ToString() + CityId);

                    IWebElement NodeSearchResult = driver.FindElement(By.CssSelector("div.result-count"));
                    if (NodeSearchResult != null)
                    {
                        if (NodeSearchResult.Text.Contains("לא נמצאו תוצאות תואמות"))
                        {
                            break;
                        }
                        else
                        {
                            //page++;
                        }
                    }
                    //Console.WriteLine(driver.Title);



                    // Google's search is rendered dynamically with JavaScript.
                    // Wait for the page to load, timeout after 5 seconds
                    //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                    //Wait.Until((d) => { return d.Title.Contains("דירות להשכרה | יד2"); });

                    //Check that the Title is what we are expecting
                    //Console.WriteLine(driver.Title);



                    IList<IWebElement> ResultsNodes = driver.FindElements(By.CssSelector("div.ad-item-container > a"));
                    if (ResultsNodes != null)
                    {
                        foreach (IWebElement node1 in ResultsNodes)
                        {

                            if (count > 9)
                            {
                                break;
                            }

                            count++;


                            if (handles != null)
                            {
                                driver.SwitchTo().Window(handles.First());
                            }

                            string link = node1.GetAttribute("href");
                            if (link != "")
                            {
                                string url5 = link;

                                Uri Url = new Uri(url5);
                                string NadlanID = HttpUtility.ParseQueryString(Url.Query).Get("NadlanID");

                                IJavaScriptExecutor jscript = driver as IJavaScriptExecutor;
                                jscript.ExecuteScript("window.open()");

                                handles = driver.WindowHandles.ToList<string>();
                                driver.SwitchTo().Window(handles.Last());

                                Thread.Sleep(2000);

                                driver.Navigate().GoToUrl(url5);

                                bool create = false;

                                Location location = Db.Locations.FirstOrDefault(item => (item.sourceid == NadlanID &&
                                                                                            item.sourcename == "yad2"));

                                if (location == null)
                                {
                                    location = new Location();
                                    create = true;
                                }

                                location.sourceid = NadlanID;
                                location.sourcename = "yad2";

                                //direct link : http://www.yad2.co.il/Nadlan/rent_info.php?NadlanID=1753688

                                IList<IWebElement> NodeDetails1 = driver.FindElements(By.CssSelector("div.ad-details div.key-value , div.ad-details div.double-key-value"));
                                //IList<IWebElement> NodeDetails1 = driver.FindElements(By.XPath("//div[contains(concat(' ',normalize-space(@class),' '),' key-value ')] | //div[contains(concat(' ',normalize-space(@class),' '),' double-key-value ')]"));

                                string adresse = "";
                                string numero = "";
                                string ville = "";

                                if (NodeDetails1 != null)
                                {
                                    foreach (IWebElement nodedetail in NodeDetails1)
                                    {
                                        IWebElement NodeKey = nodedetail.FindElement(By.CssSelector("div.key"));
                                        IWebElement NodeValue = nodedetail.FindElement(By.CssSelector("div.value"));

                                        if (NodeKey != null && NodeValue != null)
                                        {
                                            if (NodeValue.Text != "" && !NodeValue.Text.Contains("לא צויין"))
                                            {
                                                if (NodeKey.Text.Contains("סוג הנכס"))
                                                {
                                                    location.typebien = NodeValue.Text.Trim();
                                                }
                                                else if (NodeKey.Text.Contains("איזור מכירה"))
                                                {
                                                    location.zone = NodeValue.Text.Trim();
                                                }
                                                else if (NodeKey.Text.Contains("עיר"))
                                                {
                                                    location.ville = NodeValue.Text.Trim();
                                                    ville = NodeValue.Text.Trim();
                                                }
                                                else if (NodeKey.Text.Contains("שכונה"))
                                                {
                                                    location.quartier = NodeValue.Text.Trim();
                                                }
                                                else if (NodeKey.Text.Contains("מס' מרפסות"))
                                                {
                                                    location.nombrebalcon = int.Parse(NodeValue.Text.Trim());
                                                }
                                                else if (NodeKey.Text.Contains("מתאימה לשותפים"))
                                                {
                                                    location.plusieursresidents = NodeValue.Text.Contains("לא") ? false : true;
                                                }
                                                else if (NodeKey.Text.Contains("כתובת"))
                                                {
                                                    //location.adresse = NodeValue.Text.Trim();
                                                    adresse = NodeValue.Text.Trim();
                                                }
                                                else if (NodeKey.Text.Contains("חדרים"))
                                                {
                                                    location.nombrechambre = decimal.Parse(NodeValue.Text.Trim());
                                                }
                                                else if (NodeKey.Text.Contains("גודל במ"))
                                                {
                                                    location.superficie = int.Parse(NodeValue.Text.Trim());
                                                }
                                                else if (NodeKey.Text.Contains("קומה"))
                                                {
                                                    if (NodeValue.Text.Contains("מתוך"))
                                                    {
                                                        string etage = Regex.Replace(NodeValue.Text, @"[^0-9\. ]+", "").Trim();
                                                        string etagenumero = etage.Substring(0, etage.IndexOf(" ")).Trim();
                                                        string etagetotal = etage.Substring(etage.IndexOf(" ")).Trim();

                                                        location.etage = decimal.Parse(etagenumero);
                                                        location.nombreetage = int.Parse(etagetotal);
                                                    }
                                                    else
                                                    {
                                                        if (NodeValue.Text.Contains("קרקע"))
                                                        {
                                                            location.etage = 0;
                                                        }
                                                        else
                                                        {
                                                            location.etage = decimal.Parse(NodeValue.Text.Trim());
                                                        }
                                                    }

                                                }
                                                else if (NodeKey.Text.Contains("מספר בית"))
                                                {
                                                    //location.numeromaison = NodeValue.Text.Trim();
                                                    numero = NodeValue.Text.Trim();
                                                }
                                            }

                                        }

                                    }

                                    if(adresse != "")
                                    {
                                        if(numero != "")
                                        {
                                            adresse = adresse + " " + numero;
                                        }

                                        location.adresse = adresse;

                                        if (ville != "")
                                        {
                                            decimal[] Locations = GeoService.GetGeolocation(adresse + ", " + ville);

                                            if (Locations != null && Locations.Length >= 2)
                                            {
                                                location.latitude = Locations[0];
                                                location.longitude = Locations[1];
                                            }
                                        }
                                    }

                                }

                                IWebElement NodePrice = driver.FindElement(By.CssSelector("div.price > div.text"));

                                if (NodePrice != null && !NodePrice.Text.Contains("לא צויין"))
                                {
                                    string Price = NodePrice.Text.Trim();
                                    Price = Regex.Replace(Price, @"[^0-9]+", "");
                                    location.prix = decimal.Parse(Price);
                                }

                                IList<IWebElement> KnissaNodes = driver.FindElements(By.CssSelector("div.ad-container div.bold-value"));

                                if (KnissaNodes != null)
                                {
                                    foreach (IWebElement KnissaNode in KnissaNodes)
                                    {
                                        if (KnissaNode.Text.Contains("כניסה") && !KnissaNode.Text.Contains("לא צויין"))
                                        {
                                            if (KnissaNode.Text.Contains("מיידית"))
                                            {
                                                location.entreeimmediate = true;
                                            }
                                            else
                                            {
                                                string Knissa = KnissaNode.Text.Trim();
                                                Knissa = Regex.Replace(Knissa, @"[^0-9\-]+", "");
                                                DateTime date = DateTime.Parse(Knissa);
                                                location.dateentree = date;
                                            }
                                        }
                                    }

                                }

                                IWebElement NodeDate = driver.FindElements(By.CssSelector("div.info-details > div > div > span"))[3];

                                if (NodeDate != null)
                                {
                                    string date = NodeDate.Text.Trim();
                                    DateTime Date = DateTime.Parse(date);
                                    location.date = Date;
                                }

                                IWebElement NodeImage = driver.FindElement(By.CssSelector("div.ad-image > div img"));

                                if (NodeImage != null)
                                {
                                    string Src = NodeImage.GetAttribute("src");
                                    if (Src != null && !Src.Contains("no_picture_yad2logo.jpg"))
                                    {
                                        location.urlimage = Src;
                                    }
                                }

                                IList<IWebElement> NodeOptions = driver.FindElements(By.CssSelector("div.ad-options > span"));

                                if (NodeOptions != null && NodeOptions.Count > 0)
                                {
                                    foreach (IWebElement NodeOption in NodeOptions)
                                    {

                                        string style = NodeOption.FindElement(By.XPath("span")).GetAttribute("style");

                                        if (NodeOption.Text.Contains("מיזוג אוויר"))
                                        {


                                            if (style != null)
                                            {
                                                if (style.Contains("left top"))
                                                {
                                                    location.climatisation = true;
                                                }
                                                else
                                                {
                                                    location.climatisation = false;
                                                }
                                            }

                                        }

                                        else if (NodeOption.Text.Contains("מרפסת שמש"))
                                        {


                                            if (style != null)
                                            {
                                                if (style.Contains("left top"))
                                                {
                                                    location.balconsoleil = true;
                                                }
                                                else
                                                {
                                                    location.balconsoleil = false;
                                                }
                                            }

                                        }

                                        else if (NodeOption.Text.Contains("מרפסת"))
                                        {


                                            if (style != null)
                                            {
                                                if (style.Contains("left top"))
                                                {
                                                    location.balcon = true;
                                                }
                                                else
                                                {
                                                    location.balcon = false;
                                                }
                                            }

                                        }

                                        else if (NodeOption.Text.Contains("מעלית"))
                                        {


                                            if (style != null)
                                            {
                                                if (style.Contains("left top"))
                                                {
                                                    location.ascenseur = true;
                                                }
                                                else
                                                {
                                                    location.ascenseur = false;
                                                }
                                            }

                                        }

                                        else if (NodeOption.Text.Contains("ממ"))
                                        {


                                            if (style != null)
                                            {
                                                if (style.Contains("left top"))
                                                {
                                                    location.chambreforte = true;
                                                }
                                                else
                                                {
                                                    location.chambreforte = false;
                                                }
                                            }

                                        }

                                        else if (NodeOption.Text.Contains("חניה"))
                                        {


                                            if (style != null)
                                            {
                                                if (style.Contains("left top"))
                                                {
                                                    location.garage = true;
                                                }
                                                else
                                                {
                                                    location.garage = false;
                                                }
                                            }

                                        }

                                        else if (NodeOption.Text.Contains("גישה לנכים"))
                                        {


                                            if (style != null)
                                            {
                                                if (style.Contains("left top"))
                                                {
                                                    location.acceshandicape = true;
                                                }
                                                else
                                                {
                                                    location.acceshandicape = false;
                                                }
                                            }

                                        }

                                        else if (NodeOption.Text.Contains("מחסן"))
                                        {


                                            if (style != null)
                                            {
                                                if (style.Contains("left top"))
                                                {
                                                    location.cave = true;
                                                }
                                                else
                                                {
                                                    location.cave = false;
                                                }
                                            }

                                        }

                                        else if (NodeOption.Text.Contains("סורגים"))
                                        {


                                            if (style != null)
                                            {
                                                if (style.Contains("left top"))
                                                {
                                                    location.bareaux = true;
                                                }
                                                else
                                                {
                                                    location.bareaux = false;
                                                }
                                            }

                                        }



                                        else if (NodeOption.Text.Contains("משופצת"))
                                        {


                                            if (style != null)
                                            {
                                                if (style.Contains("left top"))
                                                {
                                                    location.renovee = true;
                                                }
                                                else
                                                {
                                                    location.renovee = false;
                                                }
                                            }

                                        }

                                        else if (NodeOption.Text.Contains("מרוהטת"))
                                        {


                                            if (style != null)
                                            {
                                                if (style.Contains("left top"))
                                                {
                                                    location.meublee = true;
                                                }
                                                else
                                                {
                                                    location.meublee = false;
                                                }
                                            }

                                        }

                                        else if (NodeOption.Text.Contains("יחידת דיור"))
                                        {


                                            if (style != null)
                                            {
                                                if (style.Contains("left top"))
                                                {
                                                    location.unitelogement = true;
                                                }
                                                else
                                                {
                                                    location.unitelogement = false;
                                                }
                                            }

                                        }

                                        else if (NodeOption.Text.Contains("חיות מחמד"))
                                        {


                                            if (style != null)
                                            {
                                                if (style.Contains("left top"))
                                                {
                                                    location.animauxdomestique = true;
                                                }
                                                else
                                                {
                                                    location.animauxdomestique = false;
                                                }
                                            }

                                        }

                                    }

                                }
        

                                if (!Db.Locations.Any(item => (item.sourcename == "winwin"
                                                            && item.typebien == location.typebien
                                                            && item.ville == location.ville
                                                            && item.quartier == location.quartier
                                                            && item.nombrechambre == location.nombrechambre
                                                            && item.adresse == location.adresse
                                                            && item.etage == location.etage
                                                            && (item.superficie == location.superficie || (item.superficie == null && location.superficie == null))
                                                            && (item.prix == location.prix || (item.prix == null && location.prix == null))
                                                            && item.entreeimmediate == location.entreeimmediate
                                                            && ((item.dateentree == location.dateentree) || (item.dateentree == null && location.dateentree == null))
                                                            && item.garage == location.garage
                                                            && item.climatisation == location.climatisation
                                                            && item.chambreforte == location.chambreforte
                                                            && item.ascenseur == location.ascenseur
                                                            && item.balcon == location.balcon
                                                            && item.acceshandicape == location.acceshandicape
                                                            && item.meublee == location.meublee)))
                                {


                                    if (create)
                                    {
                                        Db.Locations.InsertOnSubmit(location);
                                    }
                                    Db.SubmitChanges();

                                }

                                driver.Close();
                            }

                        }
                    }



                }

                driver.Quit();
            }
        }
    
    }
}
