var status = true;
let columnCategory = '';
let colStateList = [];
let filteredQuery = '';
var savedFilterModel = null;
var langOutput = false;
var studentSeasonListingGridOption = '';
$(document).ready(function () {
    $('.filterName').keyup(function () {
        $('.filterName-errormsg').text('');
    });
});

(function ($) {
    $.fn.serializeFormJSON = function () {
        var jsonObject = {};
        var formArray = this.serializeArray();
        $.each(formArray, function () {
            if (jsonObject[this.name]) {
                if (!jsonObject[this.name].push) {
                    jsonObject[this.name] = [jsonObject[this.name]];
                }
                jsonObject[this.name].push(this.value || '');
            } else {
                jsonObject[this.name] = this.value || '';
            }
        });
        return jsonObject;
    };

    $.fn.isInViewport = function () {
        if ($(this).length != 0) {
            var elementTop = $(this).offset().top;
            var elementBottom = elementTop + $(this).outerHeight();

            var viewportTop = $(window).scrollTop();
            var viewportBottom = viewportTop + $(window).height();

            return elementBottom > viewportTop && elementTop < viewportBottom;
        }
    };

})(jQuery);

$(document).ajaxError(function (event, jqXhr, ajaxSettings, thrownError) {
    switch (jqXhr.status) {
        case 401:
            window.location = "/Authorize/logout";
            break;
        default:
            break;
    }
});

Date.prototype.isValid = function () {
    return this.getTime() === this.getTime();
}

