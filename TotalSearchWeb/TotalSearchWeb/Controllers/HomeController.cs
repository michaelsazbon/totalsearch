using System;
using System.Linq;
using System.Web.Mvc;
using TotalSearchWeb.Models;
using PagedList;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Collections.Generic; //NOTE: use Nuget to reference PagedList
using System.Security.Cryptography;
using System.Text;
using System.IO;


namespace SearchFormResultPagingExample.Controllers
{

    public class HomeController : Controller
    {

        const int RecordsPerPage = 25;


        public ActionResult Map(SearchViewModel model)
        {
            if (!string.IsNullOrEmpty(model.SearchButton) || model.Page.HasValue)
            {

                var entities = new DataLayerDataContext();

                var results = entities.Locations.Where(
                    c =>
                        (c.longitude != null && c.latitude != null)
                        &&
                        (c.ville.StartsWith(model.ville) || (string.IsNullOrEmpty(model.ville)))
                        && (c.zone.StartsWith(model.zone) || (string.IsNullOrEmpty(model.zone)))
                        && (c.quartier.StartsWith(model.quartier) || (string.IsNullOrEmpty(model.quartier)))
                        && (c.renovee == model.renovee || (model.renovee == false))
                        && (c.acceshandicape == model.acceshandicape || (model.acceshandicape == false))
                        && (c.animauxdomestique == model.animauxdomestique || (model.animauxdomestique == false))
                        && (c.ascenseur == model.ascenseur || (model.ascenseur == false))
                        && (c.balcon == model.balcon || (model.balcon == false))
                        && (c.balconsoleil == model.balconsoleil || (model.balconsoleil == false))
                        && (c.bareaux == model.bareaux || (model.bareaux == false))
                        && (c.cave == model.cave || (model.cave == false))
                        && (c.chambreforte == model.chambreforte || (model.chambreforte == false))
                        && (c.climatisation == model.climatisation || (model.climatisation == false))
                        && (c.dateentree >= model.dateentree || (model.dateentree == null) || (model.dateentree <= DateTime.Today && c.entreeimmediate == true))
                        && (c.entreeimmediate == model.entreeimmediate || (model.entreeimmediate == false))
                        && (c.garage == model.garage || (model.garage == false))
                        && (c.meublee == model.meublee || (model.meublee == false))
                        && (c.nombrechambre >= model.nombrechambremin || (model.nombrechambremin == null))
                        && (c.nombrechambre <= model.nombrechambremax || (model.nombrechambremax == null))
                        && (c.etage >= model.etagemin || (model.etagemin == null))
                        && (c.etage <= model.etagemax || (model.etagemax == null))
                        && (c.prix >= model.prixmin || (model.prixmin == null))
                        && (c.prix <= model.prixmax || (model.prixmax == null))
                        && (c.plusieursresidents == model.plusieursresidents || (model.plusieursresidents == false))
                        && (c.typebien == model.typebien || (string.IsNullOrEmpty(model.typebien)))
                        && (c.superficie >= model.superficiemin || (model.superficiemin == null))
                        && (c.superficie <= model.superficiemax || (model.superficiemax == null))

                );

                var pageIndex = model.Page ?? 1;

                model.SearchResults = results.ToPagedList(pageIndex, 25);

            }

            return View(model);
        }


        //public ActionResult MobileMap(SearchViewModel model)
        //{
        //    if (!string.IsNullOrEmpty(model.SearchButton) || model.Page.HasValue)
        //    {

        //        var entities = new DataLayerDataContext();

