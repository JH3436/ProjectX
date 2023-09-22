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