function isDecimal(evt, ele) {
    var self = $(ele);
    evt = (evt) ? evt : window.event;
    self.val(self.val().replace(/[^0-9\.]/g, ''));
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if ((charCode != 46 || self.val().indexOf('.') != -1) && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}

function rebindvalidators(formId) {
    var form = $('.' + formId)
        .removeData("validator") /* added by the raw jquery.validate plugin */
        .removeData("unobtrusiveValidation");  /* added by the jquery unobtrusive plugin*/

    $.validator.unobtrusive.parse(form);
    $(form).data('unobtrusiveValidation');
}

function escapeRegExp(string) {
    return string.replace(/[.*+?^${}()|[\]\\]/g, "\\$&");
}

function replaceAll(str, term, replacement) {
    return str.replace(new RegExp(escapeRegExp(term), 'g'), replacement);
}

//--Start Ag-Grid Section--//
var listingGridOptions;
if (typeof agGrid !== 'undefined') {
    agGrid.LicenseManager.setLicenseKey("CompanyName=Nineyard,LicensedApplication=Nineyard,LicenseType=SingleApplication,LicensedConcurrentDeveloperCount=1,LicensedProductionInstancesCount=1,AssetReference=AG-013615,ExpiryDate=15_February_2022_[v2]_MTY0NDg4MzIwMDAwMA==01a8bcb72ddb10589e7cd0257c59b9d2");
}

function initAgGrid(elementClass, rowSelection, suppressRowClickSelection, rowSize, rowEdit, rowDelete, rowCopy, onSelectionChanged, listingColumnDefs) {
    class CustomStatsToolPanel {
        init(params) {

            this.eGui = document.createElement('div');
            this.eGui.id = 'detailsPanel';
            this.eGui.className = "ag-theme-balham";
            this.eGui.innerHTML = ``;
        }
        getGui() {
            return this.eGui;
        }
    }
    listingGridOptions = {
        rowMultiSelectWithClick: true,
        onRowSelected: onRowSelected,
        rowHeight: 40,
        getRowStyle: params => {
            if (params.data != undefined) {
                if (params.data.paymentOutstanding === 0) {
                    return { background: '#f5f5f5' };
                }
            }
        },
        onColumnResized: params => columnResized(params),
        onCellClicked(params) {
            var selectedRow = params.data;
            if (params.column.colId === "delete") {
                if (rowDelete && selectedRow) {
                    rowDelete(selectedRow);
                }
            }
            else if (params.column.colId === "copyLink") {
                if (rowCopy && selectedRow) {
                    rowCopy(selectedRow);
                }
            }
            else {
                if (rowEdit && selectedRow && params.column.colId != "none-selectable-row" && params.column.colId != "none-selectable-row_1" && params.column.colId != "none-selectable-row_2" && params.column.colId != "none-selectable-row_3" && params.column.colId != "Recurred") {
                    rowEdit(selectedRow);
                }
            }
        },

        onColumnMoved(params) {
            const indexOfSearchedColumn = columnDefs.findIndex(item => item.columnId === params.column.colDef.columnId);
            [columnDefs[indexOfSearchedColumn].displayOrder, columnDefs[params.toIndex].displayOrder] = [columnDefs[params.toIndex].displayOrder, columnDefs[indexOfSearchedColumn].displayOrder];
        },
        onSelectionChanged: onSelectionChanged,
        class: elementClass,
        columnDefs: listingColumnDefs,
        icons: {
            'custom-stats': '<span class="ag-icon ag-icon-custom-stats"></span>'
        },
        sideBar: {
            maxWidth: 600,
            toolPanels: [
                {
                    id: 'columns',
                    labelDefault: 'Columns',
                    labelKey: 'columns',
                    iconKey: 'columns',
                    toolPanel: 'agColumnsToolPanel',
                    toolPanelParams: {
                        suppressRowGroups: true,
                        suppressValues: true,
                        suppressPivots: true,
                        suppressPivotMode: true
                    },
                },
                {
                    id: 'filters',
                    labelDefault: 'Filters',
                    labelKey: 'filters',
                    iconKey: 'filter',
                    toolPanel: 'agFiltersToolPanel',
                },
            ],
        },
        components: { customStatsToolPanel: CustomStatsToolPanel },
        defaultColDef: { width: 150, filter: true, resizable: true, sortable: true, enableRowGroup: false, floatingFilter: true },
        multiSortKey: 'ctrl',
        rowSelection: rowSelection,
        suppressRowClickSelection: suppressRowClickSelection,
        autoGroupColumnDef: { minWidth: 200 },
        rowModelType: 'serverSide',
        serverSideStoreType: 'partial',
        animateRows: true,
        paginationPageSize: rowSize,
        cacheBlockSize: rowSize,
        maxBlocksInCache: 1,
        enableColResize: true,
        rowBuffer: 0,
        maxConcurrentDatasourceRequests: 1,
        debug: true
    };

    var eGridDiv = document.querySelector(elementClass);
    new agGrid.Grid(eGridDiv, listingGridOptions);
    listingGridOptions.api.showNoRowsOverlay();
    listingGridOptions.api.setRowData(null);
    listingGridOptions.api.showLoadingOverlay();
    return listingGridOptions;
}

function ReloadGrid() {
    listingGridOptions.api.refreshServerSideStore({ purge: true });
}

function whereSql(request) {
    var filterModels = [];
    var filterModel = request.filterModel;
    if (filterModel) {
        Object.keys(filterModel).forEach(function (key) {
            var item = filterModel[key];
            if (item.filterType == "date") {
                if (item.dateFrom != undefined) {
                    date = item.dateFrom;
                    Filter = date.split(' ')[0];
                    item.dateFrom = Filter;
                }
                if (item.condition1 != undefined) {
                    date = item.condition1.dateFrom;
                    Filter = date.split(' ')[0];
                    item.condition1.Filter = Filter;
                }
                if (item.condition2 != undefined) {
                    date = item.condition2.dateFrom;
                    Filter = date.split(' ')[0];
                    item.condition2.Filter = Filter;
                }
                filterModels.push({
                    "key": key,
                    "filterType": item.filterType,
                    "type": item.type,
                    "contains": item.operator,
                    "condition1": item.condition1,
                    "condition2": item.condition2,
                    "filter": item.dateFrom,
                    "operator": item.operator,
                    "category": item.columnCategory,
                    "headerName": item.headerName
                });
            }
            else {
                filterModels.push({
                    "key": key,
                    "filterType": item.filterType,
                    "type": item.type,
                    "contains": item.operator,
                    "condition1": item.condition1,
                    "condition2": item.condition2,
                    "filter": item.filter,
                    "operator": item.operator,
                    "category": item.columnCategory,
                    "headerName": item.headerName
                });
            }

        });
    }
    if (filterModels.length > 0 || request.sortModel.length > 0) {
        if (filterModels.length > 0) {
            filteredQuery = JSON.stringify(filterModels);
        }
    } else {
        filteredQuery = "";
    }
    return filteredQuery;
}

function orderBySql(request) {
    var sortModel = request.sortModel;
    if (sortModel.length === 0) return '';
    var sorts = sortModel.map(function (s) {
        return s.colId + ' ' + s.sort.toUpperCase();
    });
    return ' ORDER BY ' + sorts.join(', ');
}

function dateFormatStrWithTime(dateValue) {
    if (dateValue && dateValue != '0001-01-01T00:00:00') {
        if (dateValue.includes("-")) {
            return moment(dateValue).format("MM/DD/YYYY hh:mm:ss a");
        }
        else if (dateValue.includes("/")) {
            return moment(dateValue).format("MM/DD/YYYY hh:mm:ss a");
        }
        else {
            return moment(dateValue).format('MM/DD/YYYY');
        }
    }
    else {
        return "";
    }
}

function dateFormatStrWithOutTime(dateValue) {
    if (dateValue && dateValue != '0001-01-01T00:00:00') {
        if (dateValue.includes("-")) {
            return moment(dateValue).format("MM/DD/YYYY");
        }
        else if (dateValue.includes("/")) {
            return moment(dateValue).format("MM/DD/YYYY");
        }
        else {
            return moment(dateValue).format('MM/DD/YYYY');
        }
    }
    else {
        return "";
    }
}

function serverSideDataSource(server, isHideLoader) {
    return {
        getRows: async function (params) {
            if (!isHideLoader) {
                showLoader(true);
            }
            var response = await server.getData(params.request);
            //adding delay to simulate real server call
            setTimeout(function () {
                if (response.success) {
                    // call the success callback
                    params.success({

                        rowData: response.rows,
                        rowCount: response.lastRow

                    });
                    showLoader(false);
                    selectCheckboxOnScroll();

                } else {
                    // inform the grid request failed
                    params.fail();
                    showLoader(false);
                }
            }, 200);
        }
    };
}

function rendererDeleteAction() {
    let eGui = document.createElement("div");
    eGui.innerHTML = `
        <button class="btn btn-transparent btn-sm link-gray" type="button" data-action="delete" data-toggle="tooltip" title="Delete">
                <svg width="13" height="15" class="icon me-1"><use xlink:href="../assets/images/sprite-icons.svg#trash-icon"></use></svg>
            </button>
        `;
    return eGui;
}

function onRowSelected() {

}

function columnResized(params) {
    window.colState = [];
    if (params.finished) {
        if (params.source == 'uiColumnDragged') {
            window.colState = params.columnApi.getColumnState();
            var saveColState = JSON.stringify(window.colState);
            sessionStorage.setItem("saveColStateChanges_" + columnCategory, saveColState);
        }
    }
}
//--End Ag-Grid Section--//

function restoreState(category) {
    if (colStateList.length > 0) {
        window.colState = colStateList;
        listingGridOptions.columnApi.applyColumnState({
            state: window.colState[0],
            applyOrder: true,
        });
    }
    else {
        var restoreStateSession = sessionStorage.getItem("saveColStateChanges_" + category);
        if (restoreStateSession != null && restoreStateSession != undefined) {
            window.colState = JSON.parse(restoreStateSession);
            listingGridOptions.columnApi.applyColumnState({

                state: window.colState,
                applyOrder: true,
            });
        }
    }
}

function settingTab() {
    $('.sub-menu').toggle();
}

// ----- for update profile user ---- //


function editProfileUser(userId) {
    $.ajax({
        type: "GET",
        url: "/User/EditUser",
        data: { UserId: userId },
        dataType: "html",
        beforeSend: function () {

        },
        success: function (data) {
            $(".div-modal").html("");
            $(".div-modal").html(data);
            $("#addUpdateModalLabel").text("Update User");
            $(".add-update").modal('show');
        },
        error: function () {
            $(".add-update").modal('hide');
        }
    });
}

function renderDollarAction(amount) {
    var value = "$" + amount;
    return value;

}

function showLoader(isShowLoader) {
    if (isShowLoader) {
        $(".import-loading").show();
    }
    else {
        $(".import-loading").hide();
    }
}

function reload() {
    location.reload();
}

function renderPercentageAction(percentage) {
    var value = percentage + "%";
    return value;
}

function displayMessage(data, moduleName) {
    if (data.success) {
        var successMessage = "Your " + moduleName + " " + data.message + " successfully!";
        $('.add-update').modal('hide');
        $('.account-student-modal').modal('hide');
        $('.student-modal').modal('hide');
        $('.sending-modal').modal('hide');
        ReloadGrid();
        $('.success-message').text('');
        $('.success-message').text(successMessage);
        $('.sucess-modal').modal('show');
    }
    else {
        if (data.isExist == true) {
            if (moduleName == "document") {
                $('.add-update').modal('hide');
                $('.info-modal').modal('show');
                $('.info-message').text('');
                $('.info-message').text('Document in review stage. if any thing change in document you can re-upload document.');
            }
            else {
                $('.' + moduleName + '-exist-errormsg').removeAttr("d-none");
                $('.' + moduleName + '-exist-errormsg').text(data.message);
            }
        }
        else {
            $('.error-message').text('');
            $('.error-message').text(data.message);
            $('.fail-modal').modal('show');
        }
    }
}

function errorMessage() {
    $('.error-message').text('');
    $('.error-message').text('An error occurred. Please try again later.');
    $('.confirmation-modal').modal('hide');
    $('.student-modal').modal('hide');
    $('.add-update').modal('hide');
    $('.fail-modal').modal('show');
}

function isBlank(str) {
    return (!str || /^\s*$/.test(str));
}

function downloadPdfFiles(documentName, data) {
    var bytes = base64ToArrayBuffer(data);
    var blob = new Blob([bytes], { type: "application/pdf" });
    var link = document.createElement("a");
    link.href = window.URL.createObjectURL(blob);
    link.download = documentName + ".pdf";
    link.click();
}

function base64ToArrayBuffer(base64) {
    var binary_string = window.atob(base64);
    var len = binary_string.length;
    var bytes = new Uint8Array(len);
    for (var i = 0; i < len; i++) {
        bytes[i] = binary_string.charCodeAt(i);
    }
    return bytes.buffer;
}

function isNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}