        //        var results = entities.Locations.Where(
        //            c =>
        //                (c.longitude != null && c.latitude != null)
        //                &&
        //                (c.ville.StartsWith(model.ville) || (string.IsNullOrEmpty(model.ville)))
        //                && (c.zone.StartsWith(model.zone) || (string.IsNullOrEmpty(model.zone)))
        //                && (c.quartier.StartsWith(model.quartier) || (string.IsNullOrEmpty(model.quartier)))
        //                && (c.renovee == model.renovee || (model.renovee == false))
        //                && (c.acceshandicape == model.acceshandicape || (model.acceshandicape == false))
        //                && (c.animauxdomestique == model.animauxdomestique || (model.animauxdomestique == false))
        //                && (c.ascenseur == model.ascenseur || (model.ascenseur == false))
        //                && (c.balcon == model.balcon || (model.balcon == false))
        //                && (c.balconsoleil == model.balconsoleil || (model.balconsoleil == false))
        //                && (c.bareaux == model.bareaux || (model.bareaux == false))
        //                && (c.cave == model.cave || (model.cave == false))
        //                && (c.chambreforte == model.chambreforte || (model.chambreforte == false))
        //                && (c.climatisation == model.climatisation || (model.climatisation == false))
        //                && (c.dateentree >= model.dateentree || (model.dateentree == null) || (model.dateentree <= DateTime.Today && c.entreeimmediate == true))
        //                && (c.entreeimmediate == model.entreeimmediate || (model.entreeimmediate == false))
        //                && (c.garage == model.garage || (model.garage == false))
        //                && (c.meublee == model.meublee || (model.meublee == false))
        //                && (c.nombrechambre >= model.nombrechambremin || (model.nombrechambremin == null))
        //                && (c.nombrechambre <= model.nombrechambremax || (model.nombrechambremax == null))
        //                && (c.etage >= model.etagemin || (model.etagemin == null))
        //                && (c.etage <= model.etagemax || (model.etagemax == null))
        //                && (c.prix >= model.prixmin || (model.prixmin == null))
        //                && (c.prix <= model.prixmax || (model.prixmax == null))
        //                && (c.plusieursresidents == model.plusieursresidents || (model.plusieursresidents == false))
        //                && (c.typebien == model.typebien || (string.IsNullOrEmpty(model.typebien)))
        //                && (c.superficie >= model.superficiemin || (model.superficiemin == null))
        //                && (c.superficie <= model.superficiemax || (model.superficiemax == null))

        //        );

        //        var pageIndex = model.Page ?? 1;

        //        model.SearchResults = results.ToPagedList(pageIndex, 25);

        //    }

        //    return View(model);
        //}


        public ActionResult Index(SearchViewModel model)
        {

            if (!string.IsNullOrEmpty(model.SearchButton) || model.Page.HasValue)
            {

                var entities = new DataLayerDataContext();

                var results = entities.Locations.Where(
                    c => 
                        (c.ville.StartsWith(model.ville) || (model.ville == null))
                        && (c.zone.StartsWith(model.zone) || (model.zone == null))
                        && (c.quartier.StartsWith(model.quartier) || (model.quartier == null))
                        && (c.renovee == model.renovee || (model.renovee == false))
                        && (c.acceshandicape == model.acceshandicape || (model.acceshandicape == false))
                        && (c.animauxdomestique == model.animauxdomestique || (model.animauxdomestique == false))
                        && (c.ascenseur == model.ascenseur || (model.ascenseur == false))
                        && (c.balcon == model.balcon || (model.balcon == false))
                        && (c.balconsoleil == model.balconsoleil || (model.balconsoleil == false))
                        && (c.bareaux == model.bareaux || (model.bareaux == false))
                        && (c.cave == model.cave || (model.cave == false))
                        && (c.chambreforte == model.chambreforte || (model.chambreforte == false))
                        && (c.climatisation == model.climatisation || (model.climatisation == false))
                        && (c.dateentree >= model.dateentree || (model.dateentree == null) || (model.dateentree <= DateTime.Today && c.entreeimmediate == true))
                        && (c.entreeimmediate == model.entreeimmediate || (model.entreeimmediate == false))
                        && (c.garage == model.garage || (model.garage == false))
                        && (c.meublee == model.meublee || (model.meublee == false))
                        && (c.nombrechambre >= model.nombrechambremin || (model.nombrechambremin == null))
                        && (c.nombrechambre <= model.nombrechambremax || (model.nombrechambremax == null))
                        && (c.etage >= model.etagemin || (model.etagemin == null))
                        && (c.etage <= model.etagemax || (model.etagemax == null))
                        && (c.prix >= model.prixmin || (model.prixmin == null))
                        && (c.prix <= model.prixmax || (model.prixmax == null))
                        && (c.plusieursresidents == model.plusieursresidents || (model.plusieursresidents == false))
                        && (c.typebien == model.typebien || (model.typebien == null))
                        && (c.superficie >= model.superficiemin || (model.superficiemin == null))
                        && (c.superficie <= model.superficiemax || (model.superficiemax == null))

                ).OrderBy(item => item.appartementid);

                var pageIndex = model.Page ?? 1;

                model.SearchResults = results.ToPagedList(pageIndex, 25);

            }

            return View(model);

        }

