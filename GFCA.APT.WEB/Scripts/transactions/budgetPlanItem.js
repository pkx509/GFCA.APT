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

    
    $("#toolbar_add_sale_grid").click(function (e) {
        //alert(urlServices.AddSale + '/' + argruments.DOC_BGH_ID + "/0");

        e.preventDefault();


        if (argruments.DOC_BGH_ID) {
            window.location.href = urlServices.AddSale + '/' + argruments.DOC_BGH_ID + "/0";
        }

       

    });
    $("#toolbar_edit_sale_grid").click(function (e) {
        e.preventDefault();

        if (argruments.data) {
            window.location.href = `/T/FixedContracts/${argruments.DOC_FCH_ID}/${argruments.data.DOC_FCD_ID}`;
        }

    });
    $("#toolbar_del_sale_grid").click(function (e) {
        e.preventDefault();
        
     

        /*
        let callBack = function (data) {
            sendPost(urlServices.Edit, data);
        };
        */
        // channelPopup.open(POPUP_MODE.DELETE, argruments.data, callBack);
    });


    $("#toolbar_add_investment_grid").click(function (e) {
        alert("toolbar_add_investment_grid")
        e.preventDefault();

        if (argruments.DOC_FCH_ID) {
            window.location.href = `/T/FixedContracts/${argruments.DOC_FCH_ID}/0`;
        }

    });
    $("#toolbar_edit_investment_grid").click(function (e) {
        e.preventDefault();

        if (argruments.data) {
            window.location.href = `/T/FixedContracts/${argruments.DOC_FCH_ID}/${argruments.data.DOC_FCD_ID}`;
        }

    });
    $("#toolbar_del_investment_grid").click(function (e) {
        e.preventDefault();



        /*
        let callBack = function (data) {
            sendPost(urlServices.Edit, data);
        };
        */
        // channelPopup.open(POPUP_MODE.DELETE, argruments.data, callBack);
    });

});