using PagedList;
using System;
using System.ComponentModel.DataAnnotations;




namespace TotalSearchWeb.Models
{

    public class SearchViewModel
    {

        public int? Page { get; set; }

        public int? PageSize { get; set; }

        public int? PagesNumber { get; set; }

        public int? TotalRecords { get; set; }

        public IPagedList<Location> SearchResults { get; set; }

        public string SearchButton { get; set; }


        public string typebien { get; set; }

        public string zone { get; set; }

        public string ville { get; set; }

        public string quartier { get; set; }

        public decimal? nombrechambremin { get; set; }

        public decimal? nombrechambremax { get; set; }

        public decimal? prixmin { get; set; }

        public decimal? prixmax { get; set; }

        public decimal? etagemin { get; set; }

        public decimal? etagemax { get; set; }

        public int? superficiemin { get; set; }

        public int? superficiemax { get; set; }

        public bool plusieursresidents { get; set; }

        public bool climatisation { get; set; }

        public bool balcon { get; set; }

        public bool ascenseur { get; set; }

        public bool chambreforte { get; set; }

        public bool garage { get; set; }

        public bool acceshandicape { get; set; }

        public bool cave { get; set; }

        public bool bareaux { get; set; }

        public bool balconsoleil { get; set; }

        public bool renovee { get; set; }

        public bool meublee { get; set; }

        public bool animauxdomestique { get; set; }

        /*public string adresse { get; set; }

        public string numeromaison { get; set; }*/

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? dateentree { get; set; }

        public bool entreeimmediate { get; set; }

        public bool entreeflexible { get; set; }


    }

}