document.addEventListener("visibilitychange", function () {
  if (document.hidden) {
    document.title = "Finish your purchase!";
  } else {
    document.title = "Ticket cart";
  }
});

document.addEventListener("DOMContentLoaded", function(){
  populateCartItems();
});

let cartItems;

function buy(){

}

function getCart() {
  const userId = localStorage.getItem('user_id');
  const endpoint = `https://localhost:7075/cart`;

  return fetch(endpoint)
      .then(response => response.json())
      .then(cartItems => {
        let finalCart = cartItems.filter(item => parseInt(item.idUser) === parseInt(userId));
        console.log(finalCart);
        return finalCart;
      })
      .catch(error => {
          console.error('Error fetching cart data:', error);
          return [];
      });
}

async function populateCartItems() {
  const cartContainer = document.getElementById('data-container');
  cartContainer.innerHTML ="";
  cartItems = await getCart();

  for (const cartItem of cartItems) {
    // Create a unique identifier for each cart item
    const cartItemId = `c${cartItem.idEvent}`;

    // Create cart item div
    const cartItemDiv = document.createElement('div');
    cartItemDiv.classList.add('cart-item');
    cartItemDiv.id = cartItemId;

    // Create item info div
    const itemInfoDiv = document.createElement('div');
    itemInfoDiv.classList.add('item-info');

    // Get title asynchronously
    let evt = await getEvt(cartItem.idEvent);
    const title = evt.title;
    itemInfoDiv.textContent = title;

    cartItemDiv.appendChild(itemInfoDiv);

    const deleteButton = document.createElement('button');
    deleteButton.classList.add('delete-from-cart');
    deleteButton.textContent = 'Delete';
    deleteButton.onclick = () => deleteItem(cartItem.idEvent);

    cartItemDiv.appendChild(deleteButton);

    let dropdown = document.createElement('select');
    dropdown.classList.add('cart-dropdowns');
    dropdown.id = cartItem.idEvent;

    for (let i = 1; i <= 25; i++) {
      const option = document.createElement('option');
      option.value = i;
      option.text = i;
      dropdown.appendChild(option);
    }

    // Set the selected value based on cartItem.ticketsNumber
    dropdown.value = cartItem.ticketsNumber;
    cartItemDiv.appendChild(dropdown);

    dropdown.addEventListener('change', function(event){
      updatePrice();
    });

    // Append the cart item to the container
    cartContainer.appendChild(cartItemDiv);
  }
}

function updateCart(){
  
}

async function updatePrice() {
  const priceElement = document.getElementById("price");
  let totalPrice = 0;

  Array.from(cartItems).forEach(async function (cartItem) {
      const evt = await getEvt(cartItem.idEvent);

      console.log(evt);
      const eventPrice = evt.price;
      const ticketsNumber = document.getElementById(cartItem.idEvent).value;

      
      totalPrice += eventPrice * ticketsNumber;

      console.log(totalPrice);
      priceElement.innerHTML = totalPrice.toFixed(2); 
  });

  
}


async function deleteItem(idEvent) {
  const idUser = localStorage.getItem('user_id');
  const deleteEndpoint = `https://localhost:7075/cart`;

  const requestOptions = {
      method: 'DELETE',
      headers: {
          'Content-Type': 'application/json',
      },
      body: JSON.stringify({ idEvent: idEvent, idUser: idUser, ticketsNumber: 0}),
  };

  try {
      const response = await fetch(deleteEndpoint, requestOptions);
      if (response.ok) {
          console.log(`Successfully deleted item with idEvent ${idEvent}`);
          populateCartItems();
      } else {
          console.error(`Error deleting item with idEvent ${idEvent}. Status: ${response.status}`);
      }
  } catch (error) {
      console.error('Error deleting item:', error);
  }
}

function getEvt(evId){
  const endpoint = `https://localhost:7075/events/${evId}`;

  return fetch(endpoint)
      .then(response => response.json())
      .then(event => {
          return event.value;
      })
      .catch(error => {
          console.error('Error fetching event data:', error);
          return null;
      });

}

function populateDropdowns() {
  var maxPlaces = get_max_places();
  let dds = document.getElementsByClassName("cart-dropdowns");
  
  Array.from(dds).forEach(function(dd){
    for (var i = 0; i <= maxPlaces; i++) {
      var option = document.createElement("option");
      option.value = i;
      option.text = i;
      dd.appendChild(option);
  }
  });
  
}

function get_max_places(id) {
  return 10;
}