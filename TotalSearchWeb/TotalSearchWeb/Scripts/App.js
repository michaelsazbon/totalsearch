
function mydump(arr, level) {
    var dumped_text = "";
    if (!level) level = 0;

    var level_padding = "";
    for (var j = 0; j < level + 1; j++) level_padding += "    ";

    if (typeof (arr) == 'object') {
        for (var item in arr) {
            var value = arr[item];

            if (typeof (value) == 'object') {
                dumped_text += level_padding + "'" + item + "' ...\n";
                dumped_text += mydump(value, level + 1);
            } else {
                dumped_text += level_padding + "'" + item + "' => \"" + value + "\"\n";
            }
        }
    } else {
        dumped_text = "===>" + arr + "<===(" + typeof (arr) + ")";
    }
    return dumped_text;
}



function GetAntiForgeryToken() {
    return encodeURIComponent($('input[name=__RequestVerificationToken]').val());
}


window.onresize = function () {
    var tailles = viewport();
    $("#sliderbox").css("height", (tailles.height - 130) + 'px');
    $("#sliderbox2").css("width", tailles.width + "px");
    $("#sliderbox3").css("width", tailles.width + "px");

    $("#maincontainer").css("height", (tailles.height - 35) + 'px');
};

function viewport() {
    var e = window, a = 'inner';
    if (!('innerWidth' in window)) {
        a = 'client';
        e = document.documentElement || document.body;
    }
    return { width: e[a + 'Width'], height: e[a + 'Height'] }
}



function SliderBox(controlDiv, map) {

    var control = this;
    control.isOpen = false;

    var tailles = viewport();
    var boxwidth = Math.floor((tailles.width * 80) / 100);

    var box = document.createElement('div');
    box.id = 'sliderbox';
    //box.style.minHeight = '100%';
    //box.style.height = '100%';
    box.style.width = boxwidth + 'px';
    box.style.height = (tailles.height - 5) + 'px';
    //box.style.opacity = '0.9';
    //box.style.backgroundColor = 'white';
    //box.style.position = 'absolute';

    box.style.marginLeft = '-' + boxwidth + 'px';

    var box2 = document.createElement('div');
    box2.id = 'SearchResultsBox';

    box2.innerHTML = document.getElementById("SearchResults").innerHTML;

    document.getElementById("SearchResults").innerHTML = "";

    box.appendChild(box2);
    controlDiv.appendChild(box);

    var toggleSpan = document.createElement('span');
    toggleSpan.id = 'toggleSpan';
    toggleSpan.className = 'ClosedSpan';
    toggleSpan.style.height = '25px';
    toggleSpan.style.width = '25px';

    var toggleBtn = document.createElement('span');
    toggleBtn.id = 'toggleBtn';
    toggleBtn.style.height = '25px';
    toggleBtn.style.width = '25px';
    toggleBtn.className = 'Closed';
    //toggleBtn.style.display = 'inline';
    //toggleBtn.style.position = 'relative';
    //toggleBtn.style.top = '0px';
    //toggleBtn.style.right = '-40px';
    //toggleBtn.style.backgroundColor = 'white';
    //toggleBtn.type = 'button';
    //toggleBtn.value = 'Close';
    toggleSpan.appendChild(toggleBtn);
    box.appendChild(toggleSpan);

    $(document).on('click', '#toggleBtn', function () {
        if (control.isOpen) {

            $("#sliderbox").animate({
                "marginLeft": "-=" + boxwidth + 'px'
            }, {
                duration: 300,
                step: function () {
                    google.maps.event.trigger(map, 'resize');
                }
            });
            $("#SearchResultsBox").css("display", "none");
            control.isOpen = false;
            //toggleBtn.value = 'Open';
            toggleBtn.className = "Closed";
            toggleSpan.className = "ClosedSpan";

            //$('#mypanel').panel().panel('close');

        } else {
            $("#sliderbox").animate({
                "marginLeft": "+=" + boxwidth + 'px'
            }, {
                duration: 300,
                step: function () {
                    google.maps.event.trigger(map, 'resize');
                }
            });
            $("#SearchResultsBox").css("display", "block");
            control.isOpen = true;
            toggleBtn.className = "Opened";
            toggleSpan.className = "OpenedSpan";

            //$('#mypanel').panel().panel('open', '');
            //toggleBtn.value = 'Close';
        };
    });
}


