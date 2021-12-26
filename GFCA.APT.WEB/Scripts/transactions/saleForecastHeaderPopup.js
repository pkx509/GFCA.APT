const POPUP_MODE = Object.freeze({
    CREATE: 1,
    EDIT: 2,
    DELETE: 3
});

let saleForecastHeaderPopup = new (function () {

    let self = null;
    let _args = null;

    //UI
    this.popup_id = "#popup-saleForecastHeader";
    this.header_title = "#pop-lbl-header-title";
    this.button_save = "#pop-btn-save";

    this.MODE_TYPE = {
        CREATE: 1,
        EDIT: 2,
        DELETE: 3
    }

    this.isCreateState = true;

    //Model Dto
    //this.field_company_code = "#pop-cmb-company_hidden";
    //this.field_company_name = "#pop-cmb-company";
    this.field_document_no = "#pop-txt-document-no";
    //this.field_client_code = "#pop-cmb-client_hidden";
    //this.field_client_name = "#pop-cmb-client";
    this.field_customer_code = "#pop-cmb-customer_hidden";
    this.field_customer_name = "#pop-cmb-customer";
    this.field_channel_code = "#pop-cmb-channel_hidden";
    this.field_channel_name = "#pop-cmb-channel";
    this.field_requester = "#pop-txt-requester";
    //this.field_position_code = "#pop-cmb-position_hidden";
    //this.field_position_name = "#pop-cmb-position";

    //SaleForecastHeaderDto
    this.jsonData = {
        DOC_TYPE_CODE: null,
        DOC_CODE: null,
        DOC_VER: null,
        DOC_REV: null,
        //DOC_MONTH: null,
        YEAR: null,
        DOC_STATUS: null,
        FLOW_CURRENT: null,
        FLOW_NEXT: null,
        REQUESTER: null,
        DOC_SFCH_ID: null,
        //CLIENT_CODE: null,
        //CLIENT_NAME: null,
        CUST_CODE: null,
        CHANNEL_CODE: null,
        CHANNEL_NAME: null,
        COMMENT: null,
        FLAG_ROW: null,
    //    COMMAND_TYPE: null,
    //    OGR_CODE: null,
    //    COMP_CODE: null
    };

    this.callBack = function (data) {
        console.log(data);
    }

    this.clearValue = function () {
        this.jsonData = {
            DOC_TYPE_CODE: null,
            DOC_CODE: null,
            DOC_VER: null,
            DOC_REV: null,
            //DOC_MONTH: null,
            YEAR: null,
            DOC_STATUS: null,
            FLOW_CURRENT: null,
            FLOW_NEXT: null,
            REQUESTER: null,
            DOC_SFCH_ID: null,
            //CLIENT_CODE: null,
            //CLIENT_NAME: null,
            CUST_CODE: null,
            CHANNEL_CODE: null,
            CHANNEL_NAME: null,
            COMMENT: null,
            FLAG_ROW: null,
        //    COMMAND_TYPE: null,
        //    ORG_CODE: null,
        //    COMP_CODE: null
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
            this.init();
            this.callBack = fn;
            this.setHeading("Create New Sale Forecast");
            $(this.button_save).val("CREATE");
            $(this.popup_id).modal("show");
        }
        /*
        if (popupMode === POPUP_MODE.EDIT) {
            this.jsonData = dataSelection;
            this.callBack = fn;

            if (!dataSelection) //validate selected item
            {
                saleForecastHeaderPopup.callBack(this.jsonData);
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
                saleForecastHeaderPopup.callBack(this.jsonData);
                return;
            }
            this.openFormDeletePermanent(this.jsonData);
        }
        */
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
        let currentDate = new Date();
        let docId = $(this.field_document_no).val() !== undefined ? $(this.field_document_no).val() : '0';

        //let COMPANY = $(this.field_company).val();
        let DOC_TYPE_CODE = 'SF';
        let DOC_CODE = docId;
        //let DOC_MONTH = currentDate.getMonth();
        let YEAR = currentDate.getFullYear();
        let DOC_STATUS = 'DRAFT';
        let DOC_SFCH_ID = docId;
        //let CLIENT_CODE = $(this.field_client_code).val();
        //let CLIENT_NAME = $(this.field_client_name).val();
        let CUST_CODE = $(this.field_customer_code).val();
        let CUST_NAME = $(this.field_customer_name).val()
        let CHANNEL_CODE = $(this.field_channel_code).val();
        let CHANNEL_NAME = $(this.field_channel_name).val();
        let REQUESTER = $(this.field_requester).val();
        //let POSITION_CODE = $(this.field_position_code).val();
        //let POSITION_NAME = $(this.field_position_name).val();
        //let COMMAND_TYPE = $(this.button_save).val();
        //let ORG_CODE = $(this.field_position_code).val();
        //let ORG_NAME = $(this.field_position_name).val();
        //let COMP_CODE = $(this.field_company_code).val();
        //let COMP_NAME = $(this.field_company_name).val();



        this.jsonData = {
            DOC_TYPE_CODE, DOC_CODE, YEAR, DOC_STATUS, DOC_SFCH_ID, CUST_CODE,
            CUST_NAME, CHANNEL_CODE, CHANNEL_NAME, REQUESTER
        };
    }
    /*
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
    */

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
            saleForecastHeaderPopup.callBack(this.jsonData);
            saleForecastHeaderPopup.close();
        }
    }

    this.onDelete = function (e) {
        let IS_ACTIVED = false;
        this.jsonData = {
            ...this.jsonData,
            IS_ACTIVED
        };
        saleForecastHeaderPopup.callBack(this.jsonData);
        //saleForecastHeaderPopup.close();
    }

    this.onDeletePerm = function (e) {
        let IS_DELETE_PERMANANT = true;
        this.jsonData = {
            ...this.jsonData,
            IS_DELETE_PERMANANT
        };
        saleForecastHeaderPopup.callBack(this.jsonData);
        //saleForecastHeaderPopup.close();
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
    //this.OnClientChangeValue = function (e) {
    //    let CLIENT_CODE = e.value;
    //    this.jsonData = {
    //        ...this.jsonData,
    //        CLIENT_CODE
    //    }
    //}

    this.OnCustomerChangeValue = function (e) {
        let CUSTOMER_CODE = e.value;
        this.jsonData = {
            ...this.jsonData,
            CUSTOMER_CODE
        }
    }

    this.OnChannelChangeValue = function (e) {
        let CHANNEL_CODE = e.value;
        this.jsonData = {
            ...this.jsonData,
            CHANNEL_CODE
        }
    }

    //this.OnPositionChangeValue = function (e) {
    //    let ORG_CODE = e.value;
    //    this.jsonData = {
    //        ...this.jsonData,
    //        ORG_CODE
    //    }
    //}

    //this.OnCompanyChangeValue = function (e) {
    //    let COMP_CODE = e.value;
    //    this.jsonData = {
    //        ...this.jsonData,
    //        COMP_CODE
    //    }
    //}

    this.openFormDeletePermanent = function () {
        let json = saleForecastHeaderPopup.jsonData;
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
                            saleForecastHeaderPopup.callBack(json);
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
        let gridObj = document.getElementById("grdSaleForecast").ej2_instances[0];
        if (args.item.id === 'grdSaleForecast_excelexport') {
            gridObj.excelExport();
        }
    }
})();