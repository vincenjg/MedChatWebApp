//var testEditor = document.getElementById("friendform");


$(document).ready(function () {
    $("#btnsubmit").click(function (e) {
        //Serialize the form datas.  
        //var valdata = $("#build-wrap").serialize();
        var valdata = $("#friendform : input")
        //to get alert popup  
        alert(valdata);
        $.ajax({
            url: "/Friend/AddFriend",
            type: "POST",
            dataType: 'json',
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            data: valdata
        });
    });
});  

