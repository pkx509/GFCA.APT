let sendPost = function (url, data) {

    let value = {
        ...data
    };

    alert(url);


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

                setTimeout(function () {
                    window.location = urlServices.PreviousUrl;
                }, 7000);

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

let budgetPlanInvestmentDetail = new (function () {
    
    this.bindField = function () {
        let currentURL = window.location.pathname;
        let subStringURL = currentURL.split('/I/');
        
        
       //DOC_BGH_INV_ID

        let DOC_BGH_INV_ID = subStringURL[1];//$("#DOC_PROM_PS_ID").val();
        let DOC_BGH_ID = subStringURL[0].split('i/')[1]; //$("#DOC_PROM_PH_ID").val();




        let DOC_CODE = $("#DOC_CODE").val();
        let BG_TYPE_CODE = $("#BG_TYPE_CODE").val();
        let COMP_CODE = $("#COMP_CODE_hidden").val();
        let BRAND_CODE = $("#BRAND_CODE_hidden").val();
        let PACK_CODE = $("#PACK_CODE_hidden").val();
        let SIZE_CODE = $("#SIZE_CODE_hidden").val();
        let PRD_CODE = $("#PRD_CODE_hidden").val();
        let COST_ELEMENT_CODE = $("#COST_ELEMENT_CODE").val();
        let COST_CENTER = $("#COST_CENTER_hidden").val();

        let ACTIVITY_CODE = $("#ACTIVITY_CODE_hidden").val();


        let YEAR = $("#YEAR").val();
        let MONTH = $("#MONTH").val();
        let TOTAL = $("#TOTAL").val();
        let M1 = $("#M1").val();
        let M2 = $("#M2").val();
        let M3 = $("#M3").val();
        let M4 = $("#M4").val();
        let M5 = $("#M5").val();
        let M6 = $("#M6").val();
        let M7 = $("#M7").val();
        let M8 = $("#M8").val();
        let M9 = $("#M9").val();
        let M10 = $("#M10").val();
        let M11 = $("#M11").val();
        let M12 = $("#M12").val();
        
        


 

        let FLAG_ROW            = $("#FLAG_ROW").val();

        this.jsonData = {
            DOC_BGH_ID,
            DOC_BGH_INV_ID,
            DOC_CODE,
            BG_TYPE_CODE,
            COMP_CODE,
            BRAND_CODE,
            PACK_CODE,
            SIZE_CODE,
            PRD_CODE,
            COST_ELEMENT_CODE,
            COST_CENTER,
            ACTIVITY_CODE,
            YEAR,
            MONTH,
            TOTAL,
            M1,
            M2,
            M3,
            M4,
            M5,
            M6,
            M7,
            M8,
            M9,
            M10,
            M11,
            M12,
            FLAG_ROW

        };
    }

    this.onSave = function (e) {

     
        this.bindField();
        //begin Validation
        let msg = '';
        //end Validation


        

        alert(this.jsonData.DOC_BGH_INV_ID);
        if (msg) {
            $.toast({
                type: "warning",
                title: "Invalid information",
                subtitle: (new Date()).toDateString(),
                content: msg,
                delay: 5000
            });
        } else {
            let FLAG_ROW = 'S';
            this.jsonData = {
                ...this.jsonData,
                FLAG_ROW
            };

            var url = urlServices.EditDetail;
            if (this.jsonData.DOC_BGH_INV_ID == 0) {
               // alert(urlServices.AddDetail);

                url = urlServices.AddDetail;
            }

            sendPost(url, this.jsonData);
        }
    }

    this.onBack = function (e) {
        //e.preventDefault();
        //window.location.href = urlServices.PreviousUrl;
        window.history.back();
    }

    // onchage
    this.OnBrandChangeValue = function (e) {
        let BRAND_CODE = e.value;
        this.jsonData = {
            ...this.jsonData,
            BRAND_CODE
        }
    }

    this.OnProductChangeValue = function (e) {
        let PROD_CODE = e.value;
        this.jsonData = {
            ...this.jsonData,
            PROD_CODE
        }
    }

    this.OnPackChangeValue = function (e) {
        let PROD_PACK = e.value;
        this.jsonData = {
            ...this.jsonData,
            PROD_PACK
        }
    }

    this.OnSizeChangeValue = function (e) {
        let PROD_SIZE = e.value;
        this.jsonData = {
            ...this.jsonData,
            PROD_SIZE
        }
    }

})();