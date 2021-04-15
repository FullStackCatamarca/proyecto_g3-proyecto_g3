//Variables
const hospedaje_selectDpto = document.querySelector('#hospedaje_listDpto');
const hospedaje_selectLoc = document.querySelector('#hospedaje_listLoc');


//Eventos
EventListener();

function EventListener() {
    document.addEventListener('DOMContentLoaded', obtenerdpto);
    hospedaje_selectDpto.addEventListener('change', mostrarLocl);
    hospedaje_selectLoc.addEventListener('change', filtrarHospedaje);
}

//Obtiene los departamentos
async function obtenerdpto() { 
    await fetch('/Hospedajes/ObtenerDepartamentos')
        .then(request => request.json())
        .then(result => mostrarDpto(result))
        .catch(error => console.log(error));
}

//Muestra los Departamentos en el HTML
function mostrarDpto(Dpts) {
    Dpts.map(Dpto => {
        const Option = document.createElement('option');
        Option.value = Dpto.Id;
        Option.textContent = Dpto.Nombre;
        hospedaje_selectDpto.appendChild(Option);
    })
}

//Muestra las Localidades en el HTML
async function mostrarLocl() {
    //Obtiene las localidades dependiendo del dpto
    await fetch('/Hospedajes/ObtenerLocalidades/', {
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        method: 'POST',
        body: JSON.stringify({ idDepartamento: hospedaje_selectDpto.value })
    })
        .then(request => request.json())
        .then(result => Locls = result)
        .catch(error => console.log(error));

    
    limpiarHtml(hospedaje_selectLoc);
    Locls.map(Locl => {
        const Option = document.createElement('option');
        Option.value = Locl.Id;
        Option.textContent = Locl.Nombre;
        hospedaje_selectLoc.appendChild(Option);
    })



}

//Limpia El Select De Localidades Cuando Se selecciona otro Dpto
function limpiarHtml(idelement) {
    
    while (idelement.firstChild) {
        
       idelement.removeChild(idelement.firstChild);
    }
    
}


async function filtrarHospedaje() {
    await fetch('/Hospedajes/Index/', {
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        method: 'POST',
        body: JSON.stringify({ order: "0",localidad:hospedaje_selectLoc.value })
    })
}



