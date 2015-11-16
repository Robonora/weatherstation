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
	if(desc == "Snow") return "������";
	if(desc == "Clear") return "��������";
	if(desc == "Clouds") return "�������";
	if(desc == "Rain") return "��������";
}

function to_wind_direction(wind){
	if(wind == "S" ) return "�����";
	if(wind == "SW" ) return "���-��������";
	if(wind == "W" ) return "��������";
	if(wind == "NW" ) return "������-��������";
	if(wind == "N" ) return "��������";
	if(wind == "NE" ) return "������-���������";
	if(wind == "E" ) return "���������";
	if(wind == "SE" ) return "���-���������";
	return "NA";
}

function to_month(month){
	if(month == '01') return '������';
	else 
	if(month == '02') return '�������';
	else 
	if(month == '03') return '�����';
	else 
	if(month == '04') return '������';
	else 
	if(month == '05') return '���';
	else 
	if(month == '06') return '����';
	else 
	if(month == '07') return '����';
	else 
	if(month == '08') return '�������';
	else 
	if(month == '09') return '��������';
	else 
	if(month == '10') return '�������';
	else 
	if(month == '11') return '������';
	else 
	if(month == '12') return '�������';
}