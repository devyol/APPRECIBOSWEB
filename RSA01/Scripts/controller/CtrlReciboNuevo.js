app.controller('CtrlReciboNuevo', ['$scope', 'SendBox', 'ServicioRecibo', '$window', function ($scope, SendBox, ServicioRecibo, $window) {
    
    $scope.mostrarFormularioRecibo = false;
    $scope.mostrarListaRecibos = false;
    $scope.mostrarBotonNuevo = false;
    $scope.mostrarBotonRetroceder = false;
    $scope.configList = new Array();

    $scope.eventos = {};
    $scope.paises = {};
    $scope.listarecibos = new Array();
    $scope.cantidad = 0;
    $scope.nombre = '';
    $scope.recibo = {};
    $scope.recibo.idrecibo = null;
    $scope.recibo.nombre = '';
    $scope.recibo.cantidad = 0;
    $scope.recibo.valor = 0;
    $scope.recibo.total = 0;
    $scope.recibo.detalle = '';
    $scope.recibo.evento = 0;
    $scope.recibo.opcion_ = {};
    $scope.recibo.pais_ = {};

    //array para el pagineo
    $scope.configList = {
        itemsPerPage: 10,
        fillLastPage: false,
        maxPages: 10
    };
    

    //Se obtiene el evento
    ServicioRecibo.ObtenerEventos().then(function (data) {
        $scope.eventos = data;
        $scope.idEvento = data.idevento;

    });

    //Se obtiene el listado de Recibos por Evento
    $scope.listarRecibosEvento = function (eve) {

        ServicioRecibo.listarRecibos(eve.idevento).then(function (data) {
            $scope.listarecibos = data;
            $scope.nombreevento = eve.nombre;
            $scope.mostrarListaRecibos = true;
            $scope.mostrarFormularioRecibo = false;
            $scope.mostrarBotonNuevo = true;
            $scope.mostrarBotonRetroceder = false;

            //Se cargan las opciones del evento para el Formulario
            ServicioRecibo.ObtenerOpcionesEvento(eve.idevento).then(function (data) {
                $scope.opciones = data;

            });

            //Se cargan los paises para el Formulario
            ServicioRecibo.ObtenerPaises().then(function (data) {
                $scope.paises = data;

            });
        })
    };

    //Se asigna el valor del precio de la opcion
    $scope.detalleOpcion = function (objOpcion) {
        $scope.recibo.valor = objOpcion.precio;
    };

    //Metodo para Registrar Nuevo Recibo
    $scope.nuevoRecibo = function () {
        $scope.mostrarFormularioRecibo = true;
        $scope.mostrarListaRecibos = false;
        $scope.mostrarBotonNuevo = false;
        $scope.mostrarBotonRetroceder = true;
        limpiarCamposFormulario();
    };

    //Metodo para Retroceder y mostrar Listado de Recibos
    $scope.retroceder = function (eve) {
        $scope.mostrarFormularioRecibo = false;
        $scope.mostrarListaRecibos = true;
        $scope.mostrarBotonNuevo = true;
        $scope.mostrarBotonRetroceder = false;
        ServicioRecibo.listarRecibos(eve.idevento).then(function (data) {
            $scope.listarecibos = data;
        });
    };

    
    $scope.guardar = function (objrecibo, eve) {

        objrecibo.total = $scope.recibo.valor * $scope.recibo.cantidad;

        var respuesta = confirm("¿Esta Seguro de Realizar el Cobro?");

        if (respuesta == true) {
            SendBox.post(objrecibo, 'api/Recibo/GuardarRecibo')
            .then(function (data) {
                if (data.code == 0) {
                    alert(data.message);
                    //$scope.refrescar(data.data.idrecibo);
                    //Se carga el listado de recibos
                    ServicioRecibo.listarRecibos(eve.idevento).then(function (data) {
                        $scope.listarecibos = data;
                    });
                    //Dinamica de las Pantallas
                    $scope.mostrarFormularioRecibo = false;
                    $scope.mostrarListaRecibos = true;
                    $scope.mostrarBotonNuevo = true;
                    $scope.mostrarBotonRetroceder = false;

                } else {
                    alert(data.message);
                }
            });
        }
    };

    $scope.anularRecibo = function (nrec) {

        var respuesta = confirm("¿Esta Seguro de Anular el Recibo Numero " + nrec.idrecibo + " ?");

        if (respuesta == true) {
            SendBox.post(nrec.idrecibo, 'api/Recibo/anularRecibo')
            .then(function (data) {
                if (data.code == 0) {
                    alert(data.message);
                    ServicioRecibo.listarRecibos(nrec.idevento).then(function (data) {
                        $scope.listarecibos = data;
                    });
                } else {
                    alert(data.message);
                }
            })
        }

    };

    $scope.refrescar = function (obj) {
        ServicioRecibo.ObtenerRecibo(obj).then(function (data) {
            $scope.recibo = data;
        })
    };

    $scope.imprimirRecibo = function (objRec) {
        $window.open("http://localhost:19190/ImpresionRecibo/imprimir?idRecibo=" + objRec.idrecibo);
    };

    //scope para el filtro del listado de recibos
    $scope.LimpiarFiltro = function () {
        $scope.buscar = {};
    };

    var limpiarCamposFormulario = function () {
        $scope.recibo.cantidad = 0;
        $scope.recibo.valor = 0;
        $scope.recibo.nombre = '';
        $scope.recibo.opcion_ = {};
        $scope.recibo.pais_ = {};
    };

}]);