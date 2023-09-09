// 導覽列 

// 下拉清單
const exploreLink = document.getElementById('explore-link');
const exploreDropdown = document.getElementById('explore-dropdown');
const groupLink = document.getElementById("group-link");


exploreLink.addEventListener('mouseenter', function () {
    exploreDropdown.style.display = 'block';
});

exploreDropdown.addEventListener('mouseleave', function () {
    exploreDropdown.style.display = 'none';
});

// 確保移動到"發起揪團"懸浮視窗關閉
groupLink.addEventListener('mouseenter', function () {
  exploreDropdown.style.display = 'none';
});
