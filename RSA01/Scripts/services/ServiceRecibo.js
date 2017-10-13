app.service('ServicioRecibo', function ($http, $q, SendBox) {

    var CatalogosServices = new Object();

    //-----------------------------------------------------------------------
    //  LISTA DE EVENTOS
    //----------------------------------------------------------------------

    this.ObtenerEventos = function () {
        var deferred = $q.defer();

        try {
            SendBox.post("", 'api/Recibo/listarEventos')
            .then(function (data) {
                if (data.code == 0) {
                    CatalogosServices.eventos = data.data;
                } else {
                    alert(data.message);
                }
                deferred.resolve(CatalogosServices.eventos);
            }),
            function (data) { };

        } catch (e) {
            deferred.reject(CatalogosServices.eventos);
        };
        return deferred.promise;
    };

    //-----------------------------------------------------------------------
    //  LISTA DE OPCIONES DEL EVENTO SELECCIONADO
    //----------------------------------------------------------------------

    this.ObtenerOpcionesEvento = function (evento) {
        var deferred = $q.defer();

        try {
            SendBox.post(evento, 'api/Recibo/listarOpcionesEvento')
            .then(function (data) {
                if (data.code == 0) {
                    CatalogosServices.opciones = data.data;
                } else {
                    alert(data.message);
                }
                deferred.resolve(CatalogosServices.opciones);
            }),
            function (data) { };

        } catch (e) {
            deferred.reject(CatalogosServices.opciones);
        };
        return deferred.promise;
    };

    //-----------------------------------------------------------------------
    //  LISTA DE PAISES
    //----------------------------------------------------------------------

    this.ObtenerPaises = function () {
        var deferred = $q.defer();

        try {
            SendBox.post("", 'api/Recibo/listarPaises')
            .then(function (data) {
                if (data.code == 0) {
                    CatalogosServices.paises = data.data;
                } else {
                    alert(data.message);
                }
                deferred.resolve(CatalogosServices.paises);
            }),
            function (data) { };

        } catch (e) {
            deferred.reject(CatalogosServices.paises);
        };
        return deferred.promise;
    };

    //-----------------------------------------------------------------------
    //  OBTENER INFORMACION DEL RECIBO
    //----------------------------------------------------------------------

    this.ObtenerRecibo = function (recibo) {
        var deferred = $q.defer();

        try {
            SendBox.post(recibo, 'api/Recibo/obtenerRecibo')
            .then(function (data) {
                if (data.code == 0) {
                    CatalogosServices.inforecibo = data.data;
                } else {
                    alert(data.message);
                }
                deferred.resolve(CatalogosServices.inforecibo);
            }),
            function (data) { };

        } catch (e) {
            deferred.reject(CatalogosServices.inforecibo);
        };
        return deferred.promise;
    };

    //-----------------------------------------------------------------------
    //  OBTENER LISTADO DE RECIBOS POR EVENTO
    //----------------------------------------------------------------------

    this.listarRecibos = function (recibo) {
        var deferred = $q.defer();

        try {
            SendBox.post(recibo, 'api/Recibo/listarRecibos')
            .then(function (data) {
                if (data.code == 0) {
                    CatalogosServices.recibos = data.data;
                } else {
                    alert(data.message);
                }
                deferred.resolve(CatalogosServices.recibos);
            }),
            function (data) { };

        } catch (e) {
            deferred.reject(CatalogosServices.recibos);
        };
        return deferred.promise;
    };

    //-----------------------------------------------------------------------
    //  LISTA ESTADISTICAS POR PAIS
    //----------------------------------------------------------------------

    this.ObtenerDatosPais = function (evento) {
        var deferred = $q.defer();

        try {
            SendBox.post(evento, 'api/ReciboEstadistica/estadisticasPais')
            .then(function (data) {
                if (data.code == 0) {
                    CatalogosServices.datospais = data.data;
                } else {
                    alert(data.message);
                }
                deferred.resolve(CatalogosServices.datospais);
            }),
            function (data) { };

        } catch (e) {
            deferred.reject(CatalogosServices.datospais);
        };
        return deferred.promise;
    };

    //-----------------------------------------------------------------------
    //  LISTA ESTADISTICAS POR CONCEPTO
    //----------------------------------------------------------------------

    this.ObtenerDatosConcepto = function (evento) {
        var deferred = $q.defer();

        try {
            SendBox.post(evento, 'api/ReciboEstadistica/estadisticasConcepto')
            .then(function (data) {
                if (data.code == 0) {
                    CatalogosServices.datosconcepto = data.data;
                } else {
                    alert(data.message);
                }
                deferred.resolve(CatalogosServices.datosconcepto);
            }),
            function (data) { };

        } catch (e) {
            deferred.reject(CatalogosServices.datosconcepto);
        };
        return deferred.promise;
    };

    //-----------------------------------------------------------------------
    //  LISTA ESTADISTICAS POR CONCEPTO
    //----------------------------------------------------------------------

    this.ObtenerDatosTotales = function (evento) {
        var deferred = $q.defer();

        try {
            SendBox.post(evento, 'api/ReciboEstadistica/estadisticasTotales')
            .then(function (data) {
                if (data.code == 0) {
                    CatalogosServices.datostotales = data.data;
                } else {
                    alert(data.message);
                }
                deferred.resolve(CatalogosServices.datostotales);
            }),
            function (data) { };

        } catch (e) {
            deferred.reject(CatalogosServices.datostotales);
        };
        return deferred.promise;
    };

});
