
    
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
      console.log($('calendar'));
      confirm('確定資料正確');
      infoModal.close();
    })

    
    $(() => {
      // $('#signBtn').click(function(){
      //   $('#signDialog').show();

      // });
      
      // $('#dialogSign').click(function(){
      //   // confirm('確定資料正確');
      //   $(this).close();
      // });
    })