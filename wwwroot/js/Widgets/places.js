(function (component) {

    $('.OurValuesSlide.places').slick({
        dots: false,
        arrows: true,
        //centerPadding: 0,
        mobileFirst: true,
        infinite: true,
        responsive: [{
            breakpoint: 0,
            settings: {
                arrows: false,
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

    $('.PlacesSectionSlide').slick({
        mobileFirst: true,
        centerMode: true,
        slidesToShow: 1,
        dots: false,
        arrows: true,
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
    });

}(window.$eloPlaces = window.$eloPlaces || {}));