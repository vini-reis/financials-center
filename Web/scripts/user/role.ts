import * as utils from '../site'

$(document).ready(function () {
    console.log('opened script!');
})

function OpenRoleModal(id: utils.Form) {
    jQuery('#modal-role').modal('show');
}

$('#form-role').on('submit', function (event) {
    event.preventDefault();
    event.stopImmediatePropagation();
    debugger

    utils.SubmitForm(this as HTMLFormElement, SaveRoleCallback);
})

function SaveRoleCallback(res: utils.Result) {
    console.log(res);

    if (!res.success)
        utils.ShowToast(res.message, utils.ColorEnum.Danger);
    else {
        utils.ShowToast('Successfully saved!', utils.ColorEnum.Success);
        //location.reload();
    }
}
