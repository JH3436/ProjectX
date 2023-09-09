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