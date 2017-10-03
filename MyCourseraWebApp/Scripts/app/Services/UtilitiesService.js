function Utilities() {
};

Utilities.prototype.doAjaxCall = function (async, url, httpVerb, headers, contentType, dataType, requestData, successCallback, failCallback) {
    if (typeof (headers) == 'undefined') {
        headers = null;
    }

    if (typeof (contentType) == 'undefined' || contentType == null) {
        contentType = 'application/json; charset=utf-8';
    }

    if (typeof (dataType) == 'undefined' || dataType == null) {
        dataType = 'json';
    }

    $.ajax({
        type: httpVerb,
        url: url,
        headers: headers,
        contentType: contentType,
        data: requestData,
        async: async,
        dataType: dataType,
        success: function (response) {
            if (typeof (successCallback) != 'undefined' && null != successCallback) {
                successCallback(JSON.parse(JSON.stringify(response || null)));
            }
        },
        error: function (xhr, err) {
            if (typeof (failCallback) != 'undefined' && null != failCallback) {
                failCallback(xhr, err);
            }
            else {
                //alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                //alert("responseText: " + xhr.responseText);
            }
        }
    });
};