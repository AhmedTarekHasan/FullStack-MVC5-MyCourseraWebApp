function NotificationsController(notificationsService) {
    var self = this;

    self.hubName = "notificationsHub";
    self.notificationsHub = null;

    self.notificationsService = notificationsService;

    self.connectToHub = function () {
        self.notificationsHub = $.connection[self.hubName];
        self.notificationsHub.client.receiveNotifications = self.onReceiveNotifications;
        $.connection.hub.start().done(self.onConnectedToHub);
    };

    self.onConnectedToHub = function () {
        self.notificationsHub.server.getMyNotifications();
    };

    self.onReceiveNotifications = function (notificationsToken) {
        //console.log(notificationsToken);
        
        var helper = new NotificationHelper();
        var adminMessageUserNotifications = helper.GetFromAdminMessageUserNotifications(notificationsToken.AdminMessageUserNotifications);
        var courseCancelledUserNotifications = helper.GetFromCourseCancelledUserNotifications(notificationsToken.CourseCancelledUserNotifications);
        var courseCreatedUserNotifications = helper.GetFromCourseCreatedUserNotifications(notificationsToken.CourseCreatedUserNotifications);
        var courseUpdatedUserNotifications = helper.GetFromCourseUpdatedUserNotifications(notificationsToken.CourseUpdatedUserNotifications);

        var allNotifications = ((adminMessageUserNotifications.concat(courseCancelledUserNotifications)).concat(courseCreatedUserNotifications)).concat(courseUpdatedUserNotifications);
        
        allNotifications.filter(function (notification) {
            return (self.viewmodel.allNotifications().find(function (alreadyExistingNotification) { alreadyExistingNotification.id() == notification.id() }) == null);
        });

        allNotifications = self.viewmodel.allNotifications().concat(allNotifications);

        allNotifications.sort(function (notification1, notification2) {
            var when1 = new Date(notification1.when());
            var when2 = new Date(notification2.when());

            return (when2 - when1);
        });

        //console.log(allNotifications);
        self.viewmodel.allNotifications(allNotifications);
    };

    self.connectToHub();

    self.markUserNotificationAsSeen = function (notificationId, onNotificationMarkedAsSeenSuccessfully, onNotificationMarkedAsSeenFailure) {
        self.notificationsService.markUserNotificationAsSeen(notificationId, onNotificationMarkedAsSeenSuccessfully, onNotificationMarkedAsSeenFailure);
    };

    self.markUserNotificationAsUnSeen = function (notificationId, onNotificationMarkedAsUnSeenSuccessfully, onNotificationMarkedAsUnSeenFailure) {
        self.notificationsService.markUserNotificationAsUnSeen(notificationId, onNotificationMarkedAsUnSeenSuccessfully, onNotificationMarkedAsUnSeenFailure);
    };

    self.markUserNotificationAsDismissed = function (notificationId, onNotificationMarkedAsDismissedSuccessfully, onNotificationMarkedAsDismissedFailure) {
        self.notificationsService.markUserNotificationAsDismissed(notificationId, onNotificationMarkedAsDismissedSuccessfully, onNotificationMarkedAsDismissedFailure);
    };

    self.bindNotificationsControls = function () {
        $(document).on('click', "#notificationLink", function () {
            $("#notificationContainer").fadeToggle(300);
            return false;
        });

        $(document).on('click', "#notification_count", function () {
            $("#notificationContainer").fadeToggle(300);
            return false;
        });

        $(document).on('click', "#notificationContainer", function () {
            return false;
        });

        $(document).click(function () {
            $("#notificationContainer").hide();
        });

        self.viewmodel = new ViewModel(self.markUserNotificationAsSeen, self.markUserNotificationAsUnSeen, self.markUserNotificationAsDismissed);
        ko.applyBindings(self.viewmodel, $("#notification_li")[0]);
    };
};

function ViewModel(markUserNotificationAsSeen, markUserNotificationAsUnSeen, markUserNotificationAsDismissed) {
    var self = this;

    self.notificationsService = notificationsService;

    self.allNotifications = ko.observableArray([]);

    self.notificationsCount = ko.computed(function() {
        return self.allNotifications().filter(function (notification) {
            return !(notification.isSeen());
        }).length;
    }, this);

    self.markAsSeen = function (notification) {
        markUserNotificationAsSeen(notification.id(), function () {
            self.allNotifications().filter(function (not) { return notification.id() == not.id() })[0].isSeen(true);
            return false;
        },
        function () {        
        });
    };

    self.markAsUnSeen = function (notification) {
        markUserNotificationAsUnSeen(notification.id(), function () {
            self.allNotifications().filter(function (not) { return notification.id() == not.id() })[0].isSeen(false);
            return false;
        },
        function () {
        });
    };

    self.markAsDismissed = function (notification) {
        markUserNotificationAsDismissed(notification.id(), function () {
            var not = self.allNotifications().filter(function (not) { return notification.id() == not.id() })[0];
            self.allNotifications.remove(not);
            return false;
        },
        function () {
        });
    };
};

function Notification() {
    var self = this;

    self.id = ko.observable(0);
    self.html = ko.observable("");
    self.byWhomFullName = ko.observable("");
    self.when = ko.observable(new Date());
    self.whenFormatted = ko.computed(function () {
        var result = '';
        var temp = new Date(self.when());

        if (temp.setHours(0, 0, 0, 0) == new Date().setHours(0, 0, 0, 0)) {
            //today
            result = 'Today at ' + self.when().getHours() + ":" + self.when().getMinutes();
        }
        else {
            result = self.when().getFullYear() + "/" + (self.when().getMonth() + 1) + "/" + self.when().getDate();
        }

        return result;
    }, this);
    self.isSeen = ko.observable(false);
    self.isDismissed = ko.observable(false);
};

