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



//點擊鈴鐺後，數字通知消失
// 使用事件委託，當點擊<i>元素時執行以下操作
  $(".notification").on("click", "i", function () {
    // 移除包含.notification--num的元素
    $(this).parent().find(".notification--num").remove();
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

  // 彈出視窗顯示表單內容
  alert(
    `姓名：${name}\n電子郵件：${email}\n郵件標題：${emailSubject}\n手機號碼：${mobileNumber}\n訊息：${message}`
  );
};