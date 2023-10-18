
let userId = $("#currentUserId").val();

//登入後的操作
if (userId = !0) {
    //限制選日期
    document.addEventListener('DOMContentLoaded', function () {
        const startDate = document.getElementById("StartDate");
        const endDate = document.getElementById("EndDate");

        let today = new Date();
        let nextMonth = new Date(today);
        nextMonth.setMonth(today.getMonth() + 1);

        startDate.min = formatDate(nextMonth);

        startDate.addEventListener("change", function () {
            let selectedDate = new Date(this.value);
            selectedDate.setDate(selectedDate.getDate() + 1);
            endDate.min = formatDate(selectedDate);
            endDate.disabled = false;
        });
    });

    function formatDate(date) {
        let d = new Date(date),
            month = '' + (d.getMonth() + 1),
            day = '' + d.getDate(),
            year = d.getFullYear();

        if (month.length < 2) month = '0' + month;
        if (day.length < 2) day = '0' + day;

        return [year, month, day].join('-');
    }


    //選照片
    $(document).ready(function () {
        var images = [];
        var currentIndex = -1;
        var totalImages = 0;

        $("#ImageData").change(function () {
            var files = this.files;

            if (totalImages + files.length > 4) {
                alert("最多只能選4張圖片！");
                this.value = null;
                return;
            }

            for (var i = 0; i < files.length; i++) {
                var reader = new FileReader();

                reader.onload = function (e) {
                    images.push(e.target.result);
                    totalImages++;
                    currentIndex = images.length - 1;
                    showImage(images[currentIndex]);
                };

                reader.readAsDataURL(files[i]);
            }
        });

        $("#deleteImage").click(function () {
            if (currentIndex !== -1) {
                images.splice(currentIndex, 1);
                totalImages--;
                if (images.length === 0) {
                    $("#photosPreview").empty();
                    currentIndex = -1;
                } else {
                    currentIndex = Math.max(currentIndex - 1, 0);
                    showImage(images[currentIndex]);
                }
            }
        });

        $("#prevImage").click(function () {
            if (currentIndex > 0) {
                currentIndex--;
                showImage(images[currentIndex]);
            }
        });

        $("#nextImage").click(function () {
            if (currentIndex < images.length - 1) {
                currentIndex++;
                showImage(images[currentIndex]);
            }
        });

        function showImage(src) {
            var img = $("<img>").attr("src", src).css({ "width": "300px", "height": "200px" });

            if (currentIndex !== -1) {
                $("#imageLabel").text(`第${currentIndex + 1}張`);

            }

            $("#photosPreview").empty().append(img);
        }

    });
}

// 在表單提交成功後觸發SweetAlert2
//$('form').on('submit', function (e) {
//    e.preventDefault();


//    // 假設處理成功
//    Swal.fire({
//        title: '提交成功!',
//        text: '您已投票',
//        icon: 'success',
//        confirmButtonText: 'Ok!',
//        confirmButtonColor: 'var(--deepBlue)',
//    })
//});

//沒登入的提醒JS
var textWrapper = document.querySelector('.ml6 .letters');
textWrapper.innerHTML = textWrapper.textContent.replace(/\S/g, "<span class='letter'>$&</span>");

anime.timeline({ loop: true })
    .add({
        targets: '.ml6 .letter',
        translateY: ["1.1em", 0],
        translateZ: 0,
        duration: 750,
        delay: (el, i) => 50 * i
    }).add({
        targets: '.ml6',
        opacity: 0,
        duration: 1000,
        easing: "easeOutExpo",
        delay: 1000
    });




