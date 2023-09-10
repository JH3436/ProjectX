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


// 介紹網站測試
$(function(){
  $('.mycarousel-item').eq(0).addClass('active');
  var total = $('.mycarousel-item').length;
  var current = 0;
  $('#moveRight').on('click', function(){
    var next=current;
    current= current+1;
    setSlide(next, current);
  });
  $('#moveLeft').on('click', function(){
    var prev=current;
    current = current- 1;
    setSlide(prev, current);
  });
  function setSlide(prev, next){
    var slide= current;
    if(next>total-1){
     slide=0;
      current=0;
    }
    if(next<0){
      slide=total - 1;
      current=total - 1;
    }
           $('.mycarousel-item').eq(prev).removeClass('active');
           $('.mycarousel-item').eq(slide).addClass('active');
      setTimeout(function(){

      },800);
    

    
    console.log('current '+current);
    console.log('prev '+prev);
  }
});