function NotificationHelper() {
    var self = this;

    var GetNotificationFromToken = function(token, messageBuilder){
        var notification = new Notification();

        if (token.Notification != null) {
            notification.html(messageBuilder(token));
            notification.id(token.Notification.Notification.Id);
            notification.when(new Date(token.Notification.Notification.When));
            notification.byWhomFullName(token.Notification.Notification.ByWhom.FullName);
            notification.isSeen(token.IsSeen);
            notification.isDismissed(token.IsDismissed);
        }

        return notification;
    };

    self.GetFromAdminMessageUserNotifications = function (adminMessageUserNotifications) {
        var allNotifications = [];

        if (typeof (adminMessageUserNotifications) != 'undefined' && adminMessageUserNotifications != null) {
            for (var i = 0; i < adminMessageUserNotifications.length; i++) {
                if (typeof (adminMessageUserNotifications[i]) != 'undefined' && adminMessageUserNotifications[i] != null) {
                    var notification = GetNotificationFromToken(adminMessageUserNotifications[i], function (token) { return token.Notification.Message; });
                    allNotifications.push(notification);
                }
            }
        }

        return allNotifications;
    };

    self.BuildCourseCancelledUserNotificationsHtml = function (courseCancelledUserNotification) {
        var html = '';

        if (typeof (courseCancelledUserNotification) != 'undefined' && courseCancelledUserNotification != null) {
            var courseTitle = courseCancelledUserNotification.Notification.Course.Title;
            var cancelledBy = courseCancelledUserNotification.Notification.Notification.ByWhom.FullName;

            html = 'The course "' + courseTitle + '" is cancelled by "' + cancelledBy + '"';
        }

        return html;
    }
    self.GetFromCourseCancelledUserNotifications = function (courseCancelledUserNotifications) {
        var allNotifications = [];

        if (typeof (courseCancelledUserNotifications) != 'undefined' && courseCancelledUserNotifications != null && courseCancelledUserNotifications.length > 0) {
            for (var i = 0; i < courseCancelledUserNotifications.length; i++) {
                if (typeof (courseCancelledUserNotifications[i]) != 'undefined' && courseCancelledUserNotifications[i] != null) {
                    var notification = GetNotificationFromToken(courseCancelledUserNotifications[i], self.BuildCourseCancelledUserNotificationsHtml);
                    allNotifications.push(notification);
                }
            }
        }

        return allNotifications;
    };

    self.BuildCourseCreatedUserNotificationsHtml = function (courseCreatedUserNotification) {
        var html = '';

        if (typeof (courseCreatedUserNotification) != 'undefined' && courseCreatedUserNotification != null) {
            var courseId = courseCreatedUserNotification.Notification.Course.CourseId;
            var courseTitle = courseCreatedUserNotification.Notification.Course.Title;
            var createdBy = courseCreatedUserNotification.Notification.Notification.ByWhom.FullName;

            html = 'The course <a href="/courses/view/' + courseId + '">' + courseTitle + '</a> is created by "' + createdBy + '"';
        }

        return html;
    }
    self.GetFromCourseCreatedUserNotifications = function (courseCreatedUserNotifications) {
        var allNotifications = [];

        if (typeof (courseCreatedUserNotifications) != 'undefined' && courseCreatedUserNotifications != null && courseCreatedUserNotifications.length > 0) {
            for (var i = 0; i < courseCreatedUserNotifications.length; i++) {
                if (typeof (courseCreatedUserNotifications[i]) != 'undefined' && courseCreatedUserNotifications[i] != null) {
                    var notification = GetNotificationFromToken(courseCreatedUserNotifications[i], self.BuildCourseCreatedUserNotificationsHtml);
                    allNotifications.push(notification);
                }
            }
        }

        return allNotifications;
    };

    self.BuildCourseUpdatedUserNotificationsHtml = function (courseUpdatedUserNotification) {
        var html = '';

        if (typeof (courseUpdatedUserNotification) != 'undefined' && courseUpdatedUserNotification != null) {
            var courseId = courseUpdatedUserNotification.Notification.Course.CourseId;
            var courseTitle = courseUpdatedUserNotification.Notification.Course.Title;
            var updatedBy = courseUpdatedUserNotification.Notification.Notification.ByWhom.FullName;

            html = 'The course <a href="/courses/view/' + courseId + '">' + courseTitle + '</a> is updated by "' + updatedBy + '"';
        }

        return html;
    }
    self.GetFromCourseUpdatedUserNotifications = function (courseUpdatedUserNotifications) {
        var allNotifications = [];

        if (typeof (courseUpdatedUserNotifications) != 'undefined' && courseUpdatedUserNotifications != null && courseUpdatedUserNotifications.length > 0) {
            for (var i = 0; i < courseUpdatedUserNotifications.length; i++) {
                if (typeof (courseUpdatedUserNotifications[i]) != 'undefined' && courseUpdatedUserNotifications[i] != null) {
                    var notification = GetNotificationFromToken(courseUpdatedUserNotifications[i], self.BuildCourseUpdatedUserNotificationsHtml);
                    allNotifications.push(notification);
                }
            }
        }

        return allNotifications;
    };
};