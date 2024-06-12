(function (component) {

    $('.contentGallery').slick({
        dots: false,
        arrows: true,
        centerPadding: 0,
        responsive: [{
            breakpoint: 0,
            settings: {
                slidesToShow: 1,
                slidesToScroll: 1
            }
        }, {
            breakpoint: 1199,
            settings: {
                slidesToShow: 1,
                slidesToScroll: 1
            }
        }]
    })

}(window.$eloGallery = window.$eloGallery || {}));