


function mydump2(arr, level) {
    var dumped_text = "";
    if (!level) level = 0;

    var level_padding = "";
    for (var j = 0; j < level + 1; j++) level_padding += "    ";

    if (typeof (arr) == 'object') {
        for (var item in arr) {
            var value = arr[item];

            if (typeof (value) == 'object') {
                dumped_text += level_padding + "'" + item + "' ...\n";
                dumped_text += mydump2(value, level + 1);
            } else {
                dumped_text += level_padding + "'" + item + "' => \"" + value + "\"\n";
            }
        }
    } else {
        dumped_text = "===>" + arr + "<===(" + typeof (arr) + ")";
    }
    return dumped_text;
}

function TestApi() {

    var apiUrl = 'api/location';

    $.getJSON(apiUrl + '/?zone=&ville=&quartier=&typebien=&nombrechambremin=&nombrechambremax=&prixmin=&prixmax=&etagemin=&etagemax=&superficiemin=&superficiemax=&meublee=true&garage=true&garage=false&ascenseur=false&bareaux=false&climatisation=false&chambreforte=false&balcon=false&renovee=false&cave=false&acceshandicape=false&animauxdomestique=false&plusieursresidents=false&entreeimmediate=false&entreeflexible=false&dateentree=&SearchButton=%D7%97%D7%A4%D7%A9')
    .done(function (data) {
        alert(mydump2(data, 0));
    })
    .fail(function (jqXHR, textStatus, err) {
        alert('Error: ' + err);
    });

}

