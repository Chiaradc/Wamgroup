(function (component) {
    $(document).ready(function () {
        $('.pie-container').each(function () {
            $(this).find('[data-show-type]').each(function () {
                var link = $(this);
                link.on('click', function (e) {
                    e.stopPropagation();
                    e.preventDefault();
                    link.closest('.pie-container').find('[data-show-type]').attr('aria-pressed', false);
                    link.attr('aria-pressed', true);
                    if (link.attr('data-show-type') == 'pie') {
                        link.closest('.pie-container').find('.pie-content').removeClass('position-absolute').addClass('position-relative');
                        link.closest('.pie-container').find('.table-content').addClass('position-absolute').removeClass('position-relative');
                        link.closest('.pie-container').find('.pie-content').fadeIn().focus();
                        link.closest('.pie-container').find('.table-content').fadeOut();
                    }
                    else {
                        link.closest('.pie-container').find('.table-content').removeClass('position-absolute').addClass('position-relative');
                        link.closest('.pie-container').find('.pie-content').addClass('position-absolute').removeClass('position-relative');
                        link.closest('.pie-container').find('.table-content').fadeIn().focus();
                        link.closest('.pie-container').find('.pie-content').fadeOut();
                    }
                });
            });
        });
    });
}(window.$eloPies = window.$eloPies || {}));