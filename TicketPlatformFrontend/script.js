var eventsItem;
var wishlistItem;
var adminItem;
var myAccountItem;

let isAdmin =false;

//endpoints
const eventsEndpoint = 'https://localhost:7075/events';
const wishlistEndpoint = 'https://localhost:7075/favourites';


const locationFilterSelect = document.getElementById('location-filter');
const venueFilterSelect = document.getElementById('host-filter');

function redirectToBrowse(){
    window.location.href = "../BrowsePage/browsePage.html";
    console.log("isadmin",isAdmin);
    console.log("ok");
    console.log(localStorage.getItem('user_id'));

}

window.onload = function() {
  //console.log(localStorage.getItem('isAdmin'));
  if(localStorage.getItem('isAdmin')=="true")
    isAdmin = true;
  setTimeout(function() {
      hideAdmin(isAdmin);
  }, 50);
};


function hideAdmin(isAdmin){
  var adminMenuItem = document.getElementById('admin-menu-button');
  //console.log("hide");

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

////////////////////////////////////////////////////////////////////////
                  //EVENTS CODE and SCRIPTS
////////////////////////////////////////////////////////////////////////


let eventList = [];
let cities = [];
let venues = [];

let selectedCity = "All";
let selectedVenue = "All";
let searchText = "";


const searchFilter = document.getElementById('search-filter');

locationFilterSelect.addEventListener('change', function() {
  // Handle the selected city
  selectedCity = this.value;
  filterEvents();

});

venueFilterSelect.addEventListener('change', function() {
  // Handle the selected venue
  selectedVenue = this.value;
  filterEvents();

});

searchFilter.addEventListener('input', function() {
  searchText = searchFilter.value;
  filterEvents();
});

const startDate = document.getElementById('start-date-filter');
const endDate = document.getElementById('end-date-filter');

startDate.addEventListener('change', function() {
  filterEvents();
});

endDate.addEventListener('change', function() {
  filterEvents();
});

function getEvents(){

  let eventsContainer = document.getElementById('events-container');

  fetch(eventsEndpoint)
  .then(response => response.json())
  .then(data => {
    eventList = data;

    eventList.forEach(event => {

    if (!cities.includes(event.city)) {
        cities.push(event.city);
    }

    if (!venues.includes(event.venue)) {
        venues.push(event.venue);
    }
    
    let eventDiv = document.createElement('div');
    eventDiv.classList.add('event');
    eventDiv.id = `ev${event.id}`;

    // Wishlist button
    const wishlistBtn = document.createElement('div');
    wishlistBtn.classList.add('wishlist-btn');
    console.log("evId",event.id,checkWishlist(localStorage.getItem('user_id'), event.id));
    checkWishlist(localStorage.getItem('user_id'), event.id)
    .then(result => {
        if (result.length > 0) {
            wishlistBtn.innerHTML = '★';
        } else {
            wishlistBtn.innerHTML = '☆';
        }
    });

    wishlistBtn.addEventListener('click', function() {
        add_remove_wishlist(this.parentNode.id);
    });
    eventDiv.appendChild(wishlistBtn);

    // Event data (image)
    const eventData = document.createElement('div');
    eventData.classList.add('event-data');
    const eventImg = document.createElement('img');
    eventImg.classList.add('event-img');
    eventImg.src = event.thumbnail;
    eventData.appendChild(eventImg);
    eventDiv.appendChild(eventData);

    // Event description
    const eventDescription = document.createElement('div');
    eventDescription.classList.add('event-description');
    const eventTitle = document.createElement('h1');
    eventTitle.classList.add('event-title');
    eventTitle.textContent = event.title;
    const eventText = document.createTextNode(event.description);
    eventDescription.appendChild(eventTitle);
    eventDescription.appendChild(eventText);

    // Event info
    const eventInfo = document.createElement('div');
    eventInfo.classList.add('event-info');
    const infoLabels = ['City', 'Venue', 'Date/Time', 'Tickets', 'Price'];
    const labelsAndProperties = [
      { label: 'City', property: 'city' },
      { label: 'Venue', property: 'venue' },
      { label: 'Date/Time', property: 'date' },
      { label: 'Tickets left', property: 'totalTickets' },
      { label: 'Price', property: 'price' }
  ];

  // Append each info label and data
  labelsAndProperties.forEach(item => {
      const infoData = document.createElement('span');
      infoData.classList.add('event-info-data');

      // Special case for formatting the date
      if (item.label === 'Date/Time') {
          const date = new Date(event[item.property]);
          infoData.textContent = `${item.label}: ${date.toLocaleDateString('en-US', { year: 'numeric', month: 'short', day: 'numeric' })}`;
      } else {
          infoData.textContent = `${item.label}: ${event[item.property]}`;
      }
      
      eventInfo.appendChild(infoData);
  });
  
    eventDescription.appendChild(eventInfo);

    // See more button
    const seeMoreButton = document.createElement('button');
    seeMoreButton.classList.add('see-more-button');
    seeMoreButton.textContent = 'See more >>';
    eventDescription.appendChild(seeMoreButton);

    // Append the event description to the event div
    eventDiv.appendChild(eventDescription);

    // Append the event div to the events container
    eventsContainer.appendChild(eventDiv);
      
    });

    const opt1=document.createElement('option');
    opt1.value = "All";
    opt1.text = "All";
    locationFilterSelect.appendChild(opt1);
    const opt2=document.createElement('option');
    opt2.value = "All";
    opt2.text = "All";
    venueFilterSelect.appendChild(opt2);

    cities.forEach(city => {
      let option = document.createElement('option');
      option.value = city;
      option.text = city;
      locationFilterSelect.appendChild(option);
    });
  
    venues.forEach(venue => {
      const option = document.createElement('option');
      option.value = venue;
      option.text = venue;
      venueFilterSelect.appendChild(option);
    });
  })
  .catch(error => {
    console.error('Error fetching events:', error);
  });
  
}


function filterEvents(){
  let eventsContainer = document.getElementById("events-container");
  eventsContainer.innerHTML = "";

  fetch(eventsEndpoint)
  .then(response => response.json())
  .then(data => {
    eventList = data;

    eventList.forEach(event => {
    if(!((searchText!="" && !event.title.toLowerCase().includes(searchText.toLowerCase())) || (selectedCity!="All"&&event.city!=selectedCity) || (selectedVenue!="All" && event.venue!=selectedVenue) || (startDate.value && new Date(event.date) < new Date(startDate.value)) || (endDate.value && new Date(event.date) > new Date(endDate.value)))) 
    {
      let eventDiv = document.createElement('div');
      eventDiv.classList.add('event');
      eventDiv.id = `ev${event.id}`;

      // Wishlist button
      const wishlistBtn = document.createElement('div');
      wishlistBtn.classList.add('wishlist-btn');
      console.log("wishlistBtn:", checkWishlist(localStorage.getItem('user_id'), event.id));
      checkWishlist(localStorage.getItem('user_id'), event.id)
      .then(result => {
        if (result.length > 0) {
            wishlistBtn.innerHTML = '★';
        } else {
            wishlistBtn.innerHTML = '☆';
        }
      });
      wishlistBtn.addEventListener('click', function() {
          add_remove_wishlist(this.parentNode.id);
      });
      eventDiv.appendChild(wishlistBtn);

      // Event data (image)
      const eventData = document.createElement('div');
      eventData.classList.add('event-data');
      const eventImg = document.createElement('img');
      eventImg.classList.add('event-img');
      eventImg.src = event.thumbnail;
      eventData.appendChild(eventImg);
      eventDiv.appendChild(eventData);

      // Event description
      const eventDescription = document.createElement('div');
      eventDescription.classList.add('event-description');
      const eventTitle = document.createElement('h1');
      eventTitle.classList.add('event-title');
      eventTitle.textContent = event.title;
      const eventText = document.createTextNode(event.description);
      eventDescription.appendChild(eventTitle);
      eventDescription.appendChild(eventText);

      // Event info
      const eventInfo = document.createElement('div');
      eventInfo.classList.add('event-info');
      const infoLabels = ['City', 'Venue', 'Date/Time', 'Tickets', 'Price'];
      const labelsAndProperties = [
        { label: 'City', property: 'city' },
        { label: 'Venue', property: 'venue' },
        { label: 'Date/Time', property: 'date' },
        { label: 'Tickets left', property: 'totalTickets' },
        { label: 'Price', property: 'price' }
    ];

    // Append each info label and data
    labelsAndProperties.forEach(item => {
        const infoData = document.createElement('span');
        infoData.classList.add('event-info-data');

        // Special case for formatting the date
        if (item.label === 'Date/Time') {
            const date = new Date(event[item.property]);
            infoData.textContent = `${item.label}: ${date.toLocaleDateString('en-US', { year: 'numeric', month: 'short', day: 'numeric' })}`;
        } else {
            infoData.textContent = `${item.label}: ${event[item.property]}`;
        }
        
        eventInfo.appendChild(infoData);
      });
    
      eventDescription.appendChild(eventInfo);

      // See more button
      const seeMoreButton = document.createElement('button');
      seeMoreButton.classList.add('see-more-button');
      seeMoreButton.textContent = 'See more >>';
      eventDescription.appendChild(seeMoreButton);

      // Append the event description to the event div
      eventDiv.appendChild(eventDescription);

      // Append the event div to the events container
      eventsContainer.appendChild(eventDiv);
    }
    });
  })
  .catch(error => {
    console.error('Error fetching events:', error);
  });
}

function add_remove_wishlist(id) {
  const eventId = id.substring(2);

  let wishlistBody = {
    userId: -1,      
    adminId: -1,
    eventId: Number(eventId)
  };
  if(localStorage.getItem('isAdmin')=="true"){
    wishlistBody.adminId = localStorage.getItem('user_id');
  }
  else
  {
    wishlistBody.userId = localStorage.getItem('user_id');
  }

  checkWishlist(localStorage.getItem('user_id'), eventId).then(result => {
    if (result === false) {
        // Add to wishlist
        fetch(wishlistEndpoint, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(wishlistBody)
        })
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }
            return response.json();
        })
        .then(data => {
            console.log('Wishlist updated successfully:', data);
            // Handle the response data as needed
        })
        .catch(error => {
            console.error('Error updating wishlist:', error);
        });
    } else {
        // Remove from wishlist
        result.forEach(res=>
        {let endpointDel = wishlistEndpoint + '/' + res.id;
        fetch(endpointDel, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json',
            },
        })
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }
            return response.json();
        })
        .then(data => {
            console.log('Wishlist item deleted successfully:', data);
            // Handle the response data as needed
        })
        .catch(error => {
            console.error('Error deleting wishlist item:', error);
        });});
    }
    });

  var ev = document.getElementById(id);
          var starDiv = ev.querySelector('.wishlist-btn');
          
          if (starDiv.textContent === '☆') {
              starDiv.textContent = '★';

          } else {
              
              starDiv.textContent = '☆'; 

          }
}


function checkWishlist(userId, evId) {
  return fetch('https://localhost:7075/favourites')
      .then(response => response.json())
      .then(data => {
          const matchingRows = data.filter(item => 
            (parseInt(item.userId) === parseInt(userId) && parseInt(item.eventId) === parseInt(evId)) ||
            (parseInt(item.adminId) === parseInt(userId) && parseInt(item.eventId) === parseInt(evId))
          );
          data.forEach(item => {
            
          });

          if (matchingRows.length > 0) {
              //console.log('Matching rows found:', matchingRows);
              return matchingRows;
          } else {
              //console.log('No matching rows found.');
              return false;
          }
     })
      .catch(error => {
          console.error('Error fetching wishlist:', error);
          return false;
      });
}





