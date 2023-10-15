



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

$('#myCarousel').on('slide.bs.carousel', function (e) {
  var id = parseInt($(e.relatedTarget).attr('data-slide-number'));
  $('[id^=carousel-selector-]').removeClass('selected');
  $('[id=carousel-selector-' + id + ']').addClass('selected');
});

$('#myCarousel .carousel-item img').on('click', function (e) {
  var src = $(e.target).attr('data-remote');
  if (src) $(this).ekkoLightbox();
});


$(document).on('toggle.bs.modal', '.modal fade', function () {
  $('.modal:visible').length && $(document.body).addClass('modal-open');
});


$(document).ready(function() {
  var myModal = new bootstrap.Modal(document.getElementById('exampleModal'));

  myModal.show();
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
                window.location.reload();
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
        window.location.reload();
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

