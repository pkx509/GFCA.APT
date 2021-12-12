const POPUP_MODE = Object.freeze({
    CREATE: 1,
    EDIT: 2,
    DELETE: 3
});

let budgetPlanHeaderPopup = new (function () {

    let self = null;
    let _args = null;

    //UI
    this.popup_id = "#popup-budgetHeader";
    this.header_title = "#pop-lbl-sale-header-title";
    this.button_remove = "#pop-btn-del-permanat";
    this.button_del = "#pop-btn-del";
    this.button_save = "#pop-btn-save";

    this.MODE_TYPE = {
        CREATE: 1,
        EDIT: 2,
        DELETE: 3
    }

    this.isCreateState = true;

    //Model Dto

    this.field_emis_code_text = "#pop-cmb-emis-code";
    this.field_emis_code_value = "#pop-cmb-emis-code_hidden";

    this.field_channel_code = "#pop-txt-channel-code";
    this.field_channel_name = "#pop-txt-channel-name";
    this.field_channel_desc = "textarea[name='pop-txt-channel-desc']";
    this.field_is_active = "input[id='pop-ckb-is-active']";
    this.field_permanant_del = "#pop-hid-permanant-del";

    //Value Dto
    this.jsonData = {
        EMIS_CODE: null,
        CHANNEL_CODE: null,
        CHANNEL_NAME: null,
        CHANNEL_DESC: null,
        BRAND_NAME: null,
        FLAG_ROW: null,
        IS_ACTIVED: null,
        IS_DELETE_PERMANANT: null,
    };

    this.callBack = function (data) {
        console.log(data);
    }
    this.clearValue = function () {
        this.jsonData = {
            EMIS_CODE: null,
            CHANNEL_CODE: null,
            CHANNEL_NAME: null,
            CHANNEL_DESC: null,
            BRAND_NAME: null,
            FLAG_ROW: null,
            IS_ACTIVED: null,
            IS_DELETE_PERMANANT: null,
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

            //this.jsonData = dataSelection;
            this.init();
            this.callBack = fn;

            $(this.field_channel_code).prop("disabled", false);
            $(this.field_channel_code).addClass("mandatory");

            this.setHeading("Create New Sale");
            $(this.button_save).html("Save");
            $(this.button_save).show();
            $(this.button_remove).hide();
            $(this.button_del).hide();
            $(this.popup_id).modal("show");

        }
        if (popupMode === POPUP_MODE.EDIT) {

            this.jsonData = dataSelection;
            this.callBack = fn;

            if (!dataSelection) //validate selected item
            {
                detailGridSalePartialPopup.callBack(this.jsonData);
                return;
            }
            this.bindDom(this.jsonData);

            $(this.field_channel_code).prop("disabled", true);
            $(this.field_channel_code).parent().addClass("e-disabled")
            $(this.field_channel_code).parent().removeClass("mandatory");



            $(this.header_title).html("Edit Channel");
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
                detailGridSalePartialPopup.callBack(this.jsonData);
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



        $(this.field_emis_code_text).empty();
        var o = new Option(data.EMIS_NAME, data.EMIS_CODE, true);
        $(this.field_emis_code_text).append(o);
        $(this.field_emis_code_text).val(data.EMIS_NAME);
        $(this.field_emis_code_text).attr("aria-label", data.EMIS_NAME);

        $(this.field_channel_code).val(data.CHANNEL_CODE);
        $(this.field_channel_name).val(data.CHANNEL_NAME);
        $(this.field_channel_desc).val(data.CHANNEL_DESC);
        $(this.field_permanant_del).val(data.IS_DELETE_PERMANANT);


        if (data.FLAG_ROW == 'D') {


            $(this.field_is_active).prop("checked", "");
            $(this.field_is_active).parent().parent().attr("aria-checked", "false");
            $(this.field_is_active).parent().children().removeClass("e-check");

        } else {
            $(this.field_is_active).prop("checked", "checked");
            $(this.field_is_active).parent().parent().attr("aria-checked", "true");
            $(this.field_is_active).parent().children().addClass("e-check");
            $(this.field_is_active).removeClass("e-check");
        }


    }
    this.bindField = function () {


        let EMIS_CODE = $(this.field_emis_code_value).val();
        let EMIS_NAME = $(this.field_emis_code_text).val();

        //  alert(EMIS_CODE);
        // alert(EMIS_NAME);

        let CHANNEL_CODE = $(this.field_channel_code).val();
        let CHANNEL_NAME = $(this.field_channel_name).val();
        let CHANNEL_DESC = $(this.field_channel_desc).val();
        //    let IS_ACTIVED = $(this.field_is_active).val();
        let IS_ACTIVED = $(this.field_is_active).prop("checked");

        let IS_DELETE_PERMANANT = $(this.field_permanant_del).val();

        this.jsonData = {
            EMIS_CODE, EMIS_NAME, CHANNEL_CODE, CHANNEL_NAME, CHANNEL_DESC,
            IS_ACTIVED, IS_DELETE_PERMANANT
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
            detailGridSalePartialPopup.callBack(this.jsonData);
            detailGridSalePartialPopup.close();
        }

    }
    this.onDelete = function (e) {
        let IS_ACTIVED = false;
        this.jsonData = {
            ...this.jsonData,
            IS_ACTIVED
        };
        detailGridSalePartialPopup.callBack(this.jsonData);
        //detailGridSalePartialPopup.close();
    }

    this.onDeletePerm = function (e) {
        let IS_DELETE_PERMANANT = true;
        this.jsonData = {
            ...this.jsonData,
            IS_DELETE_PERMANANT
        };
        detailGridSalePartialPopup.callBack(this.jsonData);
        //detailGridSalePartialPopup.close();
    }

    this.onTestNestModal = function (e) {
        BootstrapDialog.show({
            type: BootstrapDialog.TYPE_DEFAULT,
            size: BootstrapDialog.SIZE_WIDE,
            closable: false,
            closeByBackdrop: false,
            closeByKeyboard: false,
            draggable: true,
            title: 'Confirm',
            message: 'Write your example here.',
            buttons: [
                {
                    label: 'Save',
                    cssClass: 'btn-primary',
                    icon: 'glyphicon glyphicon-send',
                    action: function (self) {
                        alert("do process something")

                        self.enableButtons(false);
                        self.setClosable(false);
                        self.getModalBody().html('Inprocessing...');
                        setTimeout(function () {
                            self.close();
                        }, 3000);
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
    this.OnClientChangeValue = function (e) {
        //let v = e.itemData.Value;
        let t = e.itemData.Text;
        let CLIENT_ID = e.value;
        this.jsonData = {
            ...this.jsonData,
            //CLIENT_CODE,
            CLIENT_ID
        }
    }

    this.openFormDeletePermanent = function () {
        let json = detailGridSalePartialPopup.jsonData;
        BootstrapDialog.show({
            type: BootstrapDialog.TYPE_WARNING,
            size: BootstrapDialog.SIZE_SMALL,
            closable: true,
            closeByBackdrop: true,
            closeByKeyboard: true,
            draggable: true,
            title: 'Confirmation',
            message: `<label><input type="checkbox" class="e-control"></input> You're deleting a Channel  "${json.CHANNEL_CODE}", Are you? </label>`,
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
                            detailGridSalePartialPopup.callBack(json);
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
    let appendElement = function (el, form) {
        let dialogTemp = form.querySelector("#dialogTemp");
        dialogTemp.innerHTML = el;
        let formInstance = form.ej2_instances[0];
        //formInstance.addRules('BRAND_ID', { required: true });
        formInstance.addRules('CHANNEL_CODE', { required: true, minLength: 10 }); //adding the form validation rules
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
    this.OnExcelExportClick = function (args) {
        let gridObj = document.getElementById("grdChannel").ej2_instances[0];
        if (args.item.id === 'grdChannel_excelexport') {
            gridObj.excelExport();
        }
    }
})();

// budget plan sale
let detailGridSalePartialPopup = new (function () {

    let self = null;
    let _args = null;

    //UI
    this.popup_id = "#popup-budgetHeader";
    this.header_title = "#pop-lbl-sale-header-title";
    this.button_remove = "#pop-btn-del-permanat";
    this.button_del = "#pop-btn-del";
    this.button_save = "#pop-btn-save";

    this.MODE_TYPE = {
        CREATE: 1,
        EDIT: 2,
        DELETE: 3
    }

    this.isCreateState = true;

    //Model Dto

    this.field_emis_code_text = "#pop-cmb-emis-code";
    this.field_emis_code_value = "#pop-cmb-emis-code_hidden";

    this.field_channel_code = "#pop-txt-channel-code";
    this.field_channel_name = "#pop-txt-channel-name";
    this.field_channel_desc = "textarea[name='pop-txt-channel-desc']";
    this.field_is_active = "input[id='pop-ckb-is-active']";
    this.field_permanant_del = "#pop-hid-permanant-del";

    //Value Dto
    this.jsonData = {
        EMIS_CODE: null,
        CHANNEL_CODE: null,
        CHANNEL_NAME: null,
        CHANNEL_DESC: null,
        BRAND_NAME: null,
        FLAG_ROW: null,
        IS_ACTIVED: null,
        IS_DELETE_PERMANANT: null,
    };

    this.callBack = function (data) {
        console.log(data);
    }
    this.clearValue = function () {
        this.jsonData = {
            EMIS_CODE: null,
            CHANNEL_CODE: null,
            CHANNEL_NAME: null,
            CHANNEL_DESC: null,
            BRAND_NAME: null,
            FLAG_ROW: null,
            IS_ACTIVED: null,
            IS_DELETE_PERMANANT: null,
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

            //this.jsonData = dataSelection;
            this.init();
            this.callBack = fn;

            $(this.field_channel_code).prop("disabled", false);
            $(this.field_channel_code).addClass("mandatory");

            this.setHeading("Create New Sale");
            $(this.button_save).html("Save");
            $(this.button_save).show();
            $(this.button_remove).hide();
            $(this.button_del).hide();
            $(this.popup_id).modal("show");

        }
        if (popupMode === POPUP_MODE.EDIT) {

            this.jsonData = dataSelection;
            this.callBack = fn;

            if (!dataSelection) //validate selected item
            {
                detailGridSalePartialPopup.callBack(this.jsonData);
                return;
            }
            this.bindDom(this.jsonData);

            $(this.field_channel_code).prop("disabled", true);
            $(this.field_channel_code).parent().addClass("e-disabled")
            $(this.field_channel_code).parent().removeClass("mandatory");



            $(this.header_title).html("Edit Channel");
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
                detailGridSalePartialPopup.callBack(this.jsonData);
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



        $(this.field_emis_code_text).empty();
        var o = new Option(data.EMIS_NAME, data.EMIS_CODE, true);
        $(this.field_emis_code_text).append(o);
        $(this.field_emis_code_text).val(data.EMIS_NAME);
        $(this.field_emis_code_text).attr("aria-label", data.EMIS_NAME);

        $(this.field_channel_code).val(data.CHANNEL_CODE);
        $(this.field_channel_name).val(data.CHANNEL_NAME);
        $(this.field_channel_desc).val(data.CHANNEL_DESC);
        $(this.field_permanant_del).val(data.IS_DELETE_PERMANANT);


        if (data.FLAG_ROW == 'D') {


            $(this.field_is_active).prop("checked", "");
            $(this.field_is_active).parent().parent().attr("aria-checked", "false");
            $(this.field_is_active).parent().children().removeClass("e-check");

        } else {
            $(this.field_is_active).prop("checked", "checked");
            $(this.field_is_active).parent().parent().attr("aria-checked", "true");
            $(this.field_is_active).parent().children().addClass("e-check");
            $(this.field_is_active).removeClass("e-check");
        }


    }
    this.bindField = function () {


        let EMIS_CODE = $(this.field_emis_code_value).val();
        let EMIS_NAME = $(this.field_emis_code_text).val();

        //  alert(EMIS_CODE);
        // alert(EMIS_NAME);

        let CHANNEL_CODE = $(this.field_channel_code).val();
        let CHANNEL_NAME = $(this.field_channel_name).val();
        let CHANNEL_DESC = $(this.field_channel_desc).val();
        //    let IS_ACTIVED = $(this.field_is_active).val();
        let IS_ACTIVED = $(this.field_is_active).prop("checked");

        let IS_DELETE_PERMANANT = $(this.field_permanant_del).val();

        this.jsonData = {
            EMIS_CODE, EMIS_NAME, CHANNEL_CODE, CHANNEL_NAME, CHANNEL_DESC,
            IS_ACTIVED, IS_DELETE_PERMANANT
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
            detailGridSalePartialPopup.callBack(this.jsonData);
            detailGridSalePartialPopup.close();
        }

    }
    this.onDelete = function (e) {
        let IS_ACTIVED = false;
        this.jsonData = {
            ...this.jsonData,
            IS_ACTIVED
        };
        detailGridSalePartialPopup.callBack(this.jsonData);
        //detailGridSalePartialPopup.close();
    }

    this.onDeletePerm = function (e) {
        let IS_DELETE_PERMANANT = true;
        this.jsonData = {
            ...this.jsonData,
            IS_DELETE_PERMANANT
        };
        detailGridSalePartialPopup.callBack(this.jsonData);
        //detailGridSalePartialPopup.close();
    }

    this.onTestNestModal = function (e) {
        BootstrapDialog.show({
            type: BootstrapDialog.TYPE_DEFAULT,
            size: BootstrapDialog.SIZE_WIDE,
            closable: false,
            closeByBackdrop: false,
            closeByKeyboard: false,
            draggable: true,
            title: 'Confirm',
            message: 'Write your example here.',
            buttons: [
                {
                    label: 'Save',
                    cssClass: 'btn-primary',
                    icon: 'glyphicon glyphicon-send',
                    action: function (self) {
                        alert("do process something")

                        self.enableButtons(false);
                        self.setClosable(false);
                        self.getModalBody().html('Inprocessing...');
                        setTimeout(function () {
                            self.close();
                        }, 3000);
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
    this.OnClientChangeValue = function (e) {
        //let v = e.itemData.Value;
        let t = e.itemData.Text;
        let CLIENT_ID = e.value;
        this.jsonData = {
            ...this.jsonData,
            //CLIENT_CODE,
            CLIENT_ID
        }
    }

    this.openFormDeletePermanent = function () {
        let json = detailGridSalePartialPopup.jsonData;
        BootstrapDialog.show({
            type: BootstrapDialog.TYPE_WARNING,
            size: BootstrapDialog.SIZE_SMALL,
            closable: true,
            closeByBackdrop: true,
            closeByKeyboard: true,
            draggable: true,
            title: 'Confirmation',
            message: `<label><input type="checkbox" class="e-control"></input> You're deleting a Channel  "${json.CHANNEL_CODE}", Are you? </label>`,
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
                            detailGridSalePartialPopup.callBack(json);
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
    let appendElement = function (el, form) {
        let dialogTemp = form.querySelector("#dialogTemp");
        dialogTemp.innerHTML = el;
        let formInstance = form.ej2_instances[0];
        //formInstance.addRules('BRAND_ID', { required: true });
        formInstance.addRules('CHANNEL_CODE', { required: true, minLength: 10 }); //adding the form validation rules
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
    this.OnExcelExportClick = function (args) {
        let gridObj = document.getElementById("grdChannel").ej2_instances[0];
        if (args.item.id === 'grdChannel_excelexport') {
            gridObj.excelExport();
        }
    }
})();

// buget plan investment 
let detailGridInvestmentPartialPopup = new (function () {

    let self = null;
    let _args = null;

    //UI
    this.popup_id = "#popup-detailGridInvestmentPartial";
    this.header_title = "#pop-lbl-investment-header-title";
    this.button_remove = "#pop-btn-del-permanat";
    this.button_del = "#pop-btn-del";
    this.button_save = "#pop-btn-save";

    this.MODE_TYPE = {
        CREATE: 1,
        EDIT: 2,
        DELETE: 3
    }

    this.isCreateState = true;

    //Model Dto

    this.field_emis_code_text = "#pop-cmb-emis-code";
    this.field_emis_code_value = "#pop-cmb-emis-code_hidden";

    this.field_channel_code = "#pop-txt-channel-code";
    this.field_channel_name = "#pop-txt-channel-name";
    this.field_channel_desc = "textarea[name='pop-txt-channel-desc']";
    this.field_is_active = "input[id='pop-ckb-is-active']";
    this.field_permanant_del = "#pop-hid-permanant-del";

    //Value Dto
    this.jsonData = {
        EMIS_CODE: null,
        CHANNEL_CODE: null,
        CHANNEL_NAME: null,
        CHANNEL_DESC: null,
        BRAND_NAME: null,
        FLAG_ROW: null,
        IS_ACTIVED: null,
        IS_DELETE_PERMANANT: null,
    };

    this.callBack = function (data) {
        console.log(data);
    }
    this.clearValue = function () {
        this.jsonData = {
            EMIS_CODE: null,
            CHANNEL_CODE: null,
            CHANNEL_NAME: null,
            CHANNEL_DESC: null,
            BRAND_NAME: null,
            FLAG_ROW: null,
            IS_ACTIVED: null,
            IS_DELETE_PERMANANT: null,
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

            //this.jsonData = dataSelection;
            this.init();
            this.callBack = fn;

            $(this.field_channel_code).prop("disabled", false);
            $(this.field_channel_code).addClass("mandatory");

            this.setHeading("Create New Investment");
            $(this.button_save).html("Save");
            $(this.button_save).show();
            $(this.button_remove).hide();
            $(this.button_del).hide();
            $(this.popup_id).modal("show");

        }
        if (popupMode === POPUP_MODE.EDIT) {

            this.jsonData = dataSelection;
            this.callBack = fn;

            if (!dataSelection) //validate selected item
            {
                detailGridInvestmentPartialPopup.callBack(this.jsonData);
                return;
            }
            this.bindDom(this.jsonData);

            $(this.field_channel_code).prop("disabled", true);
            $(this.field_channel_code).parent().addClass("e-disabled")
            $(this.field_channel_code).parent().removeClass("mandatory");



            $(this.header_title).html("Edit Channel");
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
                detailGridInvestmentPartialPopup.callBack(this.jsonData);
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



        $(this.field_emis_code_text).empty();
        var o = new Option(data.EMIS_NAME, data.EMIS_CODE, true);
        $(this.field_emis_code_text).append(o);
        $(this.field_emis_code_text).val(data.EMIS_NAME);
        $(this.field_emis_code_text).attr("aria-label", data.EMIS_NAME);

        $(this.field_channel_code).val(data.CHANNEL_CODE);
        $(this.field_channel_name).val(data.CHANNEL_NAME);
        $(this.field_channel_desc).val(data.CHANNEL_DESC);
        $(this.field_permanant_del).val(data.IS_DELETE_PERMANANT);


        if (data.FLAG_ROW == 'D') {


            $(this.field_is_active).prop("checked", "");
            $(this.field_is_active).parent().parent().attr("aria-checked", "false");
            $(this.field_is_active).parent().children().removeClass("e-check");

        } else {
            $(this.field_is_active).prop("checked", "checked");
            $(this.field_is_active).parent().parent().attr("aria-checked", "true");
            $(this.field_is_active).parent().children().addClass("e-check");
            $(this.field_is_active).removeClass("e-check");
        }


    }
    this.bindField = function () {


        let EMIS_CODE = $(this.field_emis_code_value).val();
        let EMIS_NAME = $(this.field_emis_code_text).val();

        //  alert(EMIS_CODE);
        // alert(EMIS_NAME);

        let CHANNEL_CODE = $(this.field_channel_code).val();
        let CHANNEL_NAME = $(this.field_channel_name).val();
        let CHANNEL_DESC = $(this.field_channel_desc).val();
        //    let IS_ACTIVED = $(this.field_is_active).val();
        let IS_ACTIVED = $(this.field_is_active).prop("checked");

        let IS_DELETE_PERMANANT = $(this.field_permanant_del).val();

        this.jsonData = {
            EMIS_CODE, EMIS_NAME, CHANNEL_CODE, CHANNEL_NAME, CHANNEL_DESC,
            IS_ACTIVED, IS_DELETE_PERMANANT
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
            detailGridInvestmentPartialPopup.callBack(this.jsonData);
            detailGridInvestmentPartialPopup.close();
        }

    }
    this.onDelete = function (e) {
        let IS_ACTIVED = false;
        this.jsonData = {
            ...this.jsonData,
            IS_ACTIVED
        };
        detailGridInvestmentPartialPopup.callBack(this.jsonData);
        //detailGridInvestmentPartialPopup.close();
    }

    this.onDeletePerm = function (e) {
        let IS_DELETE_PERMANANT = true;
        this.jsonData = {
            ...this.jsonData,
            IS_DELETE_PERMANANT
        };
        detailGridInvestmentPartialPopup.callBack(this.jsonData);
        //detailGridInvestmentPartialPopup.close();
    }

    this.onTestNestModal = function (e) {
        BootstrapDialog.show({
            type: BootstrapDialog.TYPE_DEFAULT,
            size: BootstrapDialog.SIZE_WIDE,
            closable: false,
            closeByBackdrop: false,
            closeByKeyboard: false,
            draggable: true,
            title: 'Confirm',
            message: 'Write your example here.',
            buttons: [
                {
                    label: 'Save',
                    cssClass: 'btn-primary',
                    icon: 'glyphicon glyphicon-send',
                    action: function (self) {
                        alert("do process something")

                        self.enableButtons(false);
                        self.setClosable(false);
                        self.getModalBody().html('Inprocessing...');
                        setTimeout(function () {
                            self.close();
                        }, 3000);
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
    this.OnClientChangeValue = function (e) {
        //let v = e.itemData.Value;
        let t = e.itemData.Text;
        let CLIENT_ID = e.value;
        this.jsonData = {
            ...this.jsonData,
            //CLIENT_CODE,
            CLIENT_ID
        }
    }

    this.openFormDeletePermanent = function () {
        let json = detailGridInvestmentPartialPopup.jsonData;
        BootstrapDialog.show({
            type: BootstrapDialog.TYPE_WARNING,
            size: BootstrapDialog.SIZE_SMALL,
            closable: true,
            closeByBackdrop: true,
            closeByKeyboard: true,
            draggable: true,
            title: 'Confirmation',
            message: `<label><input type="checkbox" class="e-control"></input> You're deleting a Channel  "${json.CHANNEL_CODE}", Are you? </label>`,
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
                            detailGridInvestmentPartialPopup.callBack(json);
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
    let appendElement = function (el, form) {
        let dialogTemp = form.querySelector("#dialogTemp");
        dialogTemp.innerHTML = el;
        let formInstance = form.ej2_instances[0];
        //formInstance.addRules('BRAND_ID', { required: true });
        formInstance.addRules('CHANNEL_CODE', { required: true, minLength: 10 }); //adding the form validation rules
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
    this.OnExcelExportClick = function (args) {
        let gridObj = document.getElementById("grdChannel").ej2_instances[0];
        if (args.item.id === 'grdChannel_excelexport') {
            gridObj.excelExport();
        }
    }
})();