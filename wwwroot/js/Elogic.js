// JavaScript Document
/* eslint-disable */
(function (component) {

    $('.navbarMenu-toggler').on("click", function (e) {
        $(this).toggleClass('open');
        $("html").toggleClass("overflow");
        $('header.fixed-top').toggleClass('open');
        if ($('#menuDesktop').hasClass('filter')) {
            $('#menuDesktop').toggleClass('filter');
        }
        else if ($('#menuDesktop').hasClass('global')) {
            $('#menuDesktop').toggleClass('global');
        }
        else if ($('#menuDesktop').hasClass('search')) {
            $('#menuDesktop').toggleClass('search');
        }
        else {
            $('#menuDesktop').toggleClass('open');
        }      
           
        e.stopPropagation();
        e.preventDefault();
    });
    $('header .dropdown-s').on("click", function (e) {
        $(this).toggleClass('open');
        e.stopPropagation();
        e.preventDefault();
    });
    $('.filterAreas').on("click", function (e) {
        $(".navbarMenu-toggler").toggleClass('open');
        $('header.fixed-top').toggleClass('open');
        $('#menuDesktop').toggleClass('filter');
        e.stopPropagation();
        e.preventDefault();
    });

    $('.btnMenuGlobal').on("click", function (e) {
        $(".navbarMenu-toggler").toggleClass('open');
        $('header.fixed-top').toggleClass('open');
        $('#menuDesktop').toggleClass('global');
        e.stopPropagation();
        e.preventDefault();
    });

    $('.btnMenuSearch').on("click", function (e) {
        $(".navbarMenu-toggler").toggleClass('open');
        $('header.fixed-top').toggleClass('open');
        $('#menuDesktop').toggleClass('search');
        e.stopPropagation();
        e.preventDefault();
    });

   /* $('.navbar-nav button').on("click",function (e) {
        $(this).toggleClass('active');
    })*/

    component.isMobile = function () {
        return $('.check-mobile').is(":visible");
    }

    component.isTablet = function () {
        return $('.check-tablet').is(":visible");
    }

   


    $('[data-video-modal] .close-modal').on('click', function () {
        $('[data-video-modal]').removeClass('active');
        $('[data-video-modal] .video-yt').addClass('d-none');
        $('[data-video-modal] .img-container').addClass('d-none');
        $('[data-video-modal] .caption-container').addClass('d-none');
        $('[data-video-modal] [data-video-player-div]').html('');
        $('[data-video-modal] .img-container').html('');
        $('[data-video-modal] .caption-container .TextIntro').html('');
        $('[data-video-modal]').attr('aria-hidden', true);
    });


    component.openGlobalModal = function () {
        $('[data-video-modal]').addClass('active').attr('aria-hidden', false);
        $('[data-video-modal]').focus();
    }
    
    const buttonUser = document.querySelector('#lnk-633');
    const tooltipUser = document.querySelector('#menu-633');

    $(buttonUser).on("click", function () { $(this).toggleClass('active') });
    
    const popperInstance = Popper.createPopper(buttonUser, tooltipUser, {
        placement: 'bottom',
    });

    var elementPopper = $('button.nav-link[data-bs-target] + div');
    var targetButton =[...$('button.nav-link.dropdown-toggle-s')];

    document.addEventListener('click', (event) => {
        const arraySelected = event.composedPath();
        const withinBoundaries = arraySelected.some(x => targetButton.includes(x));
        if (!withinBoundaries) {
            $(targetButton).each(function () {
                $(targetButton, this).removeClass('active');
                if ($($(this).attr('data-bs-target')).hasClass('show'))
                    $(this).trigger('click');
            })
        }   
    })
    




}(window.$eloGlobal = window.$eloGlobal || {}))


