// notification.js

// Variabla globale për lidhjen SignalR
let connection;

// Funksioni kryesor që ekzekutohet kur dokumenti është gati
document.addEventListener('DOMContentLoaded', function () {
    if (userIsLoggedIn()) {
        initializeNotificationSystem();
    } else {
        console.log("Përdoruesi nuk është i loguar. Sistemi i njoftimeve është i çaktivizuar.");
    }
});

// Kontrollon nëse përdoruesi është i loguar
function userIsLoggedIn() {
    return document.body.classList.contains('user-logged-in');
}

// Inicializon sistemin e njoftimeve
function initializeNotificationSystem() {
    initializeSignalR();
    getUserNotifications();
    setupNotificationUI();
}

// Inicializon lidhjen SignalR
function initializeSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("/notificationHub")
        .configureLogging(signalR.LogLevel.Warning)
        .build();

    connection.start().catch(err => console.warn("Nuk mund të lidhej me serverin e njoftimeve:", err));

    connection.on("ReceiveNotification", function (notification) {
        console.log("Njoftim i ri:", notification);
        addNotificationToUI(notification);
        updateNotificationCount();
    });
}

// Merr njoftimet e përdoruesit nga serveri
function getUserNotifications() {
    fetch('/api/Notification/user')
        .then(response => response.json())
        .then(notifications => {
            notifications.forEach(addNotificationToUI);
            updateNotificationCount();
        })
        .catch(error => console.warn('Gabim gjatë marrjes së njoftimeve:', error));
}

// Konfiguron UI-në e njoftimeve
function setupNotificationUI() {
    const notificationDropdown = document.getElementById('notification-dropdown');
    if (notificationDropdown) {
        notificationDropdown.addEventListener('click', function () {
            // Logjika për hapjen/mbylljen e dropdown-it të njoftimeve
        });
    }

    document.addEventListener('click', function (e) {
        if (e.target && e.target.classList.contains('mark-as-read')) {
            const notificationId = e.target.getAttribute('data-notification-id');
            markNotificationAsRead(notificationId);
        }
    });
}

// Shton një njoftim në UI
function addNotificationToUI(notification) {
    const notificationList = document.getElementById('notification-list');
    if (notificationList) {
        const notificationElement = document.createElement('div');
        notificationElement.className = 'notification-item';
        notificationElement.innerHTML = `
            <p>${notification.message}</p>
            <button class="mark-as-read" data-notification-id="${notification.id}">Shëno si të lexuar</button>
        `;
        notificationList.prepend(notificationElement);
    }
}

// Përditëson numëruesin e njoftimeve
function updateNotificationCount() {
    const count = document.getElementById('notification-count');
    if (count) {
        const currentCount = document.querySelectorAll('.notification-item').length;
        count.textContent = currentCount;
        count.style.display = currentCount > 0 ? 'inline' : 'none';
    }
}

// Markon një njoftim si të lexuar
function markNotificationAsRead(notificationId) {
    fetch(`/api/Notification/markAsRead/${notificationId}`, { method: 'POST' })
        .then(response => {
            if (response.ok) {
                removeNotificationFromUI(notificationId);
            }
        })
        .catch(error => console.warn('Gabim gjatë shënimit të njoftimit si të lexuar:', error));
}

// Heq një njoftim nga UI
function removeNotificationFromUI(notificationId) {
    const notification = document.querySelector(`.notification-item[data-notification-id="${notificationId}"]`);
    if (notification) {
        notification.remove();
        updateNotificationCount();
    }
}

// Funksioni për të dërguar një njoftim
window.sendNotification = function (recipientId, petId) {
    if (!userIsLoggedIn()) {
        console.log("Nuk mund të dërgoni njoftime pa qenë i loguar.");
        return;
    }

    const message = `Një përdorues ka shfaqur interes për kafshën tuaj me ID: ${petId}`;
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
        .then(data => console.log('Njoftimi u dërgua:', data))
        .catch((error) => console.warn('Gabim gjatë dërgimit të njoftimit:', error));
};

// Funksion që mund të thirret pas një logini të suksesshëm
window.onSuccessfulLogin = function () {
    initializeNotificationSystem();
};

// Funksion që mund të thirret pas logout
window.onLogout = function () {
    if (connection) {
        connection.stop();
    }
    // Pastro elementet e UI-së të lidhura me njoftimet
    const notificationList = document.getElementById('notification-list');
    if (notificationList) {
        notificationList.innerHTML = '';
    }
    updateNotificationCount();
};
