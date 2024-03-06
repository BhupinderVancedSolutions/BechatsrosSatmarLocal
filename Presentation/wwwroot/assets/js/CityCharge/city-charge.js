

//ag grid start
const categoryListingColumnDefs = [
    { headerName: 'City Name', field: 'cityName', filter: 'agTextColumnFilter', width: 100, flex: 1 },
    {
        headerName: 'City Price', field: 'cityPrice', filter: 'agTextColumnFilter', width: 100, flex: 1, cellRenderer: (data) => {
            return renderPercentageAction(data.value)
        }
    },
 
    {
        //headerName: "Action", pinned: 'right',
        //children:
        //    [
        //        { headerName: '', pinned: 'right', width: 52, minWidth: 52, filter: false, sortable: false, cellRenderer: (data) => { return renderCategoryEditAction(data); } },
        //        { headerName: '', pinned: 'right', width: 52, minWidth: 52, cellRenderer: rendererDeleteAction, colId: "delete", filter: false, sortable: false }
        //    ],
    }

];
initAgGrid('.all-City', 'single', true, 20, null, null, null, null, categoryListingColumnDefs);
getCategoryListing();
//listingGridOptions = initAgGrid('.all-City', 'single', true, 20, null, null, null, null, categoryListingColumnDefs);
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

function createCategory() {
    $.ajax({
        type: 'GET',
        url: '/AdminSetting/Category/CreateCategory',
        dataType: 'html',
        success: function (data) {
            $(".div-modal").html("");
            $(".div-modal").html(data);
            $('.add-update').modal('show');
            $("#addUpdateModalLabel").text("Add Catagory");
        },
        error: function (ex) {
            $('.add-update').modal('hide');
        }
    });
}


function renderCategoryEditAction(currentRow) {
    let eGui = document.createElement("div");
    eGui.innerHTML = `<div class="" data-toggle="tooltip" title="Edit">
    <a class="btn btn-transparent btn-sm link-gray" value="Edit" onclick="editCategoryAgGrid(`+ currentRow.data.categoryId + `);"><svg width="13" height="15" class="icon me-1"><use xlink:href="../assets/images/sprite-icons.svg#edit-icon"></use></svg></a>`;
    return eGui;
}

function editCategoryAgGrid(categoryId) {
    editCategory(categoryId);
}

function editCategory(categoryId) {
    $.ajax({
        type: "GET",
        url: "/AdminSetting/Category/EditCategory",
        data: { categoryId: categoryId },
        dataType: "html",
        success: function (data) {
            $(".div-modal").html("");
            $(".div-modal").html(data);
            $('.add-update').modal('show');
            $("#addUpdateModalLabel").text("Update Catagory");
        },
        error: function () {
            $('.add-update').modal('hide');
        }
    });
}

function deleteCategoryAgGrid(selectedMember) {
    deleteCategory(selectedMember.categoryId);
}

function deleteCategory(categoryId) {
    $('.module-name').text('category');
    $('.confirmation-modal').modal('show');
    $('.confirmation-modal').modal({
    }).one('click', '.btn-confirmation', function (e) {
        e.preventDefault();
        $.ajax({
            type: "Post",
            url: "/AdminSetting/Category/DeleteCategory/",
            data: { categoryId: categoryId },
            dataType: 'json',
            beforeSend: function () {

            },
            success: function (data) {
                $('.confirmation-modal').modal('hide');
                // method in common.js file
                displayMessage(data, 'category');
            },
            error: function () {
                // method in common.js file
                errorMessage();
            }
        });

    });
}

function updateCategoryStatus(categoryId, status) {
    $.ajax({
        type: "Post",
        url: "/AdminSetting/Category/UpdateCategoryStatus/",
        data: { categoryId: categoryId, status: status },
        dataType: 'json',
        success: function (data) {
            // method in common.js file
            displayMessage(data, 'category');
        },
        error: function () {
            // method in common.js file
            errorMessage();
        }
    });
}

