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

            if (res.Success === true) {

                Toast.fire({
                    icon: res.MessageType.ToMessageType(),
                    title: res.Message
                });

                setTimeout(function () {
                    window.location = urlServices.PreviousUrl;
                }, 5000);

            } else {

                $(document).Toasts('create', {
                    class: `bg-${res.MessageType.ToMessageType()}`,
                    title: res.Title,
                    position: 'topRight',
                    body: res.Message
                });
            }
        },
        error: function (response) {
            
            $(document).Toasts('create', {
                class: 'bg-error',
                title: 'error',
                position: 'topRight',
                body: `${response.status} ${response.statusText}`
            });

        }
    });
}

let promotionSaleDetail = new (function () {
    
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
        let INCLUDE_PROMOTION   = $("#INCLUDE_PROMOTION").val();
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
            INCLUDE_PROMOTION,

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

            var url = urlServices.EditDetail;
            if (this.jsonData.DOC_PROM_PS_ID == 0) {
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