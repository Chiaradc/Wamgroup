/* eslint-disable */
(function (component) {

    var gs = $(".general-slider");

    $(gs).each(function () {
        var gsThis = $(this);
        var gsSlide = gsThis.find(".swiper-slide");
        if (gsSlide.length > 1) {
            var swiper = new Swiper(gsThis[0], {
                navigation: {
                    nextEl: ".swiper-button-next",
                    prevEl: ".swiper-button-prev",
                },
                pagination: {
                    el: ".swiper-pagination",
                },
            });
        }
    })


    /*$('.general-slider.horizontal').slick({
        mobileFirst: true,
        centerMode: true,
        slidesToShow: 1,
        dots: false,
        arrows: true,
        infinite: false,
        responsive: [{
            breakpoint: 0,
            settings: {
                centerPadding: '10%',
                slidesToShow: 1,
                slidesToScroll: 1
            }
        }, {
            breakpoint: 575,
            settings: {
                centerPadding: '8%',
                slidesToShow: 1,
                slidesToScroll: 1
            }

        }, {
            breakpoint: 767,
            settings: {
                centerPadding: '7%',
                slidesToShow: 1,
                slidesToScroll: 1
            }
        }, {
            breakpoint: 1199,
            settings: {
                centerPadding: '5%',
                slidesToShow: 1,
                slidesToScroll: 1
            }
        }, {
            breakpoint: 1399,
            settings: {
                centerPadding: '5%',
                slidesToShow: 1,
                slidesToScroll: 1
            }
        }, {
            breakpoint: 1919,
            settings: {
                centerPadding: '10%',
                slidesToShow: 1,
                slidesToScroll: 1
            }
        }]
    });*/


    $('.general-slider .general-slider-detail').each(function () {
        $(this).on('click', function (e) {
            e.preventDefault();
            e.stopPropagation();
            var self = $(this);
            var detail = self.attr('data-detail');
            var imageSrc = self.attr('data-image-src');
            var imageAlt = self.attr('data-image-alt');
            var videoCode = self.attr('data-video-code');
            var videoType = self.attr('data-video-type');

            if (videoCode != '') {
                $('[data-video-modal] [data-video-player-div]').html('<iframe style="width: 100%; height: 100%;" title="Video" class="embed-responsive-item" src="' + (videoType == '1' ? 'https://www.youtube-nocookie.com/embed/' + videoCode + '?autoplay=1&loop=1&controls=1&showinfo=1&autohide=1&modestbranding=1&mute=0&enablejsapi=1&widgetid=1' : 'https://player.vimeo.com/video/' + videoCode + '?autoplay=1&loop=1&title=0&byline=0&portrait=0') + '" frameborder="0" allowfullscreen="1" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture"></iframe>');
                $('[data-video-modal] .video-yt').removeClass('d-none');
            }
            else {
                $('[data-video-modal] .img-container').html('<img class="w-100" src="' + imageSrc + '" alt="' + imageAlt + '" />');
                $('[data-video-modal] .img-container').removeClass('d-none');
            }
            $('[data-video-modal] .caption-container .TextIntro').html(detail);
            $('[data-video-modal] .caption-container').removeClass('d-none');
            window.$eloGlobal.openGlobalModal();
        });
    });

}(window.$eloGenerealSlider = window.$eloGenerealSlider || {}));