function Magic() {
		var We = $(window).width();
			clientH = $(window).height();

		$('#future').css('height', clientH);	
		$('#currently').css('height', clientH);	
		
		if( We< 768){
			$('.imagess').css('width','35px')
			$('.temperatur').css('font-size', '12pt')
			$('.decr').css('font-size', '10pt')
		}
		else
			if(We < 992){
				$('#cur-top-title').css('font-size', '2em');
				$('.tumperatura').css('font-size', '1em');
				$('.air').css('font-size', '1em');
				$('.scrolling_future').css('height', clientH * 0.9);
				var past = $('#load').height();
				$('#past').css('height',6*past );
				
			}else{
				$('.scrolling').css('height', clientH * 0.9);

				$('#past').css('height', clientH);	
			}

		var He =  clientH/3 ;
		
		$('#cur-top').css('height', He - 25);
		
	$('#content').mCustomScrollbar( {
		scrollButtons:{
			enable:true
		},
		scrollInertia:0,
		advanced:{
			autoScrollOnFocus: true,
			updateOnContentResize: true
		}
	});
}

var timer;
	$(window).resize(function() {
	Magic();
	Centr();
});


$(document).ready(function(){
	Magic();
	Centr();
	grid();
	WeatherAll();
	Time();
	to_month();
	to_wind_direction();
	to_description();
	update_data();
});



		
		