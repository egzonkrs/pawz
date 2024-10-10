let connection;

document.addEventListener('DOMContentLoaded', function () {
    if (userIsLoggedIn()) {
        initializeNotificationSystem();
    } 
});

function userIsLoggedIn() {
    return document.body.classList.contains('user-logged-in');
}

function initializeNotificationSystem() {
    initializeSignalR();
    getUserNotifications();
    setupNotificationUI();
}

function initializeSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("/notificationHub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.start().catch(err => console.error("SignalR Connection Error: ", err.toString()));

    connection.on("ReceiveNotification", function (notification) {
        addNotificationToUI(notification);
        updateNotificationCount();
    });
}

function getUserNotifications() {
    fetch('/api/v1.0/Notification/user')
        .then(response => response.json())
        .then(notifications => {
            notifications.forEach(addNotificationToUI);
            updateNotificationCount();
        })
        .catch(error => console.error('Error fetching notifications:', error));
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
}

function addNotificationToUI(notification) {
    const notificationList = document.getElementById('notification-list');
    if (notificationList) {
        const notificationElement = document.createElement('div');
        notificationElement.className = 'notification-item' + (notification.isRead ? '' : ' unread');

        // Përdorni një ID alternative nëse 'id' mungon
        const notificationId = notification.id || `temp-${Date.now()}`;
        notificationElement.setAttribute('data-notification-id', notificationId);

        notificationElement.innerHTML = `
            <p>${notification.message}</p>
        `;
        notificationList.prepend(notificationElement);
       console.log('Added notification to UI:', notification);
    }
}

function updateNotificationCount() {
    const unreadCount = document.querySelectorAll('.notification-item.unread').length;
    const countElement = document.getElementById('notification-count');
    if (countElement) {
        countElement.textContent = unreadCount;
        countElement.style.display = unreadCount > 0 ? 'inline' : 'none';
    }
}

function markNotificationAsRead(notificationId) {
    if (isNaN(notificationId) || notificationId <= 0) {
        return;
    }
    fetch(`/api/v1.0/Notification/markAsRead/${notificationId}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            return response.json();
        })
        .then(data => {
            const notification = document.querySelector(`.notification-item[data-notification-id="${notificationId}"]`);
            if (notification) {
                notification.classList.remove('unread');
                updateNotificationCount();
            }
        })
        .catch(error => console.error('Error marking notification as read:', error));
}

document.addEventListener('DOMContentLoaded', function () {
    const makeAdoptionRequest = document.getElementById('makeAdoptionRequest');
    if (makeAdoptionRequest) {
        makeAdoptionRequest.addEventListener('click', function () {
            const petId = this.getAttribute('data-pet-id');
            const petName = this.getAttribute('data-pet-name');
            const ownerId = this.getAttribute('data-owner-id');
            const senderName = this.getAttribute('data-sender-name');
            sendNotification(senderName, ownerId, petId, petName);
        });
    }
});

window.sendNotification = function (senderName, recipientId, petId, petName) {
    if (!userIsLoggedIn()) {
        return;
    }

    const message = `${senderName} requested to adopt you pet: ${petName}`;
    fetch('/api/v1.0/Notification/send', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            recipientId: recipientId,
            message: message,
            petId: petId,
            petName: petName,
            senderName: senderName
        }),
    })
        .then(response => response.json())
      //  .then(data => console.log('Notification sent:', data))
        .catch((error) => console.error('Error sending notification:', error));
};

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
