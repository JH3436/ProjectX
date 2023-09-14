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

$(document).ready(function() {
  // 選取探索按鈕
  var exploreLink = $("#explore-link");
  // 選取下拉內容
  var exploreDropdown = $("#explore-dropdown");
  //選取揪團按鈕
  var groupBtn = $("#group-link");

  exploreLink.hover(
    function() {
      // 滑鼠進入時顯示下拉內容
      exploreDropdown.css("display", "block");
    
  });

  exploreDropdown.hover(
    function() {
      // 滑鼠進入下拉內容時保持顯示
      exploreDropdown.css("display", "block");
    },
    function() {
      // 滑鼠離開下拉內容時隱藏
      exploreDropdown.css("display", "none");
    }
  );
    //滑鼠移動到揪團也會讓下拉消失
    groupBtn.hover(
      function() {
        exploreDropdown.css("display", "none")
      }
    )
});

//moving letter特效
var textWrapper = document.querySelector('.ml14 .letters');
textWrapper.innerHTML = textWrapper.textContent.replace(/\S/g, "<span class='letter'>$&</span>");

anime.timeline({ loop: true })
  .add({
    targets: '.ml14 .line',
    scaleX: [0, 1],
    opacity: [0.5, 1],
    easing: "easeInOutExpo",
    duration: 900
  }).add({
    targets: '.ml14 .letter',
    opacity: [0, 1],
    translateX: [40, 0],
    translateZ: 0,
    scaleX: [0.3, 1],
    easing: "easeOutExpo",
    duration: 800,
    offset: '-=600',
    delay: (el, i) => 150 + 25 * i
  }).add({
    targets: '.ml14',
    opacity: 0,
    duration: 1000,
    easing: "easeOutExpo",
    delay: 1000
  });

//活動方塊-愛心設計
const heartIcons = document.querySelectorAll('.heart-icon');

heartIcons.forEach(function (heartIcon) {
  heartIcon.addEventListener('click', function () {
    if (heartIcon.classList.contains('fa-regular')) {
      heartIcon.classList.remove('fa-regular');
      heartIcon.classList.add('fa-solid');
      heartIcon.style.color = "#B44163";

      const cardInfo = heartIcon.closest('.card').querySelector('.card__info');

      const likedText = document.createElement('span');
      likedText.textContent = '已收藏';
      likedText.classList.add('liked-text');

      cardInfo.appendChild(likedText);
    } else {
      heartIcon.classList.remove('fa-solid');
      heartIcon.classList.add('fa-regular');
      heartIcon.style.color = "#1E3050";
      // 移除已經存在的 "Liked" 文字
      const likedText = heartIcon.closest('.card').querySelector('.liked-text');
      if (likedText) {
        likedText.remove();
      }
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
      $(this).css("border", "0.1rem rgba(48, 230, 106, 0.749) solid");
    } else {
      $(this).css("border", "0.1rem red solid");
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







