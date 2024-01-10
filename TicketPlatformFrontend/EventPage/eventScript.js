
var currentEvent = -1;

document.addEventListener("DOMContentLoaded", function(){
    currentEvent = localStorage.getItem("currentEvent");
    getEventData();
});

function getEventData(){
    console.log(currentEvent);
    const endpoint = `https://localhost:7075/events/${currentEvent}`;

    fetch(endpoint)
        .then(response => {
            if (!response.ok) {
                throw new Error(`Failed to fetch event details. Status: ${response.status}`);
            }
            return response.json();
        })
        .then(data => {
            if (data && data.value) {
                populateEventData(data.value);
            } else {
                console.error('Invalid response format:', data);
            }
        })
        .catch(error => {
            console.error('Error fetching event details:', error);
        });
}

function populateEventData(evt){
    console.log(evt);
    const titleElement = document.getElementById('event-title');
    const descriptionElement = document.getElementById('event-description');
    const thumbnailElement = document.getElementById('event-thumbnail');

    titleElement.innerHTML = evt.title;
    descriptionElement.innerHTML = evt.description;
    thumbnailElement.src = evt.thumbnail;

    document.getElementById('evt-city').innerHTML ="City: "+ evt.city;
    document.getElementById('evt-venue').innerHTML ="Venue: "+ evt.venue;
    document.getElementById('evt-date').innerHTML ="Date: "+ new Date(evt.date).toLocaleDateString('en-US', {
        year: 'numeric',
        month: 'short',
        day: 'numeric'
    });
    document.getElementById('evt-tickets').innerHTML ="Tickets: "+ evt.totalTickets;
    document.getElementById('evt-price').innerHTML ="Price: "+ evt.price;


}

function addToCart() {
    const endpoint = 'https://localhost:7075/cart';
    checkCart(currentEvent, localStorage.getItem('user_id')).then(result => {
        if (result == -1) {
            let postData = {
                idEvent: localStorage.getItem('currentEvent'),
                idUser: localStorage.getItem('user_id'),
                ticketsNumber: 1
            };

            console.log(postData);

            const requestOptions = {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(postData),
            };

            // Make the POST request
            fetch(endpoint, requestOptions)
                .then(response => {
                    if (!response.ok) {
                        throw new Error(`HTTP error! Status: ${response.status}`);
                    }
                    return response.json();
                })
                .then(data => {
                    console.log('POST request successful:', data);
                })
                .catch(error => {
                    console.error('Error during POST request:', error);
                });
        } else {
            // Check if result is a valid number
            const ticketsNumber = parseInt(result);
            if (!isNaN(ticketsNumber)) {
                let postData = {
                    idEvent: localStorage.getItem('currentEvent'),
                    idUser: localStorage.getItem('user_id'),
                    ticketsNumber: ticketsNumber + 1
                };

                console.log(postData);

                const requestOptions = {
                    method: 'PUT',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify(postData),
                };

                let endpointUpdate = `${endpoint}?user=${postData.idUser}&event=${postData.idEvent}`;
                
                // Make the PUT request
                fetch(endpointUpdate, requestOptions)
                    .then(response => {
                        if (!response.ok) {
                            throw new Error(`HTTP error! Status: ${response.status}`);
                        }
                        return response.json();
                    })
                    .then(data => {
                        console.log('PUT request successful:', data);
                    })
                    .catch(error => {
                        console.error('Error during PUT request:', error);
                    });
            } else {
                console.error('Invalid result returned from checkCart');
            }
        }
    });
}


function checkCart(evtId, userId){
    const endpoint = 'https://localhost:7075/cart';

    return fetch(endpoint)
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }
            return response.json();
        })
        .then(data => {
            const matchingRow = data.find(row => row.idEvent == evtId && row.idUser == userId);

            return matchingRow ? matchingRow.ticketsNumber : -1;
        })
        .catch(error => {
            console.error('Error during GET request:', error);
            return 0;
        });
}


