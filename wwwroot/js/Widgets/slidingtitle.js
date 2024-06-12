(function (component) {

    let direction = 'up';
    let prevYPosition = 0;
    var margin = 0; // $('header').height();

    const setScrollDirection = () => {
        if ($(window).scrollTop() > prevYPosition) {
            direction = 'down';
        } else {
            direction = 'up';
        }

        prevYPosition = $(window).scrollTop();
    }

    const topOptions = {
        threshold: 0,
        rootMargin: '-' + margin + 'px'
    }
    const topObserver = new IntersectionObserver((entries) => {
        entries.forEach((entry) => {
            setScrollDirection();
            var elem = $(entry.target);
            if (entry.intersectionRatio > 0) {
                elem.attr('data-init', 'true');
                $(entry.target).closest('.slidingTitleContainer').find('.boxeditorialeImm')
                    .addClass('position-absolute')
                    .addClass('start-0')
                    .addClass('top-0')
                    .removeClass('bottom-0')
                    .removeClass('position-fixed');
            }
            else if (entry.intersectionRatio <= 0 && elem.attr('data-init') == 'true' && direction == 'down') {
                //se esco dallo schermo e sto scendendo, devo mettere in sticky l'immagine'
                var img = $(entry.target).closest('.slidingTitleContainer').find('.boxeditorialeImm');
                //correzione per accordion
                const rect = img[0].getBoundingClientRect();
                if (
                    rect.top >= 0 &&
                    rect.left >= 0 &&
                    rect.bottom <= (window.innerHeight || document.documentElement.clientHeight) &&
                    rect.right <= (window.innerWidth || document.documentElement.clientWidth)

                ) {
                    img.removeClass('position-absolute')
                        .removeClass('start-0')
                        .removeClass('top-0')
                        .addClass('position-fixed');
                }
            }
        });
    }, topOptions);

    const bottomOptions = {
        threshold: 0
    }
    const bottomObserver = new IntersectionObserver((entries) => {
        entries.forEach((entry) => {
            setScrollDirection();
            var elem = $(entry.target);
            if (entry.intersectionRatio > 0) {
                elem.attr('data-init', 'true');
                $(entry.target).closest('.slidingTitleContainer').find('.boxeditorialeImm')
                    .addClass('position-absolute')
                    .addClass('start-0')
                    .addClass('bottom-0')
                    .removeClass('top-0')
                    .removeClass('position-fixed');
            }
            else if (entry.intersectionRatio <= 0 && elem.attr('data-init') == 'true' && direction == 'up') {
                //se esco dallo schermo e sto scendendo, devo mettere in sticky l'immagine'
                $(entry.target).closest('.slidingTitleContainer').find('.boxeditorialeImm')
                    .removeClass('position-absolute')
                    .removeClass('start-0')
                    .removeClass('top-0')
                    .addClass('position-fixed');
            }
        });
    }, bottomOptions);

    if (!window.$eloGlobal.isMobile()) {
        $('.slidingTitleContainer').each(function () {
            var top = $(this).find('.slidingTitleTop');
            topObserver.observe(top[0]);
            var bottom = $(this).find('.slidingTitleBottom');
            bottomObserver.observe(bottom[0]);
        });
    }
    else {
        $('.slidingTitleContainer .boxeditorialeImm').each(function () {
            $(this).removeClass('position-absolute').removeClass('start-0').removeClass('top-0');
            $(this).find('.bg-image').removeClass('object-fit-cover').removeClass('bg-image').addClass('h-auto');
        });
        $('.slidingTitleContainer .container').each(function () {
            $(this).addClass('position-absolute').addClass('top-50').addClass('translate-middle').addClass('start-50');
        });
    }
}(window.$eloSlidingTitle = window.$eloSlidingTitle || {}));