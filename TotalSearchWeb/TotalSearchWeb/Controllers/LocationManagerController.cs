using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TotalSearchWeb.Models;
using TotalSearchWeb.Filters;

namespace TotalSearchWeb.Controllers
{
    public class LocationManagerController : ApiController
    {

        [ValidateAddLocationPasswordAttribute]
        public HttpResponseMessage PostLocation(Location location)
        {
            using (var DataLayer = new DataLayerDataContext())
            {
                Location loc = null;
                
                if(location.sourcename == "winwin")
                {
                    loc = DataLayer.Locations.FirstOrDefault(item => (item.sourceid == location.sourceid &&
                                                                                            item.sourcename == "winwin"));

                } else if(location.sourcename == "yad2")
                {
                    loc = DataLayer.Locations.FirstOrDefault(item => (item.sourceid == location.sourceid &&
                                                                                            item.sourcename == "yad2"));
                }

                if(loc == null)
                {
                    if(location.sourcename == "winwin" &&
                        (!DataLayer.Locations.Any(item => (item.sourcename == "yad2"
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
                                                                && item.meublee == location.meublee))))
                                    {
                                        DataLayer.Locations.InsertOnSubmit(location);
                                        DataLayer.SubmitChanges();
                                    }

                     else if(location.sourcename == "yad2" &&
                        (!DataLayer.Locations.Any(item => (item.sourcename == "winwin"
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
                                                                && item.meublee == location.meublee))))
                                    {
                                        DataLayer.Locations.InsertOnSubmit(location);
                                        DataLayer.SubmitChanges();
                                    }
                        

                    
                }
                else
                {

                    DataLayer.SubmitChanges();
                }


                var response = Request.CreateResponse<Location>(HttpStatusCode.Created, location);
                return response;
            }
        }
    }
}
