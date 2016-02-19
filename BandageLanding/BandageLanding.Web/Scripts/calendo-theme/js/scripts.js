//jQuery code
jQuery(function($) {
	"use strict";

$(window).load(function(){
	$(".slider").bxSlider({
		pager: false,
		speed: 1000,
		infiniteLoop: false,
		hideControlOnEnd: true
	});
	$(".slider").css("display","block");

    $(".event-slider").bxSlider({
        pager: false
    });
});
	

	$(".album-slider").bxSlider({
		minSlides: 1,
		maxSlides: 10,
		slideWidth: 257,
		slideMargin: 17,
		ticker: true,
		tickerHover: true,
		speed: 50000,
		useCSS: false, 
		infiniteLoop: false
	});

	$(".popular-albums").bxSlider({
		pager: false,
	    slideWidth: 81,
	    minSlides: 1,
	    maxSlides: 4,
	    moveSlides: 1,
	    slideMargin: 10
	 });

	$('.posters-slider').bxSlider({
	    slideWidth: 257,
	    maxSlides: 4,
	    moveSlides: 1,
	    slideMargin: 16,
	    pager: false
	});

	$('.video-nav__icon_popup').magnificPopup({
		disableOn: 700,
		type: 'iframe',
		mainClass: 'mfp-fade',
		removalDelay: 160,
		preloader: false,

		fixedContentPos: false
	});

	$('.gallery-popup').magnificPopup({
		delegate: 'a',
		type: 'image',
		tLoading: 'Loading image #%curr%...',
		mainClass: 'mfp-img-mobile',
		gallery: {
			enabled: true,
			navigateByImgClick: true,
			preload: [0,1] // Will preload 0 - before current, and 1 after the current image
		},
		image: {
			tError: '<a href="%url%">The image #%curr%</a> could not be loaded.',
			titleSrc: function(item) {
				return item.el.attr('title');
			}
		}
	});

   $("<a href='#' class='btn-nav-toggle'>Drop Menu</idv>").insertAfter(".nav");

	$(".btn-nav-toggle").on('click', function(e) {
		e.preventDefault();
		$(".nav").slideToggle();
	});
	/* End toggle nav */

	$("body").fitVids();
	/* End responsive video */
});		
