jQuery(($) => {
  const fbEditor = document.getElementById("build-wrap");
  const formBuilder = $(fbEditor).formBuilder();

    document.getElementById("saveData").addEventListener("click", () => {

        const result = formBuilder.actions.getData();
        
        console.log("result:", result);
        var dataToSend = JSON.stringify(result);
        var formName = document.getElementById("nameInput").value;
        /*var dataToSend = {
            TemplateData: $("build-wrap").val()
        }*/

        alert(formName);
     
        $.ajax({
            url: '/Template/SendTemplateData',
            type: 'POST',
            data: {TemplateName: formName, TemplateData: dataToSend },
            success: function (response) {
                alert("Saved!")
            },
            error: function (response) {
                alert("Error - Contact Admins!")
            }
            
        });



    });
/*  document.getElementById("saveData").addEventListener("click", () => {
    console.log("external save clicked");
    const result = formBuilder.actions.getData();
    console.log("result:", result);
  });*/
});

