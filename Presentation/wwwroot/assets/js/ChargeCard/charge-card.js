function addNewform() { 
    debugger
    $.ajax({
        type: "GET",
        url: "/Payment/ChargeCardform",
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



function ChargeCard() {
    if ($(".charge-card").valid()) {
        ProcessChargeCard();
    
    
    }
    
   
 
}
function ProcessChargeCard() {
   
        var formData = $("#donation-method").serializeJSON();
        $.ajax({
            type: "POST",
            url: "/Payment/ChargeCardDonation",
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

function Checkbox() {
    debugger
}




