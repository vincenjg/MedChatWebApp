$(function () {
    /****set up signalr connection and configure actions****/
    var connection = new signalR.HubConnectionBuilder()
        .withUrl("/LobbyHub")
        .withAutomaticReconnect()
        .build();
    
    ConfigureSignalRConnection();

    /****Handle switch event****/
    $("#onlineStatusToggle").change(function () {
        //TODO: figure out a way to get full name and id from page element
        //values from page.
        var lobbyName = $("#lobbyName").text();
        var checked = $(this).prop("checked") ? true : false;

        //values in json format, so they can be passed to ajax call.
        var data = { "status": { "id": 14, "isOnline": checked } };

        CreateOrDestroyLobby(checked, lobbyName);
        ChangeStatus(data);
    });


    function ConfigureSignalRConnection() {
        //enable tracing for browser console.
        connection.logging = true;

        connection.on("ReceiveMessage", function (message) {
            console.log(message);
            //bool is to manage state with the new view model for the partial view.
            var checked = $("#onlineStatusToggle").prop("checked") ? true : false;
            var data = { "isOnline": checked };

            $.ajax({
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                type: "GET",
                url: "/Practitioners/Lobby/GetLobbyMembers",
                data: JSON.stringify(data),
                dataType: "html",
                success: function (result) {
                    console.log("request successful");
                    console.log(result);
                    $(".card-group").html(result);
                },
                error: function (jqxhr, status, error) {
                    console.error("request failed");
                    console.error(error);
                }
            });
        });

        connection.start().
            then(() => console.log("Connection started!"))
            .catch((error) => console.log('Error while establishing connection :('));
    }

    function CreateOrDestroyLobby(checked, lobbyname) {
        if (checked) {
            connection.invoke("CreateLobby", lobbyname);
        } else {
            connection.invoke("DestroyLobby", lobbyname);
        }
    }

    function ChangeStatus(data) {
        $.ajax({
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            type: "POST",
            url: "/Practitioners/Lobby/ChangeStatus",
            dataType: "html",
            data: JSON.stringify(data),
            success: function (result) {
                console.log("request successful");
                $(".card-group").html(result);
            },
            error: function (jqxhr, status, error) {
                console.error("request failed");
                console.error(error);
            }
        });
    }
});