const POPUP_MODE = Object.freeze({
    CREATE: 1,
    EDIT: 2,
    DELETE: 3
});
// 2021-09-21  Jirasak.m
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

    //this.field_prod_id = "#pop-txt-prod_id";
    this.field_prod_code = "#pop-txt-prod_code";
    this.field_prod_name = "#pop-txt-prod_name";


    this.field_cust_code = "#pop-cmb-cust_code_hidden";
    this.field_emis_code = "#pop-cmb-emis_code_hidden";


    this.field_cust_code_text = "#pop-cmb-cust_code";
    this.field_emis_code_text = "#pop-cmb-emis_code";



    this.field_mat_code = "#pop-txt-mat_code";
    this.field_org_code = "#pop-txt-org_code";
    this.field_div_code = "#pop-txt-div_code";




    this.field_mat_group = "#pop-txt-mat_group";
    this.field_mat_group_desc = "#pop-txt-mat_group_desc";
    this.field_mat_group1 = "#pop-txt-mat_group1";
    this.field_mat_group1_desc = "#pop-txt-mat_group1_desc";
    this.field_mat_group2 = "#pop-txt-mat_group2";
    this.field_mat_group2_desc = "#pop-txt-mat_group2_desc";
    this.field_mat_group3 = "#pop-txt-mat_group3";
    this.field_mat_group3_desc = "#pop-txt-mat_group3_desc";
    this.field_formula = "#pop-txt-formula";
    this.field_pack = "#pop-txt-pack";
    this.field_pack_desc = "#pop-txt-pack_desc";
    this.field_size = "#pop-txt-size";
    this.field_uom_size = "#pop-txt-uom_size";
    this.field_uom_sale = "#pop-txt-uom_sale";
    this.field_unit_code = "#pop-txt-unit_code";

    this.field_conv_fcl = "#pop-txt-conv_fcl";
    this.field_conv_l = "#pop-txt-conv_l";


    this.field_flag_row = "#pop-txt-flag_row";

    this.field_created_by = "#pop-txt-created_by";
    this.field_created_date = "#pop-txt-created_date";
    this.field_updated_by = "#pop-txt-updated_by";
    this.field_updated_date = "#pop-txt-updated_date";




    //Value Dto
    this.jsonData = {
      //  PROD_ID: 0,
        PROD_CODE: null,
        PROD_NAME: null,
        CUST_CODE: null,
        MAT_CODE: null,
        ORG_CODE: null,
        DIV_CODE: null,
        EMIS_CODE: null,
        MAT_GROUP: null,
        MAT_GROUP_DESC: null,
        MAT_GROUP1: null,
        MAT_GROUP1_DESC: null,
        MAT_GROUP2: null,
        MAT_GROUP2_DESC: null,
        MAT_GROUP3: null,
        MAT_GROUP3_DESC: null,
        MAT_GROUP4: null,
        MAT_GROUP4_DESC: null,
        CONV_FCL: null,
        CONV_L:null,
        FORMULA: null,
        PACK: null,
        PACK_DESC: null,
        SIZE: null,
        UOM_SIZE: null,
        UOM_SALE: null,
        UNIT_CODE: null,
        FLAG_ROW: null,
        CUST_NAME: null,
        EMIS_NAME: null
    };

    this.callBack = function (data) {
        console.log(data);
    }
    this.clearValue = function () {
        this.jsonData = {
          //  PROD_ID: 0,
            PROD_CODE: null,
            PROD_NAME: null,
            CUST_CODE: null,
            MAT_CODE: null,
            ORG_CODE: null,
            DIV_CODE: null,
            EMIS_CODE: null,
            MAT_GROUP: null,
            MAT_GROUP_DESC: null,
            MAT_GROUP1: null,
            MAT_GROUP1_DESC: null,
            MAT_GROUP2: null,
            MAT_GROUP2_DESC: null,
            MAT_GROUP3: null,
            MAT_GROUP3_DESC: null,
            MAT_GROUP4: null,
            MAT_GROUP4_DESC: null,
            CONV_FCL: null,
            CONV_L: null,
            FORMULA: null,
            PACK: null,
            PACK_DESC: null,
            SIZE: null,
            UOM_SIZE: null,
            UOM_SALE: null,
            UNIT_CODE: null,
            FLAG_ROW: null,
            CUST_NAME: null,
            EMIS_NAME: null
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

            this.setHeading("Create New Prdouct");
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

            $(this.header_title).html("EDIT PRODUCT");
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
            $(this.header_title).html("Delete Product");
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

       // $(this.field_prod_id).val(data.PROD_ID);
        $(this.field_cust_code).val(data.CUST_CODE);
        $(this.field_emis_code).val(data.EMIS_CODE);



        $(this.field_emis_code_text).val(data.CUST_NAME);

        $(this.field_emis_code_text).val(data.EMIS_NAME);

      // alert(data.CUST_NAME);

        //field_emis_code

        $(this.field_client_code).val(data.cust);
        $(this.field_client_id).val(data.CLIENT_ID);
        $(this.field_prod_code).val(data.PROD_CODE);
        $(this.field_prod_name).val(data.PROD_NAME);

        $(this.field_mat_code).val(data.MAT_CODE);
        $(this.field_org_code).val(data.ORG_CODE);
        $(this.field_div_code).val(data.DIV_CODE);

        $(this.field_mat_group).val(data.MAT_GROUP);
        $(this.field_mat_group_desc).val(data.MAT_GROUP_DESC);


        $(this.field_mat_group1).val(data.MAT_GROUP1);
        $(this.field_mat_group1_desc).val(data.MAT_GROUP1_DESC);



        $(this.field_mat_group2).val(data.MAT_GROUP2);
        $(this.field_mat_group2_desc).val(data.MAT_GROUP2_DESC);


        $(this.field_mat_group3).val(data.MAT_GROUP3);
        $(this.field_mat_group3_desc).val(data.MAT_GROUP3_DESC);

        $(this.field_mat_group4).val(data.MAT_GROUP4);
        $(this.field_mat_group4_desc).val(data.MAT_GROUP4_DESC);


        $(this.field_pack).val(data.PACK);
        $(this.field_pack_desc).val(data.PACK_DESC);



        $(this.field_size).val(data.SIZE);
        $(this.field_uom_size).val(data.UOM_SIZE);



        $(this.field_conv_fcl).val(data.CONV_FCL);
        $(this.field_conv_l).val(data.CONV_L);


         



        $(this.field_unit_code).val(data.UNIT_CODE);
        $(this.field_created_by).val(data.CREATED_BY);
        $(this.field_created_date).val(data.CREATED_DATE);
        $(this.field_updated_by).val(data.UPDATED_DATE);
        $(this.field_updated_date).val(data.UPDATED_DATE);

 

        $(this.field_flag_row).val(data.FLAG_ROW);
        $(this.field_uom_sale).val(data.UOM_SALE);

  

    }
    this.bindField = function () {

       // alert($(this.field_emis_code).val());

       // let PROD_ID = $(this.field_prod_id).val();
        let PROD_CODE = $(this.field_prod_code).val();
        let PROD_NAME = $(this.field_prod_name).val();
        let CUST_CODE = $(this.field_cust_code).val();
        let MAT_CODE = $(this.field_mat_code).val();
        let ORG_CODE = $(this.field_org_code).val();
        let DIV_CODE = $(this.field_div_code).val();
        let EMIS_CODE = $(this.field_emis_code).val();
        let MAT_GROUP = $(this.field_mat_group).val();
        let MAT_GROUP_DESC = $(this.field_mat_group_desc).val();
        let MAT_GROUP1 = $(this.field_mat_group1).val();
        let MAT_GROUP1_DESC = $(this.field_mat_group1_desc).val();
        let MAT_GROUP2 = $(this.field_mat_group2).val();
        let MAT_GROUP2_DESC = $(this.field_mat_group2_desc).val();
        let MAT_GROUP3 = $(this.field_mat_group3).val();
        let MAT_GROUP3_DESC = $(this.field_mat_group3_desc).val();
        let FORMULA = $(this.field_formula).val();
        let PACK = $(this.field_pack).val();
        let PACK_DESC = $(this.field_pack_desc).val();
        let SIZE = $(this.field_size).val();
        let CONV_FCL = $(this.field_conv_fcl).val();
        let CONV_L = $(this.field_conv_l).val();
        let UOM_SIZE = $(this.field_uom_size).val();
        let UOM_SALE = $(this.field_uom_sale).val();
        let UNIT_CODE = $(this.field_unit_code).val();
        let FLAG_ROW = $(this.field_flag_row).val();
        let CREATED_BY = $(this.field_created_by).val();
        let CREATED_DATE = $(this.field_created_date).val();
        let UPDATED_BY = $(this.field_updated_by).val();
        let UPDATED_DATE = $(this.field_updated_date).val();



         




        this.jsonData = {
          //  PROD_ID,
            PROD_CODE,
            PROD_NAME,
            CUST_CODE,
            MAT_CODE,
            ORG_CODE,
            DIV_CODE,
            EMIS_CODE,
            MAT_GROUP,
            MAT_GROUP_DESC,
            MAT_GROUP1,
            MAT_GROUP1_DESC,
            MAT_GROUP2,
            MAT_GROUP2_DESC,
            MAT_GROUP3,
            MAT_GROUP3_DESC,
            FORMULA,
            PACK,
            PACK_DESC,
            SIZE,
            UOM_SIZE,
            UOM_SALE,
            UNIT_CODE,
            CREATED_BY,
            CREATED_DATE,
            UPDATED_BY,
            UPDATED_DATE,
            FLAG_ROW,
            CONV_FCL,
            CONV_L


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
        formInstance.addRules('PROD_CODE', { required: true, minLength: 8 }); //adding the form validation rules
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