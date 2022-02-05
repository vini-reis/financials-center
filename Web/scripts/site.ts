import bootstrap = require('bootstrap');

const toast = new bootstrap.Toast(document.getElementById("main-toast"), {
    animation: true,
    autohide: true,
    delay: 5000,
});

export interface Result {
    success: boolean;
    message: string;
    internalMessage: string;
    data: any;
}

export interface Form {
    action: string,
    method: string,
    enctype: string,
}

export const ColorEnum = {
    Danger: 'danger',
    Success: 'success',
    Primary: 'primary',
    Secondary: 'secondary',
    Info: 'info',
}

export function Init() {

}

export function ShowToast(message : string, color : string) {
    $("#main-toast-body").empty().html(message);
    $("#main-toast").removeClass().addClass(`toast align-items-center text-white border-0 ml-3 mb-3 bg-${color || ColorEnum.Primary}`);
    toast.show();
}

export function SubmitForm(form: HTMLFormElement, callback: (res: Result) => any) {
    jQuery.ajax({
        url: form.action,
        method: form.method,
        dataType: "json",
        data: jQuery(form).serializeArray(),
        success: callback || DefaultCallbackSubmitForm,
        error: function (jqXHR, status, error) {
            ShowToast(`${status}: ${error}`, ColorEnum.Danger);
        }
    });
}

export function DefaultCallbackSubmitForm(res: Result) {
    if (!res.success)
        ShowToast(res.message, ColorEnum.Danger);
    else
        ShowToast("Finished!", ColorEnum.Success);
}

export function ReloadCallback(res) {
    if (!res.success)
        ShowToast(res.message, ColorEnum.Danger);
    else
        location.reload();
}
