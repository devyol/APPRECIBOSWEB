//Post SendBox we talk with the rest service thru here ---------->
var uiToken = "34567890df7af89gfhdggdvcx";
angular.module('appMain').factory('SendBox', function ($http, $q) {
    return { //------- return ----------------------------->
        post: function (sendme, svmethod) {
            var deferred = $q.defer();
            $http({
                url: svmethod, data: sendme, method: "POST", headers: {
                    RequestVerificationToken: uiToken
                }
            }).
               success(function (data, status, headers, config) {
                   deferred.resolve(data);
               }).
               error(function (data, status, headers, config) {
                   deferred.reject(data);
               });
            return deferred.promise;
        },//---- end post -------------------------/
        get: function (svmethod) {
            var deferred = $q.defer();
            $http({
                url: svmethod, method: "GET"
            }).
               success(function (data, status, headers, config) {
                   deferred.resolve(data);
               }).
               error(function (data, status, headers, config) {
                   deferred.reject(data);
               });
            return deferred.promise;
        }//---- end get -------------------------/
    }// end return ------------------------------------/

});