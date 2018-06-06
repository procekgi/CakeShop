$(function () {
    initializeJSComponent();
    backspaceUnbind();
    checkAll();
});

function initializeJSComponent() {
    $("form.ajax").submit(function (e) {
        e.preventDefault();
        e.stopPropagation();
        var $this = $(this);
        if ($this.validationEngine("validate")) {
            var action = $this.attr("action");
            var method = $this.attr("method");
            action = action ? action : document.location.host + document.location.pathname + document.location.search;
            method = method ? method : "GET";
            var successFunction = $this.data("success");
            successFunction = successFunction ? successFunction : "defaultSuccess";
            $.ajax({
                url: action,
                data: $this.serialize(),
                method: method,
                cache: false,
                dataType: "json",
                beforeSend: function (result) {
                    defaultBegin(result);
                },
                complete: function (result) {
                    defaultComplete(result);
                },
                success: function (result) {
                    eval(successFunction + "(result)");
                },
                error: function (result) {
                    defaultFailure(result);
                }
            });
        }
        return false;
    });
}

var loadCont = 0;
function showLoading() {
    loadCont++;
    if (loadCont == 1) {
        if ($(".backModalLoading").size() == 0) {
            $("body").prepend($("<div>").addClass("backModalLoading").append($("<div>").addClass("loading")));
        }
        $(".backModalLoading").fadeIn("fast");
    }
}

function hideLoading() {
    loadCont--;
    if (loadCont == 0) {
        $(".backModalLoading").fadeOut("fast");
    }
    loadCont = loadCont < 0 ? 0 : loadCont;
}

function defaultBegin(data) {
    showLoading();
}

function defaultFailure(data) {
    showDialog(data.Message, "Erro");
}

function defaultSuccess(data) {
    if (data.Status && data.Status == "SUCCESS") {
        if (data.Message != null && data.Message != "") {
            jAlert(data.Message, "Sucesso", function () {
                if (data.RedirectUrl != "") {
                    window.location.href = data.RedirectUrl;
                }
            });
        } else {
            if (data.RedirectUrl != "") {
                window.location.href = data.RedirectUrl;
            }
        }
    } else {
        showDialog(data.Message, 'Erro');
    }
}

function defaultComplete(data) {
    hideLoading();
}

function showDialog(message, title) {
    if (title == false)
        title = "Alerta";

    if (message == false)
        message = '';

    $("<div class='modal fade' id='myModal' role='dialog' style='z-index:1055;' aria-labelledby='myModalLabel' aria-hidden='false'>")
        .html("<div class='modal-dialog'><div class='modal-content'><div class='modal-header'><button type='button' class='close' data-dismiss='modal' aria-hidden='true'>&times;</button><h4 class='modal-title'>" + title + "</h4></div><div class='modal-body'>" + message + "</div><div class='modal-footer'><button type='button' id='okModal' class='btn btn-info' data-dismiss='modal'>Ok</button></div></div></div>")
        .modal();
}

function jAlert(msg, title, callback) {
    bootbox.dialog({
        message: msg,
        title: title,
        buttons: {
            ok: {
                label: "Ok",
                className: "btn-primary",
                callback: function () {
                    callback();
                }
            }
        }
    });
}

function jConfirm(msg, title, callback) {
    bootbox.dialog({
        message: msg,
        title: title,
        buttons: {
            success: {
                label: "Sim",
                className: "btn-primary",
                callback: function () {
                    callback();
                }
            },
            cancel: {
                label: "N&atilde;o",
                className: "btn-default",
                callback: function () {

                }
            }
        }
    });
}

function checkAll() {
    $("input[type=checkbox][name='check-all']").change(function () {
        var checked = $(this).is(":checked");
        $("input[type=checkbox][name='chkId']").each(function () {
            $(this).prop("checked", checked);
        });
    }).parent().addClass("text-center");

    $("input[type=checkbox][name='checkAll']").change(function () {
        var checked = $(this).is(":checked");
        $("input[type=checkbox][name='chkAll']").each(function () {
            $(this).prop("checked", checked);
        });
    }).parent().addClass("text-center");
}

function backspaceUnbind() {
    //prevent the backspace key from navigating back
    $(document).unbind('keydown').bind('keydown', function (event) {
        var doPrevent = false;
        if (event.keyCode === 8) {
            var d = event.srcElement || event.target;
            if ((d.tagName.toUpperCase() === 'INPUT' &&
                 (
                     d.type.toUpperCase() === 'TEXT' ||
                     d.type.toUpperCase() === 'PASSWORD' ||
                     d.type.toUpperCase() === 'FILE' ||
                     d.type.toUpperCase() === 'EMAIL' ||
                     d.type.toUpperCase() === 'SEARCH' ||
                     d.type.toUpperCase() === 'DATE')
                 ) ||
                 d.tagName.toUpperCase() === 'TEXTAREA') {
                doPrevent = d.readOnly || d.disabled;
            }
            else {
                doPrevent = true;
            }
        }
        if (doPrevent) {
            event.preventDefault();
        }
    });
}