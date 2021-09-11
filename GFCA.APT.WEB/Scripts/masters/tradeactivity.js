
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
                    type: res.MessageType,
                    title: "information",
                    subtitle: (new Date()).toDateString(),
                    content: res.Message,
                    delay: 7000
                });

                if (res.Success === true) {
                    tradeactivityPopup.close();
                    let objGrid = document.getElementById("grdTradeactivity").ej2_instances[0];
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
            sendPost(urlServices.Add, data);
        };

        tradeactivityPopup.open(POPUP_MODE.CREATE, argruments.data, callBack);

    });
    $("#toolbar_edit").click(function (e) {
        e.preventDefault();
        let callBack = function (data) {
            sendPost(urlServices.Edit, data);
        };

        tradeactivityPopup.open(POPUP_MODE.EDIT, argruments.data, callBack);
    });
    $("#toolbar_del").click(function (e) {
        e.preventDefault();
        let callBack = function (data) {
            sendPost(urlServices.Delete, data);
        };
        tradeactivityPopup.open(POPUP_MODE.DELETE, argruments.data, callBack);
    });



});

function initcbbox(id) {


    $(id).prop("checked", "");
    $(id).parent().parent().attr("aria-checked", "false");
    $(id).parent().children().removeClass("e-check");

}

function deleteproduct(id,key) {


    //alert(id);
  //  alert(key);

   
    $.ajax({
        type: 'POST',
        url: '/api/Selection/GetGLAccount/1")',
        dataType: 'json',
        data: {
            isOption: "1"
        },


        success: function (obj) {

            $.each(obj, function (key, value) {


               // $('#select1').append('<option selected="selected" value=2>kai1234 </option>');


              //  $("#pop-cmd-acc_id_hidden").append(new Option(value.Text, value.Value, true,true));

               // sum += value;
             //   alert(value.Value)
             //  alert(value.Text)
                return false;
            });

            //<option selected="" value="5">11012001 - KBANK-CA # 081-1-06793-8 Maxvalu พัฒนาการ</option>
            //<option selected="" value="5">11012001 - KBANK-CA # 081-1-06793-8 Maxvalu พัฒนาการ</option>

            //<option selected="" value="2">11011001 - เงินสดในมือ</option>


        },
        error: function (ex) {
            alert('Failed to retrieve states.' + ex);
        }
    });





}
