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

let fixedContractDetail = new (function () {
    this.field_doc_fcd_id = "#DOC_FCD_ID";
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

    this.field_apply_to_all = "#txt-apply-to-all";

    let monthPlans = $("#M01,#M02,#M03,#M04,#M05,#M06,#M07,#M08,#M09,#M10,#M11,#M12");

    this.cmbPositionChange = function (e) {
        let t = e.itemData.Text;
        let v = e.value;
    }

    $("#txt-apply-to-all").keyup(function (e) {
        this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*?)\..*/g, '$1');
        let value = $(fixedContractDetail.field_apply_to_all).val();
        monthPlans.val(value);
    });

    monthPlans.keyup(function (e) {
        this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*?)\..*/g, '$1');
    });

    this.bindField = function () {
        let currentURL = window.location.pathname;
        let subStringURL = currentURL.split('/');
       // alert(currentURL);

        let docId = subStringURL[3];
        let DOC_FCH_ID = docId;
        let DOC_FCD_ID = subStringURL[4];
        let DOC_CODE = 'FC';
        let DOC_VER = '1';
        let DOC_REV = '1';
        let BRAND_CODE = $(this.field_brand_code).val();
        let ACTIVITY_CODE = $(this.field_trade_activity_code).val();
        let CENTER_CODE = $(this.field_cost_center_code).val();
        let ACC_CODE = '';
        let SIZE = $(this.field_size_code).val();
        let UOM = $(this.field_UOM_code).val();
        let PACK = $(this.field_pack_code).val();
        let DATE_REF = $(this.field_date_reference).val();
        let CONDITION_TYPE = '';
        let CONTRACT_CATE = '';
        let CONTRACT_DESC = $(this.field_remark).val();
        let M01 = $(this.field_plan_jan).val();
        let M02 = $(this.field_plan_feb).val();
        let M03 = $(this.field_plan_mar).val();
        let M04 = $(this.field_plan_apr).val();
        let M05 = $(this.field_plan_may).val();
        let M06 = $(this.field_plan_jun).val();
        let M07 = $(this.field_plan_jul).val();
        let M08 = $(this.field_plan_aug).val();
        let M09 = $(this.field_plan_sep).val();
        let M10 = $(this.field_plan_oct).val();
        let M11 = $(this.field_plan_nov).val();
        let M12 = $(this.field_plan_dec).val();
        let DOC_STATUS = '';
        let START_DATE = '';
        let END_DATE = ''
        let FLAG_ROW = '';


        this.jsonData = {
            DOC_FCH_ID,
            DOC_FCD_ID,
            DOC_CODE,
            DOC_VER,
            DOC_REV,
            BRAND_CODE,
            ACTIVITY_CODE,
            CENTER_CODE,
            ACC_CODE,
            SIZE,
            UOM,
            PACK,
            DATE_REF,
            CONDITION_TYPE,
            CONTRACT_CATE,
            CONTRACT_DESC,
            M01,
            M02,
            M03,
            M04,
            M05,
            M06,
            M07,
            M08,
            M09,
            M10,
            M11,
            M12,
            DOC_STATUS,
            START_DATE,
            END_DATE,
            FLAG_ROW
        };
    }

    this.onSave = function (e) {

        

       // console.log('DOC_FCD_ID  >>>', this.jsonData.DOC_FCD_ID);

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

            var url = urlServices.EditFixedContractDetail;
            if ($(this.field_doc_fcd_id).val() == 0) {
              //  alert($(this.field_doc_fcd_id).val());
                url = urlServices.AddFixedContractDetail;
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
    };

    $("#btn-fixed-contact-detail-back").click(function (e) {
        e.preventDefault();
        window.location.href = document.referrer;
    });

})();