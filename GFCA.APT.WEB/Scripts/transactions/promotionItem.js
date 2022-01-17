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
                    promotionHeaderPopup.close();
                    let objGrid = document.getElementById("grdInvestment").ej2_instances[0];

                    Toast.fire({
                        icon: res.MessageType.ToMessageType(),
                        title: res.Message
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
                    class: 'bg-error',
                    title: 'error',
                    position: 'topRight',
                    body: `${response.status} : The network connection is unreachable.`
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
    
    $("#toolbar_investment_del").click(function (e) {
        e.preventDefault();
        
        let callBack = function (data) {
            sendPost(urlServices.DeleteInvestment, data);
        };
        
        promotionHeaderPopup.open(POPUP_MODE.DELETE, argruments.dataInvest, callBack);
    });
    
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

    $('button[name="btn-workflow-commands"]').click(function (e) {
        //e.preventDefault();

        let stateCode = $(e.target).data("state-code");
        let workflowCode = $(e.target).data("workflow-code");
        let flowItem = $(e.target).data("flow-item");
        let flowCode = $(e.target).data("flow-code");
        let flowSort = $(e.target).data("flow-sort");
        

        let cbSuccess = function (response) {

        };

        let data = {
            WF_STATE_ID: 0,
            WF_CODE: workflowCode,
            STATE_CODE: stateCode,
            FLOW_ITEM_ID: flowItem,
            FLOW_ITEM_CODE: flowCode,
            FLOW_ITEM_NAME: '',
            FLOW_ITEM_DESC: '',
            DIRECTION_CODE: '',
            DIRECTION_NAME: '',
            SORT: flowSort,
        };
        console.log(data);
        //let url = `${window.location.origin}/T/Promotions/PostCommand`;
        let url = "/T/Promotions/PostCommand`"
        sendPost(url, data);

        //AjaxPost(url, data, cbSuccess);
            
    });
});