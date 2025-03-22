

export function initMap() {

    coordinates("", "");
}


function coordinates(laxX, latY) {  

    if ( (laxX == "" || latY == "") || (laxX == " " || latY == " ") ) {
        var map = L.map('map').setView([51.505, -0.09], 13);
    }

    else {
        var map = L.map('map').setView([laxX, latY], 13);
    }

    L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 19,
        attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a> ' +
            ' < a href="https://www.openstreetmap.org/fixthemap">report problems</a>'
    }).addTo(map);
}

