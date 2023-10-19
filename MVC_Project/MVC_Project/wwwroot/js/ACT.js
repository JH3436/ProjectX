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
});




//--------James加的-----------
//麵包屑設計
$(document).ready(function () {
    // 獲取當前URL
    var encodedText = window.location.href;
    var currentURL = decodeURIComponent(encodedText);
    console.log(currentURL);

    // 檢查URL的分類
    switch (true) {
        case currentURL.endsWith("登山"):
            updateBreadcrumb("登山");
            break;
        case currentURL.endsWith("溯溪"):
            updateBreadcrumb("溯溪");
            break;
        case currentURL.endsWith("潛水"):
            updateBreadcrumb("潛水");
            break;
        case currentURL.endsWith("露營"):
            updateBreadcrumb("露營");
            break;
        case currentURL.endsWith("其他"):
            updateBreadcrumb("其他");
            break;
        default:
            // 如果URL不符合任何分類，保持原有麵包屑
            break;
    }
});

function updateBreadcrumb(category) {
    // 更新麵包屑內容
    $(".breadcrumb").append('<li class="separator">&nbsp;<i class="fa-solid fa-chevron-right"></i>&nbsp;</li>');
    $(".breadcrumb").append('<li class="breadcrumb-item">' + category + '</li>');
}




