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
                    saleForecastHeaderPopup.close();
                    let objGrid = document.getElementById("grdSaleForecastDetailList").ej2_instances[0];
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
        //debugger
        if (argruments.DOC_SFCH_ID) {
            //alert(`/T/SaleForecasts/${argruments.DOC_SFCH_ID}/0`);
            window.location.href = `/T/SaleForecasts/${argruments.DOC_SFCH_ID}/0`;
        }

    });
    $("#toolbar_edit").click(function (e) {
        e.preventDefault();
        //debugger
        if (argruments.data) {
            //alert(`/T/SaleForecasts/${argruments.DOC_SFCH_ID}/${argruments.data.DOC_SFCD_ID}`);
            window.location.href = `/T/SaleForecasts/${argruments.DOC_SFCH_ID}/${argruments.data.DOC_SFCD_ID}`;
        }

    });
    $("#toolbar_del").click(function (e) {
        e.preventDefault();
        
     

        /*
        let callBack = function (data) {
            sendPost(urlServices.Edit, data);
        };
        */
        // channelPopup.open(POPUP_MODE.DELETE, argruments.data, callBack);
    });
    $("#toolbar_excel").click(function (e) {
        e.preventDefault();
        //debugger
        if (argruments.DOC_SFCH_ID) {
            window.location.href = `/T/SaleForecasts/EX/ExportFiles/${argruments.DOC_SFCH_ID}`;
        }

    });
    $("#toolbar_import").click(function (e) {
        e.preventDefault();
        //debugger
        if (argruments.DOC_SFCH_ID) {
            window.location.href = `/T/SaleForecasts/IM/ImportFiles`;
        }

    });



});