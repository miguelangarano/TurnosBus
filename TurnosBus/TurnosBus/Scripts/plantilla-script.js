function onClickSalir() {
    console.log("Salir");
    window.location.assign('/Home/Login');
}

document.getElementById("option1").addEventListener("click", function () {
    console.log("Horarios y Unidades");
    window.location.assign('/Horario/Index');
});

document.getElementById("option2").addEventListener("click", function () {
    console.log("Reservar un Turno");
    window.location.assign('/Turno/Create');
});

document.getElementById("option3").addEventListener("click", function () {
    console.log("Cancelar Turno");
    window.location.assign('/Cancelar/Index');
});