document.addEventListener("visibilitychange", function () {
  if (document.hidden) {
    document.title = "Finish your purchase!";
  } else {
    document.title = "Ticket cart";
  }
});

window.onload = populateDropdown;

function buy(){

}

function updateCart(){
  
}

function populateDropdown() {
  var dropdown = document.getElementById("c1d");
  var maxPlaces = get_max_places();

  for (var i = 0; i <= maxPlaces; i++) {
      var option = document.createElement("option");
      option.value = i;
      option.text = i;
      dropdown.appendChild(option);
  }
}

function get_max_places(id) {
  return 10;
}