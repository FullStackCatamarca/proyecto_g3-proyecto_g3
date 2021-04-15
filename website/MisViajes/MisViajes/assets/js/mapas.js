
const mapa_detalle = document.querySelector("#mapid");

EventListener();

function EventListener() {
	document.addEventListener('DOMContentLoaded', getServ);
}

/*id de la URL */
function getId_url() {
	let paths = window.location.pathname.split('/');
	let id = paths[paths.length-1 ];
	return id;
}
/*controller de la URL */
function getController_url() {
	let paths = window.location.pathname.split('/');
	let controller = paths[1];
	return controller;
}

/*Mapa*/
async function getServ() {
	await fetch(`/${getController_url()}/coordenadas`, {
		headers: {
			'Accept': 'application/json',
			'Content-Type': 'application/json'
		}, method: 'POST',
		body: JSON.stringify({ idServ: getId_url() })
	})
		.then(request => request.json())
		.then(result => mostrarMapa(result))
		
	
	
}

/*Mostrar Mapa*/
function mostrarMapa({ Latitud, Longitud }) {
	console.log(Latitud)
	console.log(Longitud)
	latitud = Number(Latitud);
	longitud = Number(Longitud);
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



