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

let fixedContractDetail = new (function () {
    this.field_brand_name = "#cmb-brand";
    this.field_brand_code = "#cmb-brand_hidden";
    this.field_trade_activity_name = "#cmb-trade-activity";
    this.field_trade_activity_code = "#cmb-trade-activity_hidden";
    this.field_category_name = "#cmb-category";
    this.field_category_code = "#cmb-category_hidden";
    this.field_date_reference = "#cmb-date-reference";
    this.field_UOM_name = "#cmb-uom";
    this.field_UOM_code = "#cmb-uom_hidden";
    this.field_size_name = "#cmb-size";
    this.field_size_code = "#cmb-size_hidden";
    this.field_pack_name = "#cmb-pack";
    this.field_pack_code = "#cmb-pack_hidden";
    this.field_cost_center_name = "#cmb-cost-center";
    this.field_cost_center_code = "#cmb-cost-center_hidden";
    this.field_plan_jan = "#txt-plan-jan";
    this.field_plan_feb = "#txt-plan-feb";
    this.field_plan_mar = "#txt-plan-mar";
    this.field_plan_apr = "#txt-plan-apr";
    this.field_plan_may = "#txt-plan-may";
    this.field_plan_jun = "#txt-plan-jun";
    this.field_plan_jul = "#txt-plan-jul";
    this.field_plan_aug = "#txt-plan-aug";
    this.field_plan_sep = "#txt-plan-sep";
    this.field_plan_oct = "#txt-plan-oct";
    this.field_plan_nov = "#txt-plan-nov";
    this.field_plan_dec = "#txt-plan-dec";
    this.field_remark = "#txt-remark";

    this.field_apply_to_all = "#txt-apply-to-all";

    let monthPlans = $("#txt-plan-jan,#txt-plan-feb,#txt-plan-mar,#txt-plan-apr,#txt-plan-may,#txt-plan-jun,#txt-plan-jul,#txt-plan-aug,#txt-plan-sep,#txt-plan-oct,#txt-plan-nov,#txt-plan-dec");

    this.cmbPositionChange = function (e) {
        let t = e.itemData.Text;
        let v = e.value;
        // console.log(`value = (${v}) and text = (${t})`);
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
        let docId = $(this.field_document_no).val() !== undefined ? $(this.field_document_no).val() : '0';

        let DOC_FCH_ID = '1';
        let DOC_FCD_ID = '';
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
        let CONTRACT_DESC = '';
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
        let REMARK = $(this.field_remark).val();
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
            REMARK,
            DOC_STATUS,
            START_DATE,
            END_DATE,
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
            sendPost(urlServices.AddDetail, this.jsonData);
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
    }

    this.OnRowSelected = function (args) {
        console.log(args);
        argruments.data = args.data;
    }
    this.OnRowDeselected = function (args) {
        if (argruments.data == args.data) {
            argruments.data = null;
        }
    }

})();