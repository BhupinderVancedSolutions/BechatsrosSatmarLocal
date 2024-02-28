



function ChargeCard() {
    
    var IsValidExpDate = true;
    IsValidExpDate = validateExpDate();
    if ($("#charge-card").valid() && IsValidExpDate) {
        ProcessChargeCard();


    }



}
function ProcessChargeCard() {
    debugger
    var formData = $("#charge-card").serialize();
    $.ajax({
        type: "POST",
        url: "/Payment/ChargeCard",
        data: { cardKnoxDonationRequest: formData },
        dataType: "json",

        success: function (data) {

            //addCreditCardAndSendEmail(formData, data);
            if (data.status) {
                toastr.success("Payment successfully.");
            }
        },
        error: function () {

        }
    });

}
$("#ExpDate").focusout(function () {
    
    var expDate = $("#ExpDate").val();
    if (expDate.length == 4) {
        validateExpDate();
    }
});

$("#ExpDate").keyup(function () { // focusout method to bind function
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
function showHideExpDateErrorMsg() { // Show Hide Expiry Date Messages 
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


