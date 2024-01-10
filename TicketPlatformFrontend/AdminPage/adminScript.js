
document.addEventListener('DOMContentLoaded',function(){
    getAdminEvents();
});



let eventListA = [];
let citiesA = [];
let venuesA = [];

let selectedCityA = "All";
let selectedVenueA = "All";
let searchTextA = "";

const locationFilterSelectA = document.getElementById('location-filter-admin');
const venueFilterSelectA = document.getElementById('host-filter-admin');
const searchFilterA = document.getElementById('search-filter-admin');

locationFilterSelectA.addEventListener('change', function() {
  // Handle the selected city
  selectedCityA = this.value;
  filterAdminEvents();

});

venueFilterSelectA.addEventListener('change', function() {
  // Handle the selected venue
  selectedVenueA = this.value;
  filterAdminEvents();

});

searchFilterA.addEventListener('input', function() {
  searchTextA = this.value;
  filterAdminEvents();
});

const startDateA = document.getElementById('start-date-filter-admin');
const endDateA = document.getElementById('end-date-filter-admin');

startDateA.addEventListener('change', function() {
    filterAdminEvents();
});

endDateA.addEventListener('change', function() {
    filterAdminEvents();
});

function getAdminEvents(){

    let eventsContainer = document.getElementById('events-container');
    eventsContainer.innerHTML = "";
    fetch(eventsEndpoint)
    .then(response => response.json())
    .then(data => {
      eventListA = data;
  
      eventListA.forEach(event => {
  
      if (!citiesA.includes(event.city)) {
          citiesA.push(event.city);
      }
  
      if (!venuesA.includes(event.venue)) {
          venuesA.push(event.venue);
      }
      
      let eventDiv = document.createElement('div');
      eventDiv.classList.add('event');
      eventDiv.id = `ev${event.id}`;
  
      // Wishlist button
      const wishlistBtn = document.createElement('div');
      wishlistBtn.classList.add('delete-btn');
      wishlistBtn.innerHTML = 'DEL';
  
      wishlistBtn.addEventListener('click', function() {
          delete_event(this.parentNode.id);
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
      seeMoreButton.onclick = () => {
        redirectToEvent(event.id);
      };
      eventDescription.appendChild(seeMoreButton);
  
      // Append the event description to the event div
      eventDiv.appendChild(eventDescription);
  
      // Append the event div to the events container
      eventsContainer.appendChild(eventDiv);
        
      });
  
      const opt1=document.createElement('option');
      opt1.value = "All";
      opt1.text = "All";
      locationFilterSelectA.appendChild(opt1);
      const opt2=document.createElement('option');
      opt2.value = "All";
      opt2.text = "All";
      venueFilterSelectA.appendChild(opt2);
  
      citiesA.forEach(city => {
        let option = document.createElement('option');
        option.value = city;
        option.text = city;
        locationFilterSelectA.appendChild(option);
      });
    
      venuesA.forEach(venue => {
        const option = document.createElement('option');
        option.value = venue;
        option.text = venue;
        venueFilterSelectA.appendChild(option);
      });
    })
    .catch(error => {
      console.error('Error fetching events:', error);
    });
    
}

function filterAdminEvents(){
    let eventsContainer = document.getElementById("events-container");
    eventsContainer.innerHTML = "";
  
    fetch(eventsEndpoint)
    .then(response => response.json())
    .then(data => {
      eventListA = data;
  
      eventListA.forEach(event => {
      if(!((searchTextA!="" && !event.title.toLowerCase().includes(searchTextA.toLowerCase())) || (selectedCityA!="All"&&event.city!=selectedCityA) || (selectedVenueA!="All" && event.venue!=selectedVenueA) || (startDateA.value && new Date(event.date) < new Date(startDateA.value)) || (endDateA.value && new Date(event.date) > new Date(endDateA.value)))) 
      {
        let eventDiv = document.createElement('div');
        eventDiv.classList.add('event');
        eventDiv.id = `ev${event.id}`;
  
        // Wishlist button
        const wishlistBtn = document.createElement('div');
        wishlistBtn.classList.add('delete-btn');
        wishlistBtn.innerHTML = 'DEL';
    
        wishlistBtn.addEventListener('click', function() {
            delete_event(this.parentNode.id);
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
        seeMoreButton.onclick = () => {
          redirectToEvent(event.id);
        };
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

function delete_event(evId){
    let id = parseInt(evId.substring(2));
    const deleteEndpoint = `https://localhost:7075/events/${id}`;
    const requestOptions = {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json',
        },
    };

    fetch(deleteEndpoint, requestOptions)
    .then(response => {
        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }
        console.log('Event deleted successfully!');
        getAdminEvents();
    })
    .catch(error => {
        console.error('Error deleting event:', error);
    });
    
}
