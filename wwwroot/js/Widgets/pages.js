(function (component) {

    $('.MenuSectionSlide').slick({
        dots: false,
        arrows: true,
        centerPadding: 0,
        mobileFirst: true,
        infinite: false,
        variableWidth: true,
        outerEdgeLimit: true,
        //responsive: [{
        //    breakpoint: 0,
        //    settings: {
        //        //variableWidth: false,
        //        slidesToShow: 2,
        //        slidesToScroll: 1
        //    }
        //}, {
        //    breakpoint: 575,
        //    settings: {
        //        slidesToShow: 2,
        //        slidesToScroll: 1
        //    }

        //}, {
        //    breakpoint: 767,
        //    settings: {
        //        slidesToShow: 2,
        //        slidesToScroll: 1
        //    }
        //}, {
        //    breakpoint: 1199,
        //    settings: {
        //        slidesToShow: 3,
        //        slidesToScroll: 1
        //    }
        //}]
    });

   $('.MenuSectionSlideBio').slick({
      dots: false,
      arrows: true,
      centerPadding: 0,
      mobileFirst: true,
      infinite: false,
      variableWidth: false,
      responsive: [{
         breakpoint: 0,
         settings: {
            //variableWidth: false,
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
         breakpoint: 1199,
         settings: {
            slidesToShow: 2,
            slidesToScroll: 1
         }
      }]
   });



    $('.OurValuesSlide').slick({
        dots: false,
        arrows: true,
        //centerPadding: 0,
        mobileFirst: true,
        infinite: true,
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
    });

}(window.$eloPages = window.$eloPages || {}));