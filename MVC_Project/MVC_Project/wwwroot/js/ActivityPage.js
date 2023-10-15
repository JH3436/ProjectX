
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
    console.log("id_selector:" +id_selector);
    console.log("id:" +id);
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
    console.log(id)
});



$('#myCarousel .carousel-item img').on('click', function (e) {
    var src = $(e.target).attr('data-remote');
    if (src) $(this).ekkoLightbox();
    console.log("$('#myCarousel .carousel-item img').on('click'")
});


$(document).on('toggle.bs.modal', '.modal fade', function () {
    $('.modal:visible').length && $(document.body).addClass('modal-open');
});

$(document).ready(function() {
  var myModal = new bootstrap.Modal(document.getElementById('exampleModal'));

  myModal.show();
});

// 聊天室
$(document).ready(function () {
    $("#discussBtn").click(function () {
        $("#discussInput").toggle();

    })
});


$(document).on("click", ".replyBtn", function () {
    $($(this).parent().parent().children(".replyTextDiv")).toggle();
});




$(document).ready(function () {
    // 監聽 #discussTextArea 元素的 keydown 事件
    $("#discussTextArea").on("keydown", function (event) {
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
        }
    }
});

$(document).on("click", ".messageBtn", function () {
    var temp = $(this).parent().children("#replyTextArea").val();
    if (temp == "") {
        alert("請輸入文字");
    } else {
        var sure = confirm("確定提交留言\n\n" + temp);
    }
});



// 在 #replyTextArea 上監聽 keydown 事件
$(document).on("keydown", "#replyTextArea", function (event) {
    if (event.key === "Enter") {
        event.preventDefault();  // 阻止預設的 Enter 鍵行為
        $(this).siblings(".messageBtn").click();  // 模擬點擊 .messageBtn
    }
});


/*收藏API*/
const heartIcons = document.querySelectorAll('.heart-icon');

heartIcons.forEach(function (heartIcon) {
    heartIcon.addEventListener('click', function () {
        const activityId = heartIcon.getAttribute('data-activityid');
        if (heartIcon.classList.contains('fa-regular')) {
            // 在此處執行AJAX POST請求，將activityId和userID（在這裡假設為1）發送到後端
            $.ajax({
                type: 'POST',
                url: '/MyActivity/LikeActivity',
                data: {
                    activityId: activityId,
                    userId: 1
                },
                success: function (data) {
                    console.log("處理成功的回應，可以更新UI或執行其他操作")
                    // 處理成功的回應，可以更新UI或執行其他操作
                    heartIcon.classList.remove('fa-regular');
                    heartIcon.classList.add('fa-solid');
                    heartIcon.style.color = "#B44163";

                    //const cardInfo = heartIcon.closest('.card').querySelector('.card__info');

                    //const likedText = document.createElement('span');
                    //likedText.textContent = '已收藏';
                    //likedText.classList.add('liked-text');

                    /*cardInfo.appendChild(likedText);*/
                },
                error: function (error) {
                    // 處理錯誤，如果有錯誤發生
                }
            });
        } else {
            // 在此處執行AJAX DELETE請求，從"LikeRecord"資料表中刪除對應的記錄
            $.ajax({
                type: 'DELETE',
                url: '/Activity/UnlikeActivity',
                data: {
                    activityId: activityId,
                    userId: 1
                },
                success: function (data) {
                    console.log("處理成功的回應，可以更新UI或執行其他操作delete")
                    // 處理成功的回應，可以更新UI或執行其他操作
                    heartIcon.classList.remove('fa-solid');
                    heartIcon.classList.add('fa-regular');
                    heartIcon.style.color = "#1E3050";

                    
                },
                error: function (error) {
                    // 處理錯誤，如果有錯誤發生
                }
            });
        }
    });
});






















$(document).ready(function () {
    // 選取探索按鈕
    var exploreLink = $("#explore-link");
    // 選取下拉內容
    var exploreDropdown = $("#explore-dropdown");
    //選取揪團按鈕
    var groupBtn = $("#group-link");

    exploreLink.hover(
        function () {
            // 滑鼠進入時顯示下拉內容
            exploreDropdown.css("display", "block");

        });

    exploreDropdown.hover(
        function () {
            // 滑鼠進入下拉內容時保持顯示
            exploreDropdown.css("display", "block");
        },
        function () {
            // 滑鼠離開下拉內容時隱藏
            exploreDropdown.css("display", "none");
        }
    );
    //滑鼠移動到揪團也會讓下拉消失
    groupBtn.hover(
        function () {
            exploreDropdown.css("display", "none")
        }
    )

    //點擊鈴鐺後，數字通知消失
    // 使用事件委託，當點擊<i>元素時執行以下操作
    $('.notification').on('click', 'i', function () {
        // 移除包含.notification--num的元素
        $(this).parent().find('.notification--num').remove();
    });
});