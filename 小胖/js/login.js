function show_hide() {
    var login = document.getElementById("container1");
    var signup = document.getElementById("container2");
    

    if (login.style.display === "block") {
        login.style.display = "none";    //login消失
        signup.style.display = "block";  //signup出現
        signup.style.visibility = "visible";
        
    } else {
        login.style.display = "block";    //login出現
        signup.style.display = "none";    //signup消失
        
    }
}