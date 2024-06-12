(function () {
    $(document).ready(function () {
        if ($('.slick-slide').length > 0) {  
            processChildList();
            $('.slick-prev, .slick-next').on('click', function () {
                setTimeout(function () { processChildList(); }, 500);
            });
        }
    });

    function processChildList() {
        console.log('Remove tabindex');
        $('.slick-slide').removeAttr('tabindex');
    }
}());