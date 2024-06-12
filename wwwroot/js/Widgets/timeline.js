/* eslint-disable */
(function (component) {
    $(document).ready(function () {

        var dataYear = $('.timeSlider .timeItemContainer');
        for (var  i = 0; i < dataYear.length; i++) {
            dataYear[i] = $(dataYear[i]).attr('data-year');
        }
        var stampYear = $(".timeSlider .yearStampActive");

        var swiper = new Swiper(".timeSlider", {
                    navigation: {
                        nextEl: ".swiper-button-next",
                        prevEl: ".swiper-button-prev",
                  },
                  pagination: {
                      el: ".timeSlider .yearStampActive",
                      type: 'custom',
                      renderCustom: function (swiper, current) {
                          if (current == dataYear.length + 1) {
                              return dataYear[current - 2]
                          } else {
                              return dataYear[current - 1];
                          }
                      }
                  },
                  scrollbar: {
                      el: '.swiper-scrollbar',
                      draggable: true,
                  },
                    breakpoints: {
                        0: {
                            slidesPerView: "auto",
                            spaceBetween: 30
                        },
                        768: {
                            slidesPerView: 1.4,
                            spaceBetween: 130   
                        },
                        1199: {
                            slidesPerView: 1.5,
                            spaceBetween: 145                           
                        },
                    },
              }); /*swiper*/

        $(stampYear).appendTo('.timeSlider .swiper-scrollbar-drag');
    });

}(window.$eloTimeline = window.$eloTimeline || {}));