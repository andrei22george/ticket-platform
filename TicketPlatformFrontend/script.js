isAdmin=true;


var eventsItem;
var wishlistItem;
var adminItem;
var myAccountItem;

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
        window.location.href = "../BrowsePage/browsePage.html";
        console.log("ok")
    }
    else
    {
        console.log("You have to sign in first!");
    }
}

function redirectToWishlist(){
  var loginButton = document.getElementById("login-button");

  if (!loginButton) {
      window.location.href = "../WishlistPage/wishlistPage.html";
      console.log("ok")
  }
  else
  {
      console.log("You have to sign in first!");
  }
}

function redirectToAdmin(){
  var loginButton = document.getElementById("login-button");

  if (!loginButton) {
      window.location.href = "../AdminPage/adminPage.html";
      console.log("ok")
  }
  else
  {
      console.log("You have to sign in first!");
  }
}

function redirectToAccount(){
  var loginButton = document.getElementById("login-button");

  if (!loginButton) {
      window.location.href = "../MyAccountPage/accountPage.html";
      console.log("ok")
  }
  else
  {
      console.log("You have to sign in first!");
  }
}

function redirectToCart(){
  var loginButton = document.getElementById("login-button");

  if(!loginButton){
    window.location.href = "../CartPage/cart.html";
    console.log("ok");
  }
  else
  {
    console.log("You have to sign in first!");
  }
}

