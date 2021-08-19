$(document).ready(function () {

    $("#toolbar_add").click(function (e) {
        let callBack = function (data) {
            $.ajax({
                url: urlServices.Add,
                type: 'POST',
                processData: false,
                contentType: false,
                data: JSON.stringify(data),
                success: function (response) {

                    let res = JSON.parse(response.data);

                    $.toast({
                        type: "success",
                        title: "information",
                        subtitle: (new Date()).toDateString(),
                        content: res.Message,
                        delay: 5000
                    });

                    if (res.Success === true) {
                        brandPopup.close();
                        window.location = urlServices.CurrentUrl;
                    }
                },
                error: function (response) {
                    $.toast({
                        type: "warning",
                        title: "Invalid information",
                        subtitle: (new Date()).toDateString(),
                        content: JSON.stringify(response),
                        delay: 5000
                    });
                }
            });
        };

        brandPopup.openCreate(true, argruments.data, callBack);

    });
    $("#toolbar_edit").click(function (e) {
        let callBack = function (data) {
            $.ajax({
                url: urlServices.Edit,
                type: 'POST',
                processData: false,
                contentType: false,
                data: JSON.stringify(data),
                success: function (response) {

                    let res = JSON.parse(response.data);

                    $.toast({
                        type: "success",
                        title: "information",
                        subtitle: (new Date()).toDateString(),
                        content: res.Message,
                        delay: 5000
                    });

                    if (res.Success === true) {
                        brandPopup.close();
                        window.location = urlServices.CurrentUrl;
                    }
                },
                error: function (response) {
                    $.toast({
                        type: "warning",
                        title: "Invalid information",
                        subtitle: (new Date()).toDateString(),
                        content: JSON.stringify(response),
                        delay: 5000
                    });
                }
            });
        };

        brandPopup.openEdit(true, argruments.data, callBack);
    });
    $("#toolbar_del").click(function (e) {
        let callBack = function (data) {
            $.ajax({
                url: urlServices.Delete,
                type: 'POST',
                //processData: false,
                //contentType: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(data),
                success: function (response) {
                    //let res = JSON.parse(response);
                    let res = JSON.parse(response.data);

                    $.toast({
                        type: "success",
                        title: "information",
                        subtitle: (new Date()).toDateString(),
                        content: res.Message,
                        delay: 5000
                    });

                    if (res.Success === true) {
                        brandPopup.close();
                        window.location = urlServices.CurrentUrl;
                        //$("#grdBrand").refresh();
                    }
                },
                error: function (response) {
                    $.toast({
                        type: "warning",
                        title: "Invalid information",
                        subtitle: (new Date()).toDateString(),
                        content: JSON.stringify(response),
                        delay: 5000
                    });
                }
            });
        };

        brandPopup.openDelete(true, argruments.data, callBack);
    });

});