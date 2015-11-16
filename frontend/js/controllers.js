var weatherApp = angular.module('weatherApp', []);
 
weatherApp.controller('CanvaCTRL', function($scope,$interval,$http) {	
	binding_today_graphic($scope, $http);
	
	$interval(function () {
		Centr( $scope.today_list);
		$scope.$apply();
    }, 1000, 1, false);//выполнится 1 раз с задержкой 1 секунда
		 
	
    $interval(function () {
		Centr( $scope.today_list)
		$scope.$apply();
	}, 5000, 0, false);//каждые 5 секунд обновляется бегунок

	$interval(function () {
		binding_today_graphic($scope, $http);
		$scope.$apply();
	}, 600000, 0, false);//каждые 10 минут обновляются данные для канваса 

});

function binding_today_graphic($scope, $http){
	$.ajax({
		type: "GET",
	  url: 'http://meteostationscu.azurewebsites.net/Card/GetTodayGraphicAndToday',
	  success: function(data){
		$scope.today_list=data.TodayGraphic;
		$scope.today = update_today(data.Today, $scope.today_list);
	  },
	  error: function(){
		  binding_today_graphic($scope, $http);
	  }
	  
	});
	return $scope;
}

function update_today(today, allDay){

	if(today == null){
		day.Description = "Температура"
			
		var now = new Date();
			hours = now.getHours();
		var min = Math.abs(hours - allDay[0].Time_hour),
			res =  allDay[0].Temperature;
		
		for(i in allDay){
			if (Math.abs(hours - allDay[i].Time_hour) < min){
				min = Math.abs(hours - allDay[i].Time_hour);
				res = allDay[i].Temperature;
			}
		}
		day.Temperature = res
		
		day.Humidity = "NA"
		day.WindSpeed = "NA"
		day.Pressure = "NA"
		day.Radiation = "NA"
		day.WindDirection = "NA"
		return day
	}	else {
		
		if (today.Description == null ) today.Description = "Температура"
			
		if(today.Temperature == null ){
			var now = new Date();
				hours = now.getHours();
			var min = Math.Abs(hours - allDay[0].Time_hour),
				res =  allDay[0].Temperature;
			
			for(i in allDay){
				if (Math.Abs(finger - allDay[i].Time_hour) < min){
					min = Math.Abs(finger - allDay[i].Time_hour);
					res = allDay[i].Temperature;
				}
			}
			today.Temperature = res
		}
		if (today.Humidity == null ) today.Humidity = "NA"
		if (today.WindSpeed == null ) today.WindSpeed = "NA"
		if (today.Pressure == null ) today.Pressure = "NA"
		if (today.Radiation == null ) today.Radiation = "NA"
		if (today.WindDirection == null ) today.WindDirection = "NA"
		else to_wind_direction(today.WindDirection)

	}			
	
	return today;
}

function binding($scope, $http){
	$.ajax({
		type: "GET",
	  url: 'http://meteostationscu.azurewebsites.net/Card/GetFutureAndPast',
	  success: function(data){
		$scope.future = update_data(data.Future);
		$scope.past = update_data(data.Past);
	  },
	  error: function(){
		  binding($scope, $http);
	  }
	});
	return $scope;
}


weatherApp.controller('InfoController', function($scope, $interval, $http){
	
    binding($scope, $http);

	$interval(function () {
		binding_past($scope, $http);
		binding_future($scope, $http);
        $scope.$apply();
        
    }, 2400000 , 0, false);//каждые 40 минут 2400000
	
	$scope.numLimit = 4; 
		
	$scope.loading = function(limit){
		$scope.numLimit += 3;
	}
	
	$scope.load = function(){
		var clientH = $(window).height();
		$('#past').css('height', clientH );
		$('.scrolling_past').css('height', clientH * 0.9);
		
		var btn = document.getElementById('load');
		btn.classList.add("hidden-sm");
		btn.classList.add("hidden-xs");

		var el = document.getElementById('scrolling_past');
		el.classList.add("visible-xs");

		el.classList.remove("hidden-xs");

		el.classList.add("visible-sm");
		el.classList.add("visible-md");
		el.classList.remove("hidden-md");
		el.classList.remove("hidden-sm");
	}
})

weatherApp.directive('pbWeatherCards', function(){
	return{
		restrict: 'E',
		scope: {
			day: '='
		},
		templateUrl: 'template/cards.html'
	};
});