function TestApi2() {

    var search = 
    {
        zone: "",
        ville : "",
        quartier : "",
        typebien : "",
        nombrechambremin : "",
        nombrechambremax : "",
        prixmin : "",
        prixmax : "",
        etagemin : "",
        etagemax : "",
        superficiemin : "",
        superficiemax : "",
        meublee : "true",
        garage : "false",
        ascenseur : "",
        bareaux : "",
        climatisation : "",
        chambreforte : "",
        balcon : "",
        renovee : "",
        cave : "",
        acceshandicape : "",
        animauxdomestique : "",
        plusieursresidents : "",
        entreeimmediate : "",
        entreeflexible : "",
        dateentree : "",
        SearchButton : "",
        Page : "1"
    };

    var searchJson = JSON.stringify(search);

    var antiForgeryToken = $("#antiForgeryToken").val();

    $.ajax({
        url: '/api/location',
        cache: false,
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: searchJson,
        dataType: "json",
        headers: {
            'RequestVerificationToken': antiForgeryToken
        },
        success: function (data) {
            alert(mydump2(data, 0));

            var appartids = "";
            for (var i = 0; i < data.length; i++) {
                appartids += data[i].appartementid + " , ";
            }

            alert(appartids);

        }
    }).fail(
        function (xhr, textStatus, err) {
            alert(err);
        });

    }



    function myalert(text) {

        var html = "<html><head><title>Alert</title>";
        html += "<style type=\"text/css\">body { font-family:Calibri;}";
        html += "table {border:1px solid gray; border-collapse:collapse;}";
        html += "th {text-align:left; border:1px solid gray;}";
        html += "td {border:1px solid gray;}</style>";
        html += "</head><body>";
        html += text;
        html += "</body></html>";
        var myWindow = window.open("", "_blank");
        myWindow.document.open();
        myWindow.document.write(html);
        myWindow.document.close();
    }

    function Location(data) {
        var self = this;

        self.typebien = ko.observable(data.typebien);
        self.zone = ko.observable(data.zone);
        self.ville = ko.observable(data.ville);
        self.quartier = ko.observable(data.quartier);
        self.adresse = ko.observable(data.adresse);
        self.numeromaison = ko.observable(data.numeromaison);
        self.nombrechambre = ko.observable(data.nombrechambre);
        self.prix = ko.observable(data.prix);
        self.etage = ko.observable(data.etage);
        self.superficie = ko.observable(data.superficie);
        self.plusieursresidents = ko.observable(data.plusieursresidents);
        self.climatisation = ko.observable(data.climatisation);
        self.balcon = ko.observable(data.balcon);
        self.ascenseur = ko.observable(data.ascenseur);
        self.chambreforte = ko.observable(data.chambreforte);
        self.garage = ko.observable(data.garage);
        self.acceshandicape = ko.observable(data.acceshandicape);
        self.cave = ko.observable(data.cave);
        self.bareaux = ko.observable(data.bareaux);
        self.balconsoleil = ko.observable(data.balconsoleil);
        self.renovee = ko.observable(data.renovee);
        self.meublee = ko.observable(data.meublee);
        self.animauxdomestique = ko.observable(data.animauxdomestique);
        self.dateentree = ko.observable(data.dateentree);
        self.entreeimmediate = ko.observable(data.entreeimmediate);
        self.entreeflexible = ko.observable(data.entreeflexible);
        self.date = ko.observable(data.date);
        self.longitude = ko.observable(data.longitude);
        self.latitude = ko.observable(data.latitude);

        self.urlimage = ko.observable(data.urlimage);
        self.nombrebalcon = ko.observable(data.nombrebalcon);
        self.nombreversement = ko.observable(data.nombreversement);

        self.DateEntree = ko.computed(function () {
            if (self.entreeimmediate() == true) {
                return ' מיידית ';
            }
            else if (self.entreeflexible() == true) {
                return ' גמיש ';
            }
            else {
                if (self.dateentree() != null) {

                    return self.dateentree().substring(0, 10);
                }
            }
        });

    }


    function LocationViewModel() {
        var self = this;

        self.Page = ko.observable();
        self.SearchResults = ko.observableArray([]);
        //self.SearchButton = ko.observable();
        self.typebien = ko.observable();
        self.zone = ko.observable();
        self.ville = ko.observable();
        self.quartier = ko.observable();
        self.nombrechambremin = ko.observable();
        self.nombrechambremax = ko.observable();
        self.prixmin = ko.observable();
        self.prixmax = ko.observable();
        self.etagemin = ko.observable();
        self.etagemax = ko.observable();
        self.superficiemin = ko.observable();
        self.superficiemax = ko.observable();
        self.plusieursresidents = ko.observable();
        self.climatisation = ko.observable();
        self.balcon = ko.observable();
        self.ascenseur = ko.observable();
        self.chambreforte = ko.observable();
        self.garage = ko.observable();
        self.acceshandicape = ko.observable();
        self.cave = ko.observable();
        self.bareaux = ko.observable();
        self.balconsoleil = ko.observable();
        self.renovee = ko.observable();
        self.meublee = ko.observable();
        self.animauxdomestique = ko.observable();
        self.dateentree = ko.observable();
        self.entreeimmediate = ko.observable();
        self.entreeflexible = ko.observable();


//        self.getData = function () {

//            self = this;

//            var antiForgeryToken = $("#antiForgeryToken").val();
//            alert("2");
//            $.ajax({
//                url: '/api/location',
//                cache: false,
//                type: 'POST',
//                contentType: 'application/json; charset=utf-8',
//                data: JSON.stringify(self),
//                dataType: "json",
//                headers: {
//                    'RequestVerificationToken': antiForgeryToken
//                },
//                success: function (data) {
//                    alert("1");
//                    var mappedLocations = $.map(data, function (item) { return new Location(item) });
//                    self.SearchResults(mappedLocations);

//                }
//            }).fail(
//        function (xhr, textStatus, err) {
//            alert(err);
//        });

//        };


        self.itemsTrigger = ko.computed(function () {
            //alert("1");

            //var t = ko.toJSON(self);

            var search =
            {
                zone: self.zone(),
                ville: self.ville(),
                quartier: self.quartier(),
                typebien: self.typebien(),
                nombrechambremin: self.nombrechambremin(),
                nombrechambremax: self.nombrechambremax(),
                prixmin: self.prixmin(),
                prixmax: self.prixmax(),
                etagemin: self.etagemin(),
                etagemax: self.etagemax(),
                superficiemin: self.superficiemin(),
                superficiemax: self.superficiemax(),
                meublee: self.meublee(),
                garage: self.garage(),
                ascenseur: self.ascenseur(),
                bareaux: self.bareaux(),
                climatisation: self.climatisation(),
                chambreforte: self.chambreforte(),
                balcon: self.balcon(),
                renovee: self.renovee(),
                cave: self.cave(),
                acceshandicape: self.acceshandicape(),
                animauxdomestique: self.animauxdomestique(),
                plusieursresidents: self.plusieursresidents(),
                entreeimmediate: self.entreeimmediate(),
                entreeflexible: self.entreeflexible(),
                dateentree: self.dateentree(),
                //SearchButton: self.quartier,
                Page: self.Page()
            };

            var searchJson = JSON.stringify(search);


            //            var mappedLocations = [
            //                    new Location({ typebien: "toto" }),
            //                    new Location({ typebien: "toto" })
            //                    ];
            //            //                                self.SearchResults(mappedLocations);

            //            self.SearchResults(mappedLocations);


            var antiForgeryToken = $("#antiForgeryToken").val();
            //alert("2");
            $.ajax({
                url: '/api/location',
                cache: false,
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                data: searchJson,
                dataType: "json",
                headers: {
                    'RequestVerificationToken': antiForgeryToken
                },
                success: function (data) {
                    //alert("3");

                    clearOverlays();

                    var mappedLocations = $.map(data, function (item) {

                        var location = new Location(item);

                        var position = new google.maps.LatLng(location.latitude(), location.longitude());

                        addMarker(position);

                        return location;

                    });
                    self.SearchResults(mappedLocations);



                    //                                            self.SearchResults([
                    //                                                new Location({ typebien: "toto" }),
                    //                                                new Location({ typebien: "tot1" }),
                    //                                                new Location({ typebien: "to3o" })
                    //                                                ]);


                }
            }).fail(
                                function (xhr, textStatus, err) {
                                    alert(err);
                                });

            //return Math.floor((Math.random() * 100) + 1);
        });


//        var search =
//    {
//        zone: "",
//        ville: "",
//        quartier: "",
//        typebien: "",
//        nombrechambremin: "",
//        nombrechambremax: "",
//        prixmin: "",
//        prixmax: "",
//        etagemin: "",
//        etagemax: "",
//        superficiemin: "",
//        superficiemax: "",
//        meublee: "true",
//        garage: "false",
//        ascenseur: "",
//        bareaux: "",
//        climatisation: "",
//        chambreforte: "",
//        balcon: "",
//        renovee: "",
//        cave: "",
//        acceshandicape: "",
//        animauxdomestique: "",
//        plusieursresidents: "",
//        entreeimmediate: "",
//        entreeflexible: "",
//        dateentree: "",
//        SearchButton: "",
//        Page: "1"
//    };

//        var searchJson = JSON.stringify(search);

//        var antiForgeryToken = $("#antiForgeryToken").val();

//        $.ajax({
//            url: '/api/location',
//            cache: false,
//            type: 'POST',
//            contentType: 'application/json; charset=utf-8',
//            data: searchJson,
//            dataType: "json",
//            headers: {
//                'RequestVerificationToken': antiForgeryToken
//            },
//            success: function (data) {

//                var mappedLocations = $.map(data, function (item) { return new Location(item) });
//                self.SearchResults(mappedLocations);

//            }
//        }).fail(
//        function (xhr, textStatus, err) {
//            alert(err);
//        });

                        }


                        // Add a marker to the map and push to the array.
                        function addMarker(location) {

                            var description = "";
                            description += 
                            "<table>" +
                                "<tr>" +
                                 "<td valign=\"top\" width=\"*\">";

                            if (location.urlimage() != null) {
                                description += "<img style=\"float: right; width:140px; height:100px; padding: 10px 0 0 10px\" src=\"" + location.urlimage() + "\" />";
                            }
                            description += "<div id=\"locationdetails\">" +
                             "<table cellspacing=\"5\" cellspacing=\"5\">" +
                             "<tr>" +
                             "<td width=\"220\" style=\"line-height: 21px;\">" +
                           "<b> אזור :</b>" + location.zone() + "<br />" +
                           "<b> עיר\\ישוב :</b>" + location.ville() + "<br />" +
                           "<b> שכונה :</b>" + location.quartier() + "<br />" +
                           "<b> כתובת :</b>" + location.adresse() + "<br />" +
                            "</td>" +
                            "<td width=\"220\">" +
                            "<b> סוג נכס :</b>" + location.typebien() + "<br />" +
                           "<b> מס' חדרים :</b>" + location.nombrechambre() + "<br />" +
                           "<b> קומה :</b>" + location.etage() + "<br />" +
                            "<b>מ\"ר :</b>" + location.superficie() + "<br />" +
                            "</td>" +
                             "<td width=\"220\">" +
                           "<b> מחיר :</b>" + location.prix() + "₪" + "<br />" +
                            "<b>מס' מרפסת :</b>" + location.nombrebalcon() + "<br />" +
                            "<b>כניסה : </b>";
                            if (location.entreeimmediate() == true) {
                                description += " מיידית   <br /> ";
                            }
                            else if (location.entreeflexible() == true) {
                                description += " גמיש   <br /> ";
                            }
                            else {
                                if (location.dateentree() != null) {
                                    //string.Format("{0:dd/MM/yyyy}", location.dateentree);
                                    description += location.dateentree().substring(0, 10);
                                    //Html.DisplayFor(x => location.dateentree);
                                }
                                description += "<br />";

                            }

                            description += "<b>תאריך : </b>" + location.date().substring(0, 10) + " <br />" +

                        "</td>" +
                        "<td width=\"220\">" +
                        "<b>תשלומים :</b>" + location.nombreversement() + "<br />" +
                        "<br />" +
                        "<br />" +
                        "<br />" +
                        "</td>" +
                        "</tr>" +
                        "</table>" +
                       "<div style=\"display: inline-block; width: 80px\">";

                            if (location.meublee() == true) {
                                description += "<input id=\"meublee\" name=\"meublee\" disabled=\"disabled\" value=\"location.meublee\" type=\"checkbox\" checked=\"checked\" />";
                            }
                            else {
                                description += "<input id=\"meublee\" name=\"meublee\" disabled=\"disabled\" value=\"location.meublee\" type=\"checkbox\" />";
                            }
                            description += "<b style=\"vertical-align:top\">מרוהטת</b>" +
                        "</div>" +
                        "<div style=\"display: inline-block; width: 60px\">";

                            if (location.garage() == true) {
                                description += "<input id=\"garage\" name=\"garage\" disabled=\"disabled\" value=\"location.garage\" type=\"checkbox\" checked=\"checked\" />";
                            }
                            else {
                                description += "<input id=\"garage\" name=\"garage\" disabled=\"disabled\" value=\"location.garage\" type=\"checkbox\" />";
                            }
                            description += "<b style=\"vertical-align:top\">חניה</b>" +
                        "</div>" +
                        "<div style=\"display: inline-block; width: 70px\">";

                            if (location.ascenseur() == true) {
                                description += "<input id=\"ascenseur\" name=\"ascenseur\" disabled=\"disabled\" value=\"location.ascenseur\" type=\"checkbox\" checked=\"checked\" />";
                            }
                            else {
                                description += "<input id=\"ascenseur\" name=\"ascenseur\" disabled=\"disabled\" value=\"location.ascenseur\" type=\"checkbox\" />";
                            }
                            description += "<b style=\"vertical-align:top\">מעלית</b>" +
                        "</div>" +
                        "<div style=\"display: inline-block; width: 70px\">";

                            if (location.bareaux() == true) {
                                description += "<input id=\"bareaux\" name=\"bareaux\" disabled=\"disabled\" value=\"location.bareaux\" type=\"checkbox\" checked=\"checked\" />";
                            }
                            else {
                                description += "<input id=\"bareaux\" name=\"bareaux\" disabled=\"disabled\" value=\"location.bareaux\" type=\"checkbox\" />";
                            }
                            description += "<b style=\"vertical-align:top\">סורגים</b>" +
                        "</div>" +
                        "<div style=\"display: inline-block; width: 90px\">";

                            if (location.climatisation() == true) {
                                description += "<input id=\"climatisation\" name=\"climatisation\" disabled=\"disabled\" value=\"location.climatisation\" type=\"checkbox\" checked=\"checked\" />";
                            }
                            else {
                                description += "<input id=\"climatisation\" name=\"climatisation\" disabled=\"disabled\" value=\"location.climatisation\" type=\"checkbox\" />";
                            }
                            description += "<b style=\"vertical-align:top\">מיזוג אוויר</b> " +
                        "</div>" +
                        "<div style=\"display: inline-block; width: 70px\">";

                            if (location.chambreforte() == true) {
                                description += "<input id=\"chambreforte\" name=\"chambreforte\" disabled=\"disabled\" value=\"location.chambreforte\" type=\"checkbox\" checked=\"checked\" />";
                            }
                            else {
                                description += "<input id=\"chambreforte\" name=\"chambreforte\" disabled=\"disabled\" value=\"location.chambreforte\" type=\"checkbox\" />";
                            }
                            description += "<b style=\"vertical-align:top\">ממ\"ד</b>" +
                        "</div>" +
                        "<div style=\"display: inline-block; width: 70px\">";
                            if (location.balcon() == true) {
                                description += "<input id=\"balcon\" name=\"balcon\" disabled=\"disabled\" value=\"location.balcon\" type=\"checkbox\" checked=\"checked\" />";
                            }
                            else {
                                description += "<input id=\"balcon\" name=\"balcon\" disabled=\"disabled\" value=\"location.balcon\" type=\"checkbox\" />";
                            }
                            description += "<b style=\"vertical-align:top\">מרפסת</b>" +
                        "</div>" +
                         "<div style=\"display: inline-block; width: 110px\">";

                            if (location.balconsoleil() == true) {
                                description += "<input id=\"balconsoleil\" name=\"balconsoleil\" disabled=\"disabled\" value=\"location.balconsoleil\" type=\"checkbox\" checked=\"checked\" /> ";
                            }
                            else {
                                description += "<input id=\"balconsoleil\" name=\"balconsoleil\" disabled=\"disabled\" value=\"location.balconsoleil\" type=\"checkbox\" />";
                            }
                            description += "<b style=\"vertical-align:top\">מרפסת שמש</b>" +
                        "</div>" +
                        "<div style=\"display: inline-block; width: 80px\">";

                            if (location.renovee() == true) {
                                description += "<input id=\"renovee\" name=\"renovee\" disabled=\"disabled\" value=\"location.renovee\" type=\"checkbox\" checked=\"checked\" />";
                            }
                            else {
                                description += "<input id=\"renovee\" name=\"renovee\" disabled=\"disabled\" value=\"location.renovee\" type=\"checkbox\" />";
                            }
                            description += "<b style=\"vertical-align:top\">משופצת</b>" +
                        "</div>" +
                        "<div style=\"display: inline-block; width: 70px\">";

                            if (location.cave() == true) {
                                description += "<input id=\"cave\" name=\"cave\" disabled=\"disabled\" value=\"location.cave\" type=\"checkbox\" checked=\"checked\" />";
                            }
                            else {
                                description += "<input id=\"cave\" name=\"cave\" disabled=\"disabled\" value=\"location.cave\" type=\"checkbox\" />";
                            }
                            description += "<b style=\"vertical-align:top\">מחסן</b>" +
                        "</div>" +
                        "<div style=\"display: inline-block; width: 100px\">";

                            if (location.acceshandicape() == true) {
                                description += "<input id=\"acceshandicape\" name=\"acceshandicape\" disabled=\"disabled\" value=\"location.acceshandicape\" type=\"checkbox\" checked=\"checked\" />";
                            }
                            else {
                                description += "<input id=\"acceshandicape\" name=\"acceshandicape\" disabled=\"disabled\" value=\"location.acceshandicape\" type=\"checkbox\" />";
                            }
                            description += "<b style=\"vertical-align:top\">גישה לנכים</b>" +
                        "</div>" +
                        "<div style=\"display: inline-block; width: 90px\">";

                            if (location.animauxdomestique() == true) {
                                description += "<input id=\"animauxdomestique\" name=\"animauxdomestique\" disabled=\"disabled\" value=\"location.animauxdomestique\" type=\"checkbox\" checked=\"checked\" /> ";
                            }
                            else {
                                description += "<input id=\"animauxdomestique\" name=\"animauxdomestique\" disabled=\"disabled\" value=\"location.animauxdomestique\" type=\"checkbox\" />";
                            }
                            description += "<b style=\"vertical-align:top\">חיות מחמד</b>" +
                        "</div>" +
                        "<div style=\"display: inline-block; width: 90px\">";

                            if (location.plusieursresidents() == true) {
                                description += "<input id=\"plusieursresidents\" name=\"plusieursresidents\" disabled=\"disabled\" value=\"location.plusieursresidents\" type=\"checkbox\" checked=\"checked\" /> ";
                            }
                            else {
                                description += "<input id=\"plusieursresidents\" name=\"plusieursresidents\" disabled=\"disabled\" value=\"location.plusieursresidents\" type=\"checkbox\" />";
                            }
                            description += "<b style=\"vertical-align:top\">מ.שותפים</b>" +
                        "</div>" +
                        "</div>" +
                        "</td>" +
                        "</tr>" +
                        "</table>";



                            var infoWindowOptions = {
                                content: '<div class="geoxml3_infowindow">' + description + '</div>',
                                pixelOffset: new google.maps.Size(0, 2)
                            };

                            var position = new google.maps.LatLng(location.latitude(), location.longitude());

                            var marker = new google.maps.Marker({
                                position: position,
                                map: map,
                                infoWindow: new google.maps.InfoWindow(infoWindowOptions)
                            });

                            google.maps.event.addListener(marker, 'click', function () {
                                this.infoWindow.close();
                                marker.infoWindow.setOptions(infoWindowOptions);
                                this.infoWindow.open(map, this);
                            });

                            markers.push(marker);
                        }

                        // Sets the map on all markers in the array.
                        function setAllMap(map) {
                            for (var i = 0; i < markers.length; i++) {
                                markers[i].setMap(map);
                            }
                        }

                        // Removes the overlays from the map, but keeps them in the array.
                        function clearOverlays() {
                            setAllMap(null);
                        }

                        // Shows any overlays currently in the array.
                        function showOverlays() {
                            setAllMap(map);
                        }

                        // Deletes all markers in the array by removing references to them.
                        function deleteOverlays() {
                            clearOverlays();
                            markers = [];
                        }




                        function MyViewModel() {
                            var self = this;

                            //self.Page = ko.observable();
                            self.SearchResults = ko.observableArray([]);
                            //self.SearchButton = ko.observable();
                            self.typebien = ko.observable();
                            self.zone = ko.observable();
                            self.ville = ko.observable();
                            self.quartier = ko.observable();
                            self.nombrechambremin = ko.observable();
                            self.nombrechambremax = ko.observable();
                            self.prixmin = ko.observable();
                            self.prixmax = ko.observable();
                            self.etagemin = ko.observable();
                            self.etagemax = ko.observable();
                            self.superficiemin = ko.observable();
                            self.superficiemax = ko.observable();
                            self.plusieursresidents = ko.observable();
                            self.climatisation = ko.observable();
                            self.balcon = ko.observable();
                            self.ascenseur = ko.observable();
                            self.chambreforte = ko.observable();
                            self.garage = ko.observable();
                            self.acceshandicape = ko.observable();
                            self.cave = ko.observable();
                            self.bareaux = ko.observable();
                            self.balconsoleil = ko.observable();
                            self.renovee = ko.observable();
                            self.meublee = ko.observable();
                            self.animauxdomestique = ko.observable();
                            self.dateentree = ko.observable();
                            self.entreeimmediate = ko.observable();
                            self.entreeflexible = ko.observable();


                            // pager related stuff
                            // ---------------------------------------------
                            self.Page = ko.observable(1);
                            //self.perPage = 5;
                            self.PageSize = ko.observable(10);
                            self.availablePerPage = ko.observableArray([5, 10, 20, 50]);

                            self.maxPageIndex = ko.observable(0), //the number of records
                            self.totalRecords = ko.observable(0), //the number of records

                            self.Chart2Data = ko.computed(function () {

//                                $('#statsBox2').highcharts({
//                                    series: [{
//                                        type: 'pie',
//                                        name: 'Browser share',
//                                        data: [
//                                                        ['Firefox', 45.0],
//                                                        ['IE', 26.8],
//                                                        {
//                                                            name: 'Chrome',
//                                                            y: 12.8,
//                                                            sliced: true,
//                                                            selected: true
//                                                        },
//                                                        ['Safari', 8.5],
//                                                        ['Opera', 6.2],
//                                                        ['Others', 0.7]
//                                                    ]
//                                    }]
//                                });


                            }, self);

//                            self.pagedItems = ko.computed(function () {
//                                var pg = self.Page(),
//                                start = self.PageSize() * (pg - 1),
//                                end = start + self.PageSize();
//                                return self.SearchResults().slice(start, end);
//                            }, self);
                            self.nextPage = function () {
                                if (self.nextPageEnabled())
                                    self.Page(self.Page() + 1);
                            };
                            self.nextPageEnabled = ko.computed(function () {
                                return self.totalRecords() > self.PageSize() * self.Page();
                            }, self);
                            self.previousPage = function () {
                                if (self.previousPageEnabled())
                                    self.Page(self.Page() - 1);
                            };
                            self.previousPageEnabled = ko.computed(function () {
                                return self.Page() > 1;
                            }, self);
                            self.goToPage = function (pageIndex) {
                                self.Page(parseInt(pageIndex));
                            };
                            //self.maxPageIndex = ko.computed(function () {
                            //    return Math.ceil(self.SearchResults().length / self.PageSize());
                            //}, self);
                            self.Paging = ko.computed(function () {
                                return ko.utils.range(1, self.maxPageIndex());
                            }, self);

                            self.resetPaging = function () {
                                self.Page(1);
                            };

                            self.recordMessage = ko.computed(function () {
                                if (self.SearchResults().length > 0) {
                                    return 'Records ' + ((self.Page() - 1) * self.PageSize() + 1) + ' - ' + (self.Page() < self.maxPageIndex() ? self.Page() * self.PageSize() : self.totalRecords()) + ' of ' + self.totalRecords();
                                }
                                else {
                                    return 'No records';
                                }
                            }, self);
                            /*self.gridViewModel = new ko.simpleGrid.viewModel({
                                data: self.SearchResults,
                                columns: [
                                            { headerText: "<input class=\"checkmarker\"  type=\"checkbox\" />", rowText: function () { return "<input class=\"checkmarker\"  type=\"checkbox\" />"; } },
                                            { headerText: "typebien", rowText: "typebien" },
                                            { headerText: "nombrechambre", rowText: "nombrechambre" },
                                            { headerText: "ville", rowText: "ville" },
                                            { headerText: "adresse", rowText: "adresse" },
                                            { headerText: "entreeimmediate", rowText: "entreeimmediate" },
                                            { headerText: "prix", rowText: "prix" },
                                            { headerText: "date", rowText: "date" }
                                        ],
                                pageSize: 5
                            });*/
                        }


                        ko.bindingHandlers.map = {
                            //                            init: function (element, valueAccessor, allBindingsAccessor, viewModel) {


                            //                                var search =
                            //                                {
                            ////                                    zone: allBindingsAccessor.zone(),
                            ////                                    ville: allBindingsAccessor.ville(),
                            ////                                    quartier: allBindingsAccessor.quartier(),
                            ////                                    typebien: allBindingsAccessor.typebien(),
                            //                                    nombrechambremin: allBindingsAccessor.nombrechambremin(),
                            //                                    nombrechambremax: allBindingsAccessor.nombrechambremax(),
                            ////                                    prixmin: allBindingsAccessor.prixmin(),
                            ////                                    prixmax: allBindingsAccessor.prixmax(),
                            ////                                    etagemin: allBindingsAccessor.etagemin(),
                            ////                                    etagemax: allBindingsAccessor.etagemax(),
                            ////                                    superficiemin: allBindingsAccessor.superficiemin(),
                            ////                                    superficiemax: allBindingsAccessor.superficiemax(),
                            ////                                    meublee: allBindingsAccessor.meublee(),
                            ////                                    garage: allBindingsAccessor.garage(),
                            ////                                    ascenseur: allBindingsAccessor.ascenseur(),
                            ////                                    bareaux: allBindingsAccessor.bareaux(),
                            ////                                    climatisation: allBindingsAccessor.climatisation(),
                            ////                                    chambreforte: allBindingsAccessor.chambreforte(),
                            ////                                    balcon: allBindingsAccessor.balcon(),
                            ////                                    renovee: allBindingsAccessor.renovee(),
                            ////                                    cave: allBindingsAccessor.cave(),
                            ////                                    acceshandicape: allBindingsAccessor.acceshandicape(),
                            ////                                    animauxdomestique: allBindingsAccessor.animauxdomestique(),
                            ////                                    plusieursresidents: allBindingsAccessor.plusieursresidents(),
                            ////                                    entreeimmediate: allBindingsAccessor.entreeimmediate(),
                            ////                                    entreeflexible: allBindingsAccessor.entreeflexible(),
                            ////                                    dateentree: allBindingsAccessor.dateentree(),
                            ////                                    //SearchButton: self.quartier,
                            ////                                    Page: allBindingsAccessor.Page()
                            //                                };

                            //                                var searchJson = JSON.stringify(search);


                            //                                var antiForgeryToken = $("#antiForgeryToken").val();
                            //                                //alert("2");
                            //                                $.ajax({
                            //                                    url: '/api/location',
                            //                                    cache: false,
                            //                                    type: 'POST',
                            //                                    contentType: 'application/json; charset=utf-8',
                            //                                    data: searchJson,
                            //                                    dataType: "json",
                            //                                    headers: {
                            //                                        'RequestVerificationToken': antiForgeryToken
                            //                                    },
                            //                                    success: function (data) {
                            //                                        //alert("3");

                            //                                        clearOverlays();

                            //                                        var mappedLocations = $.map(data, function (item) {

                            //                                            var location = new Location(item);

                            //                                            var position = new google.maps.LatLng(location.latitude(), location.longitude());

                            //                                            addMarker(position);

                            //                                            return location;

                            //                                        });
                            //                                        allBindingsAccessor.SearchResults(mappedLocations);


                            //                                    }
                            //                                }).fail(
                            //                                function (xhr, textStatus, err) {
                            //                                    alert(err);
                            //                                });
                            //                            },
                            update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
                                var search =
                                {
                                    zone: viewModel.zone(),
                                    ville: viewModel.ville(),
                                    quartier: viewModel.quartier(),
                                    typebien: viewModel.typebien(),
                                    nombrechambremin: viewModel.nombrechambremin(),
                                    nombrechambremax: viewModel.nombrechambremax(),
                                    prixmin: viewModel.prixmin(),
                                    prixmax: viewModel.prixmax(),
                                    etagemin: viewModel.etagemin(),
                                    etagemax: viewModel.etagemax(),
                                    superficiemin: viewModel.superficiemin(),
                                    superficiemax: viewModel.superficiemax(),
                                    meublee: viewModel.meublee(),
                                    garage: viewModel.garage(),
                                    ascenseur: viewModel.ascenseur(),
                                    bareaux: viewModel.bareaux(),
                                    climatisation: viewModel.climatisation(),
                                    chambreforte: viewModel.chambreforte(),
                                    balcon: viewModel.balcon(),
                                    renovee: viewModel.renovee(),
                                    cave: viewModel.cave(),
                                    acceshandicape: viewModel.acceshandicape(),
                                    animauxdomestique: viewModel.animauxdomestique(),
                                    plusieursresidents: viewModel.plusieursresidents(),
                                    entreeimmediate: viewModel.entreeimmediate(),
                                    entreeflexible: viewModel.entreeflexible(),
                                    dateentree: viewModel.dateentree(),
                                    //SearchButton: self.quartier,
                                    Page: viewModel.Page(),
                                    PageSize: viewModel.PageSize()
                                };

                                var searchJson = JSON.stringify(search);


                                var antiForgeryToken = $("#antiForgeryToken").val();
                                //alert("2");
                                $.ajax({
                                    url: '/api/location',
                                    cache: false,
                                    type: 'POST',
                                    contentType: 'application/json; charset=utf-8',
                                    data: searchJson,
                                    dataType: "json",
                                    headers: {
                                        'RequestVerificationToken': antiForgeryToken
                                    },
                                    success: function (data) {
                                        //alert("3");

                                        //clearOverlays();
                                        deleteOverlays();

                                        var mappedLocations = $.map(data.SearchResults, function (item) {

                                            var location = new Location(item);

                                            addMarker(location);

                                            return location;

                                        });

                                        viewModel.totalRecords(data.TotalRecords);
                                        viewModel.maxPageIndex(data.PagesNumber);
                                        viewModel.SearchResults(mappedLocations);

//                                        var seriesChart2Data = [];

//                                        seriesChart2Data.push(
//                                            ['Firefox', data.TotalRecords],
//                                            ['IE', 45.0],
//                                            {
//                                                name: 'Chrome',
//                                                y: 6.2,
//                                                sliced: true,
//                                                selected: true
//                                            },
//                                            ['Safari', 8.5],
//                                            ['Opera', 12.8],
//                                            ['Others', 0.7]
//                                        );

                                            $('#statsBox2').highcharts({
                                                series: [{
                                                    type: 'pie',
                                                    name: 'Browser share',
                                                    data: [
                                                                    ['Firefox', 45.0],
                                                                    ['IE', 26.8],
                                                                    {
                                                                        name: 'Chrome',
                                                                        y: 12.8,
                                                                        sliced: true,
                                                                        selected: true
                                                                    },
                                                                    ['Safari', 8.5],
                                                                    ['Opera', 6.2],
                                                                    ['Others', 0.7]
                                                                ]
                                                }]
                                            });


                                    }
                                }).fail(
                                function (xhr, textStatus, err) {
                                    alert(err);
                                });

                            }
                        };


