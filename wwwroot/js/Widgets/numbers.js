(function (component) {
    $(document).ready(function () {
        var isMobile = window.$eloGlobal.isMobile();
        const options = {
            threshold: isMobile ? 0.5 : 0.85,
        }
        const io = new IntersectionObserver((entries) => {
            entries.forEach((entry) => {
                if (entry.intersectionRatio > 0) {
                    $(entry.target).find('.goldNum').addClass('slide-top');
                }
            });
        }, options);

        // Declares what to observe, and observes its properties.
        const boxElList = document.querySelectorAll(".sectionGoldenNumbers");
        boxElList.forEach((el) => {
            io.observe(el);
        });
    });
}(window.$eloNumbers = window.$eloNumbers || {}));