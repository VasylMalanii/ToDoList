﻿$(document).ready(function () {
    $("#logOutButton").on("click", function(){
    $.ajax({
    type: 'POST',
    url: '/logout',
    dataType: 'xml',
    success: function (data) {
        window.location = "/home";
    }
});
});
});