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


// 通知下拉的事件
let isNotifyDropdownOpen = false;


const notify = document.getElementById('notify');
const notifyDropdown = document.getElementById('notify-dropdown');

notify.addEventListener('click', function (e) {
    e.stopPropagation(); 

    if (isNotifyDropdownOpen) {
        notifyDropdown.style.display = 'none';
        isNotifyDropdownOpen = false;
    } else {
        notifyDropdown.style.display = 'block';
        isNotifyDropdownOpen = true;
    }
});

document.addEventListener('click', function () {
    if (isNotifyDropdownOpen) {
        notifyDropdown.style.display = 'none';
        isNotifyDropdownOpen = false;
    }
});

notifyDropdown.addEventListener('click', function (e) {
    e.stopPropagation(); 
});


// 彈跳視窗
$(document).ready(function () {
    $("#AA, #BB").click(function () {
      $(".pop").fadeIn(300);
      positionPopup();
    });
  
    $(".pop > span").click(function (e) {
      e.stopPropagation(); 
      $(".pop").fadeOut(300);
    });
  
    $(".pop input[type=radio]").click(function (e) {
      e.stopPropagation();
    });

    $(".submit").click(function() {
        
        var isChecked1 = $('#f-option') .is(":checked");
        var isChecked2 = $('#s-option') .is(":checked");
        var isChecked3 = $('#t-option').is(":checked");
        console.log(isChecked1); //true or false
  

        if(isChecked1 == true || isChecked2 == true || isChecked3 == true){
            $(".pop").fadeOut(300);
        }else{
            alert("請選擇日期");
        }
    });

  });
  




  




   // 取得下拉選單項目 
  document.addEventListener('DOMContentLoaded', function() {
    const dropdownItems = document.querySelectorAll('dropdown ul li');
  
    dropdownItems.forEach(item => {
      item.addEventListener('click', function() {
        const selectedText = this.textContent;
        const correspondingLabel = this.closest('dropdown').querySelector('label');
  
        correspondingLabel.textContent = selectedText;
      });
    });
  });


    //   選取完或點擊空白處會自動縮回
    document.addEventListener('click', function(event) {
        var dropdowns = document.querySelectorAll('dropdown');
        dropdowns.forEach(function(dropdown) {
          if (!dropdown.contains(event.target) || event.target.tagName === 'LI') {
            var input = dropdown.querySelector('input');
            if (input.checked) {
              input.checked = false;
            }
          }
        });
      });



  //日曆
  
  $(document).ready(function () {
    function c(passed_month, passed_year, calNum) {
        var calendar = calNum == 0 ? calendars.cal1 : calendars.cal2;
        makeWeek(calendar.weekline);
        calendar.datesBody.empty();
        var calMonthArray = makeMonthArray(passed_month, passed_year);
        var r = 0;
        var u = false;
        while (!u) {
            if (daysArray[r] == calMonthArray[0].weekday) {
                u = true
            } else {
                calendar.datesBody.append('<div class="blank"></div>');
                r++;
            }
        }
        for (var cell = 0; cell < 42 - r; cell++) { // 42 date-cells in calendar
            if (cell >= calMonthArray.length) {
                calendar.datesBody.append('<div class="blank"></div>');
            } else {
                var shownDate = calMonthArray[cell].day;
                var iter_date = new Date(passed_year, passed_month, shownDate);
                if (
                (
                (shownDate != today.getDate() && passed_month == today.getMonth()) || passed_month != today.getMonth()) && iter_date < today) {
                    var m = '<div class="past-date">';
                } else {
                    var m = checkToday(iter_date) ? '<div class="today">' : "<div>";
                }
                calendar.datesBody.append(m + shownDate + "</div>");
            }
        }

        var color = "#444444";
        calendar.calHeader.find("h2").text(i[passed_month] + " " + passed_year);
        calendar.weekline.find("div").css("color", color);
        calendar.datesBody.find(".today").css("color", "#87b633");

        // find elements (dates) to be clicked on each time
        // the calendar is generated
        var clicked = false;
        selectDates(selected);

        clickedElement = calendar.datesBody.find('div');
        clickedElement.on("click", function () {
            clicked = $(this);
            var whichCalendar = calendar.name;

            if (firstClick && secondClick) {
                thirdClicked = getClickedInfo(clicked, calendar);
                var firstClickDateObj = new Date(firstClicked.year,
                firstClicked.month,
                firstClicked.date);
                var secondClickDateObj = new Date(secondClicked.year,
                secondClicked.month,
                secondClicked.date);
                var thirdClickDateObj = new Date(thirdClicked.year,
                thirdClicked.month,
                thirdClicked.date);
                if (secondClickDateObj > thirdClickDateObj && thirdClickDateObj > firstClickDateObj) {
                    secondClicked = thirdClicked;
                    // then choose dates again from the start :)
                    bothCals.find(".calendar_content").find("div").each(function () {
                        $(this).removeClass("selected");
                    });
                    selected = {};
                    selected[firstClicked.year] = {};
                    selected[firstClicked.year][firstClicked.month] = [firstClicked.date];
                    selected = addChosenDates(firstClicked, secondClicked, selected);
                } else { // reset clicks
                    selected = {};
                    firstClicked = [];
                    secondClicked = [];
                    firstClick = false;
                    secondClick = false;
                    bothCals.find(".calendar_content").find("div").each(function () {
                        $(this).removeClass("selected");
                    });
                }
            }
            if (!firstClick) {
                firstClick = true;
                firstClicked = getClickedInfo(clicked, calendar);
                selected[firstClicked.year] = {};
                selected[firstClicked.year][firstClicked.month] = [firstClicked.date];
            } else {
                secondClick = true;
                secondClicked = getClickedInfo(clicked, calendar);

                // what if second clicked date is before the first clicked?
                var firstClickDateObj = new Date(firstClicked.year,
                firstClicked.month,
                firstClicked.date);
                var secondClickDateObj = new Date(secondClicked.year,
                secondClicked.month,
                secondClicked.date);

                if (firstClickDateObj > secondClickDateObj) {

                    var cachedClickedInfo = secondClicked;
                    secondClicked = firstClicked;
                    firstClicked = cachedClickedInfo;
                    selected = {};
                    selected[firstClicked.year] = {};
                    selected[firstClicked.year][firstClicked.month] = [firstClicked.date];

                } else if (firstClickDateObj.getTime() == secondClickDateObj.getTime()) {
                    selected = {};
                    firstClicked = [];
                    secondClicked = [];
                    firstClick = false;
                    secondClick = false;
                    $(this).removeClass("selected");
                }


                // add between dates to [selected]
                selected = addChosenDates(firstClicked, secondClicked, selected);
            }
            selectDates(selected);
        });

    }

    function selectDates(selected) {
        if (!$.isEmptyObject(selected)) {
            var dateElements1 = datesBody1.find('div');
            var dateElements2 = datesBody2.find('div');

            function highlightDates(passed_year, passed_month, dateElements) {
                if (passed_year in selected && passed_month in selected[passed_year]) {
                    var daysToCompare = selected[passed_year][passed_month];
                    for (var d in daysToCompare) {
                        dateElements.each(function (index) {
                            if (parseInt($(this).text()) == daysToCompare[d]) {
                                $(this).addClass('selected');
                            }
                        });
                    }

                }
            }

            highlightDates(year, month, dateElements1);
            highlightDates(nextYear, nextMonth, dateElements2);
        }
    }

    function makeMonthArray(passed_month, passed_year) { // creates Array specifying dates and weekdays
        var e = [];
        for (var r = 1; r < getDaysInMonth(passed_year, passed_month) + 1; r++) {
            e.push({
                day: r,
                // Later refactor -- weekday needed only for first week
                weekday: daysArray[getWeekdayNum(passed_year, passed_month, r)]
            });
        }
        return e;
    }

    function makeWeek(week) {
        week.empty();
        for (var e = 0; e < 7; e++) {
            week.append("<div>" + daysArray[e].substring(0, 3) + "</div>")
        }
    }

    function getDaysInMonth(currentYear, currentMon) {
        return (new Date(currentYear, currentMon + 1, 0)).getDate();
    }

    function getWeekdayNum(e, t, n) {
        return (new Date(e, t, n)).getDay();
    }

    function checkToday(e) {
        var todayDate = today.getFullYear() + '/' + (today.getMonth() + 1) + '/' + today.getDate();
        var checkingDate = e.getFullYear() + '/' + (e.getMonth() + 1) + '/' + e.getDate();
        return todayDate == checkingDate;

    }

    function getAdjacentMonth(curr_month, curr_year, direction) {
        var theNextMonth;
        var theNextYear;
        if (direction == "next") {
            theNextMonth = (curr_month + 1) % 12;
            theNextYear = (curr_month == 11) ? curr_year + 1 : curr_year;
        } else {
            theNextMonth = (curr_month == 0) ? 11 : curr_month - 1;
            theNextYear = (curr_month == 0) ? curr_year - 1 : curr_year;
        }
        return [theNextMonth, theNextYear];
    }

    function b() {
        today = new Date;
        year = today.getFullYear();
        month = today.getMonth();
        var nextDates = getAdjacentMonth(month, year, "next");
        nextMonth = nextDates[0];
        nextYear = nextDates[1];
    }

    var e = 480;

    var today;
    var year,
    month,
    nextMonth,
    nextYear;

    var r = [];
    var i = [
        "JANUARY",
        "FEBRUARY",
        "MARCH",
        "APRIL",
        "MAY",
        "JUNE",
        "JULY",
        "AUGUST",
        "SEPTEMBER",
        "OCTOBER",
        "NOVEMBER",
        "DECEMBER"];
    var daysArray = [
        "Sunday",
        "Monday",
        "Tuesday",
        "Wednesday",
        "Thursday",
        "Friday",
        "Saturday"];

    var cal1 = $("#calendar_first");
    var calHeader1 = cal1.find(".calendar_header");
    var weekline1 = cal1.find(".calendar_weekdays");
    var datesBody1 = cal1.find(".calendar_content");

    var cal2 = $("#calendar_second");
    var calHeader2 = cal2.find(".calendar_header");
    var weekline2 = cal2.find(".calendar_weekdays");
    var datesBody2 = cal2.find(".calendar_content");

    var bothCals = $(".calendar");

    var switchButton = bothCals.find(".calendar_header").find('.switch-month');

    var calendars = {
        "cal1": {
            "name": "first",
                "calHeader": calHeader1,
                "weekline": weekline1,
                "datesBody": datesBody1
        },
            "cal2": {
            "name": "second",
                "calHeader": calHeader2,
                "weekline": weekline2,
                "datesBody": datesBody2
        }
    }


    var clickedElement;
    var firstClicked,
    secondClicked,
    thirdClicked;
    var firstClick = false;
    var secondClick = false;
    var selected = {};

    b();
    c(month, year, 0);
    c(nextMonth, nextYear, 1);
    switchButton.on("click", function () {
        var clicked = $(this);
        var generateCalendars = function (e) {
            var nextDatesFirst = getAdjacentMonth(month, year, e);
            var nextDatesSecond = getAdjacentMonth(nextMonth, nextYear, e);
            month = nextDatesFirst[0];
            year = nextDatesFirst[1];
            nextMonth = nextDatesSecond[0];
            nextYear = nextDatesSecond[1];

            c(month, year, 0);
            c(nextMonth, nextYear, 1);
        };
        if (clicked.attr("class").indexOf("left") != -1) {
            generateCalendars("previous");
        } else {
            generateCalendars("next");
        }
        clickedElement = bothCals.find(".calendar_content").find("div");
    });


    //  Click picking stuff
    function getClickedInfo(element, calendar) {
        var clickedInfo = {};
        var clickedCalendar,
        clickedMonth,
        clickedYear;
        clickedCalendar = calendar.name;
        clickedMonth = clickedCalendar == "first" ? month : nextMonth;
        clickedYear = clickedCalendar == "first" ? year : nextYear;
        clickedInfo = {
            "calNum": clickedCalendar,
                "date": parseInt(element.text()),
                "month": clickedMonth,
                "year": clickedYear
        }
        return clickedInfo;
    }


    // Finding between dates MADNESS. Needs refactoring and smartening up :)
    function addChosenDates(firstClicked, secondClicked, selected) {
        if (secondClicked.date > firstClicked.date || secondClicked.month > firstClicked.month || secondClicked.year > firstClicked.year) {

            var added_year = secondClicked.year;
            var added_month = secondClicked.month;
            var added_date = secondClicked.date;

            if (added_year > firstClicked.year) {
                // first add all dates from all months of Second-Clicked-Year
                selected[added_year] = {};
                selected[added_year][added_month] = [];
                for (var i = 1;
                i <= secondClicked.date;
                i++) {
                    selected[added_year][added_month].push(i);
                }

                added_month = added_month - 1;
                while (added_month >= 0) {
                    selected[added_year][added_month] = [];
                    for (var i = 1;
                    i <= getDaysInMonth(added_year, added_month);
                    i++) {
                        selected[added_year][added_month].push(i);
                    }
                    added_month = added_month - 1;
                }

                added_year = added_year - 1;
                added_month = 11; // reset month to Dec because we decreased year
                added_date = getDaysInMonth(added_year, added_month); // reset date as well

                // Now add all dates from all months of inbetween years
                while (added_year > firstClicked.year) {
                    selected[added_year] = {};
                    for (var i = 0; i < 12; i++) {
                        selected[added_year][i] = [];
                        for (var d = 1; d <= getDaysInMonth(added_year, i); d++) {
                            selected[added_year][i].push(d);
                        }
                    }
                    added_year = added_year - 1;
                }
            }

            if (added_month > firstClicked.month) {
                if (firstClicked.year == secondClicked.year) {
                    selected[added_year][added_month] = [];
                    for (var i = 1;
                    i <= secondClicked.date;
                    i++) {
                        selected[added_year][added_month].push(i);
                    }
                    added_month = added_month - 1;
                }
                while (added_month > firstClicked.month) {
                    selected[added_year][added_month] = [];
                    for (var i = 1;
                    i <= getDaysInMonth(added_year, added_month);
                    i++) {
                        selected[added_year][added_month].push(i);
                    }
                    added_month = added_month - 1;
                }
                added_date = getDaysInMonth(added_year, added_month);
            }

            for (var i = firstClicked.date + 1;
            i <= added_date;
            i++) {
                selected[added_year][added_month].push(i);
            }
        }
        return selected;
    }
});


