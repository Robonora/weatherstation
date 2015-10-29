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
		//$('#past').css('height', clientH );
		//1$('.scrolling_past').css('height', clientH * 0.9);
}

function Centr($list){	
	//	console.log($list[0].temperature)
	    var wea = document.getElementById('weather');
		
		if (wea.getContext) {

		    var ctx = wea.getContext('2d');

		    wea.width  = $('#currently').width() ;
		    wea.height = $('#currently').height()/3 ;
		    
		    var arr = $list;
			
			var i = 0;
		    var min, max;
		    min = arr[0].temperature;
		    max = arr[0].temperature;	
			
		    for(var i in arr){
				
		    	if(arr[i].temperature < min) min = arr[i].temperature;
		    	else 
		    		if(arr[i].temperature > max) max = arr[i].temperature;
				//console.log(arr[i].temperature,arr[i].time_hour )
		    }
			
			if(max<0){
				if(max % 5 != 0 ) max = max - 5 - max % 5;
				if(min % 5 != 0 ) min = min - 5 - min % 5;
			}
			else
				if(min<0){
					if(max % 5 != 0 ) max = max + 5 - max % 5;
					if(min % 5 != 0 ) min = min - 5 - min % 5;
				}
				else{
					if(max % 5 != 0 ) max = max + 5 - max % 5;
					if(min % 5 != 0 ) min = min + 5 - min % 5;
				}
				
	    	if( min >= -10  && max < 0)min = -15;
	    	if( max <= 10 && max > 0 )max = 15;

	    	Weather(max, min, ctx, wea.height, wea.width);
			
			WeatherAll(max, min ,ctx, arr, wea.height, wea.width);
		}
		Time(ctx,wea.height, wea.width );
}

function WeatherAll(max, min ,ctx, arr, He, We){
	var now = new Date();
		hours = now.getHours();
		minutes = now.getMinutes();
	
	var gran_min = 3*He/5;
	
	if( min < 0 && max > 0) {
		var part = He/50;
		var part_top = (He/10)/(max-10);
		var part_bot = -(He/10)/(min+10);
		var gran_max = He/2;
		gran_min = 2*He/5;
	}
	else {
		var part = He/25;
		var part_top = (He/5)/(max-10);
		var part_bot = -(He/5)/(min+10);
		var gran_max = 4*He/5;		
	}
	
	ctx.strokeStyle = 'red';
	ctx.fillStyle = 'red';

	if( min >= 0 ) var y = 4*He/5;
	else 
		if( max <= 0 ) var y = He/5;
		else var y = 5*He/10 ;
	
	for(var i in arr){

		if( arr[i].time_hour != 5 && arr[i].time_hour != 4 ){
			
			if(arr[i].temperature > 0 ){
				if(arr[i].temperature <= max-10)	y1 =  gran_max  - arr[i].temperature*part_top ;
				else y1 = gran_min -(arr[i].temperature-max+10)*part;
			}
			else{
				if(arr[i].temperature >= min + 10)	y1 =  Math.abs(He - gran_max) - arr[i].temperature * part_bot ;
				else y1 =  Math.abs(He - gran_min) - (arr[i].temperature - min - 10)* part;
			}
			
			if(arr[i].time_hour >= 0 && arr[i].time_hour <= 3 ) x = (24+arr[i].time_hour)*We/23 + (We/23)/60*arr[i].time_min - 5*We/23  ;
			else x = arr[i].time_hour * We/23 + arr[i].time_min * (We/23)/60 - 5 * We / 23;
			 
			if( arr[i].temperature == 0) ctx.fillRect(x-3, y-3, 6, 6);// вывод точки если температура равна 0
			else {
				// вывод столбиков или палочек
				if( arr[i].time_hour >= 0 && arr[i].time_hour <= 3)	var time_hour = 24 + arr[i].time_hour;
				else var time_hour = arr[i].time_hour;
				
				if( hours >= 0 && hours <= 3)	var hour = 24 + hours;
				else var hour = hours;
				
				ctx.beginPath();
				
				if (time_hour > hour || ( time_hour == hour && arr[i].time_min > minutes ) ){
					ctx.lineWidth = 5;
				}

				ctx.moveTo(x, y);
				ctx.lineTo(x,y1); 				
			}				
			ctx.stroke();
		}
    }
}


 function Time(ctx, He, We){
	 
	var now = new Date();
	  hours = now.getHours();
	  minutes = now.getMinutes();
	  
	
	//var wea = document.getElementById('weather');	
	//var ctx = wea.getContext('2d');
	
	  if( hours != 4 && hours != 5 ){
		  
		  if(hours >= 0 && hours<= 3 ) x = (24 + hours)*We/23 + (We/23)/60*minutes - 5*We/23;
		  else x = hours*We/23 + (We/23)/60*minutes - 5*We/23;
		  ctx.beginPath();
		  
		  ctx.strokeStyle = 'black';
		  ctx.fillStyle = 'black';
		  ctx.textAlign = "center";
	      ctx.textBaseline = "top";
	      ctx.lineWidth = 1;
	      ctx.moveTo(x,He/5);
		  ctx.lineTo(x,4*He/5);
		  

		  if(minutes >= 0 && minutes <=9)ctx.fillText(hours + ":0"+ minutes, x, 10);
		  else  ctx.fillText(hours + ":"+ minutes, x, 0);
		  ctx.fillRect(x-6,He/10,12,He/20)
		  ctx.moveTo(x, 2*He/10);
		  ctx.lineTo(x + 5, 3*He/20);
		  ctx.lineTo(x - 5, 3*He/20);
		  ctx.lineTo(x , 2*He/10);

		  ctx.fill();
		  ctx.stroke();
	  }

}

