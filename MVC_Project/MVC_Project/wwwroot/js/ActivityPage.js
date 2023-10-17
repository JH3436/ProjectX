﻿
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



//聊天室登入按鈕
$(document).ready(function () {
    $(document).on('click', '#signBtn', function () {
        window.location.href = '/member/member';
    });
});

// 聊天室
$(document).ready(function () {
    $(document).on('click', '#discussBtn', function () {
        $("#discussInput").toggle();
    });
});


$(document).on("click", ".replyBtn", function () {
    $($(this).parent().parent().children(".replyTextDiv")).toggle();
});




$(document).ready(function () {
    // 監聽 #discussTextArea 元素的 keydown 事件
    $(document).on("keydown", "#discussTextArea", function (event) {
        if (event.key === "Enter") {
            event.preventDefault();  // 阻止預設的 Enter 鍵行為
            simulateClick();  // 呼叫模擬 click 函式
        }
    });

    $(document).on("click", ".publishBtn", simulateClick);  // 點擊 publishBtn 也呼叫模擬 click 函式

    function simulateClick() {
        var temp = $("#discussTextArea").val();
        if (temp == "") {
            alert("請輸入文字");
        } else {
            var sure = confirm("確定提交嗎討論\n\n" + $("#discussTextArea").val());
        }
        discussUpdate();
        getChatData();
    }
});


$(document).on("click", ".messageBtn", function () {
    var temp = $(this).parent().children("#replyTextArea").val();
    if (temp == "") {
        alert("請輸入文字");
    } else {
        var sure = confirm("確定提交留言\n\n" + temp);
    }

    getChatData();
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

/*抓取groupid*/
function getIdFromUrl() {
    const groupidElement = document.querySelector('.groupid');  // 選取具有 class 為 groupid 的元素
    if (groupidElement) {
        return groupidElement.getAttribute('data-groupid');
    }
    return null;
}
/*抓取groupid*/
function getChatData() {
    const id = getIdFromUrl();  // 取得 id
    if (!id) {
        console.log('ID not found in the URL.');
        return;
    }

    $.ajax({
        url: `/api/chatData/${id}`,  // 使用 id 構建 URL
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            updateChatInModal(data);
            // Do something with the data, e.g., update the UI
        },
        error: function (error) {
            console.error('Error:', error);
        }
    });
}

const id = getIdFromUrl();
if (id) {
    console.log('ID:', id);
    getChatData(id);  // 呼叫 getChatData 函式並傳遞 id
}


function updateChatInModal(chatList) {
    // 清空原有的討論區內容
    $("#dialogDiv").empty();

    // 將新的聊天資料插入到討論區
    var chatBoardHTML = `<div>
                    <div id="messageBoardTitle">
                        <p class="h1">討論區</p>
                        <div id="discussBtn">建立討論</div>
                    </div>

                    <!-- 討論輸入區 -->
                    
                    <div class="commentDiv" id="discussInput">
                            <div class="userCommentDiv">
                                <img class="profile" src="" />
                                <div class="userCommentDivRight">
                                    <p class="h3 align-self-center"></p>
                                    <div class="discuss-box align-self-start" name="ChatContent">
                                        <textarea name="ChatContent" id="discussTextArea" class="col-auto" rows="3" placeholder="討論串輸入框"></textarea>
                                    </div>
                                </div>
                            </div>
                            <div class="commentBtnDiv">
                                <button class="publishBtn" type="submit">
                                    <p class="h3">發表</p>
                                    <i class="fa-solid fa-comment fa-2xl align-self-center"></i>
                                </button>
                            </div>
                    </div>
                </div>`;
    $('#dialogDiv').append(chatBoardHTML);
    chatList.forEach(function (chat) {
        if (chat.ToWhom === null) {
            var chatId = chat.ChatID;  // 注意這裡要使用 ChatID，而不是 chatId
            var userPhoto = chat.UserPhoto ? `<img src="data:image/png;base64,${chat.UserPhoto}" class="profile" />` : '';
            var chatContent = chat.ChatContent ? `<div class="comment-box align-self-start">${chat.ChatContent}</div>` : '';

            var chatCommentDiv = `
            <div class="commentDiv">
                <div class="userCommentDiv">
                    ${userPhoto}
                    <div class="userCommentDivRight">
                        <p class="h3 align-self-center">${chat.Nickname}</p>
                        ${chatContent}
                        <p class="commentDatetime">${chat.ChatTime}</p>
                    </div>
                </div>
                <div class="commentBtnDiv" id =${chatId}>
                    <div class="replyBtn" id="replyBtn">
                        <p class="h3">回覆</p>
                        <i class="fa-solid fa-comment fa-2xl align-self-center"></i>
                    </div>
                </div>`;
            chatList.forEach(function (reply) {
                if (reply.ToWhom !== null && reply.ToWhom === chatId) {
                    // 建立回覆的 HTML
                    var replyHtml = `
                    <div class="replyDiv">
                        <div class="userCommentDiv">
                            ${userPhoto}
                            <div class="userCommentDivRight">
                                <p class="h3 align-self-center">${reply.Nickname}</p>
                                <div class="comment-box align-self-start">${reply.ChatContent}</div>
                                <p class="commentDatetime">${reply.ChatTime}</p>
                            </div>
                        </div>
                    </div>`;
                    chatCommentDiv = chatCommentDiv + replyHtml; 
                }
            });
            var replyTextDiv = `
                <div class="replyTextDiv">
                    <div class="userCommentDiv">
                        <img class="profile" src="" />
                        <div class="userCommentDivRight">
                            <p class="h3 align-self-center" id="userInfoNickname" ></p>
                            <textarea name="ChatContent" id="replyTextArea" class="col-auto" rows="1"></textarea>
                            <button class="messageBtn" type="submit">
                                <p class="messageBtn-text-style" href="#">
                                    留言
                                </p>
                            </button>
                        </div>
                    </div>
                </div>
            </div>`;

            chatCommentDiv = chatCommentDiv + replyTextDiv;
            // 插入到討論區
            $('#dialogDiv').append(chatCommentDiv);
        }
    });
    getUserInfo();
}
//會員讀取自訂函式

function getUserInfo() {
    let currentUserId = $('#currentUserId').val();
    console.log(currentUserId);

    $.ajax({
        url: `/api/getUserInfo/${currentUserId}`,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            console.log('userInfo', data);
            $('#discussInput .userCommentDiv p').text(data[0].Nickname);
            $('#discussInput .userCommentDiv .profile').attr('src', 'data:image/png;base64,' + data[0].UserPhoto);
            $('.replyTextDiv .userCommentDiv #userInfoNickname').text(data[0].Nickname);
            $('.replyTextDiv .userCommentDiv .profile').attr('src', 'data:image/png;base64,' + data[0].UserPhoto);
        },
        error: function (error) {
            console.error('Error:', error);
        }
    });
}


