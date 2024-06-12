/* eslint-disable */
(function (component) {
    $(".toggle-filter-mobile").on("click", function () {
        $(".product-finder__filters").addClass("active");
        $("html:not(.overflow)").addClass("overflow");
    });

    $(".product-finder__back-btn").on("click", function () {
        $(".product-finder__filters").removeClass("active");
        $("html.overflow").removeClass("overflow");
    });

    $(".navbarMenu-toggler:not(.open)").on("click", function () {
    if ($(".product-finder__filters").hasClass("active")) {        
        $(".product-finder__filters").removeClass("active");
        $("html").addClass("overflow");
    }
    });
    
    $(".form-checkbox-input").on("click", function () {   
       sendRemote(1);
       handlerFilter(this);
    });

    var placeholderContainer = $(".placeholder_intro_products").clone();

    function generateUI(count) {
        if (count === 0) {
            $(".navigation-page").removeClass("show")
            $(".orderBy-selector-container").removeClass("show")
            $(placeholderContainer).find("p").text("No matches.");
            $(placeholderContainer).appendTo($(".product-finder_products"));
        } else {
            $(".navigation-page").addClass("show");
            $(".orderBy-selector-container").addClass("show");
            $(".placeholder_intro_products") && $(".placeholder_intro_products").remove();
        }
    }

    $(".product-filters__reset-btn").on("click", function () {
        $(".form-checkbox-input:checked").each(function () {
           $(this).trigger("click");
        })
    })

    function handleRemoveFilter(el) {
        el.on("click", function () {
            var label = $(this).find("span.filter-applied__label").text();
            $(".form-checkbox-input").each(function () {
                $(this).siblings("label").text() === label && $(this).trigger("click");
            })
        });
    }
 

    function handlerFilter(el) {
        $(el).toggleClass('active');
        var counter = $(".form-checkbox-input.active").length;
        var filterApplied = $("button.filter-applied");
        var inputSelected = $(el).siblings("label").text();
        var filterContainer = $(".product-finder__filter-applied-container");
        var counterItems = $(".product-finder__head-wrap");
        /*if this element is active, copy text and paste in filter*/
        if ($(el).hasClass('active')) {
            console.log(inputSelected, 'active');
            !$(filterContainer).hasClass("show") && $(filterContainer).addClass("show");
            !$(counterItems).hasClass("show") && $(counterItems).addClass("show");
            if (counter === 1) {
                $(filterApplied).find("span.filter-applied__label").text(inputSelected);
                handleRemoveFilter($(filterApplied));
            } else {
                var clonedFilter = $(filterApplied[0]).clone().appendTo(filterContainer);
                $(clonedFilter).find("span.filter-applied__label").text(inputSelected);
                handleRemoveFilter($(clonedFilter));
            }


        } else {
            /*if this element is not active, remove filter */
            console.log(inputSelected, 'not active');
            if (counter > 0) {
                $("span.filter-applied__label").each(function () {
                    if ($(this).text() === inputSelected) {
                        $(this).parent().remove();
                    }
                });
            } else {
                /*if this element is not active and filter is last one, hide instead of removing it*/
                $(filterApplied).find("span.filter-applied__label").text("");
                $(filterContainer).removeClass("show");
                $(counterItems).removeClass("show");
            }

        }
    }

   function sendRemote(currentPage) {
      var pageSize = 5;

      var solutionbyindustry = "";
      $(".checkbox-sbi").each(function () {
         if ($(this).is(":checked")) {
            solutionbyindustry += (solutionbyindustry === "" ? "" : "|") + this.getAttribute("value");
         }
         
      });

      var solutionbytecnology = "";
      $(".checkbox-sbt").each(function () {
         if ($(this).is(":checked")) {
            solutionbytecnology += (solutionbytecnology === "" ? "" : "|") + this.getAttribute("value");
         }

      });

      var brand = "";
      $(".checkbox-b").each(function () {
         if ($(this).is(":checked")) {
            brand += (brand === "" ? "" : "|") + this.getAttribute("value");
         }

      });

      var family = "";
      $(".checkbox-fam").each(function () {
         if ($(this).is(":checked")) {
            family += (family === "" ? "" : "|") + this.getAttribute("value");
         }

      });

      var detailUrl = $('.detail-url').html();
      
      $.get('/Product/ProductCatalog?solutionbyindustry=' + solutionbyindustry + '&solutionbytecnology=' + solutionbytecnology + '&brand=' + brand + '&family=' + family + '&page=' + currentPage + '&pagesize=' + pageSize + '&detailurl=' + detailUrl, function (data) {

         var count = (data.match(/row-product/g) || []).length;
         
         $('.product-finder_products').html(data);

          $('.numberItemsFound').html($('.total-count-result').html());
         $('.total-count-result').remove();

         $('.navigation-page').html($('.navigation-result').html());
         $('.navigation-result').remove();

         $(".page-link").click(function () {            
            sendRemote($(this).attr('value'));
         });

          generateUI(count);
      });

}

}(window.$eloCatalog = window.$eloCatalog || {}));