/*
function Location(data) {

        this.Page = ko.observable(data.Page);
        this.SearchResults =  ko.observableArray(data.SearchResults);

        public IPagedList<Location> SearchResults { get; set; }

        public string SearchButton { get; set; }


        public string typebien { get; set; }

        public string zone { get; set; }

        public string ville { get; set; }

        public string quartier { get; set; }

        public decimal nombrechambre { get; set; }

        public decimal? nombrechambremin { get; set; }

        public decimal? nombrechambremax { get; set; }

        public decimal prix { get; set; }

        public decimal? prixmin { get; set; }

        public decimal? prixmax { get; set; }

        public decimal etage { get; set; }

        public decimal? etagemin { get; set; }

        public decimal? etagemax { get; set; }

        public string commentaire { get; set; }

        public int nombrebalcon { get; set; }

        public bool plusieursresidents { get; set; }

        public string adresse { get; set; }

        public int superficie { get; set; }

        public int? superficiemin { get; set; }

        public int? superficiemax { get; set; }

        public int nombreetage { get; set; }

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

        public string numeromaison { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? dateentree { get; set; }

        public bool entreeimmediate { get; set; }

        public bool entreeflexible { get; set; }
}

/*
function Location(data) {
this.title = ko.observable(data.title);
this.isDone = ko.observable(data.isDone);

this.appartementid = ko.observable(data.appartementid);
		
private string _typebien;
		
private string _zone;
		
private string _ville;
		
private string _quartier;
		
private System.Nullable<decimal> _nombrechambre;
		
private System.Nullable<decimal> _prix;
		
private System.Nullable<decimal> _etage;
		
private string _commentaire;
		
private System.Nullable<int> _nombrebalcon;
		
private System.Nullable<bool> _plusieursresidents;
		
private string _adresse;
		
private System.Nullable<int> _superficie;
		
private System.Nullable<int> _nombreetage;
		
private System.Nullable<bool> _climatisation;
		
private System.Nullable<bool> _balcon;
		
private System.Nullable<bool> _ascenseur;
		
private System.Nullable<bool> _chambreforte;
		
private System.Nullable<bool> _garage;
		
private System.Nullable<bool> _acceshandicape;
		
private System.Nullable<bool> _cave;
		
private System.Nullable<bool> _bareaux;
		
private System.Nullable<bool> _balconsoleil;
		
private System.Nullable<bool> _renovee;
		
private System.Nullable<bool> _meublee;
		
private System.Nullable<bool> _unitelogement;
		
private System.Nullable<bool> _animauxdomestique;
		
private System.Nullable<System.DateTime> _date;
		
private string _sourceid;
		
private string _contact;
		
private string _telcontact;
		
private string _numeromaison;
		
private System.Nullable<System.DateTime> _dateentree;
		
private System.Nullable<bool> _entreeimmediate;
		
private string _urlimage;
		
private System.Nullable<int> _nombreversement;
		
private System.Nullable<bool> _entreeflexible;
		
private string _sourcename;
		
private System.Nullable<decimal> _longitude;
		
private System.Nullable<decimal> _latitude;

}

function LocationViewModel() {
    var self = this;

    self.Page = ko.observable();
    self.SearchResults =  ko.observableArray([]);
    self.SearchButton = ko.observable();
    self.typebien = ko.observable();
    self.zone = ko.observable();
    self.ville = ko.observable();
    self.quartier = ko.observable();
    self.nombrechambre = ko.observable();
    self.nombrechambremax = ko.observable();
    self.prixmin = ko.observable();
    self.prixmax = ko.observable();
    self.etagemin = ko.observable();
    self.etagemax = ko.observable();
    self.superficiemin = ko.observable();
    self.superficiemax = ko.observable();
    self.plusieursresidents = ko.observable();
    self.climatisation = ko.observable();
    self.balcon = ko.observable();
    self.ascenseur = ko.observable();
    self.chambreforte = ko.observable();
    self.garage = ko.observable();
    self.acceshandicape = ko.observable();
    self.cave = ko.observable();
    self.bareaux = ko.observable();
    self.balconsoleil = ko.observable();
    self.renovee = ko.observable();
    self.meublee = ko.observable();
    self.animauxdomestique = ko.observable();
    self.dateentree = ko.observable();
    self.entreeimmediate = ko.observable();
    self.entreeflexible = ko.observable();

}
public int? Page { get; set; }

        public IPagedList<Location> SearchResults { get; set; }

        public string SearchButton { get; set; }


        public string typebien { get; set; }

        public string zone { get; set; }

        public string ville { get; set; }

        public string quartier { get; set; }

        public decimal nombrechambre { get; set; }

        public decimal? nombrechambremin { get; set; }

        public decimal? nombrechambremax { get; set; }

        public decimal prix { get; set; }

        public decimal? prixmin { get; set; }

        public decimal? prixmax { get; set; }

        public decimal etage { get; set; }

        public decimal? etagemin { get; set; }

        public decimal? etagemax { get; set; }

        public string commentaire { get; set; }

        public int nombrebalcon { get; set; }

        public bool plusieursresidents { get; set; }

        public string adresse { get; set; }

        public int superficie { get; set; }

        public int? superficiemin { get; set; }

        public int? superficiemax { get; set; }

        public int nombreetage { get; set; }

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

        public string numeromaison { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? dateentree { get; set; }

        public bool entreeimmediate { get; set; }

        public bool entreeflexible { get; set; }

*/

