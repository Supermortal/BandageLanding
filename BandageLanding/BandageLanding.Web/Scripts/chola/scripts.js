// JavaScript for CHOLA
(function($) {
$(document).ready(function() {
		"use strict";
		
	
		// Fancybox - Litebox
		$(".fancybox").fancybox();
		
		
		
		// Isotope Filter Button
		$("#button").click(function () {
		$('.works-filter').slideToggle();
		});
		
		
		
		
		
		
		
		// Push Side Menu
		$(document).ready(function(){
        $('.toggle-menu').jPushMenu({closeOnClickLink: false});
        $('.dropdown-toggle').dropdown();
      	});
		
		
		// Dark Transition
		$(".side-menu-container").css("display", "none");
		$(".side-menu-container").fadeIn(1000);
	
    	
		$('.dark-transition').on('click', function(e) {
      	$('.dark-overlay').toggleClass("dark-overlay-show");
	    });
	
	
		$('.dark-transition').click(function (e) {
    	e.preventDefault();                  
    	var goTo = this.getAttribute("href"); 
		 

    	setTimeout(function(){
         window.location = goTo;
    	},500);       
		}); 
	
	
});


		
		
		// Masonry Portfolio
		$(function(){
   		var $container = $('.grids-masonry ');
     	$container.imagesLoaded( function(){
        $container.masonry({
           itemSelector : '.grids-masonry  li'
         });
       	});
     	});
		
		
		// Youtube Video
		$(function(){
		var options = { videoId: 'e4Is32W-ppk', start: 3 };
		$('#wrapper').tubular(options);
		});
		
		
		// Isotope works filter
		$(window).load(function(){
			var $container = $('.grids-masonry, .grids-spacing');
		$container.isotope({
			filter: '*',
			animationOptions: {
			duration: 750,
			easing: 'linear',
			queue: false
		   }
		});
	 
		$('.works-filter a').click(function(){
		$('.works-filter .current').removeClass('current');
		$(this).addClass('current');
	 
		var selector = $(this).attr('data-filter');
		$container.isotope({
			filter: selector,
			animationOptions: {
			duration: 750,
			easing: 'linear',
			queue: false
			}
		});
			return false;
		}); 
		});
		
})(jQuery);