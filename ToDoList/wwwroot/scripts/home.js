
$(document).ready(function () {
    initEvents();

    //init default
    openTab("login");
});

function initEvents() {
    $(".tablinks").on("click", function () {
        openTab($(this).data("tab"));
    });

    $("#loginButton").on("click", function() {
        var email = $("#loginForm").find("input[name='email']").val();
        var password = $("#loginForm").find("input[name='password']").val();
        login(email, password);
    });

    $('#loginForm').keypress(function (e) {
        if (e.which == 13) {
            $("#loginButton").click();
        }
    });

    $('#signupForm').keypress(function (e) {
        if (e.which == 13) {
            $("#signupButton").click();
        }
    });

    $("#signupButton").on("click", function () {
        var name = $("#signupForm").find("input[name='name']").val();
        var email = $("#signupForm").find("input[name='email']").val();
        var password = $("#signupForm").find("input[name='password']").val();
        var repeatPassword = $("#signupForm").find("input[name='repeatPassword']").val();

        if (validateSignupData(name, email, password, repeatPassword)) {
            signup(name, email, password);
        }
    });

    $("input").on("change paste keyup", function () {
        $(this).removeClass("error");
    });
}

function openTab(name) {
    $(".tabcontent").hide();
    $(".tabcontent[data-tab='" + name + "']").show();

    $(".tablinks").removeClass("active");
    $(".tablinks[data-tab='" + name + "']").addClass("active");
}

function login(email, password) {
    var params = "email=" + email + "&password=" + password;
    $.ajax({
        type: 'POST',
        url: '/login',
        data: params,
        dataType: 'xml',
        success: function (data) {
            window.location = "/";
        },
        error: function (error) {
            displayError("Incorrect Login or Password. Please try again.");
        }
    });
}

function signup(name, email, password) {
    var user = {};
    user.Name = name;
    user.Email = email;
    user.Password = password;
    $.ajax({
        type: 'POST',
        url: '/signup',
        data: user,
        dataType: 'xml',
        success: function(data) {
            window.location = "/";
        },
        error: function(error) {
            displayError("Unexpected error while trying to Sign Up. Please try again.");
        }
    });
}

function validateSignupData(name, email, password, repeatPassword) {
    var invalidFields = [];

    var nameField = $("#signupForm").find("input[name='name']");
    var emailField = $("#signupForm").find("input[name='email']");
    var passwordField = $("#signupForm").find("input[name='password']");
    var repeatPasswordField = $("#signupForm").find("input[name='repeatPassword']");

    var validationPassed = true;

    // validete name field
    if (name === "") {
        invalidFields.push(nameField);
        validationPassed = false;
    }

    // validate email field
    if (email === "" || !validateEmail(email)) {
        invalidFields.push(emailField);
        validationPassed = false;
    }

    // validate password field
    if (password.length < 3) {
        invalidFields.push(passwordField);
        validationPassed = false;
    }

    // validate repeatPassword field
    if (repeatPassword !== password) {
        invalidFields.push(passwordField);
        invalidFields.push(repeatPasswordField);
        validationPassed = false;
    }

    for (var i = 0; i < invalidFields.length; i++) {
        invalidFields[i].addClass("error");
    }
    if (invalidFields.length > 0) {
        invalidFields[0].focus();
    }

    return validationPassed;
}

function validateEmail(email) {
    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}

function displayError(message) {
    var html =
        '<div class="alert alert-danger alert-dismissible">' +
            '<a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>' +
            message +
            '</div>';
    $("#message-container").html(html);
}