Array.prototype.remove = function (predicate) {
    var array = this;
    var itemsToRemoveIndices = [];

    if (array != null && array.length > 0) {
        for (var index = 0; index < array.length; index++) {
            if (predicate(array[index])) {
                itemsToRemoveIndices.push(index);
            }
        }

        if (itemsToRemoveIndices.length > 0) {
            for (var index = itemsToRemoveIndices.length - 1; index >= 0; index--) {
                array.splice(index, 1);
            }
        }
    }
}

var utilities = new Utilities();
var pagerController = new PagerController();

var notificationsService = new NotificationsService(utilities);
var notificationsController = new NotificationsController(notificationsService);
notificationsController.bindNotificationsControls();

var coursesService = new CoursesService(utilities);
var coursesController = new CoursesController(coursesService, pagerController);