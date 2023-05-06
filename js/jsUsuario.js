function validarGuardado() {
    let respuesta = "";
    if ($("#idUsuario").val().trim() == "")
        respuesta += "\n{idUsuario}";

    if ($("#nombre").val().trim() == "")
        respuesta += "\n{nombre}";

    if ($("#contrasena").val().trim() == "")
        respuesta += "\n{contrasena}";

    if (respuesta != "")
        alert("Los siguientes campos no pueden estar vacios: " + respuesta);
}