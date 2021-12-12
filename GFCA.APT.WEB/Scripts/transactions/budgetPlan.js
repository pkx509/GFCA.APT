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
                //alert(res.Data.DOC_BGH_ID);

                $.toast({
                    type: res.MessageType.ToMessageType(),
                    title: "information",
                    subtitle: (new Date()).toDateString(),
                    content: res.Message,
                    delay: 7000
                });
                if (res.Success === true) {
                    budgetPlanHeaderPopup.close();
                    let objGrid = document.getElementById("grdBudgetPlan").ej2_instances[0];
                    if (objGrid) {
                        //objGrid.refresh();
                       // alert(urlServices.CurrentUrl + `/${res.Data.DOC_BGH_ID}`);
                        window.location = urlServices.CurrentUrl + `/${res.Data.DOC_BGH_ID}`;
                    } else {
                        //window.location = urlServices.CurrentUrl;
                        objGrid.refresh();
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
      //  console.log('data>>>', data);

        console.log('urlServices.AddHeader >>>', urlServices.AddHeader);
        let callBack = function (data) {
            sendPost(urlServices.AddHeader, data);
        };


      //  let callBack = function (data) {
          // alert("Add111");
        
         // sendPost(urlServices.AddHeader, data);
       //};

        budgetPlanHeaderPopup.open(POPUP_MODE.CREATE, argruments.data, callBack);

    });

    $("#toolbar_edit").click(function (e) {
        
        e.preventDefault();
        console.log('toolbar_edit', argruments.data);
        if (argruments.data) {
            console.log(argruments);
            window.location.href = `/T/budgetplans/${argruments.data.DOC_BGH_ID}`;
        }

    });



});
