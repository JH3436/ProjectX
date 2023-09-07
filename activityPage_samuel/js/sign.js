
    
    let btn=document.querySelector("#signBtn");
    let infoModal=document.querySelector("#signDialog");
    let close=document.querySelector("#dialogClose");
    btn.addEventListener("click", function(){
      infoModal.showModal();
    })
    close.addEventListener("click", function(){
      infoModal.close();
    })