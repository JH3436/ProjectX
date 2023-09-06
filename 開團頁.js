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