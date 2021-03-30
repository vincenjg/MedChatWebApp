$(function () {
    var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

    $("#lobbySelect").change(function () {
        $("#lobbySelect").prop("disabled", true);
        $("#leaveBtn").prop("disabled", false);
    });

    $("#leaveBtn").click(function () {
        $("#leaveBtn").prop("disabled", true);
        $("#lobbySelect").val("0");
        $("#lobbySelect").prop("disabled", false);
    }
});