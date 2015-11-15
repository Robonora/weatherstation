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
				binding_today_graphic($scope, $http)
		       $scope.$apply();
	}, 600000, 0, false);//каждые 10 минут обновляются данные для канваса

});

function binding($scope, $http){
	$http.get('info.json').success(function(data) {
        $scope.past=data.past;
        $scope.future=data.future;
	}
)}

function binding_today_graphic($scope, $http){
	$http.get('today_graphic.json').success(function(data) {
        $scope.today_list=data.today_graphic;
	}
)}

weatherApp.controller('InfoController', function($scope, $interval, $http){
       //alert('1')
	binding($scope, $http);  
	$interval(function () {
		binding($scope, $http);  
        $scope.$apply();
        
    }, 2400000, 0, false);//каждые 40 минут 
	
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

		//loading(0);
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

weatherApp.controller('Today', function($scope,$http){
	$http.get('info.json').success(function(data) {
		 $scope.today=data.today;
    }); 
});

