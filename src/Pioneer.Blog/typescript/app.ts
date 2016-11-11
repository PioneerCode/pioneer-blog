/// <reference path="typings/tsd.d.ts" />
$(() => {
  $(document).foundation();
  $(".title-bar").removeClass("hide");
});

var header = $(".nav-main");

$(window).on("scroll", () => {
  var top = header.offset().top;
  if (top >= 100) {
    header.addClass("nav-main-show");
  } else {
    header.removeClass("nav-main-show");
  }
});

$(window).trigger("scroll");