function SliderBox2(controlDiv, map) {

    var control = this;
    control.isOpen = false;

    var tailles = viewport();
    var boxheight = Math.floor((tailles.height * 30) / 100);

    var box = document.createElement('div');
    box.id = 'sliderbox2';
    //box.style.minHeight = '100%';
    //box.style.height = boxheight;
    box.style.width = tailles.width + "px";
    //box.style.height = '300px';
    //box.style.backgroundColor = 'white';
    //box.style.opacity = '0.9';
    //box.style.position = 'relative';

    box.style.marginTop = '-' + boxheight + 'px';

    var box2 = document.createElement('div');
    box2.id = 'searchFormBox';


    //box2.append($('#searchForm').html());

    box2.appendChild(document.getElementById("searchForm"));

    //document.getElementById("searchForm").innerHTML = "";

    box.appendChild(box2);
    controlDiv.appendChild(box);

    //$("#searchFormBox").appendTo("#searchForm");

    var toggleSpan = document.createElement('span');
    toggleSpan.id = 'toggleSpan2';
    toggleSpan.className = 'ClosedSpan2';
    toggleSpan.style.height = '25px';
    toggleSpan.style.width = '25px';


    var toggleBtn = document.createElement('span');
    toggleBtn.id = 'toggleBtn2';
    toggleBtn.style.height = '25px';
    toggleBtn.style.width = '25px';
    toggleBtn.className = 'Closed2';
    //toggleBtn.style.display = 'inline';
    //toggleBtn.style.position = 'relative';
    //toggleBtn.style.top = '0px';
    //toggleBtn.style.right = '-40px';
    //toggleBtn.style.backgroundColor = 'black';
    //toggleBtn.type = 'button';
    //toggleBtn.value = 'Close';
    toggleSpan.appendChild(toggleBtn);
    box.appendChild(toggleSpan);



    //$('#sliderbox2').trigger('create');

    $(document).on('click', '#toggleBtn2', function () {
        if (control.isOpen) {

            //box.style.height = '95%';
            $("#sliderbox").css("height", (viewport().height) + 'px');
            //$("#searchFormBox").slideToggle(300);
            $("#sliderbox2").animate({
                "marginTop": "-=" + boxheight + 'px'
            }, {
                duration: 300,
                step: function () {
                    google.maps.event.trigger(map, 'resize');
                }
            });
            $("#searchFormBox").css("display", "none");
            control.isOpen = false;
            //toggleBtn.value = 'Open';
            toggleBtn.className = "Closed2";
            toggleSpan.className = "ClosedSpan2";


        } else {

            box.style.height = (boxheight - 1) + 'px';
            $("#sliderbox").css("height", (viewport().height - 130) + 'px');
            $("#sliderbox2").animate({
                "marginTop": "+=" + boxheight + 'px'
            }, {
                duration: 300,
                step: function () {
                    google.maps.event.trigger(map, 'resize');
                }
            });
            $("#searchFormBox").css("display", "block");

            control.isOpen = true;
            toggleBtn.className = "Opened2";
            toggleSpan.className = "OpenedSpan2";

            //toggleBtn.value = 'Close';
        };
    });
}


