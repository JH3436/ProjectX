
$(document).ready(function () {
    // 假設你已經取得了currentUserId
    let currentUserId = $('#currentUserId').val();

    $.get('/Member/GetUserPhoto', { userId: currentUserId }, function (data) {
        if (data.imageUrl) {
            $('#item-img-output').attr('src', data.imageUrl);
        }
    });

    ////$.get('/Member/GetMemberInfo', { userId: currentUserId }, function (data) {
    ////    if (data.Nickname) {
    ////        $('#nickname').val(data.Nickname);
    ////    }
    ////    if (data.Intro) {
    ////        $('#introduceyrself').val(data.Intro);
    ////    }
    ////});



// 找到 textarea 元素
var textarea = document.getElementById('introduceyrself');

// 設定字數限制
var maxLength = 10; // 這裡設置為你想要的字數限制

// 綁定 input 事件處理程序
textarea.addEventListener('input', function () {
    var currentLength = textarea.value.length;
    if (currentLength > maxLength) {
        // 如果超過字數限制，截斷文本
        textarea.value = textarea.value.substring(0, maxLength);
        // 或者顯示錯誤消息
        // alert('字數超過限制！');
    }
});


// Start upload preview image

$(".gambar").attr("src", "https://user.gadjian.com/static/images/personnel_boy.png");
var $uploadCrop,
    tempFilename,
    rawImg,
    imageId;
function readFile(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('.upload-demo').addClass('ready');
            $('#cropImagePop').modal('show');
            rawImg = e.target.result;
        }
        reader.readAsDataURL(input.files[0]);
    }
    else {
        swal("Sorry - you're browser doesn't support the FileReader API");
    }
}

$uploadCrop = $('#upload-demo').croppie({
    viewport: {
        width: 300,
        height: 300,
    },
    enforceBoundary: false,
    enableExif: true
});
$('#cropImagePop').on('shown.bs.modal', function () {
    // alert('Shown pop');
    $uploadCrop.croppie('bind', {
        url: rawImg
    }).then(function () {
        console.log('jQuery bind complete');
    });
});

$('.item-img').on('change', function () {
    imageId = $(this).data('id'); tempFilename = $(this).val();
    $('#cancelCropBtn').data('id', imageId); readFile(this);
});
$('#cropImageBtn').on('click', function (ev) {
    $uploadCrop.croppie('result', {
        type: 'base64',
        format: 'jpeg',
        size: { width: 300, height: 300 }
    }).then(function (resp) {
        $('#item-img-output').attr('src', resp);
        $('#cropImagePop').modal('hide');

        // AJAX 請求傳送照片到後端
        $.ajax({
            url: '/Member/UpdateUserPhoto',
            method: 'POST',
            data: {
                userId: 1, // 這裡應該是當前登入的用戶ID
                imageBase64: resp
            },
            success: function (response) {
                if (response.success) {
                    alert('Photo updated successfully');
                } else {
                    alert('Failed to update photo: ' + response.message);
                }
            }
        });
    });
});

    
});


