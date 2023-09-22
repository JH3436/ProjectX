$("button-container").click(function() {
        
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
