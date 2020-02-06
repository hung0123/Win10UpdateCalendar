var LockTable = new jQuery.Hashtable();
var agt = navigator.userAgent.toLowerCase();
var ie = ((agt.indexOf("msie") != -1) && (agt.indexOf("opera") == -1) && (agt.indexOf("omniweb") == -1));

function lockPanel(id, Lock) {
    if (Lock) {
        LockTable.clear();
        $("#" + id).find('input').each(function() {
            if ($(this).attr('disabled') == undefined) {
                LockTable.add($(this).attr('id'), $(this).attr('id'));
                $(this).attr('disabled', "disabled");
            }
        });
        $("#" + id).find('select').each(function() {
            if ($(this).attr('disabled') == undefined) {
                LockTable.add($(this).attr('id'), $(this).attr('id'));
                $(this).attr('disabled', "disabled");
            }
        });
    }
    else {
        $("#" + id).find('input').each(function() {
            if (LockTable.containsKey($(this).attr('id'))) {
                $(this).removeAttr('disabled');
            }
        });
        $("#" + id).find('select').each(function() {
            if (LockTable.containsKey($(this).attr('id'))) {
                $(this).removeAttr('disabled');
            }
        });
        LockTable.clear();
    }
}
function doGetCaretPosition(oField, oIsEnd) {

    // Initialize
    var iCaretPos = 0;

    // IE Support
    if (document.selection) {

        // Set focus on the element
        oField.focus();

        // To get cursor position, get empty selection range
        var oSel = document.selection.createRange();

        // Move selection start to 0 position
        oSel.moveStart('character', -oField.value.length);

        // The caret position is selection length
        iCaretPos = !oIsEnd ? (oSel.text.length - document.selection.createRange().text.length) : oSel.text.length;
    }

    // Firefox support
    else if (oField.selectionStart || oField.selectionStart == '0') {
        iCaretPos = !oIsEnd ? oField.selectionStart : oField.selectionEnd;
    }
    // Return results
    return (iCaretPos);
}
function IsNumeric(input) {
    var RE = /^\d+$/;
    return (RE.test(input));
}

function Blength(val) {

    var arr = val.match(/[^\x00-\xff]/ig);
    return arr == null ? val.length : val.length + arr.length;
}
function regInputDel(event, obj, reg) {

    var DeleteKey = new Array(8, 46);
    var key = (window.event) ? event.keyCode : event.which;
    if ($.inArray(key, DeleteKey) != -1) {
        //reg.test
        var src = obj.value;
        var PstStart = doGetCaretPosition(obj, false);
        var PstEnd = doGetCaretPosition(obj, true);
        if (PstStart == PstEnd && PstStart != 0 && key == 8) PstStart--;
        if (PstStart == PstEnd && PstEnd != src.length && key == 46) PstEnd++;
        var str = src.substring(0, PstStart) + src.substring(PstEnd);
        return reg.test(str);
    }

    if (key == 229) {
        var src = obj.value;
        var PstStart = doGetCaretPosition(obj, false);
        var PstEnd = doGetCaretPosition(obj, true);
        var str = src.substring(0, PstStart) + src.substring(PstEnd);

        //length test
        var ReMaxlength = /^\d+$/;
        if (obj.attributes['maxlength'] != null && ReMaxlength.test(obj.attributes['maxlength'].value)) {
            if ((Blength(str) + 2) > parseInt(obj.attributes['maxlength'].value)) return false;

        }
    }

    return true;
}

function regInputIns(event, obj, eventType, reg) {
    //inputStr
    var inputStr = '';
    var DeleteKey = new Array(8, 46);
    var FunctionsKey = new Array(0, 8, 9, 13, 16, 17);

    //onkeypress
    if (eventType == 'onkeypress') {
        var key = (window.event) ? event.keyCode : event.which;
        if ($.inArray(key, FunctionsKey) != -1) {
            return true;
        }
        inputStr = String.fromCharCode(key);
    }
    //onpaste
    else if (eventType == 'onpaste') {
        try {
            if (window.clipboardData) inputStr = window.clipboardData.getData('Text');
            else if (event.clipboardData) inputStr = event.clipboardData.getData('text/plain');
            else
                return false;
        }
        catch (err) {
            return false;
        }
    }

    //ondrop
    else if (eventType == 'ondrop') {
        try {
            if (event.dataTransfer)
                inputStr = event.dataTransfer.getData('Text');
            else
                return false;
        }
        catch (err) {
            return false;
        }
    }
    //else
    else {
        return true;
    }

    var src = obj.value;
    var PstStart = doGetCaretPosition(obj, false);
    var PstEnd = doGetCaretPosition(obj, true);
    var str = src.substring(0, PstStart) + inputStr + src.substring(PstEnd);

    //reg.test
    if (!reg.test(str)) return false;

    //length test
    var ReMaxlength = /^\d+$/;
    if (obj.attributes['maxlength'] != null && ReMaxlength.test(obj.attributes['maxlength'].value)) {
        if (Blength(str) > parseInt(obj.attributes['maxlength'].value)) return false;
    }

    return true;
}
function MessageError(Message, OkFunction) {
    Message = Message.replace(new RegExp("/r", "gm"), "");
    Message = Message.replace(new RegExp("/n", "gm"), "<br/>");
    if (!OkFunction) {
        $.prompt(Message, { persistent: true, prefix: 'ErrorJqi', buttons: { Yes: true} });
    }
    else {
        $.prompt(Message, { persistent: true, prefix: 'ErrorJqi', buttons: { Yes: true },
            submit: function(e, v, m, f) { if (v) { OkFunction(); return true; } }
        });
    }
}
function MessageInformation(Message, OkFunction) {
    Message = Message.replace(new RegExp("/r", "gm"), "");
    Message = Message.replace(new RegExp("/n", "gm"), "<br/>");
    if (!OkFunction) {
        $.prompt(Message, { persistent: true, prefix: 'NoneJqi', buttons: { Yes: true} });
    }
    else {
        $.prompt(Message, { persistent: true, prefix: 'NoneJqi', buttons: { Yes: true },
            submit: function(e, v, m, f) { if (v) { OkFunction(); return true; } }
        });
    }
}
function MessageNone(Message, OkFunction) {
    Message = Message.replace(new RegExp("/r", "gm"), "");
    Message = Message.replace(new RegExp("/n", "gm"), "<br/>");
    if (!OkFunction) {
        $.prompt(Message, { persistent: true, prefix: 'NoneJqi', buttons: { Yes: true} });
    }
    else {
        $.prompt(Message, { persistent: true, prefix: 'NoneJqi', buttons: { Yes: true },
            submit: function(e, v, m, f) { if (v) { OkFunction(); return true; } }
        });
    }
}
function MessageQuestion(Message, OkFunction, CancelFunction) {
    Message = Message.replace(new RegExp("/r", "gm"), "");
    Message = Message.replace(new RegExp("/n", "gm"), "<br/>");
    $.prompt(Message, { persistent: true, prefix: 'QuestionJqi', buttons: { Yes: true, No: false },
        submit: function(e, v, m, f) { if (v) { OkFunction(); return true; } else { CancelFunction(); return true; } }
    });
}