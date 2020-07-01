"use strict";

// Class Definition
var KTLoginGeneral = function() {

    var login = $('#kt_login');

    var showErrorMsg = function(form, type, msg) {
        var alert = $('<div class="mb-10 alert alert-custom alert-light-' + type + ' alert-dismissible" role="alert">\
			<div class="alert-text font-weight-bold ">'+msg+'</div>\
			<div class="alert-close">\
                <i class="ki ki-remove" data-dismiss="alert"></i>\
            </div>\
		</div>');

        form.find('.alert').remove();
        alert.prependTo(form);
        //alert.animateClass('fadeIn animated');
        KTUtil.animateClass(alert[0], 'fadeIn animated');
        alert.find('span').html(msg);
    }

    // Private Functions
    var displaySignUpForm = function() {
        login.removeClass('login-forgot-on');
        login.removeClass('login-signin-on');

        login.addClass('login-signup-on');
        KTUtil.animateClass(login.find('.login-signup')[0], 'flipInX animated');
    }

    var displaySignInForm = function() {
        login.removeClass('login-forgot-on');
        login.removeClass('login-signup-on');

        login.addClass('login-signin-on');
        KTUtil.animateClass(login.find('.login-signin')[0], 'flipInX animated');
        //login.find('.login-signin').animateClass('flipInX animated');
    }

    var displayForgotForm = function() {
        login.removeClass('login-signin-on');
        login.removeClass('login-signup-on');

        login.addClass('login-forgot-on');
        //login.find('.login-forgot-on').animateClass('flipInX animated');
        KTUtil.animateClass(login.find('.login-forgot')[0], 'flipInX animated');

    }

    var handleFormSwitch = function() {
        $('#kt_login_forgot').click(function(e) {
            e.preventDefault();
            displayForgotForm();
        });

        $('#kt_login_forgot_cancel').click(function(e) {
            e.preventDefault();
            displaySignInForm();
        });

        $('#kt_login_signup').click(function(e) {
            e.preventDefault();
            displaySignUpForm();
        });

        $('#kt_login_signup_cancel').click(function(e) {
            e.preventDefault();
            displaySignInForm();
        });
    }

    

    // Public Functions
    return {
        // public functions
        init: function() {
            handleFormSwitch();
        }
    };
}();

// Class Initialization
jQuery(document).ready(function() {
    KTLoginGeneral.init();
});
