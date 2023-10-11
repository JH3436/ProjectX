
//網站介紹
$(".option").click(function () {
    $(".option").removeClass("active");
    $(this).addClass("active");
});

//活動方塊-愛心設計
const heartIcons = document.querySelectorAll('.heart-icon');

heartIcons.forEach(function (heartIcon) {
    heartIcon.addEventListener('click', function () {
        const activityId = heartIcon.getAttribute('data-activityid');

        if (heartIcon.classList.contains('fa-regular')) {
            // 在此處執行AJAX POST請求，將activityId和userID（在這裡假設為1）發送到後端
            $.ajax({
                type: 'POST',
                url: '/MyActivity/LikeActivity',
                data: {
                    activityId: activityId,
                    userId: 1
                },
                success: function (data) {
                    // 處理成功的回應，可以更新UI或執行其他操作
                    heartIcon.classList.remove('fa-regular');
                    heartIcon.classList.add('fa-solid');
                    heartIcon.style.color = "#B44163";

                    const cardInfo = heartIcon.closest('.card').querySelector('.card__info');

                    const likedText = document.createElement('span');
                    likedText.textContent = '已收藏';
                    likedText.classList.add('liked-text');

                    cardInfo.appendChild(likedText);
                },
                error: function (error) {
                    // 處理錯誤，如果有錯誤發生
                }
            });
        } else {
            // 在此處執行AJAX DELETE請求，從"LikeRecord"資料表中刪除對應的記錄
            $.ajax({
                type: 'DELETE',
                url: '/MyActivity/UnlikeActivity',
                data: {
                    activityId: activityId,
                    userId: 1
                },
                success: function (data) {
                    // 處理成功的回應，可以更新UI或執行其他操作
                    heartIcon.classList.remove('fa-solid');
                    heartIcon.classList.add('fa-regular');
                    heartIcon.style.color = "#1E3050";

                    // 移除已經存在的 "Liked" 文字
                    const likedText = heartIcon.closest('.card').querySelector('.liked-text');
                    if (likedText) {
                        likedText.remove();
                    }
                },
                error: function (error) {
                    // 處理錯誤，如果有錯誤發生
                }
            });
        }
    });
});
