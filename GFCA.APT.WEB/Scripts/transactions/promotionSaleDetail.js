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

let promotionSaleDetail = new (function () {
    this.field_brand_name = "#BRAND_CODE";
    this.field_brand_code = "#BRAND_CODE_hidden";
    this.field_trade_activity_name = "#ACTIVITY_CODE";
    this.field_trade_activity_code = "#ACTIVITY_CODE_hidden";
    this.field_category_name = "#CONTRACT_CATE";
    this.field_category_code = "#CONTRACT_CATE_hidden";
    this.field_date_reference = "#DATE_REF";
    this.field_UOM_name = "#UOM";
    this.field_UOM_code = "#UOM_hidden";
    this.field_size_name = "#SIZE";
    this.field_size_code = "#SIZE_hidden";
    this.field_pack_name = "#PACK";
    this.field_pack_code = "#PACK_hidden";
    this.field_cost_center_name = "#CENTER_CODE";
    this.field_cost_center_code = "#CENTER_CODE_hidden";
    this.field_plan_jan = "#M01";
    this.field_plan_feb = "#M02";
    this.field_plan_mar = "#M03";
    this.field_plan_apr = "#M04";
    this.field_plan_may = "#M05";
    this.field_plan_jun = "#M06";
    this.field_plan_jul = "#M07";
    this.field_plan_aug = "#M08";
    this.field_plan_sep = "#M09";
    this.field_plan_oct = "#M10";
    this.field_plan_nov = "#M11";
    this.field_plan_dec = "#M12";
    this.field_remark = "#CONTRACT_DESC";
    
    this.bindField = function () {
        let currentURL = window.location.pathname;
        let subStringURL = currentURL.split('/S/');
        
        let DOC_PROM_PS_ID      = subStringURL[1];//$("#DOC_PROM_PS_ID").val();
        let DOC_PROM_PH_ID      = subStringURL[0].split('s/')[1]; //$("#DOC_PROM_PH_ID").val();
        let DOC_CODE            = $("#DOC_CODE").val();
        let DOC_VER             = $("#DOC_VER").val();
        let DOC_REV             = $("#DOC_REV").val();
        let COMP_CODE           = $("#COMP_CODE").val();
        let COMP_NAME           = $("#COMP_NAME").val();
        let BRAND_CODE          = $("#BRAND_CODE_hidden").val();
        let BRAND_NAME          = $("#BRAND_CODE").val();
        let PROD_CODE           = $("#PROD_CODE_hidden").val();
        let PROD_SKU            = $("#PROD_SKU").val();
        let PROD_PACK           = $("#PROD_PACK_hidden").val();
        let PROD_SIZE           = $("#PROD_SIZE_hidden").val();
        let PROD_LTP_EXCL_VAT   = $("#PROD_LTP_EXCL_VAT").val();
        let NORM_PERC_DISC      = $("#NORM_PERC_DISC").val();
        let NORM_PERC_GP        = $("#NORM_PERC_GP").val();
        let NORM_SHELF_PRICE    = $("#NORM_SHELF_PRICE").val();
        let PROMO_PERC_DISC     = $("#PROMO_PERC_DISC").val();
        let PROMO_PERC_GP       = $("#PROMO_PERC_GP").val();
        let PROMO_SHELF_PRICE   = $("#PROMO_SHELF_PRICE").val();
        let DEAL_GUIDE_LINE     = $("#DEAL_GUIDE_LINE").val();
        let NET_INTO_STORE      = $("#NET_INTO_STORE").val();
        let AVG_SALE            = $("#AVG_SALE").val();
        let AVG_VOLUME          = $("#AVG_VOLUME").val();
        let SALE_QTY            = $("#SALE_QTY").val();
        let SALE_VALUE_EXCL_VAT = $("#SALE_VALUE_EXCL_VAT").val();
        let SALE_UOM            = $("#SALE_UOM").val();
        let DISC_TYPE           = $("#DISC_TYPE").val();
        let FLAG_ROW            = $("#FLAG_ROW").val();

        this.jsonData = {
            DOC_PROM_PS_ID, //PK
            DOC_PROM_PH_ID, //FK

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
            PROD_LTP_EXCL_VAT,
        
            NORM_PERC_DISC,
            NORM_PERC_GP,
            NORM_SHELF_PRICE,

            PROMO_PERC_DISC,
            PROMO_PERC_GP,
            PROMO_SHELF_PRICE,

            DEAL_GUIDE_LINE,
            NET_INTO_STORE,

            AVG_SALE,
            AVG_VOLUME,

            SALE_QTY,
            SALE_VALUE_EXCL_VAT,
            SALE_UOM,

            DISC_TYPE,
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
            let FLAG_ROW = 'S';
            this.jsonData = {
                ...this.jsonData,
                FLAG_ROW
            };

            var url = urlServices.EditDetailSale;
            if (this.jsonData.DOC_PROM_PS_ID == 0) {
                url = urlServices.AddDetailSale;
            }

            sendPost(url, this.jsonData);
        }
    }

    this.onBack = function (e) {
        window.location.href = urlServices.PreviousUrl;
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