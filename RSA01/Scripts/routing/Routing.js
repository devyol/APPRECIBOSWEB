app.config(function ($stateProvider, $urlRouterProvider) {



    $stateProvider
        .state('Home', {
            url: '/',
            templateUrl: 'home/index2'
        })
        .state('reciboNuevo', {
            url: '/Nuevo',
            templateUrl: 'recibo/ReciboNuevo',
            controller: 'CtrlReciboNuevo'
        })
        .state('reciboConsulta', {
            url: '/Consulta',
            templateUrl: 'recibo/ReciboConsulta',
            controller: 'CtrlReciboConsulta'
        })
        .state('datosPais', {
            url: '/pais',
            templateUrl: 'recibo/EstadisticaPais',
            controller: 'CtrldatosPais'
        })
        .state('datosConcepto', {
            url: '/concepto',
            templateUrl: 'recibo/EstadisticaConcepto',
            controller: 'CtrldatosConcepto'
        })
        .state('datosTotales', {
            url: '/totales',
            templateUrl: 'recibo/EstadisticaTotales',
            controller: 'CtrldatosTotales'
        })
        .state('inforeporte', {
            url: '/detalle',
            templateUrl: 'recibo/ReporteDetallado',
            controller: 'CtrlInfoDetalle'
        })
    ;


});