let page = new (function () {
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
        console.log(`value = (${v}) and text = (${t})`);
    }

    $("#txt-apply-to-all").keyup(function (e) {
        this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*?)\..*/g, '$1');
        let value = $(page.field_apply_to_all).val();
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
        // this.jsonData = dataSelection;
    }


})();