/*
//global for map
var map;

$(document).ready(function () {
   
   createMap();
    setTimeout(function() {
       ko.applyBindings(viewModel);
    },1000);
});
        function SliderBox(controlDiv, map) {
            
            var box = document.createElement('div');
            box.id = 'sliderbox';
            box.style.height = '100px';
            var box2 = document.createElement('div');
            box2.id = 'searchFormBox';
            box2.innerHTML = '<input data-bind="value: Lat" /> <input data-bind="value: Lng" />';
            
            box.appendChild(box2);
            controlDiv.appendChild(box);

        }

            

function MyViewModel() {
    var self = this;
    self.Lat = ko.observable(12.24);
    self.Lng = ko.observable(24.54);
}

    function createMap(){    
    var elevator;
    var myOptions = {
        zoom: 3,
        center: new google.maps.LatLng(12.24, 24.54),
        mapTypeId: 'terrain'
    };
    map = new google.maps.Map($('#map')[0], myOptions);
        
        var sliderBoxDiv = document.createElement('div');
        var sliderBoxControl = new SliderBox(sliderBoxDiv, map);

        sliderBoxDiv.index = -500;
        map.controls[google.maps.ControlPosition.LEFT].push(sliderBoxDiv);
}

ko.bindingHandlers.map = {
            init: function (element, valueAccessor, allBindingsAccessor, viewModel) {


                var position = new google.maps.LatLng(allBindingsAccessor().latitude(), allBindingsAccessor().longitude());

                var marker = new google.maps.Marker({
                    map: allBindingsAccessor().map,
                    position: position,
                    title: name
                });

                viewModel._mapMarker = marker;
            },
            update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
                var latlng = new google.maps.LatLng(allBindingsAccessor().latitude(), allBindingsAccessor().longitude());
                viewModel._mapMarker.setPosition(latlng);

            }
        };


var viewModel = new MyViewModel();

<div id="map"></div>
<div data-bind="latitude: viewModel.Lat, longitude:viewModel.Lng, map:map" ></div>


*/

