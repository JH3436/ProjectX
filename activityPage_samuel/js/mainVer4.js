


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
// if ($('#carousel-thumbs .carousel-item').length < 2) {
//   $('#carousel-thumbs [class^=carousel-control-]').remove();
//   $('.machine-carousel-container #carousel-thumbs').css('padding', '0 5px');
// }
// when the carousel slides, auto update
$('#myCarousel').on('slide.bs.carousel', function (e) {
  var id = parseInt($(e.relatedTarget).attr('data-slide-number'));
  $('[id^=carousel-selector-]').removeClass('selected');
  $('[id=carousel-selector-' + id + ']').addClass('selected');
});
// when user swipes, go next or previous
// $('#myCarousel').swipe({
//   fallbackToMouseEvents: true,
//   swipeLeft: function (e) {
//     $('#myCarousel').carousel('next');
//     //   console.log("swipeLeft");
//   },
//   swipeRight: function (e) {
//     $('#myCarousel').carousel('prev');
//     //   console.log("swipeRight");
//   },
//   allowPageScroll: 'vertical',
//   preventDefaultEvents: false,
//   threshold: 75
// });

  // $(document).on('click', '[data-toggle="lightbox"]', function(event) {
  //   event.preventDefault();
  //   $(this).ekkoLightbox();
  // });


$('#myCarousel .carousel-item img').on('click', function (e) {
  var src = $(e.target).attr('data-remote');
  if (src) $(this).ekkoLightbox();
});


$(document).on('toggle.bs.modal', '.modal fade', function () {
  $('.modal:visible').length && $(document.body).addClass('modal-open');
});

// var myModalEl = document.getElementById('exampleModal')
// myModalEl.addEventListener('hidden.bs.modal', function (event) {
//   console.log("??");
//   window.location.reload();
// })
$(document).ready(function() {
  $('#exampleModal').modal('show');  // 顯示 Bootstrap Modal
});
// 聊天室
$(document).ready(function(){
  $("#discussBtn").click(function(){
    $("#discussInput").toggle();

  })
});

 
$(document).on("click", ".replyBtn", function(){
    $($(this).parent().parent().children(".replyTextDiv")).toggle();
  });

 


  $(document).ready(function() {
    // 監聽 #discussTextArea 元素的 keydown 事件
    $("#discussTextArea").on("keydown", function(event) {
        if (event.key === "Enter") {
            event.preventDefault();  // 阻止預設的 Enter 鍵行為
            simulateClick();  // 呼叫模擬 click 函式
        }
    });

    $(".publishBtn").click(simulateClick);  // 點擊 publishBtn 也呼叫模擬 click 函式

    function simulateClick() {
        var temp = $("#discussTextArea").val();
        if (temp == "") {
            alert("請輸入文字");
        } else {
            var sure = confirm("確定提交嗎討論\n\n" + $("#discussTextArea").val());
            if (sure == true) {
                $("#dialogDiv").append(
                    `<div class="commentDiv">
                        <div class="userCommentDiv">
                            <img class="profile" src="./Files/godtone.png" />
                            <div class="userCommentDivRight">
                                <p class="h3 align-self-center">嘎痛</p>
                                <div class="comment-box align-self-start">` + temp + `</div>
                            </div>
                        </div>
                        <div class="commentBtnDiv">
                            <div class="replyBtn" id="replyBtn" >
                                <p class="h3">回覆</p>
                                <i class="fa-solid fa-comment fa-2xl align-self-center"></i>
                            </div>
                        </div>
                        <div class="replyTextDiv">
                            <div class="userCommentDiv">
                                <img class="profile" src="./Files/godtone.png" />
                                <div class="userCommentDivRight">
                                    <p class="h3 align-self-center">嘎痛</p>
                                    <textarea name="" id="replyTextArea" cols="col-auto" rows="1"></textarea>
                                    <div class="messageBtn">
                                        <a class="messageBtn-text-style" href="#">
                                            留言
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>`
                );
                location.reload();
                $('#exampleModal').modal('show');
            }
        }
    }
});

$(document).on("click", ".messageBtn", function() {
  var temp = $(this).parent().children("#replyTextArea").val();
  if (temp == "") {
      alert("請輸入文字");
  } else {
      var sure = confirm("確定提交留言\n\n" + temp);
      if (sure) {
          $($(this).parent().parent().parent()).before(
              `<div class="replyDiv">
                  <div class="userCommentDiv">
                      <img class="profile" src="./Files/godtone.png" />
                      <div class="userCommentDivRight">
                          <p class="h3 align-self-center">嘎痛</p>
                          <div class="comment-box align-self-start">` + temp + `</div>
                      </div>
                  </div>
              </div>`
          );
          
      }
  }
});



// 在 #replyTextArea 上監聽 keydown 事件
$(document).on("keydown", "#replyTextArea", function(event) {
  if (event.key === "Enter") {
      event.preventDefault();  // 阻止預設的 Enter 鍵行為
      $(this).siblings(".messageBtn").click();  // 模擬點擊 .messageBtn
  }
});

