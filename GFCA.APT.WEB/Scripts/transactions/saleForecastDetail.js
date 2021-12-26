let sendPost = function (url, data) {
    console.log('url>>>', url);
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
                //  console.log('url>>>', url);

                setTimeout(function () {
                    // alert(url);
                    window.location = document.referrer;
                }, 500);



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

let saleForecastDetail = new (function () {
    this.field_doc_sfcd_id = "#DOC_SFCD_ID";
    this.field_brand_name = "#BRAND_CODE";
    this.field_brand_code = "#BRAND_CODE_hidden";
    //this.field_trade_activity_name = "#ACTIVITY_CODE";
    //this.field_trade_activity_code = "#ACTIVITY_CODE_hidden";
    //this.field_category_name = "#CONTRACT_CATE";
    //this.field_category_code = "#CONTRACT_CATE_hidden";
    //this.field_date_reference = "#DATE_REF";
    this.field_UOM_name = "#UOM";
    this.field_UOM_code = "#UOM_hidden";
    this.field_size_name = "#SIZE";
    this.field_size_code = "#SIZE_hidden";
    this.field_pack_name = "#PACK";
    this.field_pack_code = "#PACK_hidden";
    //this.field_cost_center_name = "#CENTER_CODE";
    //this.field_cost_center_code = "#CENTER_CODE_hidden";
    this.field_product_name = "#PROD_CODE";
    this.field_product_code = "#PROD_CODE_hidden";
    this.field_year = "#YEAR";
    this.field_sales_jan = "#M1Sales";
    this.field_sales_feb = "#M2Sales";
    this.field_sales_mar = "#M3Sales";
    this.field_sales_apr = "#M4Sales";
    this.field_sales_may = "#M5Sales";
    this.field_sales_jun = "#M6Sales";
    this.field_sales_jul = "#M7Sales";
    this.field_sales_aug = "#M8Sales";
    this.field_sales_sep = "#M9Sales";
    this.field_sales_oct = "#M10Sales";
    this.field_sales_nov = "#M11Sales";
    this.field_sales_dec = "#M12Sales";
    this.field_foc_jan = "#M1FOC";
    this.field_foc_feb = "#M2FOC";
    this.field_foc_mar = "#M3FOC";
    this.field_foc_apr = "#M4FOC";
    this.field_foc_may = "#M5FOC";
    this.field_foc_jun = "#M6FOC";
    this.field_foc_jul = "#M7FOC";
    this.field_foc_aug = "#M8FOC";
    this.field_foc_sep = "#M9FOC";
    this.field_foc_oct = "#M10FOC";
    this.field_foc_nov = "#M11FOC";
    this.field_foc_dec = "#M12FOC";
    this.field_total_sales = "#TotalSales";
    this.field_total_foc = "#TotalFOC";
    //this.field_remark = "#CONTRACT_DESC";

    this.field_apply_to_all = "#txt-apply-to-all";

    let monthSaleFoecast = $("#M1Sales,#M2Sales,#M3Sales,#M4Sales,#M5Sales,#M6Sales,#M7Sales,#M8Sales,#M9Sales,#M10Sales,#M11Sales,#M12Sales,#M1FOC,#M2FOC,#M3FOC,#M4FOC,#M5FOC,#M6FOC,#M7FOC,#M8FOC,#M9FOC,#M10FOC,#M11FOC,#M12FOC");

    this.cmbPositionChange = function (e) {
        let t = e.itemData.Text;
        let v = e.value;
    }

    $("#txt-apply-to-all").keyup(function (e) {
        this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*?)\..*/g, '$1');
        let value = $(saleForecastDetail.field_apply_to_all).val();
        monthSaleFoecast.val(value);
    });

    monthSaleFoecast.keyup(function (e) {
        this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*?)\..*/g, '$1');
    });

    this.bindField = function () {
        let currentURL = window.location.pathname;
        let subStringURL = currentURL.split('/');
       // alert(currentURL);

        let docId = subStringURL[3];
        let DOC_SFCH_ID = docId;
        let DOC_SFCD_ID = subStringURL[4];
        let DOC_CODE = 'SF';
        let DOC_VER = '1';
        let DOC_REV = '1';
        let BRAND_CODE = $(this.field_brand_code).val();
        //let ACTIVITY_CODE = $(this.field_trade_activity_code).val();
        //let CENTER_CODE = $(this.field_cost_center_code).val();
        //let ACC_CODE = '';
        let SIZE = $(this.field_size_code).val();
        let UOM = $(this.field_UOM_code).val();
        let PACK = $(this.field_pack_code).val();
        let PROD_CODE = $(this.field_product_code).val();
        let YEAR = $(this.field_year).val();
        //let DATE_REF = $(this.field_date_reference).val();
        //let CONDITION_TYPE = '';
        //let CONTRACT_CATE = '';
        //let CONTRACT_DESC = $(this.field_remark).val();
        let M1Sales = $(this.field_sales_jan).val();
        let M2Sales = $(this.field_sales_feb).val();
        let M3Sales = $(this.field_sales_mar).val();
        let M4Sales = $(this.field_sales_apr).val();
        let M5Sales = $(this.field_sales_may).val();
        let M6Sales = $(this.field_sales_jun).val();
        let M7Sales = $(this.field_sales_jul).val();
        let M8Sales = $(this.field_sales_aug).val();
        let M9Sales = $(this.field_sales_sep).val();
        let M10Sales = $(this.field_sales_oct).val();
        let M11Sales = $(this.field_sales_nov).val();
        let M12Sales = $(this.field_sales_dec).val();
        let M1FOC = $(this.field_foc_jan).val();
        let M2FOC = $(this.field_foc_feb).val();
        let M3FOC = $(this.field_foc_mar).val();
        let M4FOC = $(this.field_foc_apr).val();
        let M5FOC = $(this.field_foc_may).val();
        let M6FOC = $(this.field_foc_jun).val();
        let M7FOC = $(this.field_foc_jul).val();
        let M8FOC = $(this.field_foc_aug).val();
        let M9FOC = $(this.field_foc_sep).val();
        let M10FOC = $(this.field_foc_oct).val();
        let M11FOC = $(this.field_foc_nov).val();
        let M12FOC = $(this.field_foc_dec).val();
        let TotalSales = $(this.field_total_sales).val();
        let TotalFOC = $(this.field_total_foc).val();
        let DOC_STATUS = '';
        //let START_DATE = '';
        //let END_DATE = ''
        let FLAG_ROW = '';


        this.jsonData = {
            DOC_SFCH_ID,
            DOC_SFCD_ID,
            DOC_CODE,
            DOC_VER,
            DOC_REV,
            BRAND_CODE,
            //ACTIVITY_CODE,
            //CENTER_CODE,
            //ACC_CODE,
            SIZE,
            UOM,
            PACK,
            PROD_CODE,
            YEAR,
            //DATE_REF,
            //CONDITION_TYPE,
            //CONTRACT_CATE,
            //CONTRACT_DESC,
            M1Sales,
            M2Sales,
            M3Sales,
            M4Sales,
            M5Sales,
            M6Sales,
            M7Sales,
            M8Sales,
            M9Sales,
            M10Sales,
            M11Sales,
            M12Sales,
            M1FOC,
            M2FOC,
            M3FOC,
            M4FOC,
            M5FOC,
            M6FOC,
            M7FOC,
            M8FOC,
            M9FOC,
            M10FOC,
            M11FOC,
            M12FOC,
            TotalSales,
            TotalFOC,
            DOC_STATUS,
            //START_DATE,
            //END_DATE,
            FLAG_ROW
        };
    }

    this.onSave = function (e) {

        

       // console.log('DOC_SFCD_ID  >>>', this.jsonData.DOC_SFCD_ID);

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

          //  alert($(this.field_doc_fcd_id).val());

            var url = urlServices.EditSaleForecastDetail;
            if ($(this.field_doc_sfcd_id).val() == 0) {
                //  alert($(this.field_doc_fcd_id).val());
                url = urlServices.AddSaleForecastDetail;
            }

            sendPost(url, this.jsonData);
        }
    }

    // onchage 
    this.OnBrandChangeValue = function (e) {
        let BRAND_CODE = e.value;
        this.jsonData = {
            ...this.jsonData,
            BRAND_CODE
        }
    }

    //this.OnTradeActivityChangeValue = function (e) {
    //    let TRADE_ACTIVITY_CODE = e.value;
    //    this.jsonData = {
    //        ...this.jsonData,
    //        TRADE_ACTIVITY_CODE
    //    }
    //}

    //this.OnCategoryChangeValue = function (e) {
    //    let CATEGORY_CODE = e.value;
    //    this.jsonData = {
    //        ...this.jsonData,
    //        CATEGORY_CODE
    //    }
    //}

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

    //this.OnCostCenterChangeValue = function (e) {
    //    let COST_CENTER_CODE = e.value;
    //    this.jsonData = {
    //        ...this.jsonData,
    //        COST_CENTER_CODE
    //    }
    //};

    this.OnProductChangeValue = function (e) {
        let PROD_CODE = e.value;
        this.jsonData = {
            ...this.jsonData,
            PROD_CODE
        }
    };

    $("#btn-sale-forecast-detail-back").click(function (e) {
        e.preventDefault();
        window.location.href = document.referrer;
    });

})();