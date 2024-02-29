﻿$(document).ready(function () {
    $(".form-check-input").prop("checked", true);
});


function ChargeCard() {debugger
    ShowLoader(true, 8000);
    var IsValidExpDate = true;
    IsValidExpDate = validateExpDate();
    if ($("#charge-card").valid() && IsValidExpDate) {

        var expDate = $("#ExpDate").val();
        if (expDate.length == 4)
        {            
            $("#ExpMonth").val(expDate.substr(0, 2));
            $("#ExpYear").val("20" + expDate.substr(2, 2));            
        }
        ProcessChargeCard();
    }



}
function ProcessChargeCard() {
    var formData = $("#charge-card").serializeArray();
    $.ajax({
        type: "POST",
        url: "/Payment/ChargeCard",
        data: formData,
        dataType: "json",
        success: function (data) {
            if (data.status) {
                toastr.success("Payment successful.");
            }
        },
        error: function () {
        },
        complete: function () {
            HideLoader();
        }
    });
}

function ShowLoader(isShowLoader, timeLimit) {
    debugger;
    if (isShowLoader) {
        $(".loader").show();
        
        setTimeout(function () {
            $(".loader").hide();
        }, timeLimit);
    } else {
        $(".loader").hide();
    }
}




$("#ExpDate").focusout(function () {
    
    var expDate = $("#ExpDate").val();
    if (expDate.length == 4) {
        validateExpDate();
    }
});

$("#ExpDate").keyup(function () { 
    $(".error-msg").addClass("d-none");
});
function validateExpDate() {
    
    var isValid = true;
    var date = $("#ExpDate").val();
    if (date != "") {
        var month = date.slice(0, 2);
        var year = date.substr(-2);
        var d = new Date();
        var currentmonth = d.getMonth() + 1;
        var currentYear = d.getFullYear();
        var newYear = currentYear.toString().substr(-2);
        if (month <= "12") {
            if (year < newYear) {
                $(".error-msg").removeClass("d-none");
                $(".error-msg").text("Please enter a valid exp date.");
                isValid = false;
            }
            else if (year == newYear) {
                if (month < currentmonth) {
                    $(".error-msg").removeClass("d-none");
                    $(".error-msg").text("Please enter a valid exp date.");
                    isValid = false;
                }
            }
        }
        else if (month > "12") {
            $(".error-msg").removeClass("d-none");
            $(".error-msg").text("Please enter a valid exp date.");
            isValid = false;
        }
        return isValid;
    }
    else {
        return isValid;
    }
}
function isNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}
function showHideExpDateErrorMsg() { 
    if ($.trim($(".exp-date").val()) != '') {
        $(".error-msg").addClass("d-none");
    }
}
function expDate() {
    var expDate = $("#ExpDate").val();
    if (expDate.length == 4) {
        IsValidExpiryDate = validateExpiryDate();
        if (IsValidExpiryDate == true) {
            $('.make-payment').prop("disabled", false);
        }
        else {
            $('.make-payment').prop("disabled", true);
        }
    }
    else {
        $(".error-msg").addClass("d-none");
    }
}

$(".form-check-input").on("change", function () {
    if ($(this).prop("checked")) {
        $(".showhide-CheckBox").show();
    } else {
        $(".showhide-CheckBox").hide();
    }
});
$(document).ready(function () {
    $("#resetBtn").click(function () {
       $("#charge-card")[0].reset();
    });
});






