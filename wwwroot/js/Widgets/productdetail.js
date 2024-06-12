/* eslint-disable */
(function (component) {
    console.log('prova-product');

   // $(".toggle-filter-mobile").click(function () {
   //     $(".product-finder__filters").addClass("active");
   //     $("html:not(.overflow)").addClass("overflow");
   // });

   // $(".product-finder__back-btn").click(function () {
   //     $(".product-finder__filters").removeClass("active");
   //     $("html.overflow").removeClass("overflow");
   // });

   // $(".navbarMenu-toggler:not(.open)").click(function () {
   // if ($(".product-finder__filters").hasClass("active")) {        
   //     $(".product-finder__filters").removeClass("active");
   //     $("html").addClass("overflow");
   // }
   // });
   $(".download-zip").click(function () {
      
      downloadZip(1);
   });

   function downloadZip() {
      var documents = "";
      $(".check-document").each(function () {
         if ($(this).is(":checked")) {
            documents += (documents === "" ? "" : "|") + this.getAttribute("value");
         }

      });

      location.href="/product/downloadDocument?documents=" + documents;
   }

   
}(window.$eloCatalog = window.$eloCatalog || {}));