function saveFilterForAll() {
    showLoader(true);
    var moduleName = "";
    var filterValue = "";
    var filterName = $("#filterName").val();
    var value = $('.gridNameListing').val();
    if (value == "accountListingGridOptions") {
        filterValue = accountListingGridOptions.api.getFilterModel();
        moduleName = "account";
    }
    if (value == "studentListingGridOptions") {
        filterValue = studentListingGridOptions.api.getFilterModel();
        moduleName = "student";
    }
    if (value == "paymentListingGridOptions") {
        filterValue = paymentListingGridOptions.api.getFilterModel();
        moduleName = "payment";
    }
    if (filterName == "") {
        showLoader(false);
        $('.filterName-errormsg').text('Filter name is required');
        $("#filterName").focus();
        return false;
    }
    else {
        $.ajax({
            type: "POST",
            url: "/FilteredValue/SaveFilteredColumnsDetail",
            dataType: "json",
            data: { filterName: filterName, filterValue: JSON.stringify(filterValue), moduleName: moduleName },
            beforeSend: function () {
            },
            success: function (data) {
                showLoader(false);
                if (data.success) {
                    $('.filter-modal').modal('hide');
                    $('.success-message').text('');
                    $('.success-message').text("your filtered value saved successfully");
                    $('.sucess-modal').modal('show');
                    getSaveFilteredColumnsDetailByUserId(data.moduleId);
                }
                else {
                    if (data.isExist == true) {
                        $('.filterName-errormsg').text('Filter name already exist.');
                    }
                    else {
                        errorMessage();
                    }
                }
            },
            error: function () {
                showLoader(false);
                $('.filter-modal').modal('hide');
                errorMessage();
            }
        });
    }
};

