var app = angular.module("App", ['ngRoute', 'mgcrea.ngStrap','ngAnimate']);
app.config(function ($routeProvider) {
        $routeProvider
            .when("/index", {
                controller: "AppCtrl",
                templateUrl: "/html/index.html"
            })
            .when("/add", {
                controller: "AppCtrl",
                templateUrl: "/html/add.html"
            })
});

app.controller("AppCtrl", function($scope) {
    $scope.name = 'Paweł';
    $scope.isHidden = false;

    $scope.contacts = cont;

    $scope.clickHandler = function () {
        //console.log($scope.isHidden);
        $scope.isHidden = !$scope.isHidden;
    };

    $scope.modal = {
        title: 'My modal window',
        content: 'Some lazy looking content'
    };

    $scope.myTooltip = {
        title: 'Tooltip (WOW)'
    }
});