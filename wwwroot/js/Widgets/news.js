/* eslint-disable */
(function (component) {

    var slidesCount = $(".MediaSlide .swiper-slide");
    console.log(slidesCount.length);
    if (slidesCount.length <= 2) { $(".MediaSlide").addClass("no-row") };
    var rowCount = slidesCount.length > 2 ? 2 : 1;
    var slidesView = slidesCount.length > 2 ? 1.6 : 2;
    var swiper = new Swiper(".MediaSlide", {
        navigation: {
            nextEl: ".swiper-button-next",
            prevEl: ".swiper-button-prev",
        },
        breakpoints: {
            0: {
                slidesPerView: 1,
                spaceBetween: 0
            },
            768: {
                slidesPerView: 2,
                spaceBetween: 25
            },
            1199: {
                slidesPerView: slidesView,
                spaceBetween: 25,
                grid: {
                    rows: rowCount
                },
            },
        },

    });

    $('.grid-news').masonry({
        itemSelector: '.grid-news-item', // use a separate class for itemSelector, other than .col-
        columnWidth: '.grid-news-sizer',
        percentPosition: true
    });
   /* const swiperr = document.querySelector(".MediaSlide").swiper;

    swiperr.on("afterInit", function () {
        swiperr.updateSlides();
        swiperr.updateSize();
    });*/
   


   /* document.addEventListener("DOMContentLoaded", function () {
      

    });*/
   /* $('.StayTunedSlide').slick({
        dots: false,
        arrows: true,
        centerPadding: 0,
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
            breakpoint: 1199,
            settings: {
                slidesToShow: 3,
                slidesToScroll: 1
            }
        }]
    });*/

    $('[data-type="readmore-news"]').on('click', function (e) {
        e.preventDefault();
        var self = $(this);
        sendRemote(self);
    });

    $('[data-filter][data-filter-for="news"]').on('click', function (e) {
        e.preventDefault();
        var self = $('[data-type="readmore-news"]');
        self.attr('data-current-page', 0);
        if ($(this).attr('data-filter') == 'year') {
            self.attr('data-year', $(this).attr('data-year'));            
        }
        else {
            self.attr('data-category', $(this).attr('data-category'));
        }
        $(this).closest('.dropdown').find('.dropdown-toggle').html($(this).attr('data-label'));
        sendRemote(self);
    });

    function sendRemote(self) {
        self.addClass('d-none');
        $('[data-type="loader-news"]').removeClass('d-none');
        var page = parseInt(self.attr('data-current-page'), 10);
        var pageSize = parseInt(self.attr('data-page-size'), 10);
        var year = parseInt(self.attr('data-year'), 10);
        var category = parseInt(self.attr('data-category'), 10);
        $.get('/news/readmore?page=' + page + '&pageSize=' + pageSize + '&year=' + year + '&category=' + category, function (data) {
            var count = (data.match(/itemST/g) || []).length;
            if (page > 0)
                $('[data-type="newscontainer"]').append(data);
            else
                $('[data-type="newscontainer"]').html(data);
            self.attr('data-current-page', page + 1);
            if (count >= pageSize) {
                self.removeClass('d-none');
                $('[data-readmore-container="news"]').removeClass('d-none');
            }
            else
                $('[data-readmore-container="news"]').addClass('d-none');
            $('[data-type="loader-news"]').addClass('d-none');
        });
   }

   var locked = false;

   function sendRemoteReadMore() {

      console.log("send remote");
      var self = $('.grid-news');
      var skip = parseInt(self.attr('data-skip'), 0);
      var take = parseInt(self.attr('data-take'), 0);
      var urldettaglio = self.attr('data-urldettaglio');
      $.get('/news/readmore?take=' + take + '&skip=' + skip + '&urldettaglio=' + urldettaglio, function (data) {
         var count = (data.match(/item-news/g) || []).length;

         console.log("send remote ok");
         //console.log(data);
         $('.grid-news').append(data);
         skip += count;
         self.attr('data-skip', skip);
         locked = false;
         
      });
   }

   //window.onscroll = function () {

   //   // @var int totalPageHeight
   //   var totalPageHeight = document.body.scrollHeight;

   //   // @var int scrollPoint
   //   var scrollPoint = window.scrollY + window.innerHeight;

   //   // check if we hit the bottom of the page
   //   if (scrollPoint >= totalPageHeight) {
   //      if (!locked) {
   //         console.log("at the bottom");
   //         locked = true;
   //         setTimeout(sendRemoteReadMore, 1000);
   //      }
   //   }
   //}

}(window.$eloNews = window.$eloNews || {}));