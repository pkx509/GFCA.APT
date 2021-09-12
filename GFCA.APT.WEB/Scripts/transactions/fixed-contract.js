let page = new (function () {

    this.cmbPositionChange = function (e) {
        let t = e.itemData.Text;
        let v = e.value;
        console.log(`value = (${v}) and text = (${t})`);
    }

})();