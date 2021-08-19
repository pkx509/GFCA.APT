let brandPopup = new (function () {

    let self = null;
    let _args = null;
    let _popupMode = null;

    //UI
    this.popup_id = "#popup-brand";
    this.header_title = "#pop-lbl-header-title";
    this.button_remove = "#pop-btn-del-permanat";
    this.button_save = "#pop-btn-save";

    this.MODE_TYPE = ["CREATE", "EDIT", "DELETE"]
    this.isCreateState = true;

    //Model Dto
    this.field_brand_id = "#pop-txt-brand-id";
    this.field_client_code = "#pop-cmb-client-code";
    this.field_client_id = "#pop-hid-client-id";
    this.field_brand_code = "#pop-txt-brand-code";
    this.field_brand_name = "#pop-txt-brand-name";
    this.field_brand_desc = "#pop-txt-brand-desc";
    this.field_is_active = "input[id='pop-ckb-is-active']:checked";
    this.field_permanant_del = "#pop-hid-permanant-del";

    //Value Dto
    this.dataFields = {
        BRAND_ID: 0,
        CLIENT_ID: null,
        CLIENT_CODE: null,
        BRAND_CODE: null,
        BRAND_NAME: null,
        BRAND_DESC: null,
        IS_ACTIVED: null,
        IS_DELETE_PERMANANT: null,
        FLAG_ROW: null
    };

    this.callBack = function (data) {
        console.log(data);
    }
    this.clearValue = function () {
        this.dataFields = {
            BRAND_ID: 0,
            CLIENT_ID: null,
            CLIENT_CODE: null,
            BRAND_CODE: null,
            BRAND_NAME: null,
            BRAND_DESC: null,
            IS_ACTIVED: null,
            IS_DELETE_PERMANANT: null,
            FLAG_ROW: null
        };
    }
    this.setHeading = function () {
        if (this.isCreateState === true) {
            $(this.header_title).html("Create New Brand");
        }
    }

    this.init = function () {
        this.clearValue();
    }
    this.openCreate = function (isCreateState, dataSelection, fn) {
        this.isCreateState = isCreateState;
        //this.dataFields = dataSelection;
        this.init();
        this.callBack = fn;
        $(this.header_title).html("Create New Brand");

        $(this.button_save).html("Save");
        $(this.button_save).show();
        $(this.button_remove).hide();
        $(this.popup_id).modal("show");
    }
    this.openEdit = function (isCreateState, dataSelection, fn) {
        this.isCreateState = isCreateState;
        this.dataFields = dataSelection;
        this.callBack = fn;

        if (!dataSelection) //validate selected item
        {
            brandPopup.callBack(this.dataFields);
            return;
        }
        $(this.header_title).html("Edit Brand");
        $(this.button_save).html("Save Changes");
        $(this.button_save).show();
        $(this.button_remove).hide();
        $(this.popup_id).modal("show");
    }
    this.openDelete = function (isCreateState, dataSelection, fn) {
        this.isCreateState = isCreateState;
        this.dataFields = dataSelection;
        this.callBack = fn;

        if (!dataSelection) //validate selected item
        {
            brandPopup.callBack(this.dataFields);
            return;
        }
        $(this.header_title).html("Delete Brand");
        $(this.button_remove).show();
        $(this.button_save).hide();
        $(this.popup_id).modal("show");
    }
    this.close = function () {
        this.clearValue();
        $(this.popup_id).modal("hide");
    }

    this.fieldsBind = function () {
        let BRAND_ID = $(this.field_brand_id).val();
        let CLIENT_CODE = $(this.field_client_code).val();
        let CLIENT_ID = $(this.field_client_id).val();
        let BRAND_CODE = $(this.field_brand_code).val();
        let BRAND_NAME = $(this.field_brand_name).val();
        let BRAND_DESC = $(this.field_brand_desc).val();
        let IS_ACTIVED = $(this.field_is_active).val();
        let IS_DELETE_PERMANANT = $(this.field_permanant_del).val();

        this.dataFields = {
            BRAND_ID, CLIENT_CODE, CLIENT_ID, BRAND_CODE, BRAND_NAME, BRAND_DESC,
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
        this.fieldsBind();
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
            this.dataFields = {
                ...this.dataFields,
                IS_DELETE_PERMANANT
            };
            brandPopup.callBack(this.dataFields);
            brandPopup.close();
        }

    }
    this.onRemovePerm = function (e) {
        let IS_DELETE_PERMANANT = true;
        this.dataFields = {
            ...this.dataFields,
            IS_DELETE_PERMANANT
        };
        brandPopup.callBack(this.dataFields);
        //brandPopup.close();
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
        //this.dataFields = args.data;
    }

    this.OnRowSelected = function (args) {
        argruments.data = args.data;
        //this.dataFields = args.data;
    }
    this.OnRowDeselected = function (args) {
        if (argruments.data == args.data) {
            argruments.data = null;
            //this.clearValue();
        }
    }
    this.OnClientChangeValue = function (e) {
        //let v = e.itemData.Value;
        let t = e.itemData.Text;
        let CLIENT_ID = e.value;
        this.fieldsBind = {
            ...this.fieldsBind,
            //CLIENT_CODE,
            CLIENT_ID
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
    this.OnExcelExportClick = function (args) {
        let gridObj = document.getElementById("grdBrand").ej2_instances[0];
        if (args.item.id === 'grdBrand_excelexport') {
            gridObj.excelExport();
        }
    }

})();