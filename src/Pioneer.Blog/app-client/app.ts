$(() => {
    /**
     * FOUT fighter
     */
    $('.title-bar').removeClass('hide');
});

/**
 * Cache navigation
 */
var header = $('.nav-main');

/**
 * Manage transparent/un-transparent navigation
 */
$(window).on('scroll', () => {
    var top = header.offset().top;
    if (top >= 100) {
        header.addClass('nav-main-show');
    } else {
        header.removeClass('nav-main-show');
    } 
});

/**
 * Trigger scroll event to handle middle of page loads for navigation
 */
$(window).trigger('scroll');

/**
 * Handle click event
 */
var closed = true;
var menu = $('.mobile-nav');
$('#hamburger').click(() => {
    //if (this.closed) {
    //    closed = false;
    //    menu.addClass('show-for-small-only');
    //    menu.removeClass('hide');
    //    return;
    //}

    menu.toggleClass('show-for-small-only');
    menu.toggleClass('hide');

    //menu.removeClass('show-for-small-only');
    //menu.addClass('hide');
    //this.closed = true;
});
