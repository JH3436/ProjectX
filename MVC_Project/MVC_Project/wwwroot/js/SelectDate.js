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


//鈴鐺+下拉
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

// 初始化目前的未讀訊息數量，這裡假設有3條未讀訊息

let unreadMessagesCount = 3;

// 點擊某一個通知後，圓點顯示灰色，表示已讀
$(document).ready(function () {
    $(".popup-content").click(function () {
        const notificationDot = $(this).find(".notification-dot");
        if (notificationDot.css("color") === "rgb(153, 155, 159)") {
            // 如果圓點是灰色，表示已讀，將其設置為未讀（藍色）
            notificationDot.css("color", "#0860f7");
            // 增加未讀訊息數量
            unreadMessagesCount++;
            // 更新通知鈴鐺上的數字
            updateNotificationCount();
        } else {
            // 如果圓點是藍色，表示未讀，將其設置為已讀（灰色）
            notificationDot.css("color", "#999b9f");
            // 更新未讀訊息數量，減少1
            unreadMessagesCount--;
            // 更新通知鈴鐺上的數字
            updateNotificationCount();
        }

    });

    // 監聽read-all按鈕的點擊事件
    $(".read-all").click(function () {
        // 選擇所有的notification-dot元素並改變顏色
        $(".notification-dot").css("color", "#999b9f");
        // 將未讀訊息數量設置為0
        unreadMessagesCount = 0;
        // 更新通知鈴鐺上的數字
        updateNotificationCount();
    });
});


//更新通知數函式
function updateNotificationCount() {
    // 選擇通知鈴鐺上的數字元素
    const notificationNum = $(".notification--num");
    if (unreadMessagesCount > 0) {
        // 如果還有未讀訊息，顯示數字並更新內容
        notificationNum.text(unreadMessagesCount);
        notificationNum.show();
    } else {
        // 如果沒有未讀訊息，隱藏數字
        notificationNum.hide();
    }
}



$("button-container").click(function () {

    var isChecked1 = $('#f-option').is(":checked");
    var isChecked2 = $('#s-option').is(":checked");
    var isChecked3 = $('#t-option').is(":checked");
    console.log(isChecked1); //true or false


    if (isChecked1 == true || isChecked2 == true || isChecked3 == true) {
        $(".pop").fadeOut(300);
    } else {
        alert("請選擇日期");
    }
});



//顯示日期時間
$(document).ready(function () {
    // 你原有的日期生成邏輯
    // ...

    // 更新label
    $(".Cli label").each(function (index) {
        if (index < weekends.length) {
            const weekend = weekends[index];
            const dateStr = `${weekend.start.getFullYear()}/${weekend.start.getMonth() + 1}/${weekend.start.getDate()} - ${weekend.end.getFullYear()}/${weekend.end.getMonth() + 1}/${weekend.end.getDate()}`;
            $(this).text(dateStr);
        }
    });

    // 添加表單提交邏輯
    $(document).ready(function () {
        // ...你的其他代碼...

        // 更新label和對應的input的value
        $(".Cli").each(function (index) {
            if (index < weekends.length) {
                const weekend = weekends[index];
                const dateStr = `${weekend.start.getFullYear()}/${weekend.start.getMonth() + 1}/${weekend.start.getDate()} - ${weekend.end.getFullYear()}/${weekend.end.getMonth() + 1}/${weekend.end.getDate()}`;
                $(this).find("label").text(dateStr);
                $(this).find("input").val(dateStr);  // 更新value
            }
        });

        // 當表單提交時
        $("#login").on("submit", function (e) {
            e.preventDefault();  // 阻止表單的默認提交行為

            let selectedDate = $("input[name='VoteResult']:checked").val();

            // 沒有選擇日期時跳出警告
            if (!selectedDate) {
                alert('請選擇一個日期。');
                return;
            }

            // 假設日期格式是 "YYYY/MM/DD - YYYY/MM/DD"，我們只取前面的那個
            let firstDate = selectedDate.split(" - ")[0];
            console.log(firstDate);
            $.ajax({
                url: '/VoteRecords/Create',  // 控制器的路由
                method: 'POST',
                data: {
                    RecordID: "",  // 你需要設定的ID
                    UserID: "",  // 使用者ID
                    ActivityID: "",  // 活動ID
                    VoteResult: selectedDate // 只取第一個日期
                },
                success: function (response) {
                    // 成功後的動作，例如重新導向到一個新頁面
                    window.location.href = '/VoteRecords/Index';
                },
                error: function (err) {
                    // 失敗後的動作
                    console.log(err);
                }
            });
        });
    });
