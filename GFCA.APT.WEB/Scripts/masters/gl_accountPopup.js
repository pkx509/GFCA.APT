const POPUP_MODE = Object.freeze({
    CREATE: 1,
    EDIT: 2,
    DELETE: 3
});

let gl_accountPopup = new (function () {

    let _args = null;

    //UI
    this.popup_id      = "#popup-brand";
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
    this.field_ACC_ID               = "#pop-txt-acc-id";
    this.field_IO_ID                = "#pop-txt-io-id";
    this.field_CENTER_ID            = "#pop-txt-centerId";
    this.field_FUND_ID              = "#pop-txt-fundId";
    this.field_FUND_CENTER_ID       = "#pop-txt-fcenterId";
    this.field_ACC_CODE             = "#pop-txt-accCode";
    this.field_ACC_NAME             = "#pop-txt-accName";
    this.field_ACC_TYPE             = "#pop-txt-accType";
    this.field_ACC_TYPE_DESC        = "#pop-txt-typeDes";
    this.field_ACC_GROUP1           = "#pop-txt-grp1";
    this.field_ACC_GROUP1_DESC      = "#pop-txt-grp1Des";
    this.field_ACC_GROUP2           = "#pop-txt-grp2";
    this.field_ACC_GROUP2_DESC      = "#pop-txt-grp2Des";
    this.field_ACC_REMARK = "textarea[name='pop-txt-accRemark']";
      

    this.field_is_active     = "input[id='pop-ckb-is-active']";
    this.field_permanant_del = "input[id='pop-ckb-is-del-perm]";

    //Value Dto
    this.jsonData = {
        ACC_ID			        : 0,
        IO_ID			        : null,
        CENTER_ID		        : null,
        FUND_ID			        : null,
        FUND_CENTER_ID	        : null,
        ACC_CODE		        : null,
        ACC_NAME: null,
        ACC_TYPE: null,
        ACC_TYPE_DESC: null,
        ACC_GROUP1: null,
        ACC_GROUP1_DESC: null,
        ACC_GROUP2: null,
        ACC_GROUP2_DESC: null,
        ACC_REMARK: null,
        FLAG_ROW           : null,
        IS_ACTIVED         : true,
        IS_DELETE_PERMANANT: false,
    };

    this.callBack = function (data) {
        console.log(data);
    }
    this.clearValue = function () {
        this.jsonData = {
            ACC_ID: 0,
            IO_ID: null,
            CENTER_ID: null,
            FUND_ID: null,
            FUND_CENTER_ID: null,
            ACC_CODE: null,
            ACC_NAME: null,
            ACC_TYPE: null,
            ACC_TYPE_DESC: null,
            ACC_GROUP1: null,
            ACC_GROUP1_DESC: null,
            ACC_GROUP2: null,
            ACC_GROUP2_DESC: null,
            ACC_REMARK: null,
            FLAG_ROW: null,
            IS_ACTIVED: true,
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
            $(this.field_ACC_CODE).prop("disabled", false);
            $(this.field_ACC_CODE).parent().removeClass("e-disabled")
            $(this.field_ACC_CODE).parent().addClass("mandatory");

            this.setHeading("Create New GL-Account");
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
                gl_accountPopup.callBack(this.jsonData);
                return;
            }
            this.bindDom(this.jsonData);

            //UI Initial
            $(this.field_ACC_CODE).prop("disabled", true);
            $(this.field_ACC_CODE).parent().addClass("e-disabled")
            $(this.field_ACC_CODE).parent().removeClass("mandatory");

            $(this.header_title).html("Edit GL-Account");
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
                gl_accountPopup.callBack(this.jsonData);
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



        $(this.field_ACC_ID).val(data.ACC_ID);
        $(this.field_IO_ID).val(data.IO_ID);
        $(this.field_CENTER_ID).val(data.CENTER_ID);
        $(this.field_FUND_ID).val(data.FUND_ID);
        $(this.field_FUND_CENTER_ID).val(data.FUND_CENTER_ID);
        $(this.field_ACC_CODE).val(data.ACC_CODE);
        $(this.field_ACC_NAME).val(data.ACC_NAME);
        $(this.field_ACC_TYPE).val(data.ACC_TYPE);
        $(this.field_ACC_TYPE_DESC).val(data.ACC_TYPE_DESC);
        $(this.field_ACC_GROUP1).val(data.ACC_GROUP1);
        $(this.field_ACC_GROUP1_DESC).val(data.ACC_GROUP1_DESC);
        $(this.field_ACC_GROUP2).val(data.ACC_GROUP2);
        $(this.field_ACC_GROUP2_DESC).val(data.ACC_GROUP2_DESC);
        $(this.field_ACC_REMARK).val(data.ACC_REMARK); 


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
          
        let ACC_ID             = $(this.field_ACC_ID         ).val();
        let IO_ID              = $(this.field_IO_ID          ).val();
        let CENTER_ID          = $(this.field_CENTER_ID      ).val();
        let FUND_ID            = $(this.field_FUND_ID        ).val();
        let FUND_CENTER_ID     = $(this.field_FUND_CENTER_ID ).val();
        let ACC_CODE           = $(this.field_ACC_CODE       ).val();
        let ACC_NAME           = $(this.field_ACC_NAME       ).val();
        let ACC_TYPE           = $(this.field_ACC_TYPE       ).val();
        let ACC_TYPE_DESC      = $(this.field_ACC_TYPE_DESC  ).val();
        let ACC_GROUP1         = $(this.field_ACC_GROUP1     ).val();
        let ACC_GROUP1_DESC    = $(this.field_ACC_GROUP1_DESC).val();
        let ACC_GROUP2         = $(this.field_ACC_GROUP2     ).val();
        let ACC_GROUP2_DESC    = $(this.field_ACC_GROUP2_DESC).val();
        let ACC_REMARK = $(this.field_ACC_REMARK).val();
        let IS_ACTIVED = $(this.field_is_active).prop("checked");
        let IS_DELETE_PERMANANT = $(this.field_permanant_del).prop("checked");

          
        this.jsonData = {
            ACC_ID			
            ,IO_ID			
            ,CENTER_ID		
            ,FUND_ID			
            ,FUND_CENTER_ID	
            ,ACC_CODE		
            ,ACC_NAME		
            ,ACC_TYPE		
            ,ACC_TYPE_DESC	
            ,ACC_GROUP1		
            ,ACC_GROUP1_DESC	
            ,ACC_GROUP2		
            ,ACC_GROUP2_DESC	
            ,ACC_REMARK 
            ,IS_ACTIVED, IS_DELETE_PERMANANT
        };
    }
    this.fieldsDisable = function () { 

        $(this.field_ACC_ID).prop("disabled", true);
        $(this.field_IO_ID).prop("disabled", true);
        $(this.field_CENTER_ID).prop("disabled", true);
        $(this.field_FUND_ID).prop("disabled", true);
        $(this.field_FUND_CENTER_ID).prop("disabled", true);
        $(this.field_ACC_CODE).prop("disabled", true);
        $(this.field_ACC_NAME).prop("disabled", true);
        $(this.field_ACC_TYPE).prop("disabled", true);
        $(this.field_ACC_TYPE_DESC).prop("disabled", true);
        $(this.field_ACC_GROUP1).prop("disabled", true);
        $(this.field_ACC_GROUP1_DESC).prop("disabled", true);
        $(this.field_ACC_GROUP2).prop("disabled", true);
        $(this.field_ACC_GROUP2_DESC).prop("disabled", true);
    }

    this.fieldsEnable = function () {

        $(this.field_ACC_ID).prop("disabled", false);
        $(this.field_IO_ID).prop("disabled", false);
        $(this.field_CENTER_ID).prop("disabled", false);
        $(this.field_FUND_ID).prop("disabled", false);
        $(this.field_FUND_CENTER_ID).prop("disabled", false);
        $(this.field_ACC_CODE).prop("disabled", false);
        $(this.field_ACC_NAME).prop("disabled", false);
        $(this.field_ACC_TYPE).prop("disabled", false);
        $(this.field_ACC_TYPE_DESC).prop("disabled", false);
        $(this.field_ACC_GROUP1).prop("disabled", false);
        $(this.field_ACC_GROUP1_DESC).prop("disabled", false);
        $(this.field_ACC_GROUP2).prop("disabled", false);
        $(this.field_ACC_GROUP2_DESC).prop("disabled", false);
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
            gl_accountPopup.callBack(this.jsonData);
            gl_accountPopup.close();
        }

    }

    this.openFormDeletePermanent = function () {
        let json = gl_accountPopup.jsonData;
        BootstrapDialog.show({
            type: BootstrapDialog.TYPE_WARNING,
            size: BootstrapDialog.SIZE_SMALL,
            closable: true,
            closeByBackdrop: true,
            closeByKeyboard: true,
            draggable: true,
            title: 'Confirmation',
            message: `<label><input type="checkbox" class="e-control"></input> You're deleting a brand "${json.ACC_CODE}", Are you? </label>`,
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
                            gl_accountPopup.callBack(json);
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
    //this.OnClientChangeValue = function (e) {
    //    //let v = e.itemData.Value;
    //    let t = e.itemData.Text;
    //    let CLIENT_ID = e.value;
    //    this.jsonData = {
    //        ...this.jsonData,
    //        //CLIENT_CODE,
    //        CLIENT_ID
    //    }
    //}

    let appendElement = function (el, form) {
        let dialogTemp = form.querySelector("#dialogTemp");
        dialogTemp.innerHTML = el;
        let formInstance = form.ej2_instances[0];
        //formInstance.addRules('BRAND_ID', { required: true });
        formInstance.addRules('ACC_CODE', { required: true, minLength: 2 }); //adding the form validation rules
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

        let gridObj = document.getElementById("grdBrand").ej2_instances[0];
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