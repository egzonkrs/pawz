// Variabla globale për lidhjen SignalR

let connection;

// Funksioni për të filluar lidhjen SignalR
function startSignalRConnection() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("/notificationHub")
        .withAutomaticReconnect()
        .build();

    connection.start().catch(err => console.error("Error starting SignalR connection:", err.toString()));

    connection.on("ReceiveNotification", function (notification) {
        updateNotificationUI(notification);
    });
}

// Funksioni për të përditësuar UI-në e njoftimeve
function updateNotificationUI(notification) {
    const count = document.getElementById('notification-count');
    const currentCount = parseInt(count.textContent) || 0;
    const list = document.getElementById('notification-list');

    // Kontrolloni nëse njoftimi ekziston tashmë
    const existingNotification = document.querySelector(`[data-notification-id="${notification.id}"]`);

    if (existingNotification) {
        // Nëse ekziston, përditësojeni atë
        existingNotification.textContent = notification.message;
    } else {
        // Nëse nuk ekziston, shtojeni si të ri
        const newNotification = document.createElement('a');
        newNotification.className = 'dropdown-item';
        newNotification.href = '#';
        newNotification.textContent = notification.message;
        newNotification.setAttribute('data-notification-id', notification.id);
        list.prepend(newNotification);

        // Rrisni numërimin vetëm nëse është një njoftim i ri
        count.textContent = currentCount + 1;
    }
}

// Funksioni për të dërguar një njoftim
function sendNotification(recipientId, petId) {
    const message = `User clicked on Pet with ID: ${petId}`;
    fetch('/api/Notification/send', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            recipientId: recipientId,
            message: message,
            petId: petId
        }),
    })
        .then(response => response.json())
        .then(data => console.log('Notification sent:', data))
        .catch((error) => console.error('Error sending notification:', error));
}


// Funksioni për të marrë njoftimet e përdoruesit
function getUserNotifications() {
    fetch('/api/Notification/user')
        .then(response => response.json())
        .then(notifications => {
            const list = document.getElementById('notification-list');
            const count = document.getElementById('notification-count');
            list.innerHTML = '';
            notifications.forEach(notification => {
                const newNotification = document.createElement('a');
                newNotification.className = 'dropdown-item';
                newNotification.href = '#';
                newNotification.textContent = notification.message;
                newNotification.setAttribute('data-notification-id', notification.id);
                list.appendChild(newNotification);
            });
            count.textContent = notifications.length;
        })
        .catch(error => console.error('Error fetching notifications:', error));
}

// Event listener për të ngarkuar njoftimet kur hapet dropdown-i
document.addEventListener('DOMContentLoaded', function () {
    const notificationDropdown = document.querySelector('.notification-dropdown');
    if (notificationDropdown) {
        notificationDropdown.addEventListener('show.bs.dropdown', function () {
            getUserNotifications();
        });
    }

    // Lidhni butonin e mesazhit me funksionin e dërgimit të njoftimit
    const messageButtons = document.querySelectorAll('[id^="messageButton"]');
    messageButtons.forEach(button => {
        button.addEventListener('click', function () {
            const petId = this.getAttribute('data-pet-id');
            const ownerId = this.getAttribute('data-owner-id');
            sendNotification(ownerId, petId);
        });
    });

    // Filloni lidhjen SignalR
    startSignalRConnection();

    // Ngarko njoftimet fillestare
    getUserNotifications();
});

// Funksioni për të shënuar një njoftim si të lexuar
function markNotificationAsRead(notificationId) {
    fetch(`/api/Notification/markAsRead/${notificationId}`, {
        method: 'POST',
    })
        .then(response => response.json())
        .then(data => {
            console.log('Notification marked as read:', data);
            // Përditëso UI-në pas shënimit si të lexuar
            const notification = document.querySelector(`[data-notification-id="${notificationId}"]`);
            if (notification) {
                notification.classList.add('read');
            }
        })
        .catch((error) => console.error('Error marking notification as read:', error));
}

// Shtoni event listener për klikimin e njoftimeve individuale
document.addEventListener('click', function (event) {
    if (event.target.matches('#notification-list .dropdown-item')) {
        const notificationId = event.target.getAttribute('data-notification-id');
        markNotificationAsRead(notificationId);
    }
});


