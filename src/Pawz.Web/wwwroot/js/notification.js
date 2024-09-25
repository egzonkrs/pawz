let connection;

document.addEventListener('DOMContentLoaded', function () {
    if (userIsLoggedIn()) {
        initializeNotificationSystem();
    } 
});

function initializeNotificationSystem() {
    initializeSignalR();
    setupNotificationUI();
}

function userIsLoggedIn() {
    return document.body.classList.contains('user-logged-in');
}

function initializeSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("/notificationHub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.start()
        .catch(err => {
            console.error("SignalR Connection Error: ", err.toString());
        });

    connection.on("ReceiveNotification", function (notification) {
    });
}

function updateNotificationCount() {
    const unreadCount = document.querySelectorAll('.notification-item.unread').length;
    const countElement = document.getElementById('notification-count');
    if (countElement) {
        countElement.textContent = unreadCount;
        countElement.style.display = unreadCount > 0 ? 'inline' : 'none';
    }
}

function setupNotificationUI() {
    const notificationDropdown = document.getElementById('notification-dropdown');
    if (notificationDropdown) {
        notificationDropdown.addEventListener('click', function (event) {
            event.preventDefault();
            const dropdownMenu = document.getElementById('notification-menu');
            if (dropdownMenu) {
                dropdownMenu.classList.toggle('show');
            }
        });
    }

    document.addEventListener('click', function (event) {
        const notificationItem = event.target.closest('.notification-item');
        if (notificationItem) {
            const notificationId = notificationItem.getAttribute('data-notification-id');
            if (notificationId) {
                markNotificationAsRead(notificationId);
            }
        }
    });
}

function addNotificationToUI(notification) {
    const notificationList = document.getElementById('notification-list');
    if (notificationList) {
        const notificationElement = document.createElement('div');
        notificationElement.className = 'notification-item' + (notification.isRead ? '' : ' unread');

        const notificationId = notification.id || `temp-${Date.now()}`;
        notificationElement.setAttribute('data-notification-id', notificationId);

        notificationElement.innerHTML = `
            <p>${notification.message}</p>
        `;
        notificationList.prepend(notificationElement);
    }
}

window.onSuccessfulLogin = function () {
    initializeNotificationSystem();
};

window.onLogout = function () {
    if (connection) {
        connection.stop();
    }
    const notificationList = document.getElementById('notification-list');
    if (notificationList) {
        notificationList.innerHTML = '';
    }
    updateNotificationCount();
};
