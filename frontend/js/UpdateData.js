function update_data(list){

	var reg_day = /\d\d./;
	var reg_month = /.\d\d/;
	var ch = /\d\d/
	
	for(i in list){
		date = list[i].Date
		
		day = date.match(reg_day)
		day = day.join('')
		day = day.match(ch)
		list[i].day = day.join('');
		
		month = date.match(reg_month)
		month = month.join('')
		month = month.match(ch)
		
		list[i].month = to_month(month.join(''));

		list[i].description = to_description(list[i].Description);

		list[i].WindDirection = to_wind_direction(list[i].WindDirection);
		
		if(list[i].Description == "-") list[i].Description = "na"
		
	}

	return list;
}

function to_description(desc){
	if(desc == "Snow") return "Снежно";
	if(desc == "Clear") return "Солнечно";
	if(desc == "Clouds") return "Облачно";
	if(desc == "Rain") return "Дождливо";
}

function to_wind_direction(wind){
	if(wind == "S" ) return "Южный";
	if(wind == "SW" ) return "Юго-западный";
	if(wind == "W" ) return "Западный";
	if(wind == "NW" ) return "Северо-западный";
	if(wind == "N" ) return "Северный";
	if(wind == "NE" ) return "Северо-восточный";
	if(wind == "E" ) return "Восточный";
	if(wind == "SE" ) return "Юго-восточный";
	return "NA";
}

function to_month(month){
	if(month == '01') return 'Января';
	else 
	if(month == '02') return 'Февраля';
	else 
	if(month == '03') return 'Марта';
	else 
	if(month == '04') return 'Апреля';
	else 
	if(month == '05') return 'Мая';
	else 
	if(month == '06') return 'Июня';
	else 
	if(month == '07') return 'Июля';
	else 
	if(month == '08') return 'Августа';
	else 
	if(month == '09') return 'Сентября';
	else 
	if(month == '10') return 'Октября';
	else 
	if(month == '11') return 'Ноября';
	else 
	if(month == '12') return 'Декабря';
}