        [AllowCrossSite]
        public string Hello()
        {
            return "Hello World !!!";
        }
        
        [KmlOutputAttribute]
        [ValidateAntiForgeryToken]
        public string LocationLayer(SearchViewModel model)
        {
//            string kml = @"<?xml version='1.0' encoding='UTF-8'?>
//                <kml xmlns='http://www.opengis.net/kml/2.2'>
//              <Placemark>
//                <name>My office</name>
//                <description>This is the location of my office.</description>
//                <Point>
//                  <coordinates>-122.087461,37.422069</coordinates>
//                </Point>
//              </Placemark>
//            </kml>";


            string kmlline = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            kmlline += "<kml xmlns=\"http://www.opengis.net/kml/2.2\">";

            if (!string.IsNullOrEmpty(model.SearchButton) || model.Page.HasValue)
            {

                using (var entities = new DataLayerDataContext())
                {

                    List<Location> Locations = entities.Locations.Where(c =>
                                            (c.longitude != null && c.latitude != null)
                                            &&
                                            (
                                                   (c.ville.StartsWith(model.ville) || (model.ville == null))
                                                && (c.zone.StartsWith(model.zone) || (model.zone == null))
                                                && (c.quartier.StartsWith(model.quartier) || (model.quartier == null))
                                                && (c.renovee == model.renovee || (model.renovee == false))
                                                && (c.acceshandicape == model.acceshandicape || (model.acceshandicape == false))
                                                && (c.animauxdomestique == model.animauxdomestique || (model.animauxdomestique == false))
                                                && (c.ascenseur == model.ascenseur || (model.ascenseur == false))
                                                && (c.balcon == model.balcon || (model.balcon == false))
                                                && (c.balconsoleil == model.balconsoleil || (model.balconsoleil == false))
                                                && (c.bareaux == model.bareaux || (model.bareaux == false))
                                                && (c.cave == model.cave || (model.cave == false))
                                                && (c.chambreforte == model.chambreforte || (model.chambreforte == false))
                                                && (c.climatisation == model.climatisation || (model.climatisation == false))
                                                && (c.dateentree >= model.dateentree || (model.dateentree == null) || (model.dateentree <= DateTime.Today && c.entreeimmediate == true))
                                                && (c.entreeimmediate == model.entreeimmediate || (model.entreeimmediate == false))
                                                && (c.garage == model.garage || (model.garage == false))
                                                && (c.meublee == model.meublee || (model.meublee == false))
                                                && (c.nombrechambre >= model.nombrechambremin || (model.nombrechambremin == null))
                                                && (c.nombrechambre <= model.nombrechambremax || (model.nombrechambremax == null))
                                                && (c.etage >= model.etagemin || (model.etagemin == null))
                                                && (c.etage <= model.etagemax || (model.etagemax == null))
                                                && (c.prix >= model.prixmin || (model.prixmin == null))
                                                && (c.prix <= model.prixmax || (model.prixmax == null))
                                                && (c.plusieursresidents == model.plusieursresidents || (model.plusieursresidents == false))
                                                && (c.typebien == model.typebien || (model.typebien == null))
                                                && (c.superficie >= model.superficiemin || (model.superficiemin == null))
                                                && (c.superficie <= model.superficiemax || (model.superficiemax == null))
                                            )
                                            ).OrderBy(item => item.appartementid).ToList();

                    var pageIndex = model.Page ?? 1;

                    Locations = Locations.ToPagedList(pageIndex, 25).ToList();

                    foreach (Location result in Locations)
                    {
                        kmlline += "<Placemark>";
                        //kmlline += "<name>" + result.appartementid + "</name>";
                        //kmlline += "<description>" + result.typebien + "," + result.adresse + "</description>";
                        kmlline += "<description><![CDATA[" +
                            "<table>" +
                                "<tr>" +
                                 "<td valign=\"top\" width=\"*\">";

                        if (result.urlimage != null)
                        {
                            kmlline += "<img style=\"float: right; width:140px; height:100px; padding: 10px 0 0 10px\" src=\"" + result.urlimage + "\" />";
                        }
                        kmlline += "<div id=\"locationdetails\">" +
                             "<table cellspacing=\"5\" cellspacing=\"5\">" +
                             "<tr>" +
                             "<td width=\"220\" style=\"line-height: 21px;\">" +
                           "<b> אזור :</b>" + result.zone + "<br />" +
                           "<b> עיר\\ישוב :</b>" + result.ville + "<br />" +
                           "<b> שכונה :</b>" + result.quartier + "<br />" +
                           "<b> כתובת :</b>" + result.numeromaison + " " + result.adresse + "<br />" +
                            "</td>" +
                            "<td width=\"220\">" +
                            "<b> סוג נכס :</b>" + result.typebien + "<br />" +
                           "<b> מס' חדרים :</b>" + string.Format("{0:0.#}", result.nombrechambre) + "<br />" +
                           "<b> קומה :</b>" + string.Format("{0:0.#}", result.etage) + "<br />" +
                            "<b>מ\"ר :</b>" + result.superficie + "<br />" +
                            "</td>" +
                             "<td width=\"220\">" +
                           "<b> מחיר :</b>" + string.Format("{0:0.00 ₪}", result.prix) + "<br />" +
                            "<b>מס' מרפסת :</b>" + result.nombrebalcon + "<br />" +
                            "<b>כניסה : </b>";
                        if (result.entreeimmediate == true)
                        {
                            kmlline += " מיידית   <br /> ";
                        }
                        else if (result.entreeflexible == true)
                        {
                            kmlline += " גמיש   <br /> ";
                        }
                        else
                        {
                            if (result.dateentree != null)
                            {
                                //string.Format("{0:dd/MM/yyyy}", result.dateentree);
                                kmlline += result.dateentree.ToString().Substring(0, 10);
                                //Html.DisplayFor(x => result.dateentree);
                            }
                            kmlline += "<br />";

                        }

                        kmlline += "<b>תאריך : </b>" + string.Format("{0:dd/MM/yyyy}", result.date) + " <br />" +

                        "</td>" +
                        "<td width=\"220\">" +
                        "<b>תשלומים :</b>" + result.nombreversement + "<br />" +
                        "<br />" +
                        "<br />" +
                        "<br />" +
                        "</td>" +
                        "</tr>" +
                        "</table>" +
                       "<div style=\"display: inline-block; width: 80px\">";

                        if (result.meublee == true)
                        {
                            kmlline += "<input id=\"meublee\" name=\"meublee\" disabled=\"disabled\" value=\"result.meublee\" type=\"checkbox\" checked=\"checked\" />";
                        }
                        else
                        {
                            kmlline += "<input id=\"meublee\" name=\"meublee\" disabled=\"disabled\" value=\"result.meublee\" type=\"checkbox\" />";
                        }
                        kmlline += "<b style=\"vertical-align:top\">מרוהטת</b>" +
                        "</div>" +
                        "<div style=\"display: inline-block; width: 60px\">";

                        if (result.garage == true)
                        {
                            kmlline += "<input id=\"garage\" name=\"garage\" disabled=\"disabled\" value=\"result.garage\" type=\"checkbox\" checked=\"checked\" />";
                        }
                        else
                        {
                            kmlline += "<input id=\"garage\" name=\"garage\" disabled=\"disabled\" value=\"result.garage\" type=\"checkbox\" />";
                        }
                        kmlline += "<b style=\"vertical-align:top\">חניה</b>" +
                        "</div>" +
                        "<div style=\"display: inline-block; width: 70px\">";

                        if (result.ascenseur == true)
                        {
                            kmlline += "<input id=\"ascenseur\" name=\"ascenseur\" disabled=\"disabled\" value=\"result.ascenseur\" type=\"checkbox\" checked=\"checked\" />";
                        }
                        else
                        {
                            kmlline += "<input id=\"ascenseur\" name=\"ascenseur\" disabled=\"disabled\" value=\"result.ascenseur\" type=\"checkbox\" />";
                        }
                        kmlline += "<b style=\"vertical-align:top\">מעלית</b>" +
                        "</div>" +
                        "<div style=\"display: inline-block; width: 70px\">";

                        if (result.bareaux == true)
                        {
                            kmlline += "<input id=\"bareaux\" name=\"bareaux\" disabled=\"disabled\" value=\"result.bareaux\" type=\"checkbox\" checked=\"checked\" />";
                        }
                        else
                        {
                            kmlline += "<input id=\"bareaux\" name=\"bareaux\" disabled=\"disabled\" value=\"result.bareaux\" type=\"checkbox\" />";
                        }
                        kmlline += "<b style=\"vertical-align:top\">סורגים</b>" +
                        "</div>" +
                        "<div style=\"display: inline-block; width: 90px\">";

                        if (result.climatisation == true)
                        {
                            kmlline += "<input id=\"climatisation\" name=\"climatisation\" disabled=\"disabled\" value=\"result.climatisation\" type=\"checkbox\" checked=\"checked\" />";
                        }
                        else
                        {
                            kmlline += "<input id=\"climatisation\" name=\"climatisation\" disabled=\"disabled\" value=\"result.climatisation\" type=\"checkbox\" />";
                        }
                        kmlline += "<b style=\"vertical-align:top\">מיזוג אוויר</b> " +
                        "</div>" +
                        "<div style=\"display: inline-block; width: 70px\">";

                        if (result.chambreforte == true)
                        {
                            kmlline += "<input id=\"chambreforte\" name=\"chambreforte\" disabled=\"disabled\" value=\"result.chambreforte\" type=\"checkbox\" checked=\"checked\" />";
                        }
                        else
                        {
                            kmlline += "<input id=\"chambreforte\" name=\"chambreforte\" disabled=\"disabled\" value=\"result.chambreforte\" type=\"checkbox\" />";
                        }
                        kmlline += "<b style=\"vertical-align:top\">ממ\"ד</b>" +
                        "</div>" +
                        "<div style=\"display: inline-block; width: 70px\">";
                        if (result.balcon == true)
                        {
                            kmlline += "<input id=\"balcon\" name=\"balcon\" disabled=\"disabled\" value=\"result.balcon\" type=\"checkbox\" checked=\"checked\" />";
                        }
                        else
                        {
                            kmlline += "<input id=\"balcon\" name=\"balcon\" disabled=\"disabled\" value=\"result.balcon\" type=\"checkbox\" />";
                        }
                        kmlline += "<b style=\"vertical-align:top\">מרפסת</b>" +
                        "</div>" +
                         "<div style=\"display: inline-block; width: 110px\">";

                        if (result.balconsoleil == true)
                        {
                            kmlline += "<input id=\"balconsoleil\" name=\"balconsoleil\" disabled=\"disabled\" value=\"result.balconsoleil\" type=\"checkbox\" checked=\"checked\" /> ";
                        }
                        else
                        {
                            kmlline += "<input id=\"balconsoleil\" name=\"balconsoleil\" disabled=\"disabled\" value=\"result.balconsoleil\" type=\"checkbox\" />";
                        }
                        kmlline += "<b style=\"vertical-align:top\">מרפסת שמש</b>" +
                        "</div>" +
                        "<div style=\"display: inline-block; width: 80px\">";

                        if (result.renovee == true)
                        {
                            kmlline += "<input id=\"renovee\" name=\"renovee\" disabled=\"disabled\" value=\"result.renovee\" type=\"checkbox\" checked=\"checked\" />";
                        }
                        else
                        {
                            kmlline += "<input id=\"renovee\" name=\"renovee\" disabled=\"disabled\" value=\"result.renovee\" type=\"checkbox\" />";
                        }
                        kmlline += "<b style=\"vertical-align:top\">משופצת</b>" +
                        "</div>" +
                        "<div style=\"display: inline-block; width: 70px\">";

                        if (result.cave == true)
                        {
                            kmlline += "<input id=\"cave\" name=\"cave\" disabled=\"disabled\" value=\"result.cave\" type=\"checkbox\" checked=\"checked\" />";
                        }
                        else
                        {
                            kmlline += "<input id=\"cave\" name=\"cave\" disabled=\"disabled\" value=\"result.cave\" type=\"checkbox\" />";
                        }
                        kmlline += "<b style=\"vertical-align:top\">מחסן</b>" +
                        "</div>" +
                        "<div style=\"display: inline-block; width: 100px\">";

                        if (result.acceshandicape == true)
                        {
                            kmlline += "<input id=\"acceshandicape\" name=\"acceshandicape\" disabled=\"disabled\" value=\"result.acceshandicape\" type=\"checkbox\" checked=\"checked\" />";
                        }
                        else
                        {
                            kmlline += "<input id=\"acceshandicape\" name=\"acceshandicape\" disabled=\"disabled\" value=\"result.acceshandicape\" type=\"checkbox\" />";
                        }
                        kmlline += "<b style=\"vertical-align:top\">גישה לנכים</b>" +
                        "</div>" +
                        "<div style=\"display: inline-block; width: 90px\">";

                        if (result.animauxdomestique == true)
                        {
                            kmlline += "<input id=\"animauxdomestique\" name=\"animauxdomestique\" disabled=\"disabled\" value=\"result.animauxdomestique\" type=\"checkbox\" checked=\"checked\" /> ";
                        }
                        else
                        {
                            kmlline += "<input id=\"animauxdomestique\" name=\"animauxdomestique\" disabled=\"disabled\" value=\"result.animauxdomestique\" type=\"checkbox\" />";
                        }
                        kmlline += "<b style=\"vertical-align:top\">חיות מחמד</b>" +
                        "</div>" +
                        "<div style=\"display: inline-block; width: 90px\">";

                        if (result.plusieursresidents == true)
                        {
                            kmlline += "<input id=\"plusieursresidents\" name=\"plusieursresidents\" disabled=\"disabled\" value=\"result.plusieursresidents\" type=\"checkbox\" checked=\"checked\" /> ";
                        }
                        else
                        {
                            kmlline += "<input id=\"plusieursresidents\" name=\"plusieursresidents\" disabled=\"disabled\" value=\"result.plusieursresidents\" type=\"checkbox\" />";
                        }
                        kmlline += "<b style=\"vertical-align:top\">מ.שותפים</b>" +
                        "</div>" +
                        "</div>" +
                        "</td>" +
                        "</tr>" +
                   "</table>]]></description>";
                        kmlline += "<Point><coordinates>" + result.longitude.ToString() + "," + result.latitude.ToString() + "</coordinates></Point>";
                        kmlline += "</Placemark>";
                    }

                    //XElement root = new XElement("root");
                    //root.Add(new XElement("element1"));
                    //root.Add(new XElement("element2"));
                    //root.Add(new XAttribute("attribute1", "a value"));
                    //return new XmlResult(kml);

                }

            }
            kmlline += "</kml>";

            //Response.ContentType = "text/xml";
            //Response.ContentType = "application/vnd.google-earth.kml+xml";
            //Response.Charset = "UTF-8";

            //string EncryptedKML = Encryptage.EncryptString("totot roorg rogrogro greog gor g", "2e35f242a46d67eeb74aabc37d5e5d05", "2547851236965624");
            //string DecryptedKML = Encryptage.DecryptString(EncryptedKML, "2e35f242a46d67eeb74aabc37d5e5d05", "2547851236965624");

            //return EncryptedKML;
            return kmlline;
            //return kml;
        }

    }

