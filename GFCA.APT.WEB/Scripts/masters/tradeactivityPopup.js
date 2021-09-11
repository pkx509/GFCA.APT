const POPUP_MODE = Object.freeze({
    CREATE: 1,
    EDIT: 2,
    DELETE: 3
});

// 2021/09/11 Jiraska.m

let tradeactivityPopup = new (function () {

    let _args = null;

    //UI
    this.popup_id = "#popup-tradeactivity";
    this.header_title = "#pop-lbl-header-title";
    this.button_del = "#pop-btn-del";
    this.button_save = "#pop-btn-save";

    this.MODE_TYPE = {
        CREATE: 1,
        EDIT: 2,
        DELETE: 3
    }

    this.isCreateState = true;

    //Model Dto

    this.field_is_active = "#pop-cmb-is_active";
    this.field_permanant_del = "input[id='pop-ckb-is-del-perm']";

    this.field_activity_id = "#pop-txt-activity_id";


    this.field_acc_id = "#pop-cmd-acc_id";
    //pop-cmd-acc_id

    this.field_activity_code = "#pop-txt-activity_code";
    this.field_activity_type = "#pop-txt-activity_type";

    this.field_activtity_name = "#pop-txt-activtity_name";


    this.field_has_fixed_contract = "#pop-txt-has_fixed_contract";
    this.field_can_deductable = "#pop-txt-can_deductable";

    this.field_in_thb_cs = "#pop-txt-in_thb_cs";
    this.field_in_gross_sale = "#pop-txt-in_gross_sale";
    this.field_in_not_sale = "#pop-txt-in_not_sale";



    this.field_out_thb_cs = "#pop-txt-out_thb_cs";
    this.field_out_gross_sale = "#pop-txt-out_gross_sale";
    this.field_out_not_sale = "#pop-txt-out_not_sale";



    this.field_no_relate_abs_amt = "#pop-txt-no_relate_abs_amt";

    this.field_valuable = "#pop-txt-valuable";
    this.field_activity_desc = "textarea[name = 'pop-txt-activity_desc']";
    //pop-txt-activtity_type

    //"textarea[name='pop-txt-activity_desc']";

    //Value Dto
    this.jsonData = {
        ACTIVITY_ID: null,
        ACC_ID: null,
        ACTIVITY_CODE: null,
        ACTIVITY_TYPE: null,
        ACTIVTITY_NAME: null,
        HAS_FIXED_CONTRACT: null,
        CAN_DEDUCTABLE: null,
        IN_THB_CS: null,
        IN_GROSS_SALE: null,
        IN_NOT_SALE: null,
        OUT_THB_CS: null,
        OUT_GROSS_SALE: null,
        OUT_NOT_SALE: null,
        NO_RELATE_ABS_AMT: null,
        VALUABLE: null,
        ACTIVITY_DESC: null,
        FLAG_ROW: null,
        IS_ACTIVED: true,
        IS_DELETE_PERMANANT: false


    };

    this.callBack = function (data) {
        console.log(data);
    }
    this.clearValue = function () {
        this.jsonData = {
            ACTIVITY_ID: 0,
            ACC_ID: null,
            ACTIVITY_CODE: null,
            ACTIVITY_TYPE: null,
            ACTIVTITY_NAME: null,
            HAS_FIXED_CONTRACT: null,
            CAN_DEDUCTABLE: null,
            IN_THB_CS: null,
            IN_GROSS_SALE: null,
            IN_NOT_SALE: null,
            OUT_THB_CS: null,
            OUT_GROSS_SALE: null,
            OUT_NOT_SALE: null,
            NO_RELATE_ABS_AMT: null,
            VALUABLE: null,
            ACTIVITY_DESC: null,
            FLAG_ROW: null,
            IS_ACTIVED: true,
            IS_DELETE_PERMANANT: false
        };
    }
    this.setHeading = function (headerTitle) {
        $(this.header_title).html(headerTitle);
    }

    this.init = function () {
        this.clearValue();
        this.bindDom(this.jsonData);
    }
    this.open = function (popupMode, dataSelection, fn) {
        if (popupMode === POPUP_MODE.CREATE) {
            // this.jsonData = dataSelection;
            this.init();
            this.callBack = fn;

            //UI Initial
            $(this.field_brand_code).prop("disabled", false);
            $(this.field_brand_code).parent().removeClass("e-disabled")
            $(this.field_brand_code).parent().addClass("mandatory");

            this.setHeading("Create New Acitivity");
            $(this.button_save).html("Save");
            $(this.button_save).show();
            $(this.button_remove).hide();
            $(this.button_del).hide();



            initcbbox(this.field_has_fixed_contract);
            initcbbox(this.field_can_deductable);
            initcbbox(this.field_in_thb_cs);
            initcbbox(this.field_in_gross_sale);
            initcbbox(this.field_in_not_sale);
            initcbbox(this.field_out_thb_cs);
            initcbbox(this.field_out_gross_sale);
            initcbbox(this.field_out_not_sale);







            $(this.popup_id).modal("show");

        }
        if (popupMode === POPUP_MODE.EDIT) {
            this.jsonData = dataSelection;
            this.callBack = fn;

            if (!dataSelection) //validate selected item
            {
                tradeactivityPopup.callBack(this.jsonData);
                return;
            }
            this.bindDom(this.jsonData);

            //UI Initial



            $(this.field_brand_code).prop("disabled", true);
            $(this.field_brand_code).parent().addClass("e-disabled")
            $(this.field_brand_code).parent().removeClass("mandatory");

            $(this.header_title).html("Edit Activity");
            $(this.button_save).html("Save Changes");
            $(this.button_save).show();
            $(this.button_remove).hide();
            $(this.button_del).hide();
            $(this.popup_id).modal("show");

        }
        if (popupMode === POPUP_MODE.DELETE) {
            this.jsonData = dataSelection;
            this.callBack = fn;


            if (!dataSelection) //validate selected item
            {
                tradeactivityPopup.callBack(this.jsonData);
                return;
            }
            this.openFormDeletePermanent(this.jsonData);
        }
    }
    this.close = function () {
        this.clearValue();
        $(this.popup_id).modal("hide");
    }
    this.bindDom = function (data) {


        $(this.field_activity_id).val(data.ACTIVITY_ID);

        $(this.field_activity_code).val(data.ACTIVITY_CODE);
        $(this.field_activity_type).val(data.ACTIVITY_TYPE);
        $(this.field_activtity_name).val(data.ACTIVTITY_NAME);
        $(this.field_has_fixed_contract).val(data.HAS_FIXED_CONTRACT);
        $(this.field_can_deductable).val(data.CAN_DEDUCTABLE);
        $(this.field_in_thb_cs).val(data.IN_THB_CS);
        $(this.field_in_gross_sale).val(data.IN_GROSS_SALE);
        $(this.field_in_not_sale).val(data.IN_NOT_SALE);
        $(this.field_out_thb_cs).val(data.OUT_THB_CS);
        $(this.field_out_gross_sale).val(data.OUT_GROSS_SALE);
        $(this.field_out_not_sale).val(data.OUT_NOT_SALE);
        $(this.field_no_relate_abs_amt).val(data.NO_RELATE_ABS_AMT);
        $(this.field_valuable).val(data.VALUABLE);
        $(this.field_activity_desc).val(data.ACTIVITY_DESC);

        //    $('#contribution_status_id').val("2");

        //pop-cmd-acc_id_hidden
        //   $("#pop-cmd-acc_id_hidden").val(data.ACC_ID);
        // alert(data.ACC_ID);
        //   $(this.field_acc_id).val(data.ACC_ID);

        //  $('#pop-cmd-acc_id_hidden').append(new Option("Kai1234", data.ACC_ID,data.ACC_ID,data.ACC_ID));
        //$('#pop-cmd-acc_id').append(new Option("Kai1234", data.ACC_ID, data.ACC_ID, data.ACC_ID));

        //pop-cmd-acc_id
        $(this.field_acc_id).val(data.ACC_ID);
        // $("#pop-cmd-acc_id_hidden").appendChild("11", "22");



        //   deleteproduct(this.field_acc_id, data.ACC_ID);


        //pop-cmd-acc_id_hidden


        if (data.HAS_FIXED_CONTRACT === false) {


            $(this.field_has_fixed_contract).prop("checked", "");
            $(this.field_has_fixed_contract).parent().parent().attr("aria-checked", "false");
            $(this.field_has_fixed_contract).parent().children().removeClass("e-check");

        }
        else {

            $(this.field_has_fixed_contract).prop("checked", "checked");
            $(this.field_has_fixed_contract).parent().parent().attr("aria-checked", "true");
            $(this.field_has_fixed_contract).parent().children().addClass("e-check");
            $(this.field_has_fixed_contract).removeClass("e-check");

        }
        if (data.CAN_DEDUCTABLE === false) {


            $(this.field_can_deductable).prop("checked", "");
            $(this.field_can_deductable).parent().parent().attr("aria-checked", "false");
            $(this.field_can_deductable).parent().children().removeClass("e-check");

        }
        else {

            $(this.field_can_deductable).prop("checked", "checked");
            $(this.field_can_deductable).parent().parent().attr("aria-checked", "true");
            $(this.field_can_deductable).parent().children().addClass("e-check");
            $(this.field_can_deductable).removeClass("e-check");

        }
        if (data.IN_THB_CS === false) {


            $(this.field_in_thb_cs).prop("checked", "");
            $(this.field_in_thb_cs).parent().parent().attr("aria-checked", "false");
            $(this.field_in_thb_cs).parent().children().removeClass("e-check");

        }
        else {

            $(this.field_in_thb_cs).prop("checked", "checked");
            $(this.field_in_thb_cs).parent().parent().attr("aria-checked", "true");
            $(this.field_in_thb_cs).parent().children().addClass("e-check");
            $(this.field_in_thb_cs).removeClass("e-check");

        }
        if (data.IN_GROSS_SALE === false) {


            $(this.field_in_gross_sale).prop("checked", "");
            $(this.field_in_gross_sale).parent().parent().attr("aria-checked", "false");
            $(this.field_in_gross_sale).parent().children().removeClass("e-check");

        }
        else {

            $(this.field_in_gross_sale).prop("checked", "checked");
            $(this.field_in_gross_sale).parent().parent().attr("aria-checked", "true");
            $(this.field_in_gross_sale).parent().children().addClass("e-check");
            $(this.field_in_gross_sale).removeClass("e-check");

        }
        if (data.IN_NOT_SALE === false) {


            $(this.field_in_not_sale).prop("checked", "");
            $(this.field_in_not_sale).parent().parent().attr("aria-checked", "false");
            $(this.field_in_not_sale).parent().children().removeClass("e-check");

        }
        else {

            $(this.field_in_not_sale).prop("checked", "checked");
            $(this.field_in_not_sale).parent().parent().attr("aria-checked", "true");
            $(this.field_in_not_sale).parent().children().addClass("e-check");
            $(this.field_in_not_sale).removeClass("e-check");

        }

        if (data.OUT_THB_CS === false) {


            $(this.field_out_thb_cs).prop("checked", "");
            $(this.field_out_thb_cs).parent().parent().attr("aria-checked", "false");
            $(this.field_out_thb_cs).parent().children().removeClass("e-check");

        }
        else {

            $(this.field_out_thb_cs).prop("checked", "checked");
            $(this.field_out_thb_cs).parent().parent().attr("aria-checked", "true");
            $(this.field_out_thb_cs).parent().children().addClass("e-check");
            $(this.field_out_thb_cs).removeClass("e-check");

        }
        if (data.OUT_GROSS_SALE === false) {


            $(this.field_out_gross_sale).prop("checked", "");
            $(this.field_out_gross_sale).parent().parent().attr("aria-checked", "false");
            $(this.field_out_gross_sale).parent().children().removeClass("e-check");

        }
        else {

            $(this.field_out_gross_sale).prop("checked", "checked");
            $(this.field_out_gross_sale).parent().parent().attr("aria-checked", "true");
            $(this.field_out_gross_sale).parent().children().addClass("e-check");
            $(this.field_out_gross_sale).removeClass("e-check");

        }

        if (data.OUT_NOT_SALE === false) {


            $(this.field_out_not_sale).prop("checked", "");
            $(this.field_out_not_sale).parent().parent().attr("aria-checked", "false");
            $(this.field_out_not_sale).parent().children().removeClass("e-check");

        }
        else {

            $(this.field_out_not_sale).prop("checked", "checked");
            $(this.field_out_not_sale).parent().parent().attr("aria-checked", "true");
            $(this.field_out_not_sale).parent().children().addClass("e-check");
            $(this.field_out_not_sale).removeClass("e-check");

        }

        if (data.OUT_NOT_SALE === false) {


            $(this.field_out_not_sale).prop("checked", "");
            $(this.field_out_not_sale).parent().parent().attr("aria-checked", "false");
            $(this.field_out_not_sale).parent().children().removeClass("e-check");

        }
        else {

            $(this.field_out_not_sale).prop("checked", "checked");
            $(this.field_out_not_sale).parent().parent().attr("aria-checked", "true");
            $(this.field_out_not_sale).parent().children().addClass("e-check");
            $(this.field_out_not_sale).removeClass("e-check");

        }

        // alert(data.FLAG_ROW);

        if ((data.FLAG_ROW === "S") || (data.FLAG_ROW == "M")) {

            $(this.field_is_active).prop("checked", "checked");
            $(this.field_is_active).parent().parent().attr("aria-checked", "true");
            $(this.field_is_active).parent().children().addClass("e-check");
            $(this.field_is_active).removeClass("e-check");




        }
        else {


            $(this.field_is_active).prop("checked", "");
            $(this.field_is_active).parent().parent().attr("aria-checked", "false");
            $(this.field_is_active).parent().children().removeClass("e-check");

        }
        /*
        if (data.IS_DELETE_PERMANANT === false) {
            $(this.field_permanant_del).prop("checked", "");
        } else {
            $(this.field_permanant_del).prop("checked", "checked");
        }
        */
    }
    this.bindField = function () {




        //$('#checkbox_id:checked').val();
        //$('#checkbox_id:checked').val();


        //  $(this).prop("checked")
        // alert($(this.field_has_fixed_contract).prop("checked"));
        // if ($(this.field_has_fixed_contract: checked).val())
        //  {
        //     let HAS_FIXED_CONTRACT = true;

        //  }
        //   else {

        //    let HAS_FIXED_CONTRACT = false;

        //    }

        //   alert($(this.field_acc_id).val());

        let ACTIVITY_ID = $(this.field_activity_id).val();

        let ACTIVITY_CODE = $(this.field_activity_code).val();
        let ACTIVITY_TYPE = $(this.field_activity_type).val();
        let ACTIVTITY_NAME = $(this.field_activtity_name).val();

        let HAS_FIXED_CONTRACT = $(this.field_has_fixed_contract).prop("checked");
        let CAN_DEDUCTABLE = $(this.field_can_deductable).prop("checked");

        let IN_THB_CS = $(this.field_in_thb_cs).prop("checked");
        let IN_GROSS_SALE = $(this.field_in_gross_sale).prop("checked");
        let IN_NOT_SALE = $(this.field_in_not_sale).prop("checked");
        let OUT_THB_CS = $(this.field_out_thb_cs).prop("checked");
        let OUT_GROSS_SALE = $(this.field_out_gross_sale).prop("checked");
        let OUT_NOT_SALE = $(this.field_out_not_sale).prop("checked");

        let NO_RELATE_ABS_AMT = $(this.field_no_relate_abs_amt).val();
        let VALUABLE = $(this.field_valuable).val();
        let ACTIVITY_DESC = $(this.field_activity_desc).val();


        //$(this).find('option:selected').val();


        //  let ACC_ID = $(this.field_acc_id).find('option:selected').val();
        let ACC_ID = $("#pop-cmd-acc_id_hidden").find('option:selected').val();
        //pop-cmd-acc_id
        // alert(ACC_ID);
        // $(this.field_acc_id).val();


        let IS_ACTIVED = $(this.field_is_active).prop("checked");
        let IS_DELETE_PERMANANT = $(this.field_permanant_del).prop("checked");

        this.jsonData = {
            ACTIVITY_ID,
            ACC_ID,
            ACTIVITY_CODE,
            ACTIVITY_TYPE,
            ACTIVTITY_NAME,
            HAS_FIXED_CONTRACT,
            CAN_DEDUCTABLE,
            IN_THB_CS,
            IN_GROSS_SALE,
            IN_NOT_SALE,
            OUT_THB_CS,
            OUT_GROSS_SALE,
            OUT_NOT_SALE,
            NO_RELATE_ABS_AMT,
            VALUABLE,
            ACTIVITY_DESC,
            IS_ACTIVED,
            IS_DELETE_PERMANANT
        };
    }
    this.fieldsDisable = function () {
        //$(this.field_brand_id).prop("disabled", true);
        //$(this.field_client_code).prop("disabled", true);
        $(this.field_client_id).prop("disabled", true);
        $(this.field_brand_code).prop("disabled", true);
        $(this.field_brand_name).prop("disabled", true);
        $(this.field_brand_desc).prop("disabled", true);
        $(this.field_is_active).prop("disabled", true);
    }

    this.fieldsEnable = function () {
        //$(this.field_brand_id).prop("disabled", true);
        //$(this.field_client_code).prop("disabled", true);
        $(this.field_client_id).prop("disabled", false);
        $(this.field_brand_code).prop("disabled", false);
        $(this.field_brand_name).prop("disabled", false);
        $(this.field_brand_desc).prop("disabled", false);
        $(this.field_is_active).prop("disabled", false);
    }
    this.onSave = function (e) {
        this.bindField();
        /*
        $(document).trigger("set-alert-id-myid", [
            {
                "message": "Data is duplicate.",
                "priority": 'warning'
            }
        ]);
        */
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
            tradeactivityPopup.callBack(this.jsonData);
            tradeactivityPopup.close();
        }

    }

    this.openFormDeletePermanent = function () {
        let json = tradeactivityPopup.jsonData;
        BootstrapDialog.show({
            type: BootstrapDialog.TYPE_WARNING,
            size: BootstrapDialog.SIZE_SMALL,
            closable: true,
            closeByBackdrop: true,
            closeByKeyboard: true,
            draggable: true,
            title: 'Confirmation',
            message: `<label><input type="checkbox" class="e-control"></input> You're deleting a Acitivity  "${json.ACTIVITY_CODE}", Are you? </label>`,
            buttons: [
                {
                    label: 'Delete',
                    cssClass: 'btn btn-danger',
                    icon: 'fas fa-paper-plane',
                    action: function (self) {
                        let chkAgree = self.getModalBody().find('input').prop("checked");
                        let IS_DELETE_PERMANANT = chkAgree;
                        json = {
                            ...json,
                            IS_DELETE_PERMANANT
                        };
                        self.enableButtons(false);
                        self.setClosable(false);
                        self.getModalBody().html('Processing...');
                        setTimeout(function () {
                            tradeactivityPopup.callBack(json);
                            self.close();
                        }, 1500);
                    }
                },
                {
                    label: 'Close',
                    action: function (self) {
                        self.close();
                    }
                }
            ]
        });
    }

    this.OnRowSelecting = function (args) {
        argruments.data = args.data;
    }

    this.OnRowSelected = function (args) {
        argruments.data = args.data;
    }
    this.OnRowDeselected = function (args) {
        if (argruments.data == args.data) {
            argruments.data = null;
        }
    }
    this.OnAccountIdChangeValue = function (e) {
        let v = e.itemData.Value;
        let t = e.itemData.Text;
        let ACC_ID = e.value;
        alert("OnAccountIdChangeValue");
        $("#pop-cmd-acc_id").val(e.value);
        this.jsonData = {
            ...this.jsonData,
            //CLIENT_CODE,
            ACC_ID
        }
    }

    let appendElement = function (el, form) {
        let dialogTemp = form.querySelector("#dialogTemp");
        dialogTemp.innerHTML = el;
        let formInstance = form.ej2_instances[0];
        //formInstance.addRules('BRAND_ID', { required: true });
        formInstance.addRules('BRAND_CODE', { required: true, minLength: 2 }); //adding the form validation rules
        formInstance.refresh();  // refresh method of the formObj
        let script = document.createElement('script');
        script.type = "text/javascript";
        let serverScript = dialogTemp.querySelector('script');
        script.textContent = serverScript.innerHTML;
        document.head.appendChild(script);
        serverScript.remove();
    }

    this.OnDataBound = function () {
        //var gridObj = document.getElementById('grdBrand')['ej2_instances'][0];
        //Object.assign(gridObj.filterModule.filterOperators, { startsWith: 'contains' });
    }
    this.OnToolbarClick = function (args) {

        let gridObj = document.getElementById("grdTradeactivity").ej2_instances[0];
        if (args.item.id === 'toolbar_excel') {
            gridObj.excelExport();
        }

        if (args.item.id === 'toolbar_add') {
            //args.preventDefault();
        }

        if (args.item.id === 'toolbar_edit') {
            //args.preventDefault();
        }

        if (args.item.id === 'toolbar_add') {
            //args.preventDefault();
        }

    }

})();