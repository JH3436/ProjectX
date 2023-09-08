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

// 探索下拉清單
const exploreLink = document.getElementById('explore-link');
const exploreDropdown = document.getElementById('explore-dropdown');

exploreLink.addEventListener('mouseenter', function () {
    exploreDropdown.style.display = 'block';
});

exploreDropdown.addEventListener('mouseleave', function () {
    exploreDropdown.style.display = 'none';
});


// 通知下拉清單
const notify = document.getElementById('notify');
const notifyDropdown = document.getElementById('notify-dropdown');


notify.addEventListener('click', function (e) {
    e.stopPropagation(); // 阻止點擊事件冒泡
    notifyDropdown.style.display = 'block';
  });
  
  document.addEventListener('click', function () {
    notifyDropdown.style.display = 'none';
  });
  
  notifyDropdown.addEventListener('click', function (e) {
    e.stopPropagation(); // 阻止點擊事件冒泡
  });

  //通知數字
  function updateBadge(number) {
    const badge = document.getElementById('badgeNumber');
    const notificationBadge = document.getElementById('notificationBadge');
    
    badge.innerText = number;
  
    const width = 16 + (number > 9 ? 6 : 0) + (number > 99 ? 6 : 0); // 調整寬度
    const height = 16; // 固定高度
  
    notificationBadge.style.width = `${width}px`;
    notificationBadge.style.height = `${height}px`;
  }
  updateBadge(999);