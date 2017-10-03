function CoursesController(coursesService, pagerController, searchConrolsMainContainerSelector) {
    var self = this;

    self.coursesService = coursesService;
    self.pagerController = pagerController;
    self.searchConrolsMainContainerSelector = searchConrolsMainContainerSelector;

    /*
    bindingMode = 1 ==> All
    bindingMode = 2 ==> Attending
    bindingMode = 3 ==> Instructing
    */
    self.bindingMode = 1;

    self.bindAttendActionButtons = function () {
        $(document).on("click", "[data-course-attend]", function () {
            var btn = $(this);
            var courseId = btn.attr("data-course-id");

            self.coursesService.markCourseAsAttending(courseId, function (response) {
                toggleAttendanceActionButtonState(btn);
            }, function (xhr, err) {
                alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                alert("responseText: " + xhr.responseText);
            });

            return false;
        });
    };

    self.bindUnAttendActionButtons = function () {
        $(document).on("click", "[data-course-unattend]", function () {
            var btn = $(this);
            var courseId = btn.attr("data-course-id");

            self.coursesService.markCourseAsNotAttending(courseId, function () {
                toggleAttendanceActionButtonState(btn);
            }, function (xhr, err) {
                alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                alert("responseText: " + xhr.responseText);
            });

            return false;
        });
    };

    self.bindPagerButtons = function () {
        self.pagerController.bindPagerButtons("userCoursesPager", onPageChanged);
    };

    self.bindSearchControls = function () {
        $(document).on("click", self.searchConrolsMainContainerSelector + " [data-user-courses-search-btn]", function () {
            var searchTerm = $(self.searchConrolsMainContainerSelector + " [data-user-courses-search-text]").val();
            $(self.searchConrolsMainContainerSelector + " [data-user-courses-search-hidden]").val(searchTerm);
            onPageChanged(0);
            return false;
        });
    };

    self.bindCancelButton = function (onSuccessCallback, onFailureCallback) {
        $(document).on("click", "[data-cancel-course]", function () {
            var btn = $(this);
            var courseId = btn.attr("data-cancel-course");

            bootbox.confirm({
                title: 'You are about to cancel this course!',
                message: "You are about to cancel this course. Once this is done it can not be undone. Are you sure you want to cancel this course?",
                className: '',
                size: 'large',
                buttons: {
                    confirm: {
                        label: 'Yes',
                        className: 'btn btn-danger'
                    },
                    cancel: {
                        label: 'No',
                        className: 'btn btn-primary'
                    }
                },
                callback: function (result) {
                    if (result) {
                        self.coursesService.cancelCourse(courseId, function (response) {
                            if (response) {
                                onSuccessCallback();
                            }
                            else {
                                onFailureCallback();
                            }
                        }, function (xhr, err) {
                            onFailureCallback(xhr, err);
                        });
                    }
                }
            });

            return false;
        });
    };

    self.bindDatePickerControls = function () {
        $(".date-picker").datepicker({
            dateFormat: "yy-mm-dd",
            showAnim: "drop"
        });
    }

    var onPageChanged = function (pageIndex) {
        var searchTerm = $("[data-user-courses-search-hidden]").val();

        if (self.bindingMode == 1) {
            self.coursesService.getUserCoursesByPageIndex(searchTerm, -1, pageIndex, getUserCoursesByPageIndexSuccessCacllback, getUserCoursesByPageIndexFailureCacllback);
        }
        else if (self.bindingMode == 2) {
            self.coursesService.getUserAttendingCoursesByPageIndex(searchTerm, -1, pageIndex, getUserCoursesByPageIndexSuccessCacllback, getUserCoursesByPageIndexFailureCacllback);
        }
        else if (self.bindingMode == 3) {
            self.coursesService.getUserInstructingCoursesByPageIndex(searchTerm, -1, pageIndex, getUserCoursesByPageIndexSuccessCacllback, getUserCoursesByPageIndexFailureCacllback);
        }
    };

    var getUserCoursesByPageIndexSuccessCacllback = function (response) {
        $('.user-courses-main-container').children().not("#userCoursesPager").addClass('animated bounceOutLeft');

        window.setTimeout(function () {
            $(".user-courses-main-container").html(response);
            $('.user-courses-main-container').children().not("#userCoursesPager").addClass('animated bounceInRight');
            window.scrollTo(0, 0);
        }, 500);
    };

    var getUserCoursesByPageIndexFailureCacllback = function (xhr, err) {
        alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
        alert("responseText: " + xhr.responseText);
    };

    var toggleAttendanceActionButtonState = function (btn) {
        var btn = $(btn);

        if (btn.attr("data-course-attend") != null) {
            btn.find("span.glyphicon").removeClass("glyphicon-remove-circle").addClass("glyphicon-ok-circle")
            btn.find("span.icon-text").html(" I'm attending");
            btn.removeAttr("data-course-attend").attr("data-course-unattend", "");
            btn.attr("title", "Click to unattend");
            btn.removeClass("btn-info").addClass("btn-success");
        }
        else if (btn.attr("data-course-unattend") != null) {
            btn.find("span.glyphicon").removeClass("glyphicon-ok-circle").addClass("glyphicon-remove-circle")
            btn.find("span.icon-text").html(" I'm not attending");
            btn.removeAttr("data-course-unattend").attr("data-course-attend", "");
            btn.attr("title", "Click to attend");
            btn.removeClass("btn-success").addClass("btn-info");
        }
    };
};