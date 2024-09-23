let connection;

document.addEventListener('DOMContentLoaded', function () {
    initializeSignalR();
});

function initializeSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("/notificationHub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.start()
        .then(() => {
            console.log("SignalR connection established.");
        })
        .catch(err => {
            console.error("SignalR Connection Error: ", err.toString());
        });

    // Listen for real-time notifications
    connection.on("ReceiveNotification", function (notification) {
        console.log("Received notification:", notification);
    });
}

