// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

var toast = null;

const ColorEnum = {
    Danger: 'danger',
    Success: 'success',
    Primary: 'primary',
    Secondary: 'secondary',
    Info: 'info',
}

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
    $("#main-toast").removeClass().addClass(`toast align-items-center text-white border-0 ml-3 mb-3 bg-${color || ColorEnum.Primary}`);
    toast.show();
}

function SubmitForm(form, callback) {
    $.ajax({
        url: form.action,
        method: form.method,
        dataType: "json",
        data: form.serializeArray(),
        success: callback || DefaultCallbackSubmitForm,
        error: function (jqXHR, status, error) {
            ShowToast(`${status}: ${error}`, ColorEnum.Danger);
        }
    });
}

function DefaultCallbackSubmitForm(res) {
    if (!res.success)
        ShowToast(res.message, ColorEnum.Danger);
    else
        ShowToast("Finished!", ColorEnum.Success);
}

function ReloadCallback(res) {
    if (!res.success)
        ShowToast(res.message, ColorEnum.Danger);
    else
        location.reload();
}
