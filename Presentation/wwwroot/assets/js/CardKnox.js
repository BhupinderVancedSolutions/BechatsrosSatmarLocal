





function addNewform() { 
    debugger
    $.ajax({
        type: "GET",
        url: "/CardKnox/CardKnoxDonationform",
        dataType: "html",
        success: function (data) {
            $("#donation-method").html("");
            $("#donation-method").html(data);
            $(".mask-number").inputmask("(999) 999-9999");
        },
        error: function () {
            //ShowLoader(false);
        }
    });
}

//function pay() {
//    debugger
//    var formData = $("#donation-method").serialize();
//    $.ajax({
//        type: "POST",
//        url: "/CardKnox/CardKnoxDonation",
//        data: { cardKnoxDonationRequest: formData },
//        dataType: "json",
//        success: function (data) {

//        }

//    });
//}

function pay() {
    debugger
    if ($("#donation-method").valid()) {
        var formData = $("#donation-method").serializeJSON();
        $.ajax({
            type: "POST",
            url: "/CardKnox/CardKnoxDonation",
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
}



function addCreditCardAndSendEmail(formData, data) {
    debugger // Send email after donation and saved entered card
  /*  if (data.isPaymentProviderExist) {*/
        $.ajax({
            type: "POST",
            url: "/CardKnox/AddSendDonationEmail",
            data: { cardKnoxDonationRequest: formData, transactionId: data.transactionId, isTransactionSucceeded: data.status, message: data.message },
            dataType: "json",
            success: function (data) { },
            error: function (data) { }
        });
    //}
}


//function pay() {
//    var formData = $("#donation-method").serialize();
//    $.ajax({
//        type: "POST",
//        url: "/CardKnox/CardKnoxDonation",
//        data: { cardKnoxDonationRequest: formData },
//        dataType: "json",
//        //beforeSend: function () {
//        //    $(".donation-processing-modal").modal("show");
//        //    $(".donation-organization-src").attr("src", $("#hdnLogoPath").val());
//        //    $(".donation-organization").text($("#hdnOrganizationName").val());
//        //    $(".donation-amount").text(formData.donationAmount);
//        //},
//        success: function (data) {

//            addCardAndSendDonationEmail(formData, data);
//            if (data.status) {
//                //$(".donation-processing-modal").modal("hide");
//                //$(".donation-successful-modal").modal("show");
//                //ClearTexboxesValue();
//                //$(".gray-bg").removeClass("active");
//                //$(".spn-pay-amount").text("");
//                //$("#hdnAmount").val(0);
//                //$(".toggle-box1").show();
//                //$(".toggle-box2").hide();
//            }
//            //else {
//            //    $(".donation-error").text("");
//            //    $(".donation-processing-modal").modal("hide");
//            //    $(".donation-error-modal").modal("show");
//            //    $(".donation-error").text(data.message);
//            //    $(".toggle-box1").show();
//            //    $(".toggle-box2").hide();
//            //    ClearTexboxesValue();
//            //    setTimeout(function () {
//            //        $(".donation-error-modal").modal("hide");
//            //    }, 4000);
//            //}
//        },
//        error: function () {

//        }
//    });
//}