function getSaveFilteredColumnsDetailByUserId(moduleId) {
    $.ajax({
        url: '/FilteredValue/GetFilteredValuesByUserId',
        type: 'Get',
        data: { moduleId: moduleId },
        dataType: 'json',
        success: function (data) {
            bindSavedFilter(data, moduleId);
        },
        error: function (xhr, ajaxOptions, thrownError) {
        }
    });
}

function bindSavedFilter(data, moduleId) {
    saveFilteredColumnsDetail = [];
    saveFilteredColumnsDetail = data;
    $("#tableStateFilter").html("");
    var tableStateFilterHtml = "";
    if (data.length == 0) {
        if (moduleId == "1") {
            accountListingGridOptions.api.setFilterModel();
        }
        if (moduleId == "2") {
            studentListingGridOptions.api.setFilterModel();
        }
        if (moduleId == "3") {
            paymentListingGridOptions.api.setFilterModel();
        }
        tableStateFilterHtml += '<li class="filter-dropdown-list">No saved filter found</li>'
    }
    else {
        $.each(data, function (index, item) {
            tableStateFilterHtml += '<li class="filter-dropdown-list"><a href="javascript:void(0);" onclick="bindFilteredColumnsDetail(' + item.filterdId + ',' + moduleId + ')">' + item.filterName + '</a><button class="btn btn-transparent btn-sm link-gray" type="button" data-toggle="tooltip" title="Delete" onclick="deleteFilter(' + item.filterdId + ',' + moduleId + ')"><svg width ="13" height="15" class="icon me-1"><use xlink:href="../assets/images/sprite-icons.svg#trash-icon"></use></svg></button></li>'
        });
    }
    $("#tableStateFilter").html(tableStateFilterHtml);
}

