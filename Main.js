$(document).ready(function() {
    GetData();
});

function GetData() {
    $.ajax({
        type: "POST",
        async: false,
        dataType: "json",
        url: "../Main.asmx/GetData",
        contentType: 'application/json; charset=UTF-8',
        success: function(results) {
            var data = JSON.parse(results.d);
            //後端回傳的東西包成Object回來,
            RenderCal(data);
        },
        error: function(msg) {
            console.log(msg);
        }
    });
}


function RenderCal(data) {
    var source;



    source = Object.entries(data); //convert data to array


    source.map(function(items) {

        var ym = items[0].substr(0, 4) + "/" + items[0].substr(4, 2);
        $("#divMain").append("<table width='100%' rules='all' border='0' cellpadding='0' cellspacing='1' id='" + items[0] + "' class='orderTable'></table>")
        $("#" + items[0] + "").append("<tr><td colspan=7 style='font-size:30px'>" + ym + "</td></tr><tr><td>日</td><td>一</td><td>二</td><td>三</td><td>四</td><td>五</td><td>六</td></tr>");
        var tr = null;
        items[1].map(function(item) {


            var d = new Date(item.Day);
            var day = d.getDay();
            console.log(d);
            //換周
            if (day == 0 || tr == null) {

                tr = document.createElement("tr");

                //補前面空白
                for (i = 0; i < day; i++) {
                    var td = document.createElement("td");
                    var textNode = document.createTextNode(" ");         // Create a text node
                    td.appendChild(textNode);
                    tr.appendChild(td);
                }
            }
            var td = document.createElement("td");
            //var textNode = document.createTextNode(item.Day);         // Create a text node

            var textNode = document.createElement("p");
            textNode.innerText = item.Day;


            var anc1st = document.createElement("button");
            var anc2nd = document.createElement("button");
            var br = document.createElement("br");

            anc1st.innerText = "早上 已登記人數:" + item.People1 + "/總人數:" + item.Total1;
            anc2nd.innerText = "下午 已登記人數:" + item.People2 + "/總人數:" + item.Total2;

            //總人數=-1隱藏:不開放
            if (item.Total1 == "-1") {
                anc1st.style.cssText = "display:none";
            }
            if (item.Total2 == "-1") {
                anc2nd.style.cssText = "display:none";
            }
            //該時段額滿 隱藏:不開放
            if (item.Total1 == item.People1) {
                anc1st.style.cssText = "display:none";
            }
            if (item.Total2 == item.People2) {
                anc2nd.style.cssText = "display:none";
            }


            anc1st.onclick = function() {
                Signup(item.Day + "1");
            }
            anc2nd.onclick = function() {
                Signup(item.Day + "2");
            }

            td.appendChild(textNode);
            td.appendChild(anc1st);
            td.appendChild(br);
            td.appendChild(anc2nd);
            if (item.Total1 == "-1" && item.Total2 == "-1") {
                var non = document.createTextNode("不開放");
                td.appendChild(non);

            }


            td.className = 'tdDay';
            tr.appendChild(td);


            if (tr != null) {
                document.getElementById(items[0]).appendChild(tr);
            }

        })
    })

}

function Signup(target) {
    console.log("click :", target);
    var modal = document.getElementById("myModal");

    // Get the button that opens the modal
    var btn = document.getElementById("myBtn");

    // Get the <span> element that closes the modal
    var span = document.getElementsByClassName("close")[0];

    // When the user clicks the button, open the modal 

    modal.style.display = "block";


    // When the user clicks on <span> (x), close the modal
    span.onclick = function() {
        modal.style.display = "none";
    }

    // When the user clicks anywhere outside of the modal, close it
    window.onclick = function(event) {
        if (event.target == modal) {
            modal.style.display = "none";
        }
    }


    var step = "";
    if (target.substr(target.length - 1) == 1) {
        step = target.substr(0, 10) + "早上";
    }
    else {
        step = target.substr(0, 10) + "下午";
    }

    document.getElementById("step").innerText = step
}
function SaveData() {


    var step = document.getElementById("step").innerText;
    var org = document.getElementById("organ").value;
    var name = document.getElementById("name").value;
    var phone = document.getElementById("phone").value;

    if (org == "") {
        alert("請填入部門");
        return;
    }
    if (name == "") {
        alert("請填入姓名");
        return;
    }
    if (phone == "") {
        alert("請填入分機");
        return;
    }

    var jsonData = JSON.stringify({
        time: step,
        org: org,
        name: name,
        phone: phone

    });



    $.ajax({
        type: "POST",
        async: false,
        dataType: "json",
        data: jsonData,
        url: "../Main.asmx/SaveData",
        contentType: 'application/json; charset=UTF-8',
        success: function(results) {
            if (results.d == "Fail") {
                alert("今日登記已額滿!!");
            }
            else if (results.d == "Success") {
                var timeshow = step.replace(/-/g, '/');



                alert("報名成功!!\n已登記於" + timeshow + "的時段");
            }
            else {
                alert(results.d);
            }
            location.reload();

        },
        error: function(msg) {
            console.log(msg);
        }
    });




}
function Cancel() {
    var modal = document.getElementById("myModal");
    modal.style.display = "none";
}
if (!Object.entries) {
    Object.entries = function(obj) {
        var ownProps = Object.keys(obj),
        i = ownProps.length,
        resArray = new Array(i); // preallocate the Array
        while (i--)
            resArray[i] = [ownProps[i], obj[ownProps[i]]];

        return resArray;
    };
}