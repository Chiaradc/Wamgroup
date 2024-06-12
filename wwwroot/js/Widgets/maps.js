(function (component) {
    $(document).ready(function () {
        $('.contentMaps').each(function () {
            $(this).find('[data-show-type]').each(function () {
                var link = $(this);
                link.on('click', function (e) {
                    e.stopPropagation();
                    e.preventDefault();
                    link.closest('.contentMaps').find('[data-show-type]').attr('aria-pressed', false);
                    link.attr('aria-pressed', true);
                    if (link.attr('data-show-type') == 'text') {
                        link.closest('.contentMaps').find('.map-text-container').removeClass('position-absolute').addClass('position-relative');
                        link.closest('.contentMaps').find('.map-visual-container').addClass('position-absolute').removeClass('position-relative');
                        link.closest('.contentMaps').find('.map-visual-container .CTALinkMenu').hide();
                        link.closest('.contentMaps').find('.map-visual-container .navigationMaps').hide();
                        link.closest('.contentMaps').find('.map-text-container').fadeIn().focus();
                        link.closest('.contentMaps').find('.map-visual-container').fadeOut();
                    }
                    else {
                        link.closest('.contentMaps').find('.map-visual-container').removeClass('position-absolute').addClass('position-relative');
                        link.closest('.contentMaps').find('.map-text-container').addClass('position-absolute').removeClass('position-relative');
                        link.closest('.contentMaps').find('.map-visual-container .CTALinkMenu').show();
                        link.closest('.contentMaps').find('.map-visual-container .navigationMaps').show();
                        link.closest('.contentMaps').find('.map-visual-container').fadeIn().focus();
                        link.closest('.contentMaps').find('.map-text-container').fadeOut();
                    }
                });
            });
        });
    });
}(window.$eloMaps = window.$eloMaps || {}));