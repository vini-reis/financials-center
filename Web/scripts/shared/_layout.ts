import * as utils from '../site'

$(document).ready(utils.Init);

function Logout() {
    jQuery.ajax({
        url: 'Auth/Logout',
        method: 'POST',
        success: function () {
            location.href = 'Home/Index'
        }
    })
}
