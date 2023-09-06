// 導覽列 

//搜尋列focus更改顏色
// 選擇search-input元素
const searchInput = document.querySelector('.search-input');

// 添加focus事件監聽器
searchInput.addEventListener('focus', function () {
    // 更改邊框顏色為橘色
    searchInput.style.border = '6px solid var(--orange)';
});

// 添加blur事件監聽器（當失去焦點時恢復原始邊框顏色）
searchInput.addEventListener('blur', function () {
    // 恢復原始邊框顏色為白色
    searchInput.style = "none"
});

// 下拉清單
const exploreLink = document.getElementById('explore-link');
const exploreDropdown = document.getElementById('explore-dropdown');

exploreLink.addEventListener('mouseenter', function () {
    exploreDropdown.style.display = 'block';
});

exploreDropdown.addEventListener('mouseleave', function () {
    exploreDropdown.style.display = 'none';
});

//moving letter特效
var textWrapper = document.querySelector('.ml14 .letters');
textWrapper.innerHTML = textWrapper.textContent.replace(/\S/g, "<span class='letter'>$&</span>");

anime.timeline({loop: true})
  .add({
    targets: '.ml14 .line',
    scaleX: [0,1],
    opacity: [0.5,1],
    easing: "easeInOutExpo",
    duration: 900
  }).add({
    targets: '.ml14 .letter',
    opacity: [0,1],
    translateX: [40,0],
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