using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PagedList;
using TotalSearchWeb.Models;
using TotalSearchWeb.Filters;

namespace TotalSearchWeb.Controllers
{
    public class LocationController : ApiController
    {
        //// GET api/<controller>
        //[ValidateHttpAntiForgeryToken]
        //public SearchViewModel Post(SearchViewModel model)
        //{
        //    using (var entities = new DataLayerDataContext())
        //    {
                
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

        //    return model;
        //}


        //[ValidateHttpAntiForgeryToken]
        //public IEnumerable<Location> Post(SearchViewModel model)
        //{
        //    using (var entities = new DataLayerDataContext())
        //    {

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
        //        var pageSize = model.PageSize ?? 25;
        //        var TotalRecords = results.Count();

        //        //model.SearchResults = results.ToPagedList(pageIndex, 25);

        //        return results.ToPagedList(pageIndex, pageSize);

        //    }

        //}


        //[ValidateHttpAntiForgeryToken]
        public SearchViewModel Post(SearchViewModel model)
        {
            using (var entities = new DataLayerDataContext())
            {

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
                var pageSize = model.PageSize ?? 25;
                var totalRecords = results.Count();

                model.SearchResults = results.ToPagedList(pageIndex, pageSize);
                model.PagesNumber = model.SearchResults.PageCount;
                model.TotalRecords = totalRecords;

                return model;

            }

        }

    }
}