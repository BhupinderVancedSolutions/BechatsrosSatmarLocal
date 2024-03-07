
//ag grid start
const categoryListingColumnDefs = [
    { headerName: 'City Name', field: 'cityName', filter: 'agTextColumnFilter', width: 100, flex: 1 },
    {
        headerName: 'City Price', field: 'price', filter: 'agTextColumnFilter', width: 100, flex: 1
    },
    {
        headerName: "Action", pinned: 'right',
        children:
            [
                { headerName: '', pinned: 'right', width: 52, minWidth: 52, filter: false, sortable: false, cellRenderer: (data) => { return renderCityEditAction(data); } },
                { headerName: '', pinned: 'right', width: 52, minWidth: 52, cellRenderer: (data) => { return renderCityDeleteAction(data); }, colId: "delete", filter: false, sortable: false}
            ],
    }
];
initAgGrid('.all-City', 'single', true, 20, null, null, deleteCityAgGrid, null, categoryListingColumnDefs);
getCategoryListing();

function getCategoryListing() {
    var categoryServer = new getCategoryServer();
    // create datasource with a reference to the fake server
    var datasource = new serverSideDataSource(categoryServer);
    // register the datasource with the grid
    listingGridOptions.api.setServerSideDatasource(datasource);
}

function getCategoryServer() {
    return {
        getData: async function (request) {
            return getFilteredCategoryListing(request).then(function (results) {
                return {
                    success: true,
                    rows: results.items,
                    lastRow: results.totalCount
                };
            });
        }
    };
}
function getFilteredCategoryListing(request) {
    var params = "?startRow=" + request.startRow + "&endRow=" + request.endRow + "&filterQuery=" + whereSql(request) + "&orderBy=" + orderBySql(request);

    return fetch('/CityCharge/GetCities' + params).then(function (response) {
        if (response.status == 401) {
            window.location = "/Authorize/logout";
        } else {
            return response.json();
        }
    }).then(function (json) {
        return json;
    });
}

function renderCheckBoxForCategory(currentRow) {
    if (currentRow.data.isActive == true) {
        let gui = `<label class='toggle-switch toggle-switch-md'><input checked type='checkbox' onchange="updateCategoryStatus(` + currentRow.data.categoryId + `,false)" /> <span class='slider'></span></label>`
        return gui;
    }
    else {
        let gui = `<label class='toggle-switch toggle-switch-md'><input type='checkbox' onchange="updateCategoryStatus(` + currentRow.data.categoryId + `,true)" /> <span class='slider'></span></label>`
        return gui;
    }
}


function GetAddModal() {


    $.ajax({
        type: 'GET',
        url: '/CityCharge/AddModel',
        dataType: 'html',
        success: function (data) {
            $(".all-show-City").html("");
            $(".all-show-City").html(data);
            $('.create-city-modal').modal('show');
        },
        error: function (ex) {
            $('.create-city-modal').modal('hide');
        }
    });
}


function saveUpdate() {
    debugger
    if ($('#CityName').val != "" && $('#Price').val != "") {
        var dataObj = new Object();
        var formData = new Object();
        formData.CityName = $("#CityName").val();
        formData.Price = $("#Price").val();
        var featuresArray = [];
        $(".add-feature").each(function (i, o) {
            var feature = {
                CityName: $("#CityName").val(),
                Price: $("#Price").val(),
            };
            featuresArray.push(feature);
        });
        dataObj = formData;
        dataObj.createUpdateRequestDtos = featuresArray;
        $.ajax({
            type: "POST",
            url: "/CityCharge/CreateUpdateCity",
            data: dataObj,
            dataType: "json",
            success: function (data) {
                $('.create-city-modal').modal('hide');
                ReloadGrid();
                toastr.success("City Added Successfully")
                ShowLoader(false);
            },
            error: function () {
                toastr.error("Error occurred");
                ShowLoader(false);
            },
            complete: function () {
                // ShowLoader(false);
            }
        });
    }
    else {
        if ($('#CityName').val() == "") {
            $('#CityName').addClass('error-input');
        }
        if ($('#Price').val() == "") {
            $('#Price').addClass('error-input');
        }
    }
}


function renderCityEditAction(currentRow) {
    let eGui = document.createElement("div");
    eGui.innerHTML = `<div class="" data-toggle="tooltip" title="Edit">
    <a class="btn btn-transparent btn-sm link-gray" value="Edit" onclick="editCityAgGrid(`+ currentRow.data.cityId + `);"><svg width="13" height="15" class="icon me-1"><use xlink:href="../assets/images/sprite-icons.svg#edit-icon"></use></svg></a>`;
    return eGui;
}

function editCityAgGrid(cityId) {
    editCity(cityId);
}

function editCity(cityId) {
    $.ajax({
        type: "GET",
        url: "/AdminPanel/CityCharge/EditCity",
        data: { cityId: cityId },
        dataType: "html",
        success: function (data) {
            $(".all-show-City").html("");
            $(".all-show-City").html(data);            
            $('.create-city-modal').modal('show');
            $("#addUpdateModalLabel").text("Update Catagory");
        },
        error: function () {
            $('.add-update').modal('hide');
        }
    });
}

function renderCityDeleteAction(currentRow) {
    let eGui = document.createElement("div");
    eGui.innerHTML = `<div class="" data-toggle="tooltip" title="delete">
    <a class="btn btn-transparent btn-sm link-gray" value="delete" onclick="deleteCityAgGrid(`+ currentRow.data.cityId + `);"><svg width="13" height="15" class="icon me-1"><use xlink:href="../assets/images/sprite-icons.svg#trash-icon"></use></svg></a>`;
    return eGui;
}

function deleteCityAgGrid(cityId) {
    deleteCity(cityId);
}

function deleteCity(cityId) {
    $('.confirmation-modal-city').modal('show');
    $('.confirmation-modal-city').modal({
    }).one('click', '.btn-confirmation', function (e) {
        e.preventDefault();
        $.ajax({
            type: "Post",
            url: "/AdminPanel/CityCharge/DeleteCity/",
            data: { cityId: cityId },
            dataType: 'json',
            beforeSend: function () {

            },
            success: function (data) {
                $('.confirmation-modal-city').modal('hide');

                if (data.success === true) {
                    ReloadGrid();
                    toastr.success("City deleted Successfully");
                } else {
                    toastr.error("Error occurred");
                }
            },
            error: function () {
                // method in common.js file
                errorMessage();
            }
        });

    });
}


function UpdateCity(cityId) {
    var formData = $('.city-update-form').serialize();
    formData += '&cityId=' + cityId;
    $.ajax({
        type: "POST",
        url: "/AdminPanel/CityCharge/UpdateCity",
        data: formData,
        dataType: "json",
        beforeSend: function () {
            showLoader(true);
        },
        success: function (data) {
            if (data.success === true) {
                ReloadGrid();
                toastr.success("City Updated Successfully");
                $('.create-city-modal').modal('hide');
            } else {
                toastr.error("Error occurred");
            }
        },
        error: function () {
            toastr.error("Error occurred");
        }
    });
}
function ReloadGrid() {
    listingGridOptions.api.refreshServerSideStore({ purge: true });
}


function deleteFilter(filterId, moduleId) {
    $('.module-name').text('filter');
    $('.confirmation-modal-city').modal('show');
    $('.confirmation-modal-city').modal({
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