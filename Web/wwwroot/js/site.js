// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

var toast = null;

function Init() {
    // Initializes the toast
    toast = new bootstrap.Toast(document.getElementById("main-toast"), {
        animation: true,
        autohide: true,
        delay: 5000,
    });
}

function ShowToast(message, color) {
    $("#main-toast-body").empty().html(message);
    $("#main-toast").removeClass().addClass(`toast align-items-center text-white border-0 ml-3 mb-3 bg-${color || 'primary'}`);
    toast.show();
}
