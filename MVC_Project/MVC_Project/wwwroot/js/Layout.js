//回到網頁頂端

const scrollButton = document.getElementById("scroll-to-top");

window.addEventListener("scroll", () => {
    const scrollY = window.scrollY;

    // 檢查滾動位置是否超過1vh
    if (scrollY > window.innerHeight * 0.3) {
        scrollButton.classList.remove("hidden");
        scrollButton.style.opacity = 1;
        scrollButton.style.pointerEvents = "auto";
    } else {
        scrollButton.classList.add("hidden");
        scrollButton.style.opacity = 0;
        scrollButton.style.pointerEvents = "none";
    }
});

// 導覽列
$(document).ready(function () {
    // 選取探索按鈕
    var exploreLink = $("#explore-link");
    // 選取下拉內容
    var exploreDropdown = $("#explore-dropdown");
    //選取揪團按鈕
    var groupBtn = $("#group-link");

    exploreLink.hover(function () {
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
    groupBtn.hover(function () {
        exploreDropdown.css("display", "none");
    });
});


//鈴鐺+下拉顯示
$(".notification").on("click", function () {
    const ele = $(".notification-popup");
    if (ele.hasClass("active")) {
        console.log("remove active");
        ele.removeClass("active");
    } else {
        console.log("add active");
        ele.addClass("active");
    }
});

document.addEventListener(
    "click",
    function (event) {
        // If user either clicks X button OR clicks outside the modal window, then close modal by calling closeModal()
        if (
            event.target.closest(".notification") == null &&
            event.target.closest(".notification-popup") == null
        ) {
            console.log("remove active click outside");
            $(".notification-popup").removeClass("active");
        }
    },
    false
);

// 在頁面載入時，向後端發送請求以獲取通知數目和通知內容
$(document).ready(function () {
    $.ajax({
        type: 'GET',
        url: '/MyActivity/GetNotifications',
        data: {
            userId: 1 // 假設的使用者ID
        },
        success: function (notificationData) {
            ////Json資料
            //console.log(notificationData); // 輸出 JSON 數據到控制台
            //console.log(JSON.stringify(notificationData, null, 2)); // 格式化 JSON 數據

            // 更新通知數目
            var notificationNum = $(".notification--num");
            notificationNum.text(notificationData.notificationCount);

            // 清空通知下拉框
            var notificationPopup = $(".notification-popup");
            notificationPopup.empty();

            // 生成通知內容
            for (var i = 0; i < notificationData.notifications.length; i++) {
                var notification = notificationData.notifications[i];
                var isReadClass = notification.IsRead ? 'read' : 'unread'; // 判斷已讀或未讀
                var notificationContent = '<div class="popup-content row ' + isReadClass + '" data-notificationid="' + notification.NotificationID + '">';

                notificationContent += '<i class="fa-solid fa-circle col-1 notification-dot ' + isReadClass + '"></i>';
                notificationContent += '<i class="fa-regular fa-newspaper col-2"></i>';
                notificationContent += '<div class="notification-message col-9">';
                notificationContent += '<h4>' + notification.NotificationContent + '</h4>';
                notificationContent += '<span class="notified-date">' + notification.NotificationDate + '</span>';
                notificationContent += '</div>';
                notificationContent += '</div>';

                notificationPopup.append(notificationContent);
            }
        },
        error: function () {
            console.log('無法獲取通知數據。');
        }
    });
});

// 點擊通知項目
$(".notification-popup").on("click", ".popup-content", function () {
    var notificationId = $(this).data("notificationid");

    // 使用AJAX向後端發送標記為已讀的請求
    $.ajax({
        type: 'POST',
        url: '/MyActivity/MarkNotificationAsRead',
        data: {
            userId: 1,   //假設值
            notificationId: notificationId
        },
        success: function (notificationData) {
            // 更新通知下拉框
            var notificationPopup = $(".notification-popup");
            notificationPopup.empty();

            // 生成通知內容
            for (var i = 0; i < notificationData.notifications.length; i++) {
                var notification = notificationData.notifications[i];
                var isReadClass = notification.IsRead ? 'read' : 'unread'; // 判斷已讀或未讀
                var notificationContent = '<div class="popup-content row ' + isReadClass + '" data-notificationid="' + notification.NotificationID + '">';

                notificationContent += '<i class="fa-solid fa-circle col-1 notification-dot ' + isReadClass + '"></i>';
                notificationContent += '<i class="fa-regular fa-newspaper col-2"></i>';
                notificationContent += '<div class="notification-message col-9">';
                notificationContent += '<h4>' + notification.NotificationContent + '</h4>';
                notificationContent += '<span class="notified-date">' + notification.NotificationDate + '</span>';
                notificationContent += '</div>';
                notificationContent += '</div>';

                notificationPopup.append(notificationContent);
            }

            // 更新通知數字
            var notificationNum = $(".notification--num");
            notificationNum.text(notificationData.notificationCount);
        },
        error: function () {
            console.log('無法標記通知為已讀。');
        }
    });
});










// 聯絡我們-表單
// 當按鈕點擊時，顯示 dialog
document.getElementById("showContactButton").addEventListener("click", function () {
    var dialog = document.getElementById("contactDialog");
    dialog.showModal();


});

// 當關閉按鈕被點擊時，關閉 dialog
document.getElementById("closeDialogButton").addEventListener("click", function () {
    var dialog = document.getElementById("contactDialog");
    dialog.close();
});

// 當 dialog 關閉時，重置表單
document.getElementById("contactDialog").addEventListener("close", function () {
    document.getElementById("myForm").reset();
});




//響應式表單
$(document).ready(function () {
    $("#contactBox form input").on("input", function () {
        if (this.checkValidity()) {
            $(this).css("border", "0.2rem green solid");
        } else {
            $(this).css("border", "0.2rem red solid");
        }
    });
});

//alert表單內容
document.getElementById("myForm").onsubmit = function (event) {
    // 獲取表單元素
    var form = event.target;
    // 獲取各input元素的值
    var name = form.elements["name"].value;
    var email = form.elements["email"].value;
    var emailSubject = form.elements["emailSubject"].value;
    var mobileNumber = form.elements["mobileNumber"].value;
    var message = form.elements["message"].value;


};