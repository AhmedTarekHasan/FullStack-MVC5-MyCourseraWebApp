function CoursesService(utilities) {
    var self = this;
    self.utilities = utilities;
};

CoursesService.prototype.markCourseAsAttending = function (courseId, successCallback, failureCallback) {
    var self = this;

    if (typeof (courseId) != 'undefined' && courseId != null && courseId != '') {
        self.utilities.doAjaxCall(true, "/api/courses/attend/" + courseId, 'POST', null, null, null, null, successCallback, failureCallback);
    }
};

CoursesService.prototype.markCourseAsNotAttending = function (courseId, successCallback, failureCallback) {
    var self = this;

    if (typeof (courseId) != 'undefined' && courseId != null && courseId != '') {
        self.utilities.doAjaxCall(true, "/api/courses/unattend/" + courseId, 'POST', null, null, null, null, successCallback, failureCallback);
    }
};

CoursesService.prototype.cancelCourse = function (courseId, successCallback, failureCallback) {
    var self = this;

    if (typeof (courseId) != 'undefined' && courseId != null && courseId != '') {
        self.utilities.doAjaxCall(true, "/api/courses/cancel/" + courseId, 'POST', null, null, null, null, successCallback, failureCallback);
    }
};

CoursesService.prototype.getUserCoursesByPageIndexGeneric = function (actionName, searchTerm, pageSize, pageIndex, successCallback, failureCallback) {
    var self = this;
    self.utilities.doAjaxCall(true, "/courses/" + actionName + "?searchTerm=" + searchTerm + "&pageSize=" + pageSize.toString() + "&pageIndex=" + pageIndex.toString(), 'GET', null, null, "html", null, successCallback, failureCallback);
};

CoursesService.prototype.getUserCoursesByPageIndex = function (searchTerm, pageSize, pageIndex, successCallback, failureCallback) {
    var self = this;
    self.getUserCoursesByPageIndexGeneric("index", searchTerm, pageSize, pageIndex, successCallback, failureCallback);
};

CoursesService.prototype.getUserAttendingCoursesByPageIndex = function (searchTerm, pageSize, pageIndex, successCallback, failureCallback) {
    var self = this;
    self.getUserCoursesByPageIndexGeneric("attending", searchTerm, pageSize, pageIndex, successCallback, failureCallback);
};

CoursesService.prototype.getUserInstructingCoursesByPageIndex = function (searchTerm, pageSize, pageIndex, successCallback, failureCallback) {
    var self = this;
    self.getUserCoursesByPageIndexGeneric("instructing", searchTerm, pageSize, pageIndex, successCallback, failureCallback);
};