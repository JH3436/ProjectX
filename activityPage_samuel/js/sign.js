
    
    let btn=document.querySelector("#signBtn");
    let infoModal=document.querySelector("#signDialog");
    let close=document.querySelector("#dialogClose");
    let sign=document.querySelector("#dialogSign");
    btn.addEventListener("click", function(){
      infoModal.showModal();      
    })
    close.addEventListener("click", function(){
      infoModal.close();
    })
    sign.addEventListener("click", function(){
      var userName = $('#nameInput').val();
      var userTel = $('#telInput').val();
      if (userName == "" || userTel == ""){
        alert("請輸入資料")
      }else{
        var n =  confirm('確定資料正確');
        if( n == true){
          infoModal.close();
        }
      }
    })

    

    
    $(() => {
      // $('#calendar').click(function(){
      //   console.log($('.day-number'));
      // })

      // $('#signBtn').click(function(){
      //   $('#signDialog').show();

      // });
      
      // $('#dialogSign').click(function(){
      //   // confirm('確定資料正確');
      //   $(this).close();
      // });
    })