    public class Encryptage
    {

        public static string EncryptString(string clearText, string strKey, string strIv)
        {

            // Place le texte à chiffrer dans un tableau d'octets
            byte[] plainText = Encoding.UTF8.GetBytes(clearText);

            // Place la clé de chiffrement dans un tableau d'octets
            byte[] key = Encoding.UTF8.GetBytes(strKey);

            // Place le vecteur d'initialisation dans un tableau d'octets
            byte[] iv = Encoding.UTF8.GetBytes(strIv);


            RijndaelManaged rijndael = new RijndaelManaged();

            // Définit le mode utilisé
            rijndael.Mode = CipherMode.CBC;
            rijndael.Padding = PaddingMode.PKCS7;
            rijndael.KeySize = 256;

            // Crée le chiffreur AES - Rijndael
            ICryptoTransform aesEncryptor = rijndael.CreateEncryptor(key, iv);

            MemoryStream ms = new MemoryStream();

            // Ecris les données chiffrées dans le MemoryStream
            CryptoStream cs = new CryptoStream(ms, aesEncryptor, CryptoStreamMode.Write);
            cs.Write(plainText, 0, plainText.Length);
            cs.FlushFinalBlock();


            // Place les données chiffrées dans un tableau d'octet
            byte[] CipherBytes = ms.ToArray();


            ms.Close();
            cs.Close();

            // Place les données chiffrées dans une chaine encodée en Base64
            return Convert.ToBase64String(CipherBytes);

        }


