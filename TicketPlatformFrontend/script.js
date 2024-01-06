var eventsItem;
var wishlistItem;
var adminItem;
var myAccountItem;

let isAdmin = JSON.parse(localStorage.getItem('isAdmin')) || false;

function redirectToBrowse(){
    window.location.href = "../BrowsePage/browsePage.html";
    console.log("isadmin",isAdmin);
    console.log("ok")

}

window.onload = function() {
  console.log(isAdmin);
  setTimeout(function() {
      hideAdmin(isAdmin);
  }, 50);
};


function hideAdmin(isAdmin){
  var adminMenuItem = document.getElementById('admin-menu-button');
  console.log("hide");

  if (!isAdmin) {
      if (adminMenuItem) {
          adminMenuItem.classList.add('hidden');
        }
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

