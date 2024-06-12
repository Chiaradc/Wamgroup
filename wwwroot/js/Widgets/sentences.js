(function (component) {
    $(document).ready(function () {
        $('.flip-box-front').each(function () {
            var self = $(this);
            self.on('click', function (e) {
                e.stopPropagation();
                e.preventDefault();
                self.closest('.flip-box').addClass('open');
            });

        });
        $('.flip-box-back').each(function () {
            var self = $(this);
            self.on('click', function (e) {
                e.stopPropagation();
                e.preventDefault();
                self.closest('.flip-box').removeClass('open');
            });
        });
    });

}(window.$eloSentences = window.$eloSentences || {}));