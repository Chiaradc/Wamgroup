(function (component) {
    $(document).ready(function () {
        if (!window.$eloGlobal.isMobile() && !window.$eloGlobal.isTablet()) {
            $('[data-value-container]').each(function () {
                var self = $(this);
                var mTop = 0;
                if (self.find('[data-img-value]').is(':visible'))
                    mTop = self.find('[data-img-value]').height() / 2;
                else
                    mTop = self.find('[data-img-value-hover]').height() / 2;
                self.find('[data-value-text]').css('margin-top', mTop + 'px');
                //self.height(self.find('[data-img-value]').height());
            });
            $('[data-value-container] .value-link').each(function () {
                var self = $(this);
                self.on('click', function (e) {
                    e.preventDefault();
                    e.stopPropagation();
                    self.closest('.values-container').find('.value-link').attr('aria-expanded', 'false');
                    self.attr('aria-expanded', 'true');
                    self.closest('.values-container').find('[data-img-value]').removeClass('d-lg-none');
                    self.closest('.values-container').find('[data-img-value-hover]').removeClass('d-lg-block').addClass('d-none');
                    self.closest('.values-container').find('[data-value-text]').addClass('d-lg-none');
                    self.closest('[data-value-container]').find('[data-img-value]').addClass('d-lg-none');
                    self.closest('[data-value-container]').find('[data-img-value-hover]').removeClass('d-none');
                    self.closest('[data-value-container]').find('[data-value-text]').removeClass('d-lg-none');
                });
            });
        }
    });
}(window.$eloValues = window.$eloValues || {}));