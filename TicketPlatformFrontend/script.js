isAdmin=true;
document.addEventListener('DOMContentLoaded', function() {
    
    if (!isAdmin) {
      var adminMenuItem = document.getElementById('admin-menu-button');
      if (adminMenuItem) {
        adminMenuItem.classList.add('hidden');
      }
    }
  });

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