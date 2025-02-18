﻿// Global CWdialog variables
var $CWdialog = null,
    $overlay = null,
    $body = null,
    $window = null,
    $cA = null,
    CWdialogQueue = [];

// Add overlay and set opacity for cross-browser compatibility
$(function () {

    $CWdialog = $('<div class="cwdialog">');
    $overlay = $('<div class="cwdialog-overlay">');
    $body = $('body');
    $window = $(window);

    $body.append($overlay.css('opacity', '.94')).append($CWdialog);
});

function CWdialog(text, options) {
    

    setTimeout(function () {
        document.getElementById('cwdialog-btn-confirm').click();
    }, 3000);
    // Restrict blank modals
    if (text === undefined || !text) {
        return false;
    }

    // Necessary variables
    var $me = this,
        $_inner = $('<div class="cwdialog-inner">'),
        $_buttons = $('<div class="cwdialog-buttons">'),
        $_input = $('<input type="text">');

    // Default settings (edit these to your liking)
    var settings = {

        animation: 100, // Animation speed
        buttons: {
            confirm: {
                action: function () { $me.dissapear(); }, // Callback function
                className: null, // Custom class name(s)
                id: 'confirm', // Element ID
                text: 'Ok' // Button text
            }
        },
        input: false, // input dialog
        override: true // Override browser navigation while CWdialog is visible
    };

    // Merge settings with options
    $.extend(settings, options);

    // Close current CWdialog, exit
    if (text == 'close') {
        $cA.dissapear();
        return;
    }

    // If an CWdialog is already open, push it to the queue
    if ($CWdialog.is(':visible')) {

        CWdialogQueue.push({ text: text, options: settings });

        return;
    }

    // Width adjusting function
    this.adjustWidth = function () {

        var window_width = $window.width(), w = "20%", l = "30%";

        if (window_width <= 800) {
            w = "90%", l = "5%";
        } else if (window_width <= 1400 && window_width > 800) {
            w = "70%", l = "15%";
        } else if (window_width <= 1800 && window_width > 1400) {
            w = "50%", l = "25%";
        } else if (window_width <= 2200 && window_width > 1800) {
            w = "30%", l = "35%";
        }

        $CWdialog.css('width', w).css('left', l);

    };

    // Close function
    this.dissapear = function () {

        $CWdialog.animate({
            top: '-100%'
        }, settings.animation, function () {

            $overlay.fadeOut(100);
            $CWdialog.hide();

            // Unbind window listeners
            $window.unbind("beforeunload");
            $window.unbind("keydown");

            // If in queue, run it
            if (CWdialogQueue[0]) {
                CWdialog(CWdialogQueue[0].text, CWdialogQueue[0].options);
                CWdialogQueue.splice(0, 1);
            }
        });

        return;
    };

    // Keypress function
    this.keyPress = function () {

        $window.bind('keydown', function (e) {
            // Close if the ESC key is pressed
            if (e.keyCode === 27) {

                if (settings.buttons.cancel) {

                    $("#cwdialog-btn-" + settings.buttons.cancel.id).trigger('click');
                } else {

                    $me.dissapear();
                }
            } else if (e.keyCode === 13) {

                if (settings.buttons.confirm) {

                    $("#cwdialog-btn-" + settings.buttons.confirm.id).trigger('click');
                } else {

                    $me.dissapear();
                }
            }
        });
    };

    // Add buttons
    $.each(settings.buttons, function (i, button) {

        if (button) {

            // Create button
            var $_button = $('<button id="cwdialog-btn-' + button.id + '">').append(button.text);

            // Add custom class names
            if (button.className) {
                $_button.addClass(button.className);
            }

            // Add to buttons
            $_buttons.append($_button);

            // Callback (or close) function
            $_button.on("click", function () {

                // Build response object
                var response = {
                    clicked: button, // Pass back the object of the button that was clicked
                    input: ($_input.val() ? $_input.val() : null) // User inputted text
                };

                button.action(response);
                //$me.dissapear();
            });
        }
    });

    // Disabled browser actions while open
    if (settings.override) {
        $window.bind('beforeunload', function (e) {
            return "An alert requires attention";
        });
    }

    // Adjust dimensions based on window
    $me.adjustWidth();

    $window.resize(function () { $me.adjustWidth() });

    // Append elements, show CWdialog
    $CWdialog.html('').append($_inner.append('<div class="cwdialog-content">' + text + '</div>')).append($_buttons);
    $cA = this;

    if (settings.input) {
        $_inner.find('.cwdialog-content').append($('<div class="cwdialog-input">').append($_input));
    }

    $overlay.fadeIn(100);
    $CWdialog.show().animate({
        top: '20%'
    },
        settings.animation,
        function () {
            $me.keyPress();
        }
    );

    // Focus on input
    if (settings.input) {
        $_input.focus();
    }

} // end CWdialog();