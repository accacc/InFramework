$(document).ready(function () {

    toastr.options = {
        "closeButton": true,
        "debug": false,
        "progressBar": false,
        "positionClass": "toast-top-right",
        "onclick": null,
        "showDuration": "400",
        "hideDuration": "1000",
        "timeOut": "7000",
        //"extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };

    handleAjaxMessages();

    if (isdefined('messageContext')) {
        displayMessage(messageContext.Context, messageContext.Type);
    }

});

function handleAjaxMessages() {
    $(document).ajaxSuccess(function (event, request) {
        checkAndHandleMessageFromHeader(request);
    }).ajaxError(function (event, request) {        
        if (request.responseText !== undefined && request.responseText !== '') {
            var messageContext = $.parseJSON(request.responseText);
            displayMessage(messageContext, "error");
        }
        
    });
}

function checkAndHandleMessageFromHeader(request) {
    var messagesRaw = request.getResponseHeader('X-Message');
    if (messagesRaw) {
        var messageContext = $.parseJSON(messagesRaw);
        var messageType = request.getResponseHeader('X-Message-Type');
        displayMessage(messageContext, messageType);
    }
}

function displayMessage(context, messageType) {

    $.each(context.Messages, function (i, message) {


        //toastr.success(message);

        messageType = messageType.toLowerCase();

        var title = "Mesaj";

        

        if (messageType === "success") {
            //alert("ok");
            title = "Başarılı";
            toastr.success(message);

        } else if (messageType === "warning") {
            title = "Uyarı";
            toastr.warning(message);
        } else if (messageType === "error") {
            title = "Hata";
            toastr.error(message);
        } else {
            title = "Hata";
            toastr.error(message);
        }

    });
}



