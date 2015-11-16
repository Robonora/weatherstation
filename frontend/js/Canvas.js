var wea = document.getElementById('weather');
var He = wea.height;
var We = wea.width;

function Centr($list){	

	if (wea.getContext) {

		var ctx = wea.getContext('2d');

		wea.width  = $('#currently').width() ;
		wea.height = $('#currently').height()/3 ;
		
		var arr = $list;
		
		var i = 0;
		var min, max;
		min = arr[0].Temperature;
		max = arr[0].Temperature;	
		
		for(var i in arr){
			
			if(arr[i].Temperature < min) min = arr[i].Temperature;
			else 
				if(arr[i].Temperature > max) max = arr[i].Temperature;
			
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

		grid(max, min);
		
		WeatherAll(max, min, arr);
	}
	Time(ctx,wea.height, wea.width );
}

function grid(max, min){ 
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
	var ch=0.00;
	var z=4;
	ctx.strokeStyle = 'black';
	for(var x=We/25; x<We; x+=We/25){
		ctx.beginPath();
		ctx.moveTo(x,y-5);
		ctx.lineTo(x, y+5);
		
		if(z==4){
	    	z=0;
	    	ctx.textAlign = "left";
	    	ctx.textBaseline = "top";
	    	ctx.fillText(ch, x, y+7);
		}
		else z++;
		ch+=1;
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

function WeatherAll(max, min, arr){
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

		if(arr[i].Temperature > 0 ){
			if(arr[i].Temperature <= max-10)	y1 =  gran_max  - arr[i].Temperature * part_top ;
			else y1 = gran_min -(arr[i].Temperature - max + 10)*part;
		}
		else{
			if(arr[i].Temperature >= min + 10)	y1 =  Math.abs(He - gran_max) - arr[i].Temperature * part_bot ;
			else y1 =  Math.abs(He - gran_min) - (arr[i].Temperature - min - 10) * part;
		}

		if(arr[i].Time_hour == 0)  x = We/25 + arr[i].Time_min * (We/25)/60 ;
		else x = arr[i].Time_hour * We/25 + arr[i].Time_min * (We/25)/60 + We/25;
		 
		if( arr[i].Temperature == 0) ctx.fillRect(x-3, y-3, 6, 6);// вывод точки если температура равна 0
		else {
			var Time_hour = arr[i].Time_hour;

			var hour = hours;
			
			ctx.beginPath();
			ctx.lineWidth = 1;
			
			if (arr[i].Time_hour > hour || ( arr[i].Time_hour == hour && arr[i].Time_min > minutes ) ){
				ctx.lineWidth = 5;
			}

			ctx.moveTo(x, y);
			ctx.lineTo(x,y1); 				
		}				
		ctx.stroke();
    }
}

function Time(ctx, He, We){
	 
	var now = new Date();
		hours = now.getHours();
		minutes = now.getMinutes();

	if(hours == 0) x = We/25 + (We/25)/60*minutes + We/25;
	else x = hours*We/25 + (We/25)/60*minutes + We/25;
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