function SliderBox3(controlDiv, map) {

    var control = this;
    control.isOpen = false;

    var tailles = viewport();

    var box = document.createElement('div');
    box.id = 'sliderbox3';
    //box.style.minHeight = '100%';
    box.style.height = '3px';
    //box.style.width = '300px';
    //box.style.height = '500px';
    //box.style.backgroundColor = 'white';

    box.style.width = tailles.width + "px";
    //box.style.position = 'absolute';


    var box0 = document.createElement('div');
    box0.id = 'statsContainer';
    box0.style.margin = "0 auto";
    box0.style.textAlign = "center";
    box0.style.width = "1210px";
    box0.style.display = "none";

    var box2 = document.createElement('div');
    box2.id = 'statsBox';
    box2.style.minWidth = "400px";
    box2.style.width = "400px";
    box2.style.height = "240px";
    box2.style.margin = "0";
    box2.style.marginLeft = "3px";
    box2.style.direction = "ltr";
    box2.style.styleFloat = "left";
    box2.style.cssFloat = "left";


    var box3 = document.createElement('div');
    box3.id = 'statsBox2';
    box3.style.minWidth = "400px";
    box3.style.width = "400px";
    box3.style.height = "240px";
    box3.style.margin = "0";
    box3.style.marginLeft = "3px";
    box3.style.direction = "ltr";
    box3.style.styleFloat = "left";
    box3.style.cssFloat = "left";

    var box4 = document.createElement('div');
    box4.id = 'statsBox3';
    box4.style.minWidth = "400px";
    box4.style.width = "400px";
    box4.style.height = "240px";
    box4.style.margin = "0";
    box4.style.marginLeft = "3px";
    box4.style.direction = "ltr";
    box4.style.styleFloat = "left";
    box4.style.cssFloat = "left";

    //box2.innerHTML = document.getElementById("StatsChart").innerHTML;



    box0.appendChild(box2);
    box0.appendChild(box3);
    box0.appendChild(box4);
    box.appendChild(box0);
    controlDiv.appendChild(box);

    var toggleSpan = document.createElement('span');
    toggleSpan.id = 'toggleSpan3';
    toggleSpan.className = 'ClosedSpan3';
    toggleSpan.style.height = '25px';
    toggleSpan.style.width = '25px';
    //toggleSpan.className = 'Closed3';

    var toggleBtn = document.createElement('span');
    toggleBtn.id = 'toggleBtn3';
    toggleBtn.style.height = '25px';
    toggleBtn.style.width = '25px';
    toggleBtn.className = 'Closed3';

    toggleSpan.appendChild(toggleBtn);
    box.appendChild(toggleSpan);





    $(document).on('click', '#toggleBtn3', function () {
        if (control.isOpen) {


            $("#sliderbox3").animate({
                "height": "-=243px"
            }, {
                duration: 300,
                step: function () {
                    google.maps.event.trigger(map, 'resize');
                }
            });
            $("#statsContainer").css("display", "none");
            //$("#statsBox2").css("display", "none");
            //$("#statsBox3").css("display", "none");
            $("#sliderbox").css("height", (viewport().height) + 'px');
            control.isOpen = false;
            //toggleBtn.value = 'Open';
            toggleBtn.className = "Closed3";
            toggleSpan.className = "ClosedSpan3";

        } else {

            $("#sliderbox3").animate({
                "height": "+=243px"
            }, {
                duration: 300,
                step: function () {
                    google.maps.event.trigger(map, 'resize');
                }
            });
            $("#statsContainer").css("display", "block");
            //$("#statsBox2").css("display", "block");
            //$("#statsBox3").css("display", "block");
            $("#sliderbox").css("height", (viewport().height - 260) + 'px');
            control.isOpen = true;
            toggleBtn.className = "Opened3";
            toggleSpan.className = "OpenedSpan3";

            //toggleBtn.value = 'Close';
        };
    });
}

//        function SliderBox4(controlDiv, map) {

//            var control = this;
//            control.isOpen = true;

//            var box = document.createElement('div');
//            box.id = 'sliderbox4';
//            box.style.minHeight = '100%';
//            //box.style.height = '100%';
//            box.style.width = '300px';
//            box.style.height = '500px';
//            box.style.backgroundColor = 'white';
//            //box.style.position = 'absolute';
//            controlDiv.appendChild(box);

//            var toggleBtn = document.createElement('span');
//            toggleBtn.id = 'toggleBtn4';
//            toggleBtn.style.height = '25px';
//            toggleBtn.style.width = '25px';
//            toggleBtn.className = 'Opened4';
//            //toggleBtn.style.display = 'inline';
//            //toggleBtn.style.position = 'relative';
//            //toggleBtn.style.top = '0px';
//            //toggleBtn.style.right = '-40px';
//            //toggleBtn.style.backgroundColor = 'white';
//            //toggleBtn.type = 'button';
//            //toggleBtn.value = 'Close';
//            box.appendChild(toggleBtn);

//            $(document).on('click', '#toggleBtn4', function () {
//                if (control.isOpen) {

