/**
 * Manage transparent/un-transparent navigation
 */
var header = document.getElementById('nav-main') as any;
window.onscroll = () => {
    if (window.pageYOffset >= 100) {
        header.classList.add('nav-main-show');
    } else {
        header.classList.remove('nav-main-show');
    }
};

/**
 * Trigger scroll event to handle middle of page loads for navigation
 */
window.dispatchEvent(new Event('scroll'));

/**
 * Handle click event
 */
var closed = true;
var menu = document.getElementById('mobile-nav') as any;
function hamburgerClick() {
    menu.classList.toggle('show-for-small-only');
    menu.classList.toggle('hide');
}