/*



  <div id="searchFormBox">


<form action="/Home/Map" method="get"><div class="validation-summary-valid" data-valmsg-summary="true"><ul><li style="display:none"></li>
</ul></div>    <div align="center">
    <table class="MainTable">
    <tbody><tr>
    <td>
<table class="table_search" style="" cellspacing="0" cellpadding="0">
    <tbody>
        <tr>
            <td>
                <table class="ParamTable" style="" cellspacing="1" cellpadding="0">
                    <tbody>
                        <tr>
                            <td align="right" id="td_area" style="width: 140px;" name="td_area">
                                אזור<br>
                               
                                <select data-bind="value: zone"  id="zone" name="zone"><option value="">כל האיזורים</option>
<option value="אזור צפון">אזור צפון</option>
<option value="חיפה וחוף הכרמל">חיפה וחוף הכרמל</option>
<option value="קריות">קריות</option>
<option value="עכו - נהריה">עכו - נהריה</option>
<option value="גליל ועמקים">גליל ועמקים</option>
<option value="טבריה והסביבה">טבריה והסביבה</option>
<option value="עמק יזרעאל">עמק יזרעאל</option>
<option value="כרמיאל והסביבה">כרמיאל והסביבה</option>
<option value="מושבים בצפון">מושבים בצפון</option>
<option value="אזור השרון והסביבה">אזור השרון והסביבה</option>
<option value="זכרון - בנימינה">זכרון - בנימינה</option>
<option value="חדרה וישובי עמק חפר">חדרה וישובי עמק חפר</option>
<option value="פרדס חנה - כרכור">פרדס חנה - כרכור</option>
<option value="יישובי השומרון">יישובי השומרון</option>
<option value="נתניה והסביבה">נתניה והסביבה</option>
<option value="קיסריה והסביבה">קיסריה והסביבה</option>
<option value="רמת השרון - הרצליה">רמת השרון - הרצליה</option>
<option value="רעננה - כפר סבא">רעננה - כפר סבא</option>
<option value="הוד השרון והסביבה">הוד השרון והסביבה</option>
<option value="מושבים בשרון">מושבים בשרון</option>
<option value="אזור המרכז">אזור המרכז</option>
<option value="תל אביב">תל אביב</option>
<option value="תל אביב - צפון">תל אביב - צפון</option>
<option value="תל אביב - מרכז">תל אביב - מרכז</option>
<option value="תל אביב - דרום">תל אביב - דרום</option>
<option value="בקעת אונו">בקעת אונו</option>
<option value="חולון - בת ים">חולון - בת ים</option>
<option value="פתח תקוה והסביבה">פתח תקוה והסביבה</option>
<option value="ראשל&quot;צ והסביבה">ראשל"צ והסביבה</option>
<option value="רמת גן - גבעתיים">רמת גן - גבעתיים</option>
<option value="ראש העין והסביבה">ראש העין והסביבה</option>
<option value="מושבים במרכז">מושבים במרכז</option>
<option value="ירושלים והסביבה">ירושלים והסביבה</option>
<option value="מודיעין והסביבה">מודיעין והסביבה</option>
<option value="בית שמש והסביבה">בית שמש והסביבה</option>
<option value="מושבים באזור ירושלים">מושבים באזור ירושלים</option>
<option value="אזור השפלה והסביבה">אזור השפלה והסביבה</option>
<option value="נס ציונה - רחובות">נס ציונה - רחובות</option>
<option value="אשדוד - אשקלון">אשדוד - אשקלון</option>
<option value="רמלה - לוד">רמלה - לוד</option>
<option value="גדרה יבנה והסביבה">גדרה יבנה והסביבה</option>
<option value="מושבים בשפלה">מושבים בשפלה</option>
<option value="אזור דרום">אזור דרום</option>
<option value="באר שבע והסביבה">באר שבע והסביבה</option>
<option value="אילת והערבה">אילת והערבה</option>
<option value="מושבים בדרום">מושבים בדרום</option>
<option value="חו&quot;ל">חו"ל</option>
</select>

                                <span class="field-validation-valid" data-valmsg-for="zone" data-valmsg-replace="true"></span>
                            </td>
                            <td align="right" id="td_city" valign="top" style="width: 95px;" name="td_city">

                                        עיר\ישוב<br>
                                        <input data-bind="value: ville" class="text-box single-line" id="ville" name="ville" type="text" value="">

                                        <span class="field-validation-valid" data-valmsg-for="ville" data-valmsg-replace="true"></span>

                            </td>
                            <td align="right" id="td_hood" valign="top" style="width: 86px;" name="td_hood">

                                        שכונה<br>
                                         <input data-bind="value: quartier" class="text-box single-line" id="quartier" name="quartier" type="text" value="">

                                         <span class="field-validation-valid" data-valmsg-for="quartier" data-valmsg-replace="true"></span>
 
                            </td>
                            <td align="right" id="td_homeType" style="width: 132px;" name="td_homeType">
                                סוג נכס<br>

                          <select data-bind="value: typebien" id="typebien" name="typebien"><option value="">כל הסוגים</option>
<option value="גג/פנטהאוזים">גג/פנטהאוזים</option>
<option value="דו משפחתיים">דו משפחתיים</option>
<option value="דופלקסים">דופלקסים</option>
<option value="דירות">דירות</option>
<option value="דירות גן">דירות גן</option>
<option value="דירות לתקופות קצרות">דירות לתקופות קצרות</option>
<option value="דירות נופש">דירות נופש</option>
<option value="החלפת דירות">החלפת דירות</option>
<option value="חיפוש נכסים">חיפוש נכסים</option>
<option value="חניות">חניות</option>
<option value="טריפלקס">טריפלקס</option>
<option value="יחידות דיור">יחידות דיור</option>
<option value="כללי">כללי</option>
<option value="מבנים ניידים/קרוואן">מבנים ניידים/קרוואן</option>
<option value="מגרשים">מגרשים</option>
<option value="מחסנים">מחסנים</option>
<option value="מרתפים/פרטרים">מרתפים/פרטרים</option>
<option value="משק חקלאי">משק חקלאי</option>
<option value="סטודיו/לופט">סטודיו/לופט</option>
<option value="פרטיים/קוטג'ים">פרטיים/קוטג'ים</option>
<option value="קבוצת רכישה">קבוצת רכישה</option>
</select>

                        <span class="field-validation-valid" data-valmsg-for="typebien" data-valmsg-replace="true"></span>
                                
                            </td>
                            <td align="right" id="td_rooms" style="width: 55px;">
                                מחדרים<br>

                                <select data-bind="value: nombrechambremin" data-val="true" data-val-number="The field nombrechambremin must be a number." id="nombrechambremin" name="nombrechambremin"><option value="">הכל</option>
<option value="1">1</option>
<option value="1.5">1.5</option>
<option value="2">2</option>
<option value="2.5">2.5</option>
<option value="3">3</option>
<option value="3.5">3.5</option>
<option value="4">4</option>
<option value="4.5">4.5</option>
<option value="5">5</option>
<option value="5.5">5.5</option>
<option value="6">6</option>
<option value="7">7</option>
<option value="8">8</option>
<option value="9">9</option>
<option value="10">10</option>
<option value="11">11</option>
<option value="12">12</option>
</select>

                            <span class="field-validation-valid" data-valmsg-for="nombrechambremin" data-valmsg-replace="true"></span>
                            </td>
                            <td align="right" id="td_until_rooms" style="width: 67px;">
                            עד חדרים<br>

                                <select data-bind="value: nombrechambremax" data-val="true" data-val-number="The field nombrechambremax must be a number." id="nombrechambremax" name="nombrechambremax"><option value="">הכל</option>
<option value="1">1</option>
<option value="1.5">1.5</option>
<option value="2">2</option>
<option value="2.5">2.5</option>
<option value="3">3</option>
<option value="3.5">3.5</option>
<option value="4">4</option>
<option value="4.5">4.5</option>
<option value="5">5</option>
<option value="5.5">5.5</option>
<option value="6">6</option>
<option value="7">7</option>
<option value="8">8</option>
<option value="9">9</option>
<option value="10">10</option>
<option value="11">11</option>
<option value="12">12</option>
</select>

                                <span class="field-validation-valid" data-valmsg-for="nombrechambremax" data-valmsg-replace="true"></span>
                            </td>
                            <td align="right" style="width: 45px;">
                                ממחיר<br>
                                <input data-bind="value: prixmin" class="text-box single-line" data-val="true" data-val-number="The field prixmin must be a number." id="prixmin" name="prixmin" type="text" value="">

                                <span class="field-validation-valid" data-valmsg-for="prixmin" data-valmsg-replace="true"></span>
                            </td>
                            <td align="right" style="width: 43px;">
                                עד מחיר<br>
                                 <input data-bind="value: prixmax" class="text-box single-line" data-val="true" data-val-number="The field prixmax must be a number." id="prixmax" name="prixmax" type="text" value="">

                                 <span class="field-validation-valid" data-valmsg-for="prixmax" data-valmsg-replace="true"></span>
                            </td>
                            <td align="right" style="width: 55px;">
                                מקומה<br>
                             
                               <select data-bind="value: etagemin" data-val="true" data-val-number="The field etagemin must be a number." id="etagemin" name="etagemin"><option value="">הכל</option>
<option value="0">קרקע</option>
<option value="1">1</option>
<option value="2">2</option>
<option value="3">3</option>
<option value="4">4</option>
<option value="5">5</option>
<option value="6">6</option>
<option value="7">7</option>
<option value="8">8</option>
<option value="9">9</option>
<option value="10">10</option>
<option value="11">11</option>
<option value="12">12</option>
<option value="13">13</option>
<option value="14">14</option>
<option value="15">15</option>
</select>

                                <span class="field-validation-valid" data-valmsg-for="etagemin" data-valmsg-replace="true"></span>
                            </td>
                            <td align="right" style="width: 55px;">
                                עד קומה<br>
                                <select data-bind="value: etagemax" data-val="true" data-val-number="The field etagemax must be a number." id="etagemax" name="etagemax"><option value="">הכל</option>
<option value="0">קרקע</option>
<option value="1">1</option>
<option value="2">2</option>
<option value="3">3</option>
<option value="4">4</option>
<option value="5">5</option>
<option value="6">6</option>
<option value="7">7</option>
<option value="8">8</option>
<option value="9">9</option>
<option value="10">10</option>
<option value="11">11</option>
<option value="12">12</option>
<option value="13">13</option>
<option value="14">14</option>
<option value="15">15</option>
</select>

                            <span class="field-validation-valid" data-valmsg-for="etagemax" data-valmsg-replace="true"></span>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table class="ParamTable" style="" cellspacing="1" cellpadding="0">
                                                    <tbody>
                                                        <tr>
                                                            <td align="right" width="40" valign="top">
                                                                ממ"ר<br>
                                                                <input data-bind="value: superficiemin" class="text-box single-line" data-val="true" data-val-number="The field superficiemin must be a number." id="superficiemin" name="superficiemin" type="number" value="">

                                                                <span class="field-validation-valid" data-valmsg-for="superficiemin" data-valmsg-replace="true"></span>
                                                            </td>
                                                            <td align="right" width="40" valign="top">
                                                                עד מ"ר<br>
                                                                  <input data-bind="value: superficiemax" class="text-box single-line" data-val="true" data-val-number="The field superficiemax must be a number." id="superficiemax" name="superficiemax" type="number" value="">

                                                                <span class="field-validation-valid" data-valmsg-for="superficiemax" data-valmsg-replace="true"></span>
                                                            </td>
                                                            <td align="center" valign="top">
                                                                ריהוט<br>
                                                                 <input data-bind="value: meublee" class="check-box" data-val="true" data-val-required="The meublee field is required." id="meublee" name="meublee" type="checkbox" value="true"><input name="meublee" type="hidden" value="false">

                                                                <span class="field-validation-valid" data-valmsg-for="meublee" data-valmsg-replace="true"></span>
                                                            </td>
                                                            <td align="center" valign="top">
                                                                חניה<br>
                                                                <input data-bind="value: garage" class="check-box" data-val="true" data-val-required="The garage field is required." id="garage" name="garage" type="checkbox" value="true"><input name="garage" type="hidden" value="false">

                                                                <span class="field-validation-valid" data-valmsg-for="garage" data-valmsg-replace="true"></span>
                                                            </td>
                                                            <td align="center" valign="top">
                                                                מעלית<br>
                                                                  <input data-bind="value: ascenseur" class="check-box" data-val="true" data-val-required="The ascenseur field is required." id="ascenseur" name="ascenseur" type="checkbox" value="true"><input name="ascenseur" type="hidden" value="false">

                                                                <span class="field-validation-valid" data-valmsg-for="ascenseur" data-valmsg-replace="true"></span>
                                                            </td>
                                                            <td align="center" valign="top">
                                                                סורגים<br>
                                                                <input data-bind="value: bareaux" class="check-box" data-val="true" data-val-required="The bareaux field is required." id="bareaux" name="bareaux" type="checkbox" value="true"><input name="bareaux" type="hidden" value="false">

                                                                <span class="field-validation-valid" data-valmsg-for="bareaux" data-valmsg-replace="true"></span>
                                                            </td>
                                                            <td align="center" valign="top">
                                                                מיזוג<br>
                                                                <input data-bind="value: climatisation" class="check-box" data-val="true" data-val-required="The climatisation field is required." id="climatisation" name="climatisation" type="checkbox" value="true"><input name="climatisation" type="hidden" value="false">

                                                                <span class="field-validation-valid" data-valmsg-for="climatisation" data-valmsg-replace="true"></span>
                                                            </td>
                                                            <td align="center" valign="top">
                                                                ממ"ד<br>
                                                                <input data-bind="value: chambreforte" class="check-box" data-val="true" data-val-required="The chambreforte field is required." id="chambreforte" name="chambreforte" type="checkbox" value="true"><input name="chambreforte" type="hidden" value="false">

                                                                <span class="field-validation-valid" data-valmsg-for="chambreforte" data-valmsg-replace="true"></span>
                                                            </td>
                                                            <td align="center" valign="top">
                                                                מרפסת<br>
                                                                <input data-bind="value: balcon" class="check-box" data-val="true" data-val-required="The balcon field is required." id="balcon" name="balcon" type="checkbox" value="true"><input name="balcon" type="hidden" value="false">

                                                                <span class="field-validation-valid" data-valmsg-for="balcon" data-valmsg-replace="true"></span>
                                                            </td>
                                                            <td align="center" valign="top">
                                                                משופצת<br>
                                                                <input data-bind="value: renovee" class="check-box" data-val="true" data-val-required="The renovee field is required." id="renovee" name="renovee" type="checkbox" value="true"><input name="renovee" type="hidden" value="false">

                                                                <span class="field-validation-valid" data-valmsg-for="renovee" data-valmsg-replace="true"></span>
                                                            </td>
                                                            <td align="center" valign="top">
                                                                מחסן<br>
                                                                <input data-bind="value: cave" class="check-box" data-val="true" data-val-required="The cave field is required." id="cave" name="cave" type="checkbox" value="true"><input name="cave" type="hidden" value="false">

                                                                <span class="field-validation-valid" data-valmsg-for="cave" data-valmsg-replace="true"></span>
                                                            </td>
                                                            <td align="center" valign="top">
                                                                גישה לנכים<br>
                                                                <input data-bind="value: acceshandicape" class="check-box" data-val="true" data-val-required="The acceshandicape field is required." id="acceshandicape" name="acceshandicape" type="checkbox" value="true"><input name="acceshandicape" type="hidden" value="false">

                                                                <span class="field-validation-valid" data-valmsg-for="acceshandicape" data-valmsg-replace="true"></span>
                                                            </td>
                                                            <td align="center" valign="top">
                                                                חיות<br>
                                                                <input data-bind="value: animauxdomestique" class="check-box" data-val="true" data-val-required="The animauxdomestique field is required." id="animauxdomestique" name="animauxdomestique" type="checkbox" value="true"><input name="animauxdomestique" type="hidden" value="false">

                                                                <span class="field-validation-valid" data-valmsg-for="animauxdomestique" data-valmsg-replace="true"></span>
                                                            </td>
                                                            <td align="center" valign="top">
                                                                מ.שותפים<br>
                                                                <input data-bind="value: plusieursresidents" class="check-box" data-val="true" data-val-required="The plusieursresidents field is required." id="plusieursresidents" name="plusieursresidents" type="checkbox" value="true"><input name="plusieursresidents" type="hidden" value="false">

                                                                <span class="field-validation-valid" data-valmsg-for="plusieursresidents" data-valmsg-replace="true"></span>
                                                            </td>
                                                            <td align="center" valign="top">
                                                                כניסה מיידית<br>
                                                                <input data-bind="value: entreeimmediate" class="check-box" data-val="true" data-val-required="The entreeimmediate field is required." id="entreeimmediate" name="entreeimmediate" type="checkbox" value="true"><input name="entreeimmediate" type="hidden" value="false">

                                                                <span class="field-validation-valid" data-valmsg-for="entreeimmediate" data-valmsg-replace="true"></span>
                                                            </td>
                                                            <td align="center" valign="top">
                                                                כניסה גמיש<br>
                                                                <input data-bind="value: entreeflexible" class="check-box" data-val="true" data-val-required="The entreeflexible field is required." id="entreeflexible" name="entreeflexible" type="checkbox" value="true"><input name="entreeflexible" type="hidden" value="false">

                                                                <span class="field-validation-valid" data-valmsg-for="entreeflexible" data-valmsg-replace="true"></span>
                                                            </td>
                                                            <td align="right" id="td_EnterDate" valign="top" name="td_EnterDate">
                                                                כניסה מתאריך<br>
                                                                
                                                                <input data-bind="value: dateentree" class="text-box single-line" data-val="true" data-val-date="The field dateentree must be a date." id="dateentree" name="dateentree" type="datetime" value="">

                                                                <span class="field-validation-valid" data-valmsg-for="dateentree" data-valmsg-replace="true"></span>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
     </tbody>
</table>
</td>
<td>
<input name="SearchButton" id="SearchButton" type="submit" value="חפש">
</td>
</tr>
  </tbody></table>   
  </div>                      
</form>


  </div>

  */