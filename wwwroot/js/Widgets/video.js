(function (component) {

    $(() => {
        const videoIOOptions = {
            threshold: 0
        }
        const videoIO = new IntersectionObserver((entries) => {
            entries.forEach((entry) => {
                if (entry.intersectionRatio > 0) {
                    var elem = $(entry.target);
                    if (elem.attr('data-loaded') == 'true') return;
                    if (elem.parent().find('[data-video-code]').length > 0) {
                        console.log('loading video..')
                        var videoCode = elem.parent().find('[data-video-code]').attr('data-video-code');
                        var videoType = elem.parent().find('[data-video-code]').attr('data-video-type');
                        elem.parent().find('[data-video-container]').html('<iframe title="Video" class="embed-responsive-item" src="' + (videoType == '1' ? 'https://www.youtube-nocookie.com/embed/' + videoCode + '?autoplay=1&mute=1&controls=0&showinfo=0&autohide=1&modestbranding=1&mute=1&enablejsapi=1&widgetid=1' : 'https://player.vimeo.com/video/' + videoCode + '?autoplay=1&loop=1&title=0&byline=0&portrait=0&muted=1&controls=0&sidedock=0') + '" frameborder="0" allowfullscreen="1" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture"></iframe>');
                        elem.parent().find('[data-video-code]').on('click', function () {
                            $('[data-video-modal] [data-video-player-div]').html('<iframe style="width: 100%; height: 100%;" title="Video" class="embed-responsive-item" src="' + (videoType == '1' ? 'https://www.youtube-nocookie.com/embed/' + videoCode + '?autoplay=1&loop=1&controls=1&showinfo=1&autohide=1&modestbranding=1&mute=0&enablejsapi=1&widgetid=1' : 'https://player.vimeo.com/video/' + videoCode + '?autoplay=1&loop=1&title=0&byline=0&portrait=0') + '" frameborder="0" allowfullscreen="1" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture"></iframe>');
                            $('[data-video-modal] .video-yt').removeClass('d-none');
                            window.$eloGlobal.openGlobalModal();

                        });
                        console.log('..video loaded')
                    }
                    elem.attr('data-loaded', 'true');
                }
            });
        }, videoIOOptions);

        // Declares what to observe, and observes its properties.
        const videoLinkList = document.querySelectorAll(".video-sentinel");
        videoLinkList.forEach((el) => {
            videoIO.observe(el);
        });
    });

}(window.$eloVideo = window.$eloVideo || {}));