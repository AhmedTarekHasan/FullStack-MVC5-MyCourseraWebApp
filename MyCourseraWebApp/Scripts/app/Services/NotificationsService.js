function NotificationsService(utilities) {
    var self = this;
    self.utilities = utilities;
};

NotificationsService.prototype.getUserNotifications = function (successCallback, failureCallback) {
    var self = this;
    self.utilities.doAjaxCall(true, "/api/notifications", 'GET', null, null, null, null, successCallback, failureCallback);
};

NotificationsService.prototype.markUserNotificationAsSeen = function (notificationId, successCallback, failureCallback) {
    var self = this;

    if (typeof (notificationId) != 'undefined' && notificationId != null && notificationId > 0) {
        self.utilities.doAjaxCall(true, "/api/notifications/seen/" + notificationId, 'POST', null, null, null, null, successCallback, failureCallback);
    }
};

NotificationsService.prototype.markUserNotificationAsUnSeen = function (notificationId, successCallback, failureCallback) {
    var self = this;

    if (typeof (notificationId) != 'undefined' && notificationId != null && notificationId > 0) {
        self.utilities.doAjaxCall(true, "/api/notifications/unseen/" + notificationId, 'POST', null, null, null, null, successCallback, failureCallback);
    }
};

NotificationsService.prototype.markUserNotificationAsDismissed = function (notificationId, successCallback, failureCallback) {
    var self = this;

    if (typeof (notificationId) != 'undefined' && notificationId != null && notificationId > 0) {
        self.utilities.doAjaxCall(true, "/api/notifications/dismiss/" + notificationId, 'POST', null, null, null, null, successCallback, failureCallback);
    }
};