// 選擇日曆事件 


$(document).ready(function () {
    var selectedStartDate = null;
    var selectedEndDate = null;

    $('#toggle3').on('click', function (e) {
        e.stopPropagation(); 
        $('.my-element .calendar').toggle(); 
    });

    
    $(".my-element .calendar .calendar_content").on("click", 'div', function () {
        var clicked = $(this);

       
        var selectedYear = parseInt(clicked.closest(".calendar").find(".calendar_header h2").text().split(" ")[1]);
        var selectedMonth = clicked.closest(".calendar").find(".calendar_header h2").text().split(" ")[0];
        var selectedDay = parseInt(clicked.text());

       
        var monthMapping = {
            "JANUARY": 0,
            "FEBRUARY": 1,
            "MARCH": 2,
            "APRIL": 3,
            "MAY": 4,
            "JUNE": 5,
            "JULY": 6,
            "AUGUST": 7,
            "SEPTEMBER": 8,
            "OCTOBER": 9,
            "NOVEMBER": 10,
            "DECEMBER": 11
        };

        selectedMonth = monthMapping[selectedMonth];

        
        var today = new Date();
        today.setHours(0, 0, 0, 0);

        
        var selectedDate = new Date(selectedYear, selectedMonth, selectedDay);

       
        if (selectedDate < today) {
            // 如果选择的日期是过去日期，清空选择
            selectedStartDate = null;
            selectedEndDate = null;
        } else {
            if (selectedStartDate === null) {
                selectedStartDate = selectedDate;
                selectedEndDate = selectedStartDate;
            } else if (selectedStartDate.getTime() === selectedEndDate.getTime()) {
                selectedEndDate = selectedDate;
            } else {
                selectedStartDate = selectedDate;
                selectedEndDate = selectedStartDate;
            }
        }

        
        if (selectedStartDate === null) {
            // 清空日期显示
            $("#toggle3").next().text("");
        } else if (selectedStartDate.getTime() === selectedEndDate.getTime()) {
            $("#toggle3").next().text(formatDate(selectedStartDate));
        } else {
            $("#toggle3").next().text(formatDate(selectedStartDate) + " - " + formatDate(selectedEndDate));
        }
    });

    
    $(".my-element .calendar").on("click", function (e) {
        e.stopPropagation();
    });

    
    $(document).on('click', function () {
        $('.my-element .calendar').hide();
    });

    
    $(".my-element .calendar .calendar_content").on("click", function (e) {
        e.stopPropagation();
    });

   
    function formatDate(date) {
        if (date) {
            var month = (date.getMonth() + 1).toString().padStart(2, '0');
            var day = date.getDate().toString().padStart(2, '0');
            return month + '/' + day;
        }
        return "";
    }
});




