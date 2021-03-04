

jQuery(($) => {
  const fbEditor = document.getElementById("build-wrap");
  const formBuilder = $(fbEditor).formBuilder();

    document.getElementById("saveData").addEventListener("click", () => {

        /*const result = formBuilder.actions.getData();
        alert(result)
        console.log("result:", result);*/

        var dataToSend = {
            TemplateData: $("build-wrap").val()
        }

        $.ajax({
            url: '/Template/SendTemplateData',
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json',
            success: function (data) {
                alert: ("okay");
            },
            data: JSON.stringify(dataToSend)
        });

        /*$.ajax({
            url: "Template/SendTemplateData",
            type: "POST",
            data: result,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                alert: ("okay");
            }
        });*/

    });
/*  document.getElementById("saveData").addEventListener("click", () => {
    console.log("external save clicked");
    const result = formBuilder.actions.getData();
    console.log("result:", result);
  });*/
});

/*document.getElementById("saveData").addEventListener("click", function (e) {

    var dataToSend = JSON.stringify({ testEditor });
    alert(dataToSend)
    $.ajax({
        url: "Template/SendTemplateData",
        type: "POST",
        data: dataToSend,
        contentType: "application/json; charset=utf-8",
        sucess: function (data) {
            alert: ("okay");
        }
    });

});
*/