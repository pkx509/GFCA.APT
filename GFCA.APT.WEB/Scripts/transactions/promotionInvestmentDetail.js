$("#SALE_QUANTITY").numeric({ decimalPlaces: 2 });
$("#INVEST_AMOUNT").numeric({ decimalPlaces: 2 });
$("#OTHER_AMOUNT").numeric({ decimalPlaces: 2 });
$("#TOTAL_AMOUNT").numeric({ decimalPlaces: 2 });

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

let action = {

    Initialize: function (callback) {
        callback();
    },

    LoadData: function () {

    },

    Save: function (e) {
        this.bindField();
        //begin Validation
        let msg = '';
        //end Validation
        if (msg) {
            $.toast({
                type: "warning",
                title: "Invalid information",
                subtitle: (new Date()).toDateString(),
                content: msg,
                delay: 5000
            });
        } else {
            let IS_DELETE_PERMANANT = false;
            this.jsonData = {
                ...this.jsonData,
                IS_DELETE_PERMANANT
            };
            sendPost(urlServices.AddDetail, this.jsonData);
        }
    },

    Back: function (e) {
        e.preventDefault();
        window.history.back();
    },

    clear: function (callback) {
        callback();
    }

};

let promotionInvestmentDetail = new (function () {
    this.jsondata = {
    };

    this.cmbPositionChange = function (e) {
        let t = e.itemData.Text;
        let v = e.value;
    }

    this.onMutualFieldChanged = function (e) {
        console.log(e);
    }
    
    this.bindField = function () {
        
        let DOC_PROM_PI_ID = $("#DOC_PROM_PI_ID").val(); //PK
        let DOC_PROM_PS_ID = $("#DOC_PROM_PS_ID").val(); //FK
        let DOC_PROM_PH_ID = $("#DOC_PROM_PH_ID").val(); //FK

        let DOC_CODE = $("#DOC_CODE").val();
        let DOC_VER = $("#DOC_VER").val();
        let DOC_REV = $("#DOC_REV").val();
        
        let COMP_CODE = $("#COMP_CODE_hidden").val();
        let COMP_NAME = $("#COMP_CODE").val();
        
        let BRAND_CODE = $("#BRAND_CODE_hidden").val();
        let BRAND_NAME = $("#BRAND_CODE").val();
        
        let PROD_CODE = $("#PROD_CODE_hidden").val();
        let PROD_SKU  = $("#PROD_CODE").val();
        let PROD_PACK = $("#PROD_PACK").val();
        let PROD_SIZE = $("#PROD_SIZE").val();
        
        let ACTIVITY_CODE = $("#ACTIVITY_CODE_hidden").val();
        let ACTIVITY_NAME = $("#ACTIVITY_CODE").val();
        
        //let INVEST_TYPE   = $("#INVEST_TYPE").val();
        let INVEST_TYPE   = $("input[name='INVEST_TYPE']:checked").val();
        let INVEST_VALUE  = $("#INVEST_VALUE").val();
        let INVEST_AMOUNT = $("#INVEST_AMOUNT").val();
        
        let OTHER_ACTIVITY_CODE     = $("#OTHER_ACTIVITY_CODE_hidden").val();
        let OTHER_ACTIVITY_NAME     = $("#OTHER_ACTIVITY_CODE").val();
        let OTHER_ACTIVITY_COMBINED = $("#OTHER_ACTIVITY_COMBINED").prop("checked") === true ? 'Y': 'N';
        let OTHER_AMOUNT            = $("#OTHER_AMOUNT").val();

        let TOTAL_AMOUNT          = $("#TOTAL_AMOUNT").val();
        let INCREMENT_SALE_INVEST = $("#INCREMENT_SALE_INVEST").val();
        
        //var INVEST_ACC_CODE = $("#INVEST_ACC_CODE_hidden").val();
        //var INVEST_ACC_NAME = $("#INVEST_ACC_CODE").val();
        
        let FUND1_CODE        = $("#FUND1_CODE").val();
        let FUND1_NAME        = $("#FUND1_CODE").val();
        let FUND1_CENTER_CODE = $("#FUND1_CENTER_CODE_hidden").val();
        let FUND1_CENTER_NAME = $("#FUND1_CENTER_CODE").val();
        let FUND1_AMOUNT      = $("#FUND1_AMOUNT").val();

        let FUND2_CODE        = $("#FUND2_CODE").val();
        let FUND2_NAME        = $("#FUND2_CODE").val();
        let FUND2_CENTER_CODE = $("#FUND2_CENTER_CODE_hidden").val();
        let FUND2_CENTER_NAME = $("#FUND2_CENTER_CODE").val();
        let FUND2_AMOUNT      = $("#FUND2_AMOUNT").val();

        let REMARKS = document.getElementById("REMARKS").ej2_instances[0].value;
        let FLAG_ROW = 'S'


        this.jsonData = {
            DOC_PROM_PI_ID,
            DOC_PROM_PS_ID,
            DOC_PROM_PH_ID,
            
            DOC_CODE,
            DOC_VER,
            DOC_REV,
            
            COMP_CODE,
            COMP_NAME,
            
            BRAND_CODE,
            BRAND_NAME,
            
            PROD_CODE,
            PROD_SKU,
            PROD_PACK,
            PROD_SIZE,
            
            ACTIVITY_CODE,
            ACTIVITY_NAME,
            
            INVEST_TYPE,
            INVEST_VALUE,
            INVEST_AMOUNT,
            
            OTHER_ACTIVITY_CODE,
            OTHER_ACTIVITY_NAME,
            OTHER_ACTIVITY_COMBINED,
            OTHER_AMOUNT,

            TOTAL_AMOUNT,
            INCREMENT_SALE_INVEST,
            
            //INVEST_ACC_CODE,
            //INVEST_ACC_NAME,
            
            FUND1_CODE,
            FUND1_NAME,
            FUND1_CENTER_CODE,
            FUND1_CENTER_NAME,
            FUND1_AMOUNT,
            
            FUND2_CODE,
            FUND2_NAME,
            FUND2_CENTER_CODE,
            FUND2_CENTER_NAME,
            FUND2_AMOUNT,
            
            REMARKS,
            FLAG_ROW
        };
    }

    this.onSave = function (e) {
        this.bindField();

        //begin Validation
        let msg = '';
        //end Validation
        if (msg) {
            $.toast({
                type: "warning",
                title: "Invalid information",
                subtitle: (new Date()).toDateString(),
                content: msg,
                delay: 5000
            });
        } else {
            let IS_DELETE_PERMANANT = false;
            this.jsonData = {
                ...this.jsonData,
                IS_DELETE_PERMANANT
            };

            let url = urlServices.EditDetail;
            if (this.jsonData.DOC_PROM_PI_ID === 0) {
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

    this.onAmountKeyup = function (e) {
        let json = this.jsonData;
        let INVEST_AMOUNT = e.value;
        let OTHER_AMOUNT = $("#OTHER_AMOUNT").val();
        
        let TOTAL_AMOUNT = parseFloat(INVEST_AMOUNT) + parseFloat(OTHER_AMOUNT);
        $("#TOTAL_AMOUNT").val(TOTAL_AMOUNT.toFixed(2));

        let INCREMENT_SALE_INVEST = parseFloat(INVEST_AMOUNT) / TOTAL_AMOUNT;
        $("#INCREMENT_SALE_INVEST").val(INCREMENT_SALE_INVEST.toFixed(2));

        this.jsonData = {
            ...json,
            INVEST_AMOUNT,
            TOTAL_AMOUNT,
            INCREMENT_SALE_INVEST
        };
    }

    this.onAmountOtherKeyup = function (e) {
        let json = this.jsonData;
        let INVEST_AMOUNT = $("#INVEST_AMOUNT").val();
        let OTHER_AMOUNT = e.value;

        let TOTAL_AMOUNT = parseFloat(INVEST_AMOUNT) + parseFloat(OTHER_AMOUNT);
        $("#TOTAL_AMOUNT").val(TOTAL_AMOUNT.toFixed(2));

        let INCREMENT_SALE_INVEST = parseFloat(INVEST_AMOUNT) / TOTAL_AMOUNT;
        $("#INCREMENT_SALE_INVEST").val(INCREMENT_SALE_INVEST.toFixed(2));
        this.jsonData = {
            ...json,
            INVEST_AMOUNT,
            TOTAL_AMOUNT,
            INCREMENT_SALE_INVEST
        };

    }

    // onchage 
    this.OnBrandChangeValue = function (e) {
        let BRAND_CODE = e.value;
        this.jsonData = {
            ...this.jsonData,
            BRAND_CODE
        }
    }

    this.OnTradeActivityChangeValue = function (e) {
        let TRADE_ACTIVITY_CODE = e.value;
        this.jsonData = {
            ...this.jsonData,
            TRADE_ACTIVITY_CODE
        }
    }

    this.OnCategoryChangeValue = function (e) {
        let CATEGORY_CODE = e.value;
        this.jsonData = {
            ...this.jsonData,
            CATEGORY_CODE
        }
    }

    this.OnUOMChangeValue = function (e) {
        let UOM_CODE = e.value;
        this.jsonData = {
            ...this.jsonData,
            UOM_CODE
        }
    }

    this.OnSizeChangeValue = function (e) {
        let SIZE_CODE = e.value;
        this.jsonData = {
            ...this.jsonData,
            SIZE_CODE
        }
    }

    this.OnPackChangeValue = function (e) {
        let PACK_CODE = e.value;
        this.jsonData = {
            ...this.jsonData,
            PACK_CODE
        }
    }

    this.OnCostCenterChangeValue = function (e) {
        let COST_CENTER_CODE = e.value;
        this.jsonData = {
            ...this.jsonData,
            COST_CENTER_CODE
        }
    }

    this.disableFields = function (isEnabled) {

        $("#OTHER_ACTIVITY_CODE, #OTHER_AMOUNT").removeAttr("disabled");
        $("#OTHER_ACTIVITY_CODE, #OTHER_AMOUNT").removeClass("e-disabled");
        $("#OTHER_ACTIVITY_CODE, #OTHER_AMOUNT").parent().removeClass("e-disabled");

        if (isEnabled == false) {
            $("#OTHER_ACTIVITY_CODE, #OTHER_AMOUNT").attr("disabled", "disabled");
            $("#OTHER_ACTIVITY_CODE, #OTHER_AMOUNT").addClass("e-disabled");
            $("#OTHER_ACTIVITY_CODE, #OTHER_AMOUNT").parent().addClass("e-disabled");
        }
    }

    this.OnOtherActivityCombined = function (e) {
        let OTHER_ACTIVITY_COMBINED = e.value;
        let json = this.jsonData;
        this.jsonData = {
            ...json,
            OTHER_ACTIVITY_COMBINED
        };

        promotionInvestmentDetail.disableFields(e.checked);
    }
})();

$(document).ready(function () {

    $("input[name='INVEST_TYPE']").change(function (e) {

        let INVEST_TYPE = e.target.value;
        let json = promotionInvestmentDetail.jsonData;
        promotionInvestmentDetail.jsonData = {
            ...json,
            INVEST_TYPE
        }
        console.log(promotionInvestmentDetail.jsonData);
    });

})