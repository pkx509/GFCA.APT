let brand = new (function () {

    let _args = null;
    let _popupMode = null;

    this.OnActionComplete = function (args) {
        _args = args;

        let ajax = new ej.base.Ajax({
            type: "POST",
            contentType: "application/json",
        });

        if (args.requestType === 'beginEdit' || args.requestType === 'add') {
            _popupMode = args.requestType;

            if (_popupMode === 'beginEdit') {
                ajax.url = urlServices.BeforeEdit; //render the partial view
            }
            if (_popupMode === 'add') {
                ajax.url = urlServices.BeforeAdd;  //render the partial view
            }
            ajax.data = JSON.stringify({ value: args.rowData });
            ajax
                .send()
                .then(renderPatialFormSuccess)
                .catch(onPopupAjaxCatch)
                ;
        }

        if (args.requestType === 'save') { // popup save
            if (!_popupMode)
                return false;

            if (_popupMode === 'beginEdit') {
                ajax.url = urlServices.Edit;
            }
            if (_popupMode === 'add') {
                ajax.url = urlServices.Add;
            }

            ajax.data = JSON.stringify({ value: args.data });
            ajax
                .send()
                .then(saveSuccess)
                .catch(onPopupAjaxCatch)
                ;

        }
        /*
        if (args.requestType === 'delete') {
            ajax.url = urlServices.PostDelete;
            ajax.data = JSON.stringify({ value: args.data });
            ajax
                .send()
                .then(saveSuccess)
                .catch(onPopupAjaxCatch)
                ;
        }
        */

    }

    let renderPatialFormSuccess = function (data) {
        appendElement(data, _args.form); //render the edit form with selected record
        _args.form.elements.namedItem('BRAND_ID').focus();
        ej.popups.hideSpinner(_args.dialog.element);
    }

    let saveSuccess = function (data) {
      //  if (_popupMode === 'add') {
           // window.location = 'http://localhost:57628/M/Brand';
          //  return;
        //}
        appendElement(data, _args.form); //render the edit form with selected record
        let objItem = _args.form.elements.namedItem('BRAND_CODE');
        if (objItem != undefined) {
            objItem.focus();
        }
        ej.popups.hideSpinner(_args.dialog.element);
    }

    let cancel = function () {
    }

    let onPopupAjaxCatch = function (xhr) {
        console.log(xhr);
        ej.popups.hideSpinner(_args.dialog.element);
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