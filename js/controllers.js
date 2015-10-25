var weatherApp = angular.module('weatherApp', []);
 
weatherApp.controller('CanvaCTRL', function($scope,$interval,$http) {	
	
	binding($scope, $http);
	
    $interval(function () {
		        binding($scope, $http);
		        $scope.$apply();
		    }, 5000, 0, false);//каждые 5 секунд
});

function binding($scope, $http){
	$http.get('info.json').success(function(data) {
        $scope.all=data.all;
        $scope.past=data.past;
        $scope.future=data.future;
       Centr($scope.all);
})
}

weatherApp.controller('InfoController', function($scope, $interval, $http){
       //alert('1')
	binding($scope, $http);  
	$interval(function () {
		binding($scope, $http);  
        $scope.$apply();
        
    }, 600000, 0, false);//каждые 10 минут 
	
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

		if($(window).width()<=768) {
			var el = document.getElementById('scrolling_past');
			el.classList.add("visible-xs");
			el.classList.add("visible-sm");
			el.classList.remove("hidden-xs");
			el.classList.remove("hidden-sm");
		}
		else{
			var el = document.getElementById('scrolling_past');
			el.classList.add("visible-sm");
			el.classList.add("visible-md");
			el.classList.remove("hidden-md");
			el.classList.remove("hidden-sm");
		}
		loading();
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