function Weather(max, min ,ctx, He, We){

	//рисуем нулевую границу
	var y;
	if(max > 0 && min >= 0 ){ 
		y = He-He/5;
		var y1 = He/5;
		tmp = 5;
		}
	else
		if(max <= 0 && min < 0 ) {
			y = He/5;
			var y1 =2*He/5;
			tmp = 5;
			}
		else  {
			y = He/2;
			var y1 = He/5;
			tmp = 10;
			}
	
	ctx.strokeStyle = 'black';
	ctx.beginPath();
	ctx.moveTo(0,y);
	ctx.lineTo(We, y);
	ctx.stroke();
	
	//рисуем деления на нулевой границе
	var ch=5.00;
	var z=4;
	ctx.strokeStyle = 'black';
	for(var x=We/23; x<We; x+=We/23){
		ctx.beginPath();
		ctx.moveTo(x,y-5);
		ctx.lineTo(x, y+5);
		if(ch>23) ch=1;
		else ch+=1;
		if(z==4){
	    	z=0;
	    	ctx.textAlign = "left";
	    	ctx.textBaseline = "top";
	    	ctx.fillText(ch, x, y+7);
		}
		else z++;
		ctx.stroke();
	}
	
	ctx.textAlign = "left";
	ctx.textBaseline = "bottom";
	
	var z=max, k = min+10;
	for(y1; y1<He; y1+=He/5){
		ctx.beginPath();
		if(y1 == y) ctx.strokeStyle = 'black';
		else
			if( y1 < y) {
				ctx.strokeStyle = 'green';
				ctx.fillText(z, 0, y1);
				z-=tmp;
			}
			else {
				ctx.strokeStyle = 'blue';
				ctx.fillText(k, 0, y1);
				k-=tmp;
			}
		ctx.moveTo(10,y1);
		ctx.lineTo(We, y1);   		    	
		ctx.stroke();
	}
}

var timer;
	$(window).resize(function() {
	Magic();
	Centr();
});


$(document).ready(function(){
	Magic();
	Centr();
	Weather();
	WeatherAll();
	Time();

});


		
		