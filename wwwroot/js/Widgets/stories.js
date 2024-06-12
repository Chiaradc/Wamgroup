(function (component) {

    $('.slick-stories').slick({
        dots: false,
        arrows: true,
        //centerPadding: 0,
        mobileFirst: true,
        infinite: false,
        responsive: [{
            breakpoint: 0,
            settings: {
                slidesToShow: 1,
                slidesToScroll: 1
            }
        }, {
            breakpoint: 575,
            settings: {
                slidesToShow: 1,
                slidesToScroll: 1
            }

        }, {
            breakpoint: 767,
            settings: {
                slidesToShow: 2,
                slidesToScroll: 1
            }
        }, {
            breakpoint: 991,
            settings: {
                slidesToShow: 3,
                slidesToScroll: 1
            }
        }]
    })

    $('[data-type="readmore-stories"]').on('click', function (e) {
        e.preventDefault();
        var self = $(this);
        sendRemote(self);
    });

    function sendRemote(self) {
        self.addClass('d-none');
        $('[data-type="loader-news"]').removeClass('d-none');
        var page = parseInt(self.attr('data-current-page'), 10);
        var pageSize = parseInt(self.attr('data-page-size'), 10);
        var doNotShowPreview = self.attr('data-do-not-show-preview');
        var orderType = parseInt(self.attr('data-order-type'), 10);
        var selectedPageGuid = self.attr('data-selected-page-guid');
        $.get('/story/readmore?page=' + page + '&pageSize=' + pageSize + '&doNotShowPreview=' + doNotShowPreview + '&orderType=' + orderType + '&selectedPageGuid=' + selectedPageGuid, function (data) {
            var count = (data.match(/itemOV/g) || []).length;
            if (page > 0)
                $('[data-type="storiescontainer"]').append(data);
            else
                $('[data-type="storiescontainer"]').html(data);
            self.attr('data-current-page', page + 1);
            if (count >= pageSize) {
                self.removeClass('d-none');
                $('[data-readmore-container="stories"]').removeClass('d-none');
            }
            else
                $('[data-readmore-container="stories"]').addClass('d-none');
            $('[data-type="loader-stories"]').addClass('d-none');
        });
    }

    $(document).ready(function () {
        const optionsStories = {
            threshold: 0.2,
        }
        const ioStories = new IntersectionObserver((entries) => {
            entries.forEach((entry) => {
                if (entry.intersectionRatio > 0) {
                    $(entry.target).find('.itemOV').addClass('slide-top-stories');
                }
            });
        }, optionsStories);

        const boxStoriesList = document.querySelectorAll(".animatedStories");
        boxStoriesList.forEach((el) => {
            ioStories.observe(el);
        });
    });

}(window.$eloStories = window.$eloStories || {}));