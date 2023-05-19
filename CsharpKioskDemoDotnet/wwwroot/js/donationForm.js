class DonationForm {
    paymentArray;
    maximumDonation

    constructor(
        paymentArray,
        maximumDonation
    ) {
        this.paymentArray = paymentArray;
        this.maximumDonation = maximumDonation
    }

    submitForm() {
        var $price = $("#price").val()
        var isValid = true;
        $(".inputField").each(function () {
            var element = $(this);
            if (element.prop('required') && element.val() == "") {
                isValid = false;
            }
        });
        if (!isValid || $("#buyerRegion").val() == "-") {
            alert('Please fill in all fields')
            return
        }
        if ($price > this.maximumDonation) {
            alert("Your donation was larger than the allowed maximum of " + Number(this.maximumDonation).toFixed(2))
            return
        }
        if (isNaN($price) || $price == '') {
            alert('Please enter a donation amount')
            return
        }

        document.getElementById("donateForm").submit()
    }

    updateVal(val) {
        //monitors the clicks and/or the input field, handles css updatess
        $(".payment").each(function () {
            $(this).removeClass("selectedPayment");
        });
        if (this.paymentArray.includes(val)) {
            $("#payment_" + val).addClass('selectedPayment')
        } else {
            $("#payment_other").addClass('selectedPayment')
        }
        $("#price").val(val)
    }

    updateCSS() {
        //called when using the OTHER input field
        $(".payment").each(function () {
            $(this).removeClass("selectedPayment");
        });
        $("#payment_other").addClass('selectedPayment')
    }
}