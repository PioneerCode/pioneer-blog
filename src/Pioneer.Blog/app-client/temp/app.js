$(function () {
    var doc = $(document);
    doc.foundation();
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
$(window).on('scroll', function () {
    var top = header.offset().top;
    if (top >= 100) {
        header.addClass('nav-main-show');
    }
    else {
        header.removeClass('nav-main-show');
    }
});
/**
 * Trigger scroll event to handle middle of page loads for navigation
 */
$(window).trigger('scroll');
