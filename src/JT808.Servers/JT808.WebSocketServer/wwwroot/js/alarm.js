//API https://docs.microsoft.com/zh-cn/javascript/api/%40aspnet/signalr/?view=signalr-js-latest
var connection = null;

document.getElementById("connectButton").onclick = function () {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:5000/alarmHub", signalR.HttpTransportType.WebSocket)
        .build();
    connection.on("ReceiveMessage", (message) => {
        console.log(message);
        const li = document.createElement("li");
        li.textContent = message;
        document.getElementById("messagesList").appendChild(li);
    });

    connection.start().catch(err => console.error(err.toString()));
};

document.getElementById("sendButton").addEventListener("click", event => {
    const message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", message).catch(err => console.error(err.toString()));
    event.preventDefault();
});