function bindFilteredColumnsDetail(filteredColumnsDetailId, moduleId) {
    var savedFilter = saveFilteredColumnsDetail.find(item => item.filterdId == filteredColumnsDetailId);
    if (savedFilter != undefined) {
        if (moduleId == "1") {
            accountListingGridOptions.api.setFilterModel(JSON.parse(savedFilter.filterValue));
        }
        if (moduleId == "2") {
            studentListingGridOptions.api.setFilterModel(JSON.parse(savedFilter.filterValue));
        }
        if (moduleId == "3") {
            paymentListingGridOptions.api.setFilterModel(JSON.parse(savedFilter.filterValue));
        }
    }
}

function deleteFilter(filterId, moduleId) {
    $('.module-name').text('filter');
    $('.confirmation-modal').modal('show');
    $('.confirmation-modal').modal({
    }).one('click', '.btn-confirmation', function (e) {
        e.preventDefault();
        showLoader(true);
        $.ajax({
            type: "Post",
            url: "/FilteredValue/DeleteFilter/",
            data: { filterId: filterId },
            dataType: 'json',
            success: function (data) {
                showLoader(false);
                $('.confirmation-modal').modal('hide');
                // method in common.js file
                displayMessage(data, 'filter');
                getSaveFilteredColumnsDetailByUserId(moduleId);
            },
            error: function () {
                showLoader(false);
                // method in common.js file
                errorMessage();
            }
        });

    });
}

function saveLayout(moduleId) {
    showLoader(true);
    var columnState = "";
    var sortState = "";
    if (moduleId == "1") {
        columnState = JSON.stringify(accountListingGridOptions.columnApi.getColumnState());
        sortState = JSON.stringify(accountListingGridOptions.api.getSortModel());
    }
    if (moduleId == "2") {
        columnState = JSON.stringify(studentListingGridOptions.columnApi.getColumnState())
        sortState = JSON.stringify(studentListingGridOptions.api.getSortModel());
    }
    if (moduleId == "3") {
        columnState = JSON.stringify(paymentListingGridOptions.columnApi.getColumnState())
        sortState = JSON.stringify(paymentListingGridOptions.api.getSortModel());
    }
    $.ajax({
        type: "POST",
        url: "/SaveState/SaveState",
        dataType: "json",
        data: { columnState: columnState, sortState: sortState, moduleId: moduleId },
        success: function (data) {
            showLoader(false);
            if (data.success) {
                $('.success-message').text('');
                $('.success-message').text("your state saved successfully");
                $('.sucess-modal').modal('show');
                getSaveStateDetailByUserId(data.moduleId);
            }
            else {
                errorMessage();
            }
        },
        error: function () {
            errorMessage();
        }
    });
}

function getSaveStateDetailByUserId(moduleId) {
    $.ajax({
        url: '/SaveState/GetSaveStateByUserId',
        type: 'Get',
        data: { moduleId: moduleId },
        dataType: 'json',
        success: function (data) {
            if (data.result != null) {
                if (moduleId == "1") {
                    accountListingGridOptions.columnApi.setColumnState(JSON.parse(data.result.columnState));
                    accountListingGridOptions.api.setSortModel(JSON.parse(data.result.sortState));
                }
                if (moduleId == "2") {
                    studentListingGridOptions.columnApi.setColumnState(JSON.parse(data.result.columnState));
                    studentListingGridOptions.api.setSortModel(JSON.parse(data.result.sortState));
                }
                if (moduleId == "3") {
                    paymentListingGridOptions.columnApi.setColumnState(JSON.parse(data.result.columnState));
                    paymentListingGridOptions.api.setSortModel(JSON.parse(data.result.sortState));
                }
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            errorMessage();
        }
    });
}

function resetLayout(moduleId) {
    showLoader(true);
    $.ajax({
        type: "POST",
        url: "/SaveState/DeleteState",
        dataType: "json",
        data: { moduleId: moduleId },
        success: function (data) {
            showLoader(false);
            if (data.success) {
                location.reload();
            }
            else {
                errorMessage();
            }
        },
        error: function () {
            showLoader(false);
            errorMessage();
        }
    });
}

function selectCheckboxOnScroll() {

}

function closeHistoryModel() {
    studentSeasonListingGridOption.api.destroy();
    $('.student-history-modal').modal('hide');
    studentSeasonListingGridOption = initAgGrid('.student-season', 'single', false, 20, studentSeassonRowSelectEvent, null, null, null, studentSeasonColumnDefs);
    getStudentSeasonListing();
}