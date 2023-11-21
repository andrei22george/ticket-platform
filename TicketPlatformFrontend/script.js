function redirectToBrowse(){
    var loginButton = document.getElementById("login-button");

    if (!loginButton) {
        window.location.href = "browsePage.html";
        console.log("ok")
    }
    else
    {
        console.log("You have to sign in first!");
    }
}