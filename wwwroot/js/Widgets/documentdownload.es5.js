'use strict';

(function (component) {

    function init() {
        $('.contentDownload').each(function () {
            $(this).find('.dropdown-menu a').each(function () {
                var self = $(this).closest('[data-document-row]');
                var link = $(this);
                $(this).on('click', function (e) {
                    e.preventDefault();
                    e.stopPropagation();
                    var culture = link.attr('data-culture');
                    self.find('[data-file-date] span:not(.lineOrizontal)').each(function () {
                        if ($(this).attr('data-culture') != culture) $(this).hide();else $(this).show();
                    });
                    self.find('[data-file-description] span').each(function () {
                        if ($(this).attr('data-culture') != culture) $(this).hide();else $(this).show();
                    });
                    self.find('[data-file-description] span').each(function () {
                        if ($(this).attr('data-culture') != culture) $(this).hide();else $(this).show();
                    });
                    self.find('.download-actions ul li > a > span').each(function () {
                        if ($(this).attr('data-culture') != culture) $(this).hide();else $(this).show();
                    });
                    self.find('.download-actions ul .dropdown-menu a').each(function () {
                        if ($(this).attr('data-culture') == culture) $(this).hide();else $(this).show();
                    });
                    self.find('.download-actions ul .dropdown-menu').removeClass('show');
                });
            });
        });
    }

    init();

    $('[data-type="readmore-docs"]').on('click', function (e) {
        e.preventDefault();
        var self = $(this);
        sendRemote(self);
    });

    $('[data-filter][data-filter-for="docs"]').on('click', function (e) {
        e.preventDefault();
        var self = $(this).closest('.document-list').find('[data-type="readmore-docs"]');
        self.attr('data-current-page', 0);
        if ($(this).attr('data-filter') == 'year') {
            self.attr('data-year', $(this).attr('data-year'));
        } else {
            self.attr('data-category', $(this).attr('data-category'));
        }
        $(this).closest('.dropdown').find('.dropdown-toggle').html($(this).attr('data-label'));
        sendRemote(self);
    });

    function sendRemote(self) {
        self.addClass('d-none');
        self.closest('.document-list').find('[data-type="loader-docs"]').removeClass('d-none');
        var page = parseInt(self.attr('data-current-page'), 10);
        var pageSize = parseInt(self.attr('data-page-size'), 10);
        var year = parseInt(self.attr('data-year'), 10);
        var category = parseInt(self.attr('data-category'), 10);
        var folder = self.attr('data-folder');
        $.get('/documents/readmore?page=' + page + '&pageSize=' + pageSize + '&year=' + year + '&category=' + category + '&folder=' + folder, function (data) {
            var count = (data.match(/row/g) || []).length;
            if (page > 0) self.closest('.document-list').find('[data-type="docscontainer"]').append(data);else self.closest('.document-list').find('[data-type="docscontainer"]').html(data);
            self.attr('data-current-page', page + 1);
            if (count >= pageSize) {
                self.removeClass('d-none');
                self.closest('.document-list').find('[data-readmore-container="docs"]').removeClass('d-none');
            } else self.closest('.document-list').find('[data-readmore-container="docs"]').addClass('d-none');
            self.closest('.document-list').find('[data-type="loader-docs"]').addClass('d-none');
            init();
        });
    }
})(window.$eloDocumentDownload = window.$eloDocumentDownload || {});

