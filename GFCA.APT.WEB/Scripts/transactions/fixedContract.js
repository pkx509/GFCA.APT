



$(document).ready(function () {
    let sendPost = function (url, data) {
        // console.log('url>>>', url);
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
                    let objGrid = document.getElementById("grdFixedContract").ej2_instances[0];
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
            // console.log('data>>>', data);
            sendPost(urlServices.AddHeader, data);
        };

        fixedContractHeaderPopup.open(POPUP_MODE.CREATE, argruments.data, callBack);

    });
    $("#toolbar_edit").click(function (e) {
        // console.log('click edit>>');
        e.preventDefault();

        console.log('toolbar_edit', argruments.data);

        if (argruments.data) {
            window.location.replace(`/T/FixedContracts/${argruments.data.DOC_CODE}`);
        }
        /*
        let callBack = function (data) {
            // console.log('data>>>', data);
            // sendPost(urlServices.AddHeader, data);
            window.location.replace(`/T/FixedContracts/${data.DocCode}`);
        }; 
        fixedContractDetail.open(POPUP_MODE.CREATE, argruments.data, callBack);
        */
        // var abc = '123';
        // window.location.href = `/T/FixedContracts/${abc}`;

    });
    $("#toolbar_del").click(function (e) {
        e.preventDefault();
        let callBack = function (data) {
            sendPost(urlServices.Edit, data);
        };

        // channelPopup.open(POPUP_MODE.DELETE, argruments.data, callBack);
    });

});
