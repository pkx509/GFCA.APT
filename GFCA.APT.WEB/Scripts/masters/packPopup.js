const POPUP_MODE = Object.freeze({
    CREATE: 1,
    EDIT: 2,
    DELETE: 3
});

let packPopup = new (function () {

    let _args = null;

    //UI
    this.popup_id      = "#popup-pack";
    this.header_title  = "#pop-lbl-header-title";
    this.button_del    = "#pop-btn-del";
    this.button_save   = "#pop-btn-save";

    this.MODE_TYPE = {
        CREATE : 1,
        EDIT   : 2,
        DELETE : 3
    }

    this.isCreateState = true;

    //Model Dto
   
    this.field_pack_code    = "#pop-txt-pack-code";
    this.field_pack_name    = "#pop-txt-pack-name";
    this.field_pack_desc    = "textarea[name='pop-txt-pack-desc']";
    this.field_is_active     = "input[id='pop-ckb-is-active']";
    this.field_permanant_del = "input[id='pop-ckb-is-del-perm]";

    //Value Dto
    this.jsonData = {
       
        pack_CODE         : null,
        pack_NAME         : null,
        pack_DESC         : null,
        FLAG_ROW           : null,
        IS_ACTIVED         : true,
        IS_DELETE_PERMANANT: false,
    };

    this.callBack = function (data) {
        console.log(data);
    }
    this.clearValue = function () {
        this.jsonData = {
          
            PACK_CODE         : null,
            PACK_NAME         : null,
            PACK_DESC         : null,
            FLAG_ROW           : null,
            IS_ACTIVED         : true,
            IS_DELETE_PERMANANT: false,
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

            //UI Initial
            $(this.field_pack_code).prop("disabled", false);
            $(this.field_pack_code).parent().removeClass("e-disabled")
            $(this.field_pack_code).parent().addClass("mandatory");

            this.setHeading("Create New pack");
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
                packPopup.callBack(this.jsonData);
                return;
            }
            this.bindDom(this.jsonData);

            //UI Initial
            $(this.field_pack_code).prop("disabled", true);
            $(this.field_pack_code).parent().addClass("e-disabled")
            $(this.field_pack_code).parent().removeClass("mandatory");

            $(this.header_title).html("Edit pack");
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
                packPopup.callBack(this.jsonData);
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

       
        $(this.field_pack_code).val(data.PACK_CODE);
        $(this.field_pack_name).val(data.PACK_NAME);
        $(this.field_pack_desc).val(data.PACK_DESC);
        if (data.IS_ACTIVED === false) {
            $(this.field_is_active).prop("checked", "");
            $(this.field_is_active).parent().parent().attr("aria-checked", "false");
            $(this.field_is_active).parent().children().removeClass("e-check");

        } else {
            $(this.field_is_active).prop("checked", "checked");
            $(this.field_is_active).parent().parent().attr("aria-checked", "true");
            $(this.field_is_active).parent().children().addClass("e-check");
            $(this.field_is_active).removeClass("e-check");
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
       
       
        let PACK_CODE          = $(this.field_pack_code).val();
        let PACK_NAME          = $(this.field_pack_name).val();
        let PACK_DESC          = $(this.field_pack_desc).val();
        let IS_ACTIVED          = $(this.field_is_active).prop("checked");
        let IS_DELETE_PERMANANT = $(this.field_permanant_del).prop("checked");

        this.jsonData = {
            PACK_CODE, PACK_NAME, PACK_DESC,
            IS_ACTIVED, IS_DELETE_PERMANANT
        };
    }
    this.fieldsDisable = function () {
        //$(this.field_pack_id).prop("disabled", true);
        //$(this.field_client_code).prop("disabled", true);
       
        $(this.field_pack_code).prop("disabled", true);
        $(this.field_pack_name).prop("disabled", true);
        $(this.field_pack_desc).prop("disabled", true);
        $(this.field_is_active).prop("disabled", true);
    }

    this.fieldsEnable = function () {
        //$(this.field_pack_id).prop("disabled", true);
        //$(this.field_client_code).prop("disabled", true);
       
        $(this.field_pack_code).prop("disabled", false);
        $(this.field_pack_name).prop("disabled", false);
        $(this.field_pack_desc).prop("disabled", false);
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
            packPopup.callBack(this.jsonData);
            packPopup.close();
        }

    }

    this.openFormDeletePermanent = function () {
        let json = packPopup.jsonData;
        BootstrapDialog.show({
            type: BootstrapDialog.TYPE_WARNING,
            size: BootstrapDialog.SIZE_SMALL,
            closable: true,
            closeByBackdrop: true,
            closeByKeyboard: true,
            draggable: true,
            title: 'Confirmation',
            message: `<label><input type="checkbox" class="e-control"></input> You're deleting a pack "${json.PACK_CODE}", Are you? </label>`,
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
                            packPopup.callBack(json);
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
    this.OnClientChangeValue = function (e) {
        //let v = e.itemData.Value;
        let t = e.itemData.Text;
        let CLIENT_ID = e.value;
        $("#pop-hid-client-id").val(e.value);
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
        //formInstance.addRules('pack_ID', { required: true });
        formInstance.addRules('pack_CODE', { required: true, minLength: 2 }); //adding the form validation rules
        formInstance.refresh();  // refresh method of the formObj
        let script = document.createElement('script');
        script.type = "text/javascript";
        let serverScript = dialogTemp.querySelector('script');
        script.textContent = serverScript.innerHTML;
        document.head.appendChild(script);
        serverScript.remove();
    }

    this.OnDataBound = function () {
        //var gridObj = document.getElementById('grdPack')['ej2_instances'][0];
        //Object.assign(gridObj.filterModule.filterOperators, { startsWith: 'contains' });
    }
    this.OnToolbarClick = function (args) {

        let gridObj = document.getElementById("grdPack").ej2_instances[0];
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