//                    $("#sliderbox4").animate({
//                        "marginLeft": "-=275px"
//                    }, {
//                        duration: 300,
//                        step: function () {
//                            google.maps.event.trigger(map, 'resize');
//                        }
//                    });
//                    control.isOpen = false;
//                    //toggleBtn.value = 'Open';
//                    toggleBtn.className = "Closed4";
//                } else {
//                    $("#sliderbox4").animate({
//                        "marginLeft": "+=275px"
//                    }, {
//                        duration: 300,
//                        step: function () {
//                            google.maps.event.trigger(map, 'resize');
//                        }
//                    });
//                    control.isOpen = true;
//                    toggleBtn.className = "Opened4";
//                    //toggleBtn.value = 'Close';
//                };
//            });
//        }

map = null;
markers = [];

function initialize() {





    var mapOptions = {
        zoom: 12,
        //center: new google.maps.LatLng(31.780286, 35.186039),
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        panControl: false,
        panControlOptions: {
            position: google.maps.ControlPosition.RIGHT_BOTTOM
        },
        zoomControl: true,
        zoomControlOptions: {
            style: google.maps.ZoomControlStyle.LARGE,
            position: google.maps.ControlPosition.RIGHT_BOTTOM
        },
        scaleControl: true,
        scaleControlOptions: {
            position: google.maps.ControlPosition.BOTTOM_LEFT
        },
        streetViewControl: true,
        streetViewControlOptions: {
            position: google.maps.ControlPosition.RIGHT_BOTTOM
        },
        mapTypeControl: false,
        mapTypeControlOptions: {
            position: google.maps.ControlPosition.RIGHT_BOTTOM
        }
    };
    map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);


    var initialLocation;

    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {

            initialLocation = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
            map.setCenter(initialLocation);

            var usermarker = new google.maps.Marker({
                position: new google.maps.LatLng(position.coords.latitude, position.coords.longitude),
                map: map,
                title: "Moi",
                icon: "/Content/images/blue_MarkerM.png"
            });

        }, function () {
            initialLocation = new google.maps.LatLng(31.780286, 35.186039);
            map.setCenter(initialLocation);
        });
    }
        // Browser doesn't support Geolocation
    else {
        initialLocation = new google.maps.LatLng(31.780286, 35.186039);
        map.setCenter(initialLocation);
    }




    /*var ctaLayer = new google.maps.KmlLayer({
        url: 'http://127.0.0.1:3538/Home/LocationLayer'
    });
    ctaLayer.setMap(map);
    */
    //var token = '__RequestVerificationToken=' + GetAntiForgeryToken();
    //var KmlUrl = '/Home/LocationLayer?Page=@Model.Page&' + token + '&api_key=FEWRRHVCX1234534JGHUYi&zone=@Model.zone&ville=@Model.ville&quartier=@Model.quartier&typebien=@Model.typebien&nombrechambremin=@Model.nombrechambremin&nombrechambremax=@Model.nombrechambremax&prixmin=@Model.prixmin&prixmax=@Model.prixmax&etagemin=@Model.etagemin&etagemax=@Model.etagemax&superficiemin=@Model.superficiemin&superficiemax=@Model.superficiemax&meublee=@Model.meublee&garage=@Model.garage&ascenseur=@Model.ascenseur&bareaux=@Model.bareaux&climatisation=@Model.climatisation&chambreforte=@Model.chambreforte&balcon=@Model.balcon&renovee=@Model.renovee&cave=@Model.cave&acceshandicape=@Model.acceshandicape&animauxdomestique=@Model.animauxdomestique&plusieursresidents=@Model.plusieursresidents&entreeimmediate=@Model.entreeimmediate&entreeflexible=@Model.entreeflexible&dateentree=@Model.dateentree&SearchButton=@Model.SearchButton';
    //var KmlUrl = '../../Scripts/KML_Samples.kml';

    //            var xmlhttp = new XMLHttpRequest();
    //            xmlhttp.open("GET", KmlUrl, false);
    //            //xmlhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    //            xmlhttp.send(null);

    //var myParser = new geoXML3.parser({ map: map });
    //myParser.parse(KmlUrl); 


    var sliderBoxDiv = document.createElement('div');
    var sliderBoxControl = new SliderBox(sliderBoxDiv, map);

    sliderBoxDiv.index = -500;
    map.controls[google.maps.ControlPosition.LEFT].push(sliderBoxDiv);


    var sliderBoxDiv2 = document.createElement('div');
    var sliderBoxControl2 = new SliderBox2(sliderBoxDiv2, map);

    sliderBoxDiv2.index = -1000;
    map.controls[google.maps.ControlPosition.TOP_LEFT].push(sliderBoxDiv2);


    var sliderBoxDiv3 = document.createElement('div');
    var sliderBoxControl3 = new SliderBox3(sliderBoxDiv3, map);

    sliderBoxDiv3.index = -1500;
    map.controls[google.maps.ControlPosition.LEFT_BOTTOM].push(sliderBoxDiv3);




    //            var sliderBoxDiv4 = document.createElement('div');
    //            var sliderBoxControl4 = new SliderBox4(sliderBoxDiv4, map);

    //            sliderBoxDiv4.index = -2000;
    //            map.controls[google.maps.ControlPosition.TOP].push(sliderBoxDiv4);
}