        public static string DecryptString(string cipherText, string strKey, string strIv)
        {

            byte[] cipheredData = Convert.FromBase64String(cipherText);

            // Place la clé de déchiffrement dans un tableau d'octets
            byte[] key = Encoding.UTF8.GetBytes(strKey);

            // Place le vecteur d'initialisation dans un tableau d'octets
            byte[] iv = Encoding.UTF8.GetBytes(strIv);

            RijndaelManaged rijndael = new RijndaelManaged();
            rijndael.Mode = CipherMode.CBC;
            rijndael.Padding = PaddingMode.PKCS7;
            rijndael.KeySize = 256;


            // Ecris les données déchiffrées dans le MemoryStream
            ICryptoTransform decryptor = rijndael.CreateDecryptor(key, iv);
            MemoryStream ms = new MemoryStream(cipheredData);
            CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);

            // Place les données déchiffrées dans un tableau d'octet
            byte[] plainTextData = new byte[cipheredData.Length];

            int decryptedByteCount = cs.Read(plainTextData, 0, plainTextData.Length);

            ms.Close();
            cs.Close();

            return Encoding.UTF8.GetString(plainTextData, 0, decryptedByteCount);

        }
    }

    public class AllowCrossSiteAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", "*");
            base.OnActionExecuting(filterContext);
        }
    }


    public class KmlOutputAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.RequestContext.HttpContext.Response.ContentType = "application/vnd.google-earth.kml+xml";
            filterContext.RequestContext.HttpContext.Response.Charset = "utf-8";
            base.OnActionExecuting(filterContext);
        }
    }

    public class XmlResult : ActionResult
    {
        private object _objectToSerialize;

        public XmlResult(object objectToSerialize)
        {
            _objectToSerialize = objectToSerialize;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (_objectToSerialize != null)
            {
                var xs = new XmlSerializer(_objectToSerialize.GetType());
                context.HttpContext.Response.ContentType = "text/xml";
                xs.Serialize(context.HttpContext.Response.Output, _objectToSerialize);
            }
        }
    }



}