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


function AjaxPost(url, data, cbSuccess) {
    $.ajax({
        type: 'POST',
        url: url,
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: cbSuccess,
        error: function (response) {
            $(document).Toasts('create', {
                class: 'bg-error',
                title: 'error',
                position: 'topRight',
                body: JSON.stringify(response)
            });
        }
    });
}