//上傳自訂函式
function discussUpdate() {
    let discussContent = $('#discussTextArea').val(); 
    let currentUserId = $('#currentUserId').val();

    let updateData = {
        ActivityID : 1,
        UserID: currentUserId,
        ChatContent : discussContent,
        ToWhom : null,
        // 可以添加其他需要上传的数据字段
    };

    // 发起AJAX请求
    $.ajax({
        url: '/api/discussUpdate', // 后端API的URL
        type: 'POST',
        dataType: 'json',
        data: JSON.stringify(dataToSend), // 将数据转为JSON字符串
        contentType: 'application/json', // 设置Content-Type为JSON
        success: function (response) {
            console.log('Data uploaded successfully:', response);
            // 在这里处理成功响应
        },
        error: function (error) {
            console.error('Error uploading data:', error);
            // 在这里处理错误响应
        }
    });
}



//討論上傳自訂函數
function discussUpdate() {
    let discussContent = $('#discussTextArea').val();
    const id = getIdFromUrl();  // 取得 id

    let updateData = {
        ActivityID: id,
        UserID: 1,
        ChatContent: discussContent,
        ToWhom: null,
        // 可以添加其他需要上传的数据字段
    };

    // 发起AJAX请求
    $.ajax({
        url: '/api/discussUpdate', // 后端API的URL
        type: 'POST',
        dataType: 'json',
        data: JSON.stringify(updateData), // 将数据转为JSON字符串
        contentType: 'application/json', // 设置Content-Type为JSON
        success: function (response) {
            console.log('数据成功上传:', response);
            // 在这里处理成功响应
        },
        error: function (error) {
            console.error('上传数据时出错:', error);
            console.log(JSON.stringify(updateData));
            // 在这里处理错误响应
        }
    });
}
















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

