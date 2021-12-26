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

                if (res.Success === true) {
                    fixedContractHeaderPopup.close();
                    let objGrid = document.getElementById("grdFixedContract").ej2_instances[0];
                    if (objGrid) {
                        Toast.fire({
                            icon: res.MessageType.ToMessageType(),
                            title: res.Message
                        });

                        objGrid.refresh();
                        if (res.Data) {
                            window.location = urlServices.CurrentUrl + `/${res.Data.DOC_FCH_ID}`;
                        }
                    } else {
                        objGrid.refresh();
                    }
                } else {
                    $(document).Toasts('create', {
                        class: `bg-${res.MessageType.ToMessageType()}`,
                        title: res.Title,
                        position: 'bottomRight',
                        body: res.Message,
                        delay: 3000,
                        autohide: true,
                        animation: true
                    });
                }
            },
            error: function (response) {
                $(document).Toasts('create', {
                    class: `bg-${res.MessageType.ToMessageType()}`,
                    title: res.Title,
                    position: 'bottomRight',
                    body: res.Message
                });
            }
        });
    }

    $("#toolbar_add").click(function (e) {
        e.preventDefault();

        let callBack = function (data) {
            // console.log('data>>>', data);
            sendPost(urlServices.AddHeader, data);
        };

        fixedContractHeaderPopup.open(POPUP_MODE.CREATE, argruments.data, callBack);

    });
    $("#toolbar_edit").click(function (e) {
        e.preventDefault();
        if (argruments.data) {
            console.log(argruments);
            window.location.href = `/T/FixedContracts/${argruments.data.DOC_FCH_ID}`;
        }
    });
    $("#toolbar_del").click(function (e) {
        e.preventDefault();
        let callBack = function (data) {
            sendPost(urlServices.DeleteHeader, data);
        };

        fixedContractHeaderPopup.open(POPUP_MODE.DELETE, argruments.data, callBack);
    });

});
