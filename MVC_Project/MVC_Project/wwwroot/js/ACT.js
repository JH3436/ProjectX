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

var checkbox = document.getElementById("cktoggle_id2");

// 監聽 checkbox 變化事件
checkbox.addEventListener("change", function () {
    // 檢查 checkbox 的狀態
    if (checkbox.checked) {
        // checkbox 被選中
        console.log("Checkbox 已選中");
        $.ajax({
            url: '/api/loadData', // 控制器的路徑
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                console.log(data);
                $('.cards').empty();
                data.forEach(function (item) {
                    var StartDate = new Date(item.StartDate).toLocaleString('en-US', {
                        year: 'numeric',
                        month: '2-digit',
                        day: '2-digit'

                    });
                    var EndDate = new Date(item.EndDate).toLocaleString('en-US', {
                        year: 'numeric',
                        month: '2-digit',
                        day: '2-digit'

                    });
                    
                    var photo = item.PhotoData;
                    console.log(photo);
                    var cardHtml = `
            <article class="card card--${item.GroupID}">
                <div class="card__info-hover">
                    <div class="card__clock-info">
                        <svg xmlns="http://www.w3.org/2000/svg" height="1.25em" viewBox="0 0 448 512" class="card__clock">
                                    <path d="M152 24c0-13.3-10.7-24-24-24s-24 10.7-24 24V64H64C28.7 64 0 92.7 0 128v16 48V448c0 35.3 28.7 64 64
                  64H384c35.3 0 64-28.7 64-64V192 144 128c0-35.3-28.7-64-64-64H344V24c0-13.3-10.7-24-24-24s-24 10.7-24
                  24V64H152V24zM48 192H400V448c0 8.8-7.2 16-16 16H64c-8.8 0-16-7.2-16-16V192z" />
                                </svg>
                                 <span class="card__time">天數：${EndDate - StartDate} 天</span>
                    </div>
                </div>
                <div class="card__img">${item.PersonalPhotoID}</div>
                <a href="#" class="card_link">
                    <div class="card__img--hover"></div>
                </a>
                <div class="card__info">
                    <span class="card__category">${item.GroupCategory}</span>
                    <h3 class="card__title">${item.GroupName}</h3>
                    <p class="card__period" style="font-size: 1.25rem;font-weight: bold;">${StartDate} ~ ${EndDate}</p>
                    <span class="card__by">主揪<a href="#" class="card__author" title="author"> ${item.Nickname}</a></span>
                </div>  
            </article>
        `;
                    $('.cards').append(cardHtml); // 將生成的卡片添加到.cards元素中
                });
                
            },
            error: function (error) {
                console.error(error);
            }
        });

    } else {
        // checkbox 未被選中
        console.log("Checkbox 未選中");
    }
});

