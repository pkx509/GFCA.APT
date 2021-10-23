let fixedContractDetail = new (function () {

    this.field_brand_name = "#";
    this.field_brand_code = "#";
    this.field_trade_activity_name = "#";
    this.field_trade_activity_code = "#";
    this.field_category_name = "#";
    this.field_category_code = "#";
    this.field_UOM_name = "#";
    this.field_UOM_code = "#";
    this.field_size_name = "#";
    this.field_size_code = "#";
    this.field_pack_name = "#";
    this.field_pack_code = "#";
    this.field_cost_center_name = "#";
    this.field_cost_center_code = "#";
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

    /*
     console.log('link...', window.location.href);

    this.init = function () {
        console.log('init>>>>>');
        // this.clearValue();
        // this.bindDom(this.jsonData);
    }
    */
    this.open = function (popupMode, dataSelection, fn) {

        // console.log('opennnn>>>', dataSelection);
        // this.jsonData = dataSelection;
    }

    this.onSave = function (e) {
        // console.log('test save');
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
    //

    this.OnRowSelected = function (args) {

        argruments.data = args.data;
        console.log('OnRowSelected>>>', argruments);
    }
    this.OnRowDeselected = function (args) {
        if (argruments.data == args.data) {
            argruments.data = null;
        }
    }

})();