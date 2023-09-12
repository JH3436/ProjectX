$(() => {
    $('.morePicture').click(function(){
        var n = $(this).attr('src').substr();
        $("#picture").attr("src",n);
        console.log(n);

    });
  })


  