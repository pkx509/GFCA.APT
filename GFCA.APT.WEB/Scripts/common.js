Number.prototype.ToMessageType = function () {
    let messageTypeName = "error";
    switch (parseInt(this)) {
        case 0:
            messageTypeName = 'info';
            break;
        case 1:
            messageTypeName = 'success';
            break;
        case 2:
            messageTypeName = "warning";
            break;
        default:
            messageTypeName = "error";
            break;
    }

    return messageTypeName;
};

var Toast = Swal.mixin({
    toast: true,
    position: 'top-end',
    showConfirmButton: false,
    timer: 4000
});


function AjaxPost(form, callbackSuccess) {

    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        let verifyToken = $(form).find("input[name='__RequestVerificationToken']").val();
        let formData = new FormData(form);
        formData.append('__RequestVerificationToken', verifyToken);
        let ajaxConfig = {
            type: 'POST',
            url: form.action,
            data: formData,
            //contentType: false,
            //processData: false,
            success: callbackSuccess,
            error: function (response) {
                $('.content-wrapper').Toasts('create', {
                    class: 'bg-error',
                    title: 'error',
                    position: 'topRight',
                    body: `${response.status} : The network connection is unreachable.`
                });
            }
        }
        if ($(form).attr('enctype') == "multipart/form-data") {
            ajaxConfig["contentType"] = false;
            ajaxConfig["processData"] = false;
        }

        $.ajax(ajaxConfig);
    }

    return false;

}