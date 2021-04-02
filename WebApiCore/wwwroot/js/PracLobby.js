$(function () {
    /****Handle signalr connection and functions****/
    var connection = new signalR.HubConnectionBuilder().withUrl("/LobbyHub").build();

    connection.start().catch(function (err) {
        return console.error(err.toString());
    });

    connection.on("ReceiveMessage", function (message) {
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
            contentType: "application/json",
            dataType: "json",
            success: function (data) {
                console.log("success", data);
                $("#card-group").html(data);
            },
            error: function (error) {
                console.log(error);
            }
        });
    });

    /****Handle switch event****/
    $("#onlineStatusToggle").change(function () {
        var checked = $(this).prop("checked") ? true : false;

        var data = { "status": { "id": 13, "isOnline": checked } };

        $.ajax({
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json' 
            },
            type: "POST",
            url: "/Practitioners/Lobby/ChangeStatus",
            data: JSON.stringify(data),
            contentType: "application/json",
            dataType: "json",
            success: function (data) {
                console.log("success", data);
                $("#card-group").html(data);
            },
            error: function (error) {
                console.log(error);
            }
        });
    });
});