const POPUP_MODE = Object.freeze({
    CREATE: 1,
    EDIT: 2,
    DELETE: 3
});

let productPopup = new (function () {

    let self = null;
    let _args = null;

    //UI
    this.popup_id = "#popup-product";
    this.header_title = "#pop-lbl-header-title";
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
    this.field_brand_id = "#pop-txt-product-id";
    this.field_brand_code = "#pop-cmb-brand-code";
    this.field_client_id = "#pop-hid-client-id";
    this.field_prod_code = "#pop-txt-prod_code";
    this.field_brand_name = "#pop-txt-brand-name";
    this.field_brand_desc = "textarea[name='pop-txt-brand-desc']";
    this.field_is_active = "input[id='pop-ckb-is-active']";
    this.field_permanant_del = "#pop-hid-permanant-del";

    //Value Dto
    this.jsonData = {
        BRAND_ID: 0,
        CLIENT_ID: null,
        CLIENT_CODE: null,
        BRAND_CODE: null,
        BRAND_NAME: null,
        BRAND_DESC: null,
        FLAG_ROW: null,
        IS_ACTIVED: null,
        IS_DELETE_PERMANANT: null,
    };

    this.callBack = function (data) {
        console.log(data);
    }
    this.clearValue = function () {
        this.jsonData = {
            BRAND_ID: 0,
            CLIENT_ID: null,
            CLIENT_CODE: null,
            BRAND_CODE: null,
            BRAND_NAME: null,
            BRAND_DESC: null,
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

            $(this.field_brand_code).prop("disabled", false);
            $(this.field_brand_code).addClass("mandatory");

            this.setHeading("Create New Brand");
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
                productPopup.callBack(this.jsonData);
                return;
            }
            this.bindDom(this.jsonData);

            $(this.field_brand_code).prop("disabled", true);
            $(this.field_brand_code).removeClass("mandatory");

            $(this.header_title).html("Edit Brand");
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
                productPopup.callBack(this.jsonData);
                return;
            }
            $(this.header_title).html("Delete Brand");
            $(this.button_remove).show();
            $(this.button_del).show();
            $(this.button_save).hide();
            $(this.popup_id).modal("show");

        }
    }
    this.close = function () {
        this.clearValue();
        $(this.popup_id).modal("hide");
    }
    this.bindDom = function (data) {

        $(this.field_brand_id).val(data.BRAND_ID);
        $(this.field_client_code).val(data.CLIENT_CODE);
        $(this.field_client_id).val(data.CLIENT_ID);
        $(this.field_prod_code).val(data.PROD_CODE);
        $(this.field_brand_name).val(data.BRAND_NAME);
        $(this.field_brand_desc).val(data.BRAND_DESC);
        $(this.field_is_active).prop("checked", data.IS_ACTIVED);
        $(this.field_permanant_del).val(data.IS_DELETE_PERMANANT);
    }
    this.bindField = function () {
        let BRAND_ID = $(this.field_brand_id).val();
        let CLIENT_CODE = $(this.field_client_code).val();
        let CLIENT_ID = $(this.field_client_id).val();
        let PROD_CODE = $(this.field_prod_code).val();
        let BRAND_NAME = $(this.field_brand_name).val();
        let BRAND_DESC = $(this.field_brand_desc).val();
        let IS_ACTIVED = $(this.field_is_active).val();
        let IS_DELETE_PERMANANT = $(this.field_permanant_del).val();

        this.jsonData = {
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
            productPopup.callBack(this.jsonData);
            productPopup.close();
        }

    }
    this.onDelete = function (e) {
        let IS_ACTIVED = false;
        this.jsonData = {
            ...this.jsonData,
            IS_ACTIVED
        };
        productPopup.callBack(this.jsonData);
        //productPopup.close();
    }

    this.onDeletePerm = function (e) {
        let IS_DELETE_PERMANANT = true;
        this.jsonData = {
            ...this.jsonData,
            IS_DELETE_PERMANANT
        };
        productPopup.callBack(this.jsonData);
        //productPopup.close();
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
        let gridObj = document.getElementById("grdProduct").ej2_instances[0];
        if (args.item.id === 'grdProduct_excelexport') {
            gridObj.excelExport();
        }
    }

})();