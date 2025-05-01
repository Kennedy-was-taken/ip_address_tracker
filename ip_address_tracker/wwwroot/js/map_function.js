
// global variable
let map;

export function initMap() {

	// sets the map
	map = L.map('map').setView([52.37403, 4.88969], 13);

	// sets the marker
	let marker = L.marker([51.5, -0.09]).addTo(map);


	L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
		maxZoom: 19,
		attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a> ' +
			' <a href="https://www.openstreetmap.org/fixthemap">report problems</a>'
	}).addTo(map);
}

export function coordinates(latX, longY) {

	map.setView([latX, longY], 13);
	let marker = L.marker([latX, longY]).addTo(map);
}

export function error(message) {
	alert(message);
}

