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
                    type: "success",
                    title: "information",
                    subtitle: (new Date()).toDateString(),
                    content: res.Message,
                    delay: 7000
                });

                if (res.Success === true) {
                    fixedContractHeaderPopup.close();
                    let objGrid = document.getElementById("grdFixedContractDetailList").ej2_instances[0];
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

    /* summary tab */
    $("#toolbar_summary_add").click(function (e) {
        e.preventDefault();

        // if (argruments.DOC_FCH_ID) {
        window.location.href = `/T/Promotions/Investment/1/0`;
        // }

    });
    $("#toolbar_summary_edit").click(function (e) {
        e.preventDefault();

        if (argruments.data) {
            window.location.href = `/T/Promotions/Investment/${argruments.DOC_FCH_ID}/${argruments.data.DOC_FCD_ID}`;
        }

    });
    $("#toolbar_summary_del").click(function (e) {
        e.preventDefault();
        /*
        let callBack = function (data) {
            sendPost(urlServices.Edit, data);
        };
        */
        // channelPopup.open(POPUP_MODE.DELETE, argruments.data, callBack);
    });

    /* tradeActivity tab */
    $("#toolbar_tradeActivity_add").click(function (e) {
        e.preventDefault();

        // if (argruments.DOC_FCH_ID) {
        window.location.href = `/T/Promotions/TradeActivity/1/0`;
        // }

    });
    $("#toolbar_tradeActivity_edit").click(function (e) {
        e.preventDefault();

        if (argruments.data) {
            window.location.href = `/T/Promotions/TradeActivity/${argruments.DOC_FCH_ID}/${argruments.data.DOC_FCD_ID}`;
        }

    });
    $("#toolbar_tradeActivity_del").click(function (e) {
        e.preventDefault();
        /*
        let callBack = function (data) {
            sendPost(urlServices.Edit, data);
        };
        */
        // channelPopup.open(POPUP_MODE.DELETE, argruments.data, callBack);
    });

});