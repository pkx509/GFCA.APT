Number.prototype.ToMessageType = function () {
    let messageTypeName = "error";
    switch (parseInt(this)) {
        case 0:
            messageTypeName = 'info';
            break;
        case 1:
            messageTypeName = 'success';
            break;
        case 2:
            messageTypeName = "warning";
            break;
        default:
            messageTypeName = "error";
            break;
    }

    return messageTypeName;
}