function Client_Change(funcName, obj) {
    switch (funcName) {
        case "btnAllP": //全選
            var hidId = $(obj).attr('hidIsAll');

            if (hidId != undefined) {
                $("#" + hidId).val('true');                                         //紀錄是否全選資料
                $("#" + hidId.replace("hidIsAll", "hidChooseList")).val('');       //清除紀錄勾選資料
            
                $('span[OnlyKeyAll=' + $(obj).attr('Id') + ']').each(function() {
                    $(this).children('input').attr('checked', true);
                });
            }
            break;
        case "btnNotAllP": //全不選
            var hidId = $(obj).attr('hidIsAll');

            if (hidId != undefined) {
                $("#" + hidId).val('false');                                        //紀錄是否全選資料
                $("#" + hidId.replace("hidIsAll", "hidChooseList")).val('');        //清除紀錄勾選資料

                $('span[OnlyKeyNotAll =' + $(obj).attr('Id') + ']').each(function() {
                    $(this).children('input').attr('checked', false);
                });
            }

            break;
        case "ckChoose": //勾選
            var hidId = $(obj).parent().attr('hidChooseList');      //抓取上一次資料

            if (hidId != undefined) {
                var hidValue = $("#" + hidId).val();

                //紀錄勾選資料
                if ($("#" + hidId.replace("hidChooseList", "hidIsAll")).val() == "true") {
                    $('span[Key]').each(function() {
                        if ($(this).children('input').attr('checked')) {
                            hidValue = hidValue + $(this).attr('Key') + "|";
                        }
                    });

                    $("#" + hidId).val(hidValue);
                }

                var Key = $(obj).parent().attr('key');
                var arrChooseList = hidValue.split('|');

                if ($(obj).attr('checked')) {
                    $("#" + hidId).val(hidValue + Key + "|");  //紀錄勾選資料
                }
                else {
                    $.each(arrChooseList, function(index, val) {
                        if (Key == val) {
                            hidValue = hidValue.replace(val + "|", "")
                        }
                    });

                    $("#" + hidId).val(hidValue);   //紀錄勾選資料
                }
                
                $("#" + hidId.replace("hidChooseList", "hidIsAll")).val('');       //清除是否全選資料
            }
            break;
    }
}