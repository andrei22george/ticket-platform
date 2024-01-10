

document.addEventListener('DOMContentLoaded', function () {
  getFavourites()
      .then(() => {
          //console.log('citiesF', citiesF); // Check if citiesF is populated correctly
          //console.log('venuesF', venuesF); // Check if venuesF is populated correctly
          populateFiltersF();
      })
      .catch(error => {
          console.error('Error in getFavourites:', error);
      });
});




let eventListF = [];
let citiesF = [];
let venuesF = [];

let selectedCityF = "All";
let selectedVenueF = "All";
let searchTextF = "";

const locationFilterSelectF = document.getElementById('location-filter');
const venueFilterSelectF = document.getElementById('host-filter');
const searchFilterF = document.getElementById('search-filter');

locationFilterSelectF.addEventListener('change', function() {
    // Handle the selected city
    selectedCityF = this.value;
    filterEvents();
  
  });
  
  venueFilterSelectF.addEventListener('change', function() {
    // Handle the selected venue
    selectedVenueF = this.value;
    filterEvents();
  
  });
  
  searchFilterF.addEventListener('input', function() {
    searchTextF = searchFilterF.value;
    filterFavourites();
  });


function getFavourites(){

    let eventsContainer = document.getElementById('events-container');
  
    return fetch(eventsEndpoint)
    .then(response => response.json())
    .then(data => {
      eventListF = data;
  
      const promises = [];

      eventListF.forEach(event => {
      ///////
      checkWishlist(localStorage.getItem('user_id'), event.id)
      .then(result => {
          if (result.length > 0) {
            if (!citiesF.includes(event.city)) {
                citiesF.push(event.city);
            }
        
            if (!venuesF.includes(event.venue)) {
                venuesF.push(event.venue);
            }
            
            let eventDiv = document.createElement('div');
            eventDiv.classList.add('event');
            eventDiv.id = `ev${event.id}`;
        
            // Wishlist button
            const wishlistBtn = document.createElement('div');
            wishlistBtn.classList.add('wishlist-btn');
            //console.log("evId",event.id,checkWishlist(localStorage.getItem('user_id'), event.id));
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
              { label: 'Tickets', property: 'totalTickets' },
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
      });
      console.log("get done");
      return true;
    })
    .catch(error => {
      console.error('Error fetching events:', error);
      return false;
    });
    
  }
  
  function populateFiltersF() {
    console.log('Entering populateFiltersF');
    console.log('citiesF:', citiesF);
    console.log('venuesF:', venuesF);

    const opt1 = document.createElement('option');
    opt1.value = "All";
    opt1.text = "All";
    locationFilterSelectF.appendChild(opt1);
    console.log('Option "All" added to locationFilterSelectF');

    const opt2 = document.createElement('option');
    opt2.value = "All";
    opt2.text = "All";
    venueFilterSelectF.appendChild(opt2);
    console.log('Option "All" added to venueFilterSelectF');

    citiesF.forEach(city => {
        console.log('City:', city);
        let option = document.createElement('option');
        option.value = city;
        option.text = city;
        locationFilterSelectF.appendChild(option);
        console.log('Option added to locationFilterSelectF');
    });

    venuesF.forEach(venue => {
        console.log('Venue:', venue);
        const option = document.createElement('option');
        option.value = venue;
        option.text = venue;
        venueFilterSelectF.appendChild(option);
        console.log('Option added to venueFilterSelectF');
    });

    console.log('Exiting populateFiltersF');
}



  
  function filterFavourites(){
    let eventsContainer = document.getElementById("events-container");
    eventsContainer.innerHTML = "";
  
    fetch(eventsEndpoint)
    .then(response => response.json())
    .then(data => {
      eventListF = data;
  
      eventListF.forEach(event => {
      if(!((searchTextF!="" && !event.title.toLowerCase().includes(searchTextF.toLowerCase())) || (selectedCityF!="All"&&event.city!=selectedCityF) || (selectedVenueF!="All" && event.venue!=selectedVenueF) || (startDate.value && new Date(event.date) < new Date(startDate.value)) || (endDate.value && new Date(event.date) > new Date(endDate.value)))) 
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
          { label: 'Tickets', property: 'totalTickets' },
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