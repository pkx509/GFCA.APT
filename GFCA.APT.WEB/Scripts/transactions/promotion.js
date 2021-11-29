
$(document).ready(function () {
    let sendPost = function (url, data) {
        let value = {
            ...data
        };
        $.ajax({
            type: 'POST',
            url: url,
            data: JSON.stringify(value),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                let res = JSON.parse(response.data);
                $.toast({
                    type: res.MessageType.ToMessageType(),
                    title: "information",
                    subtitle: (new Date()).toDateString(),
                    content: res.Message,
                    delay: 7000
                });

                if (res.Success === true) {
                    promotionHeaderPopup.close();
                    let objGrid = document.getElementById("grdPromotion").ej2_instances[0];
                    if (objGrid) {
                        objGrid.refresh();
                    } else {
                        window.location = urlServices.CurrentUrl;
                    }
                }
            },
            error: function (response) {
                $.toast({
                    type: "error",
                    title: "error",
                    subtitle: (new Date()).toDateString(),
                    content: JSON.stringify(response),
                    delay: 7000
                });
            }
        });
    }

    $("#toolbar_add").click(function (e) {
        e.preventDefault();
        let callBack = function (data) {
            sendPost(urlServices.AddHeader, data);
        };

        promotionHeaderPopup.open(POPUP_MODE.CREATE, argruments.data, callBack);

    });
    $("#toolbar_edit").click(function (e) {
        e.preventDefault();

        console.log('toolbar_edit', argruments.data);
        if (argruments.data) {
            console.log(argruments);
            // TODO: redirect page
            // window.location.href = `/T/Promotions/${argruments.data.DOC_PROPLH_ID}`;
        }
    });
    $("#toolbar_del").click(function (e) {
        e.preventDefault();
        let callBack = function (data) {
            sendPost(urlServices.Edit, data);
        };
        promotionHeaderPopup.open(POPUP_MODE.DELETE, argruments.data, callBack);
    });

});
