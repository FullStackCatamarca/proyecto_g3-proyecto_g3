
const mapa_detalle = document.querySelector("#mapid");

EventListener();

function EventListener() {
    document.addEventListener('DOMContentLoaded', mostrarMapa);
}


/*Mapa*/
function mostrarMapa(e, latitud = -28.46688, longitud = -65.76299) {
	e.preventDefault();
	var mymap = L.map('mapid').setView([latitud, longitud], 17);

	L.tileLayer('https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpejY4NXVycTA2emYycXBndHRqcmZ3N3gifQ.rJcFIG214AriISLbB6B5aw', {
		maxZoom: 18,
		attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors, ' +
			'Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
		id: 'mapbox/streets-v11',
		tileSize: 512,
		zoomOffset: -1
	}).addTo(mymap);

	L.marker([latitud, longitud]).addTo(mymap)
		.bindPopup("<b>Te Esperamos!!</b><br />").openPopup();
}




