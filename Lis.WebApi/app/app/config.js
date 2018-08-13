// config

var app =
  angular.module('app').config(
    ['$controllerProvider', '$compileProvider', '$filterProvider', '$provide','$httpProvider','$cookiesProvider',
      function ($controllerProvider, $compileProvider, $filterProvider, $provide,$httpProvider,$cookiesProvider) {
          $httpProvider.defaults.withCredentials = true;
          $cookiesProvider.defaults = $cookiesProvider.defaults || {};
          $cookiesProvider.defaults.path = "/";
        // lazy controller, directive and service
        app.controller = $controllerProvider.register;
        app.directive = $compileProvider.directive;
        app.filter = $filterProvider.register;
        app.factory = $provide.factory;
        app.service = $provide.service;
        app.constant = $provide.constant;
        app.value = $provide.value;
      }
    ]).config(['$translateProvider', '$httpProvider', '$cookiesProvider', function ($translateProvider, $httpProvider, $cookiesProvider) {
      $translateProvider.useStaticFilesLoader({
        prefix: 'i18n/',
        suffix: '.js'
      });
      $translateProvider.preferredLanguage('zh_cn');
      $translateProvider.useLocalStorage();
      $httpProvider.defaults.withCredentials = true;
      $cookiesProvider.defaults = $cookiesProvider.defaults || {};
      $cookiesProvider.defaults.path = "/";
    }]);

// 翻译快捷方式
var T = {};
// 本地存储快捷方式
var S = {};
app.run(['$translate', '$localStorage',
  function ($translate, $localStorage) {
    // 定义翻译快捷方式
    T = function (key) {
      return $translate.instant(key);
    };

    S = $localStorage;
  }
]);

app.constant('config', {
  host: location.origin
});