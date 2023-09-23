


//輪播圖的JS好像會吃其他的，所以我放在最上面

//愛心設計
$(document).ready(function () {
  // 監聽心形圖標的點擊事件
  $(".heart-icon").click(function () {
      // 切換圖標的類名和樣式
      var heartIcon = $(this).find("i");
      if (heartIcon.hasClass("fa-regular fa-heart")) {
          heartIcon.removeClass("fa-regular fa-heart").addClass("fa-solid fa-heart").css("color", "#b44163");
      } else {
          heartIcon.removeClass("fa-solid fa-heart").addClass("fa-regular fa-heart").css("color", "");
      }
  });
});


// $('#myCarousel').carousel({
//     interval: false
//   });
//   $('#carousel-thumbs').carousel({
//     interval: false
//   });

// handles the carousel thumbnails
// https://stackoverflow.com/questions/25752187/bootstrap-carousel-with-thumbnails-multiple-carousel
$('[id^=carousel-selector-]').click(function () {
  var id_selector = $(this).attr('id');
  var id = parseInt(id_selector.substr(id_selector.lastIndexOf('-') + 1));
  $('#myCarousel').carousel(id);
});
// Only display 3 items in nav on mobile.
if ($(window).width() < 575) {
  $('#carousel-thumbs .row div:nth-child(4)').each(function () {
    var rowBoundary = $(this);
    $('<div class="row mx-0">').insertAfter(rowBoundary.parent()).append(rowBoundary.nextAll().addBack());
  });
  $('#carousel-thumbs .carousel-item .row:nth-child(even)').each(function () {
    var boundary = $(this);
    $('<div class="carousel-item">').insertAfter(boundary.parent()).append(boundary.nextAll().addBack());
  });
}
// Hide slide arrows if too few items.
if ($('#carousel-thumbs .carousel-item').length < 2) {
  $('#carousel-thumbs [class^=carousel-control-]').remove();
  $('.machine-carousel-container #carousel-thumbs').css('padding', '0 5px');
}
// when the carousel slides, auto update
$('#myCarousel').on('slide.bs.carousel', function (e) {
  var id = parseInt($(e.relatedTarget).attr('data-slide-number'));
  $('[id^=carousel-selector-]').removeClass('selected');
  $('[id=carousel-selector-' + id + ']').addClass('selected');
});
// when user swipes, go next or previous
$('#myCarousel').swipe({
  fallbackToMouseEvents: true,
  swipeLeft: function (e) {
    $('#myCarousel').carousel('next');
    //   console.log("swipeLeft");
  },
  swipeRight: function (e) {
    $('#myCarousel').carousel('prev');
    //   console.log("swipeRight");
  },
  allowPageScroll: 'vertical',
  preventDefaultEvents: false,
  threshold: 75
});

//   $(document).on('click', '[data-toggle="lightbox"]', function(event) {
//     event.preventDefault();
//     $(this).ekkoLightbox();
//   });


$('#myCarousel .carousel-item img').on('click', function (e) {
  var src = $(e.target).attr('data-remote');
  if (src) $(this).ekkoLightbox();
});


