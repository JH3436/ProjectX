// 導覽列
$(document).ready(function() {
  // 選取探索按鈕
  var exploreLink = $("#explore-link");
  // 選取下拉內容
  var exploreDropdown = $("#explore-dropdown");
  //選取揪團按鈕
  var groupBtn = $("#group-link");

  exploreLink.hover(
    function() {
      // 滑鼠進入時顯示下拉內容
      exploreDropdown.css("display", "block");
    
  });

  exploreDropdown.hover(
    function() {
      // 滑鼠進入下拉內容時保持顯示
      exploreDropdown.css("display", "block");
    },
    function() {
      // 滑鼠離開下拉內容時隱藏
      exploreDropdown.css("display", "none");
    }
  );
    //滑鼠移動到揪團也會讓下拉消失
    groupBtn.hover(
      function() {
        exploreDropdown.css("display", "none")
      }
    )
});
