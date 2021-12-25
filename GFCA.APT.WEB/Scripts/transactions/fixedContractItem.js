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
                    let objGrid = document.getElementById("grdFixedContractDetailList").ej2_instances[0];

                    $(document).Toasts('create', {
                        class: `bg-${res.MessageType.ToMessageType()}`,
                        title: res.Title,
                        position: 'bottomRight',
                        body: res.Message,
                        delay: 3000,
                        autohide: true,
                        animation: true
                    });

                    if (objGrid) {
                        objGrid.refresh();
                    } else {
                        window.location = urlServices.CurrentUrl;
                    }
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

        if (argruments.DOC_FCH_ID) {
            window.location.href = `/T/FixedContracts/${argruments.DOC_FCH_ID}/0`;
        }

    });
    $("#toolbar_edit").click(function (e) {
        e.preventDefault();

        if (argruments.data) {
            window.location.href = `/T/FixedContracts/${argruments.DOC_FCH_ID}/${argruments.data.DOC_FCD_ID}`;
        }

    });
    $("#toolbar_del").click(function (e) {
        e.preventDefault();
        let callBack = function (data) {
            sendPost(urlServices.DeleteDetail, data);
        };

        fixedContractHeaderPopup.open(POPUP_MODE.DELETE, argruments.data, callBack);
    });

});