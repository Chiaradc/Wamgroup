(function (component) {

    var ms = new Array();

    $(window).resize(function () {
        setIntroContainer();
    });

    var index = 0;
    $('.stores-container .stores').each(function () {
        $(this).attr('data-ms-id', index);
        var $stores = $(this).masonry();
        ms[index] = $stores;
        $stores.on('layoutComplete', function () {
            var h = $(this).height();
            $(this).parent().css("height", h);
            console.log('layout '+h)
        });
        if (!$(this).hasClass('active')) {
            $(this).hide();
        }
        index++;
    });

    setIntroContainer();

    $('.stores-filter button').each(function () {
        $(this).on('click', function (e) {
            e.preventDefault();
            e.stopPropagation();
            var storeType = $(this).attr('data-store-type');
            $('.stores-filter button').removeClass('active');
            $(this).addClass('active');
            $('.stores-intro.active').removeClass('active').fadeOut();
            $('.stores-intro[data-store-type="' + storeType + '"]').addClass('active').fadeIn();
            $('.stores.active').each(function () {
                $(this).removeClass('active').fadeOut();
            });
            $('.stores[data-store-type="' + storeType + '"]').each(function () {
                $(this).addClass('active').fadeIn();
                var idx = parseInt($(this).attr('data-ms-id'), 10);
                ms[idx].masonry('layout');
            });
        });
    });

    $('.stores-container .space button').each(function () {
        $(this).on('click', function (e) {
            e.preventDefault();
            e.stopPropagation();
            var space = $(this).closest('.space');
            var detail = space.attr('data-detail');
            var imageSrc = space.attr('data-image-src');
            var imageAlt = space.attr('data-image-alt');
            var videoCode = space.attr('data-video-code');
            var videoType = space.attr('data-video-type');
            var linkUrl = space.attr('data-link');

            if (linkUrl != '') {
                window.open(linkUrl);
                return;
                //$('[data-video-modal] [data-video-player-div]').html('<iframe style="width: 100%; height: 100%;" title="View 360" class="embed-responsive-item" src="' + linkUrl +'" frameborder="0" allowfullscreen="1"></iframe>');
                //$('[data-video-modal] .video-yt').removeClass('d-none');
            }
            else if (videoCode != '') {
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

    

    function setIntroContainer() {
        var height = 0;
        $('.stores-intro').each(function () {
            if ($(this).height() > height)
                height = $(this).height();
        });

        $('.stores-intro-container').height(height);

        if (!window.$eloGlobal.isMobile()) {
            $('.stores-container .space').each(function () {
                var self = $(this);
                self.find('.localita').addClass('d-none');
                self.find('.explore-text').removeClass('linkDiscoverMore').addClass('linkDiscoverMoreWhite');
                self.on('click', function () {
                    self.find('.localita').toggleClass('d-none');
                });
                self.hover(function () {
                    self.find('.localita').removeClass('d-none');
                }, function () {
                    self.find('.localita').addClass('d-none');
                })
            });
        }
        else {
            $('.stores-container .space').each(function () {
                var self = $(this);
                self.find('.localita').removeClass('d-none');
                self.find('.explore-text').addClass('linkDiscoverMore').removeClass('linkDiscoverMoreWhite');
                self.off('click');
                self.off('mouseenter');
                self.off('mouseleave');
            });
        }
    }

}(window.$eloStores = window.$eloStores || {}));