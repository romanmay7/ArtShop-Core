//alert("Hello");

//var theForm = document.getElementById("theForm");
//theForm.hidden = true;

var button = document.getElementById("buyButton");
button.addEventListener("click", function () {
    alert("Buying Item");
});


$(document).ready(function () {


    var $loginToggle = $("#loginToggle");
    var $popupForm = $(".popup-form");

    $loginToggle.on("click", function () {
        $popupForm.slideToggle(1000);
    });

});

