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
                    detailGridInvestmentPartialPopup.close();
                    detailGridSalePartialPopup.close();
                    let objGridSale = document.getElementById("grdBudgetPlanSale").ej2_instances[0];
                    let objGridInvestment = document.getElementById("grdBudgetPlanInvestment").ej2_instances[0];
                    if (objGridSale || objGridInvestment) {
                        objGridSale.refresh();
                        objGridInvestment.refresh();
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
        //alert("Add");
        window.location = urlServices.Add;
        let callBack = function (data) {
           // console.log('data>>>', data);
            window.location = urlServices.AddHeader;
          //  sendPost(urlServices.AddHeader, data);

            //sendPost(urlServices.AddHeader, data);
        };

      //  fixedContractHeaderPopup.open(POPUP_MODE.CREATE, argruments.data, callBack);

    });

    // budget plan sale tab
    $("#toolbar_add_sale_grid").click(function (e) {
        e.preventDefault();
        let callBack = function (data) {
            sendPost(urlServices.Add, data);
        };
        detailGridSalePartialPopup.open(POPUP_MODE.CREATE, argruments.data, callBack);
    });
    $("#toolbar_edit_sale_grid").click(function (e) {
        e.preventDefault();
        let callBack = function (data) {
            sendPost(urlServices.Edit, data);
        };
        detailGridSalePartialPopup.open(POPUP_MODE.EDIT, argruments.data, callBack);
    });
    $("#toolbar_del_sale_grid").click(function (e) {
        e.preventDefault();
        let callBack = function (data) {
            sendPost(urlServices.Delete, data);
        };
        detailGridSalePartialPopup.open(POPUP_MODE.DELETE, argruments.data, callBack);
    });

    // budget plan investment tab
    $("#toolbar_add_investment_grid").click(function (e) {
        e.preventDefault();
        let callBack = function (data) {
            sendPost(urlServices.Add, data);
        };
        detailGridInvestmentPartialPopup.open(POPUP_MODE.CREATE, argruments.data, callBack);
    });
    $("#toolbar_edit_investment_grid").click(function (e) {
        e.preventDefault();
        let callBack = function (data) {
            sendPost(urlServices.Edit, data);
        };
        detailGridInvestmentPartialPopup.open(POPUP_MODE.EDIT, argruments.data, callBack);
    });
    $("#toolbar_del_investment_grid").click(function (e) {
        e.preventDefault();
        let callBack = function (data) {
            sendPost(urlServices.Delete, data);
        };
        detailGridInvestmentPartialPopup.open(POPUP_MODE.DELETE, argruments.data, callBack);
    });
});