google.maps.event.addDomListener(window, 'load', initialize);


function InitialiseStats() {


    if (document.getElementById("statsContainer") == null) {

        setTimeout('InitialiseStats()', 1000);
        return;
    }

    var colors = Highcharts.getOptions().colors,
    categories = ['MSIE', 'Firefox', 'Chrome', 'Safari', 'Opera'],
    name = 'Browser brands',
    data = [{
        y: 55.11,
        color: colors[0],
        drilldown: {
            name: 'MSIE versions',
            categories: ['MSIE 6.0', 'MSIE 7.0', 'MSIE 8.0', 'MSIE 9.0'],
            data: [10.85, 7.35, 33.06, 2.81],
            color: colors[0]
        }
    }, {
        y: 21.63,
        color: colors[1],
        drilldown: {
            name: 'Firefox versions',
            categories: ['Firefox 2.0', 'Firefox 3.0', 'Firefox 3.5', 'Firefox 3.6', 'Firefox 4.0'],
            data: [0.20, 0.83, 1.58, 13.12, 5.43],
            color: colors[1]
        }
    }, {
        y: 11.94,
        color: colors[2],
        drilldown: {
            name: 'Chrome versions',
            categories: ['Chrome 5.0', 'Chrome 6.0', 'Chrome 7.0', 'Chrome 8.0', 'Chrome 9.0',
                    'Chrome 10.0', 'Chrome 11.0', 'Chrome 12.0'],
            data: [0.12, 0.19, 0.12, 0.36, 0.32, 9.91, 0.50, 0.22],
            color: colors[2]
        }
    }, {
        y: 7.15,
        color: colors[3],
        drilldown: {
            name: 'Safari versions',
            categories: ['Safari 5.0', 'Safari 4.0', 'Safari Win 5.0', 'Safari 4.1', 'Safari/Maxthon',
                    'Safari 3.1', 'Safari 4.1'],
            data: [4.55, 1.42, 0.23, 0.21, 0.20, 0.19, 0.14],
            color: colors[3]
        }
    }, {
        y: 2.14,
        color: colors[4],
        drilldown: {
            name: 'Opera versions',
            categories: ['Opera 9.x', 'Opera 10.x', 'Opera 11.x'],
            data: [0.12, 0.37, 1.65],
            color: colors[4]
        }
    }];

    function setChart(name, categories, data, color) {
        chart.xAxis[0].setCategories(categories, false);
        chart.series[0].remove(false);
        chart.addSeries({
            name: name,
            data: data,
            color: color || 'white'
        }, false);
        chart.redraw();
    }

    var chart = $('#statsBox').highcharts({
        chart: {
            type: 'column'
        },
        title: {
            text: 'Browser market share, April, 2011'
        },
        xAxis: {
            categories: categories
        },
        yAxis: {
            title: {
                text: 'Total percent market share'
            }
        },
        plotOptions: {
            column: {
                cursor: 'pointer',
                point: {
                    events: {
                        click: function () {
                            var drilldown = this.drilldown;
                            if (drilldown) { // drill down
                                setChart(drilldown.name, drilldown.categories, drilldown.data, drilldown.color);
                            } else { // restore
                                setChart(name, categories, data);
                            }
                        }
                    }
                },
                dataLabels: {
                    enabled: true,
                    color: colors[0],
                    style: {
                        fontWeight: 'bold'
                    },
                    formatter: function () {
                        return this.y + '%';
                    }
                }
            }
        },
        tooltip: {
            formatter: function () {
                var point = this.point,
                s = this.x + ':<b>' + this.y + '% market share</b><br/>';
                if (point.drilldown) {
                    s += 'Click to view ' + point.category + ' versions';
                } else {
                    s += 'Click to return to browser brands';
                }
                return s;
            }
        },
        series: [{
            name: name,
            data: data,
            color: 'white'
        }],
        exporting: {
            enabled: false
        }
    })
.highcharts(); // return chart




    $('#statsBox2').highcharts({
        chart: {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false
        },
        title: {
            text: 'Browser market shares at a specific website, 2010'
        },
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
        },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: true,
                    color: '#000000',
                    connectorColor: '#000000',
                    format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                }
            }
        },
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


    $('#statsBox3').highcharts({
        title: {
            text: 'Monthly Average Temperature',
            x: -20 //center
        },
        xAxis: {
            categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun',
            'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']
        },
        yAxis: {
            title: {
                text: 'Temperature (°C)'
            },
            plotLines: [{
                value: 0,
                width: 1,
                color: '#808080'
            }]
        },
        tooltip: {
            valueSuffix: '°C'
        },
        legend: {
            layout: 'vertical',
            align: 'right',
            verticalAlign: 'middle',
            borderWidth: 0
        },
        series: [{
            name: 'Tokyo',
            data: [7.0, 6.9, 9.5, 14.5, 18.2, 21.5, 25.2, 26.5, 23.3, 18.3, 13.9, 9.6]
        }, {
            name: 'New York',
            data: [-0.2, 0.8, 5.7, 11.3, 17.0, 22.0, 24.8, 24.1, 20.1, 14.1, 8.6, 2.5]
        }, {
            name: 'Berlin',
            data: [-0.9, 0.6, 3.5, 8.4, 13.5, 17.0, 18.6, 17.9, 14.3, 9.0, 3.9, 1.0]
        }, {
            name: 'London',
            data: [3.9, 4.2, 5.7, 8.5, 11.9, 15.2, 17.0, 16.6, 14.2, 10.3, 6.6, 4.8]
        }]
    });
}


function toggleBounce(markerindex) {

    if (typeof (markers[markerindex]) != "undefined") {

        if (markers[markerindex].getAnimation() != null) {
            markers[markerindex].setAnimation(null);
            markers[markerindex].infoWindow.close();
        } else {
            markers[markerindex].setAnimation(google.maps.Animation.BOUNCE);
            markers[markerindex].infoWindow.open(map, markers[markerindex]);
        }
    }
}


//        window.setInterval(function () {

//            if (document.getElementById("statsContainer") != null) {
//                InitialiseStats();
//            }
//        }, 1000);

$(document).ready(function () {

    //            setTimeout(function () {
    //                ko.applyBindings(new LocationViewModel());
    //            }, 1000);

    viewModel = new MyViewModel();

    setTimeout(function () {
        ko.applyBindings(viewModel);

        //$('#toggleBtn').trigger('click');
        //$('#toggleBtn2').trigger('click');
        //$('#toggleBtn3').trigger('click');

    }, 1000);


    $(document).on('swipeup', '#searchForm', function () {
        $('#toggleBtn2').trigger('click');
    });

    $(document).on('swipedown', '#header', function () {
        $('#toggleBtn2').trigger('click');
    });

    $(document).on('swipe', '#SearchResultsBox', function () {
        $('#toggleBtn').trigger('click');
    });

    var tailles = viewport();

    $('.inside').css({
        'height': (tailles.height - 80) + 'px'
    });
    $(window).resize(function () {
        $('.inside').css({
            'height': (tailles.height - 80) + 'px'
        });
    });

    /*$(document).on('swipedown', '#searchForm', function () {
        $('#toggleBtn2').trigger('click');
    });*/

});

window.onload = function () {

    var tailles = viewport();
    $("#maincontainer").css("height", (tailles.height - 35) + 'px');

    InitialiseStats();

    //$("#searchForm").on('click', function () {
    //    alert("swiped up");
    //});



    //$("#searchForm").on('click', function () {
    //    alert("swiped down");
    //});


    //TestApi();

    //TestApi2();

    //setTimeout(function () {
    //    ko.applyBindings(new LocationViewModel());
    //}, 1000);

    //            setTimeout(function () {
    //                var markers = appartementsmarkers;

    //                for (var i = 0; i < markers.length; i++) {
    //                    if (markers[i].get("id") == "239") {
    //                        alert("OK");
    //                        alert(console.log(markers[i]));
    //                        break;
    //                    }
    //                }
    //            }, 5000);

    //var token = '__RequestVerificationToken=' + GetAntiForgeryToken();
    //var KmlUrl = '/Home/LocationLayer?' + token + '&api_key=FEWRRHVCX1234534JGHUYi&zone=@Model.zone&ville=@Model.ville&quartier=@Model.quartier&typebien=@Model.typebien&nombrechambremin=@Model.nombrechambremin&nombrechambremax=@Model.nombrechambremax&prixmin=@Model.prixmin&prixmax=@Model.prixmax&etagemin=@Model.etagemin&etagemax=@Model.etagemax&superficiemin=@Model.superficiemin&superficiemax=@Model.superficiemax&meublee=@Model.meublee&garage=@Model.garage&ascenseur=@Model.ascenseur&bareaux=@Model.bareaux&climatisation=@Model.climatisation&chambreforte=@Model.chambreforte&balcon=@Model.balcon&renovee=@Model.renovee&cave=@Model.cave&acceshandicape=@Model.acceshandicape&animauxdomestique=@Model.animauxdomestique&plusieursresidents=@Model.plusieursresidents&entreeimmediate=@Model.entreeimmediate&entreeflexible=@Model.entreeflexible&dateentree=@Model.dateentree&SearchButton=%D7%97%D7%A4%D7%A9';
    //var KmlUrl = '../../Scripts/KML_Samples.kml';

    //var xmlhttp = new XMLHttpRequest();
    //xmlhttp.open("GET", KmlUrl, false);
    //xmlhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    //xmlhttp.send(null);


};


/*window.onload = function () {

    // Here's my data model
    var ViewModel = function (first, last) {
        this.firstName = ko.observable(first);
        this.lastName = ko.observable(last);

        this.fullName = ko.computed(function () {
            // Knockout tracks dependencies automatically. It knows that fullName depends on firstName and lastName, because these get called when evaluating fullName.
            return this.firstName() + " " + this.lastName();
        }, this);
    };

    ko.applyBindings(new ViewModel("Planet", "Earth")); // This makes Knockout get to work

};

/* $(function () {
$('#statsBox').highcharts({
    chart: {
        type: 'bar'
    },
    title: {
        text: 'Historic World Population by Region'
    },
    subtitle: {
        text: 'Source: Wikipedia.org'
    },
    xAxis: {
        categories: ['Africa', 'America', 'Asia', 'Europe', 'Oceania'],
        title: {
            text: null
        }
    },
    yAxis: {
        min: 0,
        title: {
            text: 'Population (millions)',
            align: 'high'
        },
        labels: {
            overflow: 'justify'
        }
    },
    tooltip: {
        valueSuffix: ' millions'
    },
    plotOptions: {
        bar: {
            dataLabels: {
                enabled: true
            }
        }
    },
    legend: {
        layout: 'vertical',
        align: 'right',
        verticalAlign: 'top',
        x: -100,
        y: 100,
        floating: true,
        borderWidth: 1,
        backgroundColor: '#FFFFFF',
        shadow: true
    },
    credits: {
        enabled: false
    },
    series: [{
        name: 'Year 1800',
        data: [107, 31, 635, 203, 2]
    }, {
        name: 'Year 1900',
        data: [133, 156, 947, 408, 6]
    }, {
        name: 'Year 2008',
        data: [973, 914, 4054, 732, 34]
    }]
});
});*/


//function Toggle(trid) {
//    document.getElementById(trid).style.display = "table-row";
//}
