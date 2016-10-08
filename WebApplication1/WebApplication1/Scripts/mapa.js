

function initialize() {
    var myLatlng = new google.maps.LatLng(50.019837, 21.988111);
    var mapOptions = {
        zoom: 11,
        center: myLatlng
    };

    var map = new google.maps.Map(document.getElementById('mapa'), mapOptions);

    var contentString = '<div id="content">' +
        '<div id="siteNotice">' +
        '</div>' +
        '<h1 id="firstHeading" class="firstHeading">Politechnika Rzeszowska im. Ignacego Łukasiewicza</h1>' +
        '<div id="bodyContent">' +
        '<p><b>PRZ RZESZÓW</b><br/>35-959 Rzeszów<br/>al. Powstańców Warszawy 12 </p>' +
        '</div>' +
        '</div>';

    var infowindow = new google.maps.InfoWindow({
        content: contentString
    });

    var marker = new google.maps.Marker({
        position: myLatlng,
        map: map,
        title: 'Politechnika Rzeszowska'
    });
    google.maps.event.addListener(marker, 'click', function () {
        infowindow.open(map, marker);
    });
}

google.maps.event.addDomListener(window, 'load', initialize);