// 選擇照片


$(document).ready(function () {
    var selectedPhotos = [];
    var currentPhotoIndex = -1;
  
    // 上一張按钮事件
    $('#prevButton').on('click', function () {
      if (currentPhotoIndex > 0) {
        currentPhotoIndex--;
        displayCurrentPhoto();
        updateButtonStates();
      }
    });
  
    // 下一張按钮事件
    $('#nextButton').on('click', function () {
      if (currentPhotoIndex < selectedPhotos.length - 1) {
        currentPhotoIndex++;
        displayCurrentPhoto();
        updateButtonStates();
      }
    });
  
    // 一開始灰
    $('#prevButton').prop('disabled', true).addClass('disabled');
    $('#nextButton').prop('disabled', true).addClass('disabled');
  
    $('#photo').on('change', function () {
      const files = this.files;
  
      for (let i = 0; i < files.length; i++) {
        const file = files[i];
        const reader = new FileReader();
  
        if (!file || selectedPhotos.length >= 5) {
          break;
        }
  
        reader.onload = function (event) {
          
          selectedPhotos.push(event.target.result);
          currentPhotoIndex = selectedPhotos.length - 1; 
          displayCurrentPhoto();
          updateButtonStates();
  
          if (selectedPhotos.length === 1) {
            $('#chooseButton').text('再選一張');
          }
  
          if (selectedPhotos.length >= 5) {
            $('#chooseButton').addClass('disabled');
            $('#chooseButton').prop('disabled', true);
            $('#chooseButton').off('click');
  
            $('#chooseButton').on('click', function (event) {
              event.preventDefault();
            });
          }
        };
  
        reader.readAsDataURL(file);
      }
    });
  
    
    function displayCurrentPhoto() {
      if (selectedPhotos.length > 0) {
        $('#thumbnailsContainer img').remove();
        var currentImg = $('<img>').attr('src', selectedPhotos[currentPhotoIndex]);
        currentImg.css('max-width', '100%');
        currentImg.css('max-height', '100%');
        currentImg.css('width', 'auto');
        currentImg.css('height', 'auto');
        currentImg.css('margin-right', '0px');

        $('#thumbnailsContainer').prepend(currentImg);
      }
    }
  
    
    function updateButtonStates() {
      if (currentPhotoIndex === 0) {
        $('#prevButton').prop('disabled', true).addClass('disabled');
      } else {
        $('#prevButton').prop('disabled', false).removeClass('disabled');
      }
  
      if (currentPhotoIndex === selectedPhotos.length - 1) {
        $('#nextButton').prop('disabled', true).addClass('disabled');
      } else {
        $('#nextButton').prop('disabled', false).removeClass('disabled');
      }
    }
  });
  
  
  
  
  


// 提交按鈕
$(document).ready(function() {
    $(".submit2").click(function() {
      var isValid = true;

      if ($("#toggle1").val() === "") {
        isValid = false; 
      }
      
      if ($("#toggle2").val() === "") {
        isValid = false; 
      }

      if ($("#toggle3").val() === "") {
        isValid = false; 
      }

      if ($("#toggle4").val() === "") {
        isValid = false; 
      }
  
      // 檢查照片是否有選擇
      if ($("#photo").val() === "") {
        isValid = false; // 防止表單提交
      }
  
      // 檢查活動名稱是否填寫
      if ($("#title").val() === "") {
        isValid = false; 
      }
  
      // 檢查活動內容是否填寫
      if ($("#content").val() === "") {
        isValid = false; 
      }

      if (!isValid) {
        // 有任何一個條件不通過，彈出警示框
        alert("請檢查是否有填寫完畢");
      } else {
        // 所有條件通過，執行提交操作
      }
      
      return isValid;
    });
});
