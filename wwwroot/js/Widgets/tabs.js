(function (component) {
    $(document).ready(function () {
        $('.tab-container').each(function () {
            var self = $(this);
            var length = self.find('.nav-item button').length;
            var container = null;
            self.siblings().each(function (index) {
                if (index == 0)
                    container = $(this);
                else if (index > 0 && index <= length) {
                    $(this).detach();
                    container.append($(this));
                    $(this).wrap('<div class="tab-pane fade'+(index == 1 ? ' show active' : '')+'" id="' + self.attr('data-id') + '-' + (index - 1) + '" role="tabpanel" aria-labelledby="tab-' + self.attr('data-id') + '-' + (index - 1) + '"></div>');
                }
            });
        });

        if (window.$eloGlobal.isMobile() || window.$eloGlobal.isTablet()) {
            $('.tab-slick').slick({
                dots: false,
                arrows: true,
                centerPadding: 0,
                mobileFirst: true,
                infinite: false,
                responsive: [{
                    breakpoint: 0,
                    settings: {
                        slidesToShow: 2,
                        slidesToScroll: 1
                    }
                }, {
                    breakpoint: 575,
                    settings: {
                        slidesToShow: 2,
                        slidesToScroll: 1
                    }

                }, {
                    breakpoint: 767,
                    settings: {
                        slidesToShow: 3,
                        slidesToScroll: 1
                    }
                }, {
                    breakpoint: 1199,
                    settings: {
                        slidesToShow: 3,
                        slidesToScroll: 1
                    }
                }]
            });
        }
    });

}(window.$eloTabs = window.$eloTabs || {}));