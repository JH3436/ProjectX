document.querySelectorAll('.menu-item').forEach(item => {
    item.addEventListener('click', function(e) {
        e.preventDefault();

        // 先隱藏所有B2-content區塊
        document.querySelectorAll('.B2-content').forEach(content => {
            content.style.display = 'none';
        });

        const content = this.getAttribute('data-content');

        switch(content) {
            case 'likeit':
                document.getElementById('likeit-section').style.display = 'block';
                break;
            case 'activity':
                document.getElementById('signup-section').style.display = 'block';
                break;
            case 'settings':
                document.getElementById('settings-section').style.display = 'block';
                break;
            case 'delete':
                document.getElementById('personal-section').style.display = 'block';
                break;
        }
    });
});




    // 找到 textarea 元素
    var textarea = document.getElementById('introduceyrself');

    // 設定字數限制
    var maxLength = 10; // 這裡設置為你想要的字數限制

    // 綁定 input 事件處理程序
    textarea.addEventListener('input', function() {
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
        width: 150,
        height: 200,
      },
      enforceBoundary: false,
      enableExif: true
    });
    $('#cropImagePop').on('shown.bs.modal', function(){
      // alert('Shown pop');
      $uploadCrop.croppie('bind', {
            url: rawImg
          }).then(function(){
            console.log('jQuery bind complete');
          });
    });
    
    $('.item-img').on('change', function () { imageId = $(this).data('id'); tempFilename = $(this).val();
                                             $('#cancelCropBtn').data('id', imageId); readFile(this); });
    $('#cropImageBtn').on('click', function (ev) {
      $uploadCrop.croppie('result', {
        type: 'base64',
        format: 'jpeg',
        size: {width: 150, height: 200}
      }).then(function (resp) {
        $('#item-img-output').attr('src', resp);
        $('#cropImagePop').modal('hide');
      });
    });


    // ajax丟給controller
    
$('#cropImageBtn').on('click', function (ev) {
    $uploadCrop.croppie('result', {
        type: 'base64',
        format: 'jpeg',
        size: {
            width: 150,
            height: 200
        }
    }).then(function (resp) {
        // 使用AJAX將裁剪後的圖像數據上傳到伺服器
        $.ajax({
            url: '/your-controller/save-cropped-image', // 替換成伺服器端端點的URL
            //如果你使用ASP.NET MVC框架，這個URL可能會指向一個控制器的動作方法，如/YourController/SaveCroppedImage，其中YourController是你的控制器名稱，SaveCroppedImage是處理保存圖像的動作方法名稱。如果你使用其他後端框架或語言，URL結構可能會有所不同。 
            type: 'POST',
            data: {
                imageData: resp
            },
            success: function (response) {
                if (response.success) {
                    // 更新用戶界面上的圖像
                    $('#item-img-output').attr('src', response.imageUrl);
                    $('#cropImagePop').modal('hide');
                } else {
                    alert('圖像保存失敗');
                }
            },
            error: function () {
                alert('發送圖像數據到伺服器失敗');
            }
        });
    });
});