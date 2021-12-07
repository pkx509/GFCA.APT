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

    $("#toolbar_investment_add").click(function (e) {
        e.preventDefault();
        if (argruments.DOC_PROM_PH_ID) {
            let url = urlServices.CurrentUrl + `/I/0`;
            window.location.href = url;
        }

    });

    $("#toolbar_investment_edit").click(function (e) {
        e.preventDefault();

        if (argruments.dataInvest) {
            let url = urlServices.CurrentUrl + `/I/${argruments.dataInvest.DOC_PROM_PI_ID}`;
            window.location.href = url;
        }

    });
    /*
    $("#toolbar_investment_del").click(function (e) {
        e.preventDefault();
        
        let callBack = function (data) {
            sendPost(urlServices.Edit, data);
        };
        
        // channelPopup.open(POPUP_MODE.DELETE, argruments.data, callBack);
    });
    */
    $("#toolbar_sale_add").click(function (e) {
        e.preventDefault();

        if (argruments.DOC_PROM_PH_ID) {
            let url = urlServices.CurrentUrl + `/S/0`;
            window.location.href = url;
        }

    });

    $("#toolbar_sale_edit").click(function (e) {
        e.preventDefault();

        if (argruments.dataSale) {
            let url = urlServices.CurrentUrl + `/S/${argruments.dataSale.DOC_PROM_PS_ID}`;
            window.location.href = url;
        }

    });
    
    $("#toolbar_sale_del").click(function (e) {
        e.preventDefault();

        if (argruments.dataSale) {
            let url = urlServices.CurrentUrl + `/S/${argruments.dataSale.DOC_PROM_PS_ID}/2`;
            window.location.href = url;
        }
    });
    
});