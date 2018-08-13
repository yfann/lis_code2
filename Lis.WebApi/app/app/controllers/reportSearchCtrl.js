app.controller('ReportSearchCtrl', ['$scope', '$state', 'dataService', 'util', '$location', '$q', function ($scope, $state, dataService, util, $location, $q) {
    var editUrl = '<a class="edit-tpl" ui-sref="report_print({id: row.entity.id})">查看</a> <a class="edit-tpl" ng-click="grid.appScope.downloadPDF(row.entity.id)">下载</a>'

    $scope.gridOptions = {
        paginationPageSizes: [25, 50, 75],
        paginationPageSize: 25,
        useExternalPagination: true,
        columnDefs: [
            {
                field: 'id',
                visible: false
            },
            {
                field: 'patientName',
                displayName: '姓名'
            },
            {
                field: 'gender',
                displayName: '性别'
            },
            {
                field: 'birthDay',
                displayName: '生日'
            },
            {
                field: 'age',
                displayName: '年龄'
            },
            {
                field: 'miName',
                displayName: '检验医院名称'
            },
            {
                field: 'reportTime',
                displayName: '检验日期',
                cellTemplate: '<span>{{row.entity.reportTime|date:"yyyy-MM-dd"}}</span>'
            },
            {
                field: 'setName',
                displayName: '检验项目'
            },
            {
                field: 'sampleId',
                displayName: '检验样本号'
            },
            {
                field: 'deviceName',
                displayName: '检验仪器'
            },
            {
                name: 'edit',
                displayName: '操作',
                cellTemplate: editUrl
            }
        ],
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
            gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                $scope.paginationOptions.pageNumber = newPage;
                $scope.paginationOptions.pageSize = pageSize;
                $scope.pageLoad();
            });
        }
    };

    $scope.model = {
        selectedSite: null,
        patientName: null,
        idCard: null,
        checkoutDate: null,
        startOpened: false,
        endOpened: false,
    };


    $scope.paginationOptions = {
        pageNumber: 1,
        pageSize: 25
    };

    $scope.dateOptions = {
        formatYear: 'yy',
        startingDay: 1,
        class: 'datepicker'
    };

    $scope.startOpen = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope.model.startOpened = true;
    };

    $scope.endOpen = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope.model.endOpened = true;
    };

    $scope.pageLoad = function () {
        dataService.searchReport($scope.model.patientName, $scope.model.idCard, $scope.model.checkoutDate, $scope.paginationOptions.pageNumber, $scope.paginationOptions.pageSize).then(function (result) {
            $scope.gridOptions.data = result.data;
        });
    };

    var formatDate = function (date) {
        if (!date) return '';
        var d = new Date(date),
            month = '' + (d.getMonth() + 1),
            day = '' + d.getDate(),
            year = d.getFullYear();

        if (month.length < 2) month = '0' + month;
        if (day.length < 2) day = '0' + day;

        return [year, month, day].join('-');
    };

    $scope.loadAll = function (redirect) {
        var date = formatDate($scope.model.checkoutDate);
        $q.all([
            dataService.getReportTotalNum($scope.model.patientName, $scope.model.idCard, date),
            dataService.searchReport($scope.model.patientName, $scope.model.idCard, date, $scope.paginationOptions.pageNumber, $scope.paginationOptions.pageSize)
        ]).then(function (result) {
            $scope.gridOptions.totalItems = result[0].data;
            $scope.gridOptions.data = result[1].data;
            if (redirect) {
                if (result[1].data.length == 1) {
                    $state.go('report_print', { id: result[1].data[0].id });
                }
            }
        });
    }


    var params = $location.search();
    if (params.innerId) {
        $scope.model.patientName = null;
        $scope.model.idCard = params.innerId;
        $scope.model.checkoutDate = null;
        $scope.loadAll(true);
    } else {
        $scope.loadAll();
    }



    $scope.search = function () {
        //$scope.gridApi.grid.refresh();
        $scope.loadAll();
    };



    $scope.downloadPDF = function (id) {
        window.open(location.origin + '/home/DownloadPdf?reportId=' + id, '_blank');
    };


}]);



app.controller('ReportPrintCtrl', ['$scope', '$state', '$stateParams', 'dataService', 'util', '$location', function ($scope, $state, $stateParams, dataService, util, $location) {
    var params = $location.search();
    var id = $stateParams.id || (params ? params.reportId : null);
    if (id) {
        dataService.getReportById(id).then(function (result) {
            result.data.formatedApplicationTime = util.formateDate(result.data.applicationTime);
            result.data.formatedSendTime = util.formateDate(result.data.sendTime);
            result.data.formatedReportTime = util.formateDate(result.data.reportTime);
            if (result.data.details) {
                result.data.details.forEach(function (item) {
                    var resultValue = new Number(item.labResult.resultValue);
                    var refLo = new Number(item.labResult.refLo);
                    var refHi = new Number(item.labResult.refHi);
                    if (!isNaN(resultValue) && !isNaN(refLo) && !isNaN(refHi)) {
                        if (resultValue < refLo || resultValue > refHi) {
                            item.isRed = true;
                        }
                    } else {
                        item.isRange = true;
                        if (item.labResult.resultValue != item.labResult.refRange) {
                            item.isRed = true;
                        }
                    }
                });
            }
            $scope.model = result.data;
        });
    }

    $scope.downloadPDF = function () {
        window.open(location.origin + '/home/DownloadPdf?reportId=' + id, '_blank');
    };
}]);