"use strict";
//Var
const SelectDepartamentos = document.querySelector('#selectDepartamentos');
const SelectLocalidades = document.querySelector('#selectLocalidades');
const BtnConsultarClima = document.querySelector('#consultarClima');
const informacionClima = document.querySelector('#informacion-clima');

let localidades;
//Eventos

    BtnConsultarClima.addEventListener('click', obtenerLocalidad);

//Funciones

//Muestra los Departamentos en el HTML
function mostrarDpto(Dpts){
    Dpts.map(Dpto => {
        const Option = document.createElement('option');
        Option.value = Dpto.ID;
        Option.textContent = Dpto.Nombre ;
        SelectDepartamentos.appendChild(Option);
    })
}

//Muestra las Localidades en el HTML
function mostrarLocl(Locls) {
    localidades = Locls;
    limpiarHtml(SelectLocalidades);
    Locls.map(Locl => {
        const Option = document.createElement('option');
        Option.value = Locl.ID;
        Option.textContent = Locl.Nombre;
        SelectLocalidades.appendChild(Option);
    })
}


//Spinner
function spinner() {
    limpiarHtml(informacionClima);
    const divSpinner = document.createElement('div');
    divSpinner.classList.add('sk-circle');

    divSpinner.innerHTML = `
  <div class="sk-circle1 sk-child"></div>
  <div class="sk-circle2 sk-child"></div>
  <div class="sk-circle3 sk-child"></div>
  <div class="sk-circle4 sk-child"></div>
  <div class="sk-circle5 sk-child"></div>
  <div class="sk-circle6 sk-child"></div>
  <div class="sk-circle7 sk-child"></div>
  <div class="sk-circle8 sk-child"></div>
  <div class="sk-circle9 sk-child"></div>
  <div class="sk-circle10 sk-child"></div>
  <div class="sk-circle11 sk-child"></div>
  <div class="sk-circle12 sk-child"></div>
    `;

    informacionClima.appendChild(divSpinner);
}

 //Limpia El Select De Localidades Cuando Se selecciona otro Dpto
function limpiarHtml(idelement) { 
    while (idelement.firstChild) {
        idelement.removeChild(idelement.firstChild);
    }
}

//Obtenemos los datos de la Localidad Seleccionada  y extraemos su lat y long
function obtenerLocalidad() {
    const localidadID = parseInt(SelectLocalidades.value);
    let localidadSelect = localidades.find((loc)=> loc.ID === localidadID); //Buscamos la localidad Seleccionada y la retornamos en localidadSelect
    
    //Consultar Clima
    getClima(localidadSelect);
    
}

const convertTemp = x => x - 273.15; //Convierte kelvins a celsius  K => C°

//Ocupamos el Api de OpenWeatherMap
async function getClima({ Latitud, Longitud, Nombre }) {
   
     // const latitud = -28.406547;
     // const longitud = -65.805996;
    const key = '4f3a4ab942697c944a4bf3a501ccc131';
    const url = `https://api.openweathermap.org/data/2.5/weather?lat=${Latitud}&lon=${Longitud}&appid=${key}`;
    spinner();
    await fetch(url)
        .then(request => request.json())
        .then(result =>  mostrarTemperatura(result,Nombre))


}

function mostrarTemperatura({ weather: [{ icon }], main: { temp, temp_max, temp_min }, name },Nombre){
    limpiarHtml(informacionClima);
   
    let iconUrl = `http://openweathermap.org/img/wn/${icon}@2x.png`;
   
    const lugar = document.createElement('h1');

    const iconClima = document.createElement('img');

    iconClima.src =  iconUrl;
    iconClima.alt = "Icono Del Estado del Clima"
    iconClima.id = "iconClima";

    const lugarAproximado = document.createElement('p');

    const temperatura = document.createElement('h2');

    const min = document.createElement('p');

    const max = document.createElement('p');
    
    
    if (Nombre.toLowerCase() === name.toLowerCase()) {  // Comparamos el nombre seleccionado con el nombre que devuelve la Api, si los nombres son diferentes Mustra la Ubicacion Aproximada.
        lugar.textContent = `${name}`;
    } else {
        lugar.textContent = `${name}`;

        lugarAproximado.textContent = '*Ubicación Aproximada';
    }

   

    lugar.classList.add('ClimaText');

    lugarAproximado.classList.add('text-black');

    temperatura.textContent = `${Math.round(convertTemp(temp))}C°`;

    temperatura.classList.add('text-black');

    min.textContent = `Min: ${Math.round(convertTemp(temp_min))}C°`;

    min.classList.add('m-0', 'text-black');

    max.textContent = `Max: ${Math.round(convertTemp(temp_max))}C°`;

    max.classList.add('m-0', 'text-black');

    informacionClima.appendChild(iconClima);
    informacionClima.appendChild(lugar);
    informacionClima.appendChild(lugarAproximado);
    informacionClima.appendChild(temperatura);
    informacionClima.appendChild(min);
    informacionClima.appendChild(max);

    setTimeout(() => {
        limpiarHtml(informacionClima);
    },18000)
}





