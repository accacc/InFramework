﻿$(document).on("click", "a[if-ajax=true]", function (e) {

    e.preventDefault();

    var ajaxOptions = GetAjaxOptions(this);


    ajaxOptions.url = this.href;

    IFAjax.Init(ajaxOptions);
    IFAjax.Send();

});


$(document).on("click", "button[if-ajax-form-submit=true]", function (e)
{
    e.preventDefault();

    var formData = $(this).parents('form:first').serialize();

    var ajaxOptions = GetAjaxOptions(this);
    ajaxOptions.url = $(this).attr("if-ajax-action");
    ajaxOptions.data = formData;

    IFAjax.Init(ajaxOptions);
    IFAjax.Send();
});

function GetAjaxOptions(element) {

    var ajaxOptions = {

        updateid: $(element).attr("if-ajax-update-id"),
        modalid: $(element).attr("if-ajax-modal-id"),
        refreshGrid: $(element).attr("if-ajax-refresh-grid"),
        showDialog: $(element).attr("if-ajax-show-dialog"),
        insertionMode: $(element).attr("if-ajax-insertion-mode"),
        method: $(element).attr("if-ajax-method"),
        enctype: $(element).attr("if-ajax-enctype"),
        CloseModalAfterSuccess: $(element).attr("if-ajax-close-modal-on-success"),
        gridViewId: $(element).attr("if-ajax-gridview-id"),
        onErrorFunc: $(element).attr("if-ajax-onerror-func"),
        onSuccessFunc: $(element).attr("if-ajax-onsuccess-func"),
        onBeforeFunc: $(element).attr("if-ajax-onbefore-func"),
        onCompleteFunc: $(element).attr("if-ajax-oncomplete-func"),
        onSuccessRefresh: $(element).attr("if-ajax-on-success-refresh"),
        onSuccessRefreshAction: $(element).attr("if-ajax-on-success-refresh-action"),
        onSuccessRefreshUpdateId: $(element).attr("if-ajax-on-success-refresh-updateid"),
        antiForgeryToken: $(element).attr("if-anti-forgery-token"),
        data: {}


    };

    if ($(element).attr("if-ajax-extradatafunc")) {
        var extraData = eval($(element).attr("if-ajax-extradatafunc"));
        $.extend(ajaxOptions.data, extraData);
    }




    if (ajaxOptions.antiForgeryToken !== undefined) {
        //alert(ajaxOptions.antiForgeryToken);
        ajaxOptions.data["__RequestVerificationToken"] = ajaxOptions.antiForgeryToken;

    }



    return ajaxOptions;
}

var IFAjax = {
    ajaxOptions: {},
    IsBlockUIEnabled: true,
    Init: function (opts) {
        IFAjax.ajaxOptions = opts;
    },
    Send: function () {
        $.ajax({
            url: IFAjax.ajaxOptions.url,
            data: IFAjax.ajaxOptions.data,
            type: IFAjax.ajaxOptions.method || 'GET',
            cache: false,
            beforeSend: function (xhr) {
                if (IFAjax.ajaxOptions.onBeforeFunc !== null) {
                    getFunction(IFAjax.ajaxOptions.onBeforeFunc, []).apply(this, []);
                }
            },
            success: function (data) {
                if (IFAjax.ajaxOptions.showDialog) {
                    IFAjax.ShowDialog(data);
                }
                else if (data !== "") {
                    IFAjax.UpdateTarget(data, IFAjax.ajaxOptions.updateid);
                }

                if (IFAjax.ajaxOptions.CloseModalAfterSuccess === "true") {
                    IFAjax.closeDialog();
                }

                if (IFAjax.ajaxOptions.onSuccessRefresh === "true") {

                    $.ajax({
                        url: IFAjax.ajaxOptions.onSuccessRefreshAction,
                        data: {},
                        success: function (data) {
                            IFAjax.UpdateTarget(data, IFAjax.ajaxOptions.onSuccessRefreshUpdateId);
                        }
                    });
                }

                if (IFAjax.ajaxOptions.onSuccessFunc !== null) {
                    getFunction(IFAjax.ajaxOptions.onSuccessFunc, []).apply(this, []);
                }
            },
            complete: function () {
                if (IFAjax.ajaxOptions.onCompleteFunc !== null) {
                    getFunction(IFAjax.ajaxOptions.onCompleteFunc, []).apply(this, []);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                //alert(xhr.status);
                //alert(thrownError);
                if (IFAjax.ajaxOptions.onErrorFunc !== null) {
                    getFunction(IFAjax.ajaxOptions.onErrorFunc, []).apply(this, []);
                }
            }

        });
    },
    UpdateTarget: function (data,updateid)
    {

        var id = "#" + updateid;

        var mode = (IFAjax.ajaxOptions.insertionMode || "").toUpperCase();

        $(id).each(function (i, update) {

            switch (mode) {
                case "BEFORE":
                    $(update).prepend(data);
                    break;
                case "AFTER":
                    $(update).append(data);
                    break;
                case "REPLACE-WITH":
                    $(update).replaceWith(data);
                    break;
                default:
                    $(update).html(data);
                    break;
            }
        });

    },
    ShowDialog: function (data)
    {
        var modalIdSelector = "#" + this.ajaxOptions.modalid;

        if ($(modalIdSelector).length === 0) {
            $("<div></div>")
                .attr('role', 'dialog')
                .attr('class', 'modal fade modal-fullscreen')
                .attr('id', this.ajaxOptions.modalid)
                .appendTo('body');
        }

        data = data.replace('if-ajax-close-modal-on-success=\"true\"', 'if-ajax-close-modal-on-success="true" ' + 'if-ajax-modal-id="' + this.ajaxOptions.modalid + '"');

        $(modalIdSelector).html("");

        $(modalIdSelector).html(data);

        $(modalIdSelector).on('hidden.bs.modal', function () {

            $(this).remove();

            if (IFAjax.ajaxOptions.refreshGrid) {
                IFAjax.refreshGrid();
            }

        });

        $(modalIdSelector).on('hidden.bs.modal', function (event) {
            if ($('.modal:visible').length) {
                $('body').addClass('modal-open');
            }
        });

        $(modalIdSelector).modal('show');
    },
    closeDialog: function () {
        var idSelector = "#" + this.ajaxOptions.modalid;
        $(idSelector).modal('hide');
    },
    blockUI: function () {
        if (this.IsBlockUIEnabled) {
            $.blockUI({
                baseZ: 100003,
                message: '<img src="' + blockUILoadingGifPath + '" />'
            });
        }
    },
    unblockUI: function () {
        if (this.IsBlockUIEnabled) {
            $.unblockUI();
        }
    },
    blockUIDisable: function () {
        this.IsBlockUIEnabled = false;
    },
    blockUIEnable: function () {
        this.IsBlockUIEnabled = true;
    }
}

$(document).ajaxStart(function () {
    $.blockUI({
        baseZ: 100003,
        message: '<img src="' + blockUILoadingGifPath + '" />'
    })
}).ajaxStop(function () {
    $.unblockUI();
});

$.ajaxSetup({ cache: false, timeout: 2000000 });


