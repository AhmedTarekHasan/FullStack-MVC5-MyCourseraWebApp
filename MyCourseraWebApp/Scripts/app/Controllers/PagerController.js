function PagerController() {
};

PagerController.prototype.bindPagerButtons = function (mainContainerId, onPageChanged) {
    var self = this;

    $(document).on("click", "#" + mainContainerId + " [data-pager-previous]", function () {
        if (!$(this).is(".disabled") && !$(this).is(".active")) {
            var pageIndex = parseInt($(this).attr("data-pager-previous")) - 1;
            onPageChanged(pageIndex);
        }

        return false;
    });

    $(document).on("click", "#" + mainContainerId + " [data-pager-next]", function () {
        if (!$(this).is(".disabled") && !$(this).is(".active")) {
            var pageIndex = parseInt($(this).attr("data-pager-next")) - 1;
            onPageChanged(pageIndex);
        }

        return false;
    });

    $(document).on("click", "#" + mainContainerId + " [data-pager-page-anchor]", function () {
        if (!$(this).is(".disabled") && !$(this).is(".active")) {
            var pageIndex = parseInt($(this).attr("data-pager-page-anchor")) - 1;
            onPageChanged(pageIndex);
        }

        return false;
    });
};