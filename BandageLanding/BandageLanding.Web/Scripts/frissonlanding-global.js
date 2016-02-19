if (!Date.prototype.toISOString) {
    (function () {
        function pad(number) {
            var r = String(number);
            if (r.length === 1) {
                r = '0' + r;
            }
            return r;
        }
        Date.prototype.toISOString = function () {
            return this.getUTCFullYear()
                + '-' + pad(this.getUTCMonth() + 1)
                + '-' + pad(this.getUTCDate())
                + 'T' + pad(this.getUTCHours())
                + ':' + pad(this.getUTCMinutes())
                + ':' + pad(this.getUTCSeconds())
                + '.' + String((this.getUTCMilliseconds() / 1000).toFixed(3)).slice(2, 5)
                + 'Z';
        };
    }());
}

if (typeof String.prototype.startsWith != 'function') {
    String.prototype.startsWith = function (str) {
        return this.slice(0, str.length) == str;
    };
}

if (typeof String.prototype.endsWith != 'function') {
    String.prototype.endsWith = function (str) {
        return this.slice(-str.length) == str;
    };
}

$(function() {
    $(".datepicker").datepicker({
        changeYear: true,
        changeMonth: true
    });
});

if (!window.FrissonLanding) {
    window.FrissonLanding = {};
}

if (!window.FrissonLanding.Enums) {
    window.FrissonLanding.Enums = {};

    FrissonLanding.Enums.AJAXMethods = {
        Get: { id: 0, value: "GET" },
        Post: { id: 1, value: "POST" },
        Put: { id: 2, value: "PUT" },
        Delete: { id: 3, value: "DELETE" }
    };
}

if (!window.FrissonLanding.getData) {
    window.FrissonLanding.getData = function (url, method, data) {

        if (method.id === FrissonLanding.Enums.AJAXMethods.Post.id) {
            data = JSON.stringify(data);
        }

        return $.ajax({
            url: url,
            method: method.value,
            dataType: "json",
            contentType: "application/json",
            context: "body",
            data: data
        });

    };
}

if (!window.FrissonLanding.getBaseUrl) {
    FrissonLanding.getBaseUrl = function() {
        return window.location.protocol + "//" + window.location.host + "/" + window.location.pathname.split("/")[1];
    };
}

if (!window.FrissonLanding.addModal) {
    FrissonLanding.addModal = function (selectorString) {
        var self = FrissonLanding;

        if (!self.__modalOverlay) {
            var container = $(selectorString);
            var width = $(container).width();
            var height = $(container).height();
            var offset = $(container).offset();

            var modalOverlay = $("<div class='modal'></div>");
            var loadingImageDiv = $("<div class='full-opacity' style='margin: auto; text-align: center;'><img class='full-opacity' alt='Loading...' src='/Content/images/ajax-loader.gif'/></div>");
            $(loadingImageDiv).css("margin-top", (height / 2) - 8);
            $(modalOverlay).append(loadingImageDiv);
            $(modalOverlay).width(width);
            $(modalOverlay).height(height);
            $(modalOverlay).css("top", offset.top + "px");
            $(modalOverlay).css("left", offset.left + "px");
            $('body').append(modalOverlay);

            self.__modalOverlay = modalOverlay;
        }
    };
}

if (!window.FrissonLanding.removeModal) {
    FrissonLanding.removeModal = function() {
        var self = FrissonLanding;

        if (self.__modalOverlay) {
            $(self.__modalOverlay).remove();
            delete self.__modalOverlay;
        }
    };
}

