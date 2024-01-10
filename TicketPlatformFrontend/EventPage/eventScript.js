
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

async function addToCart() {
    const eventId = currentEvent;
    const userId = localStorage.getItem('user_id');

    // Check if cart item exists
    const cartItem = await getCartItem(eventId, userId);

    if (!cartItem) {
        const postEndpoint = 'https://localhost:7075/cart';
        const requestBody = {
            idEvent: eventId,
            idUser: userId,
            ticketsNumber: 1
        };

        const requestOptions = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(requestBody),
        };

        try {
            const response = await fetch(postEndpoint, requestOptions);
            const data = await response.json();

            if (data.isError) {
                console.error('Error adding to cart:', data.errors);
            } else {
                console.log('Added to cart successfully:', data.value);
            }
        } catch (error) {
            console.error('Error adding to cart:', error);
        }
    } else {
        // If cart item exists, update the ticketsNumber
        let newTicketsNumber = cartItem.ticketsNumber+1;
        const putEndpoint = `https://localhost:7075/cart`;
        const requestBody = {
            idEvent: cartItem.idEvent,
            idUser: cartItem.idUser,
            ticketsNumber: newTicketsNumber
        };
        console.log(requestBody);
        const requestOptions = {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(requestBody),
        };

        try {
            const response = await fetch(putEndpoint, requestOptions);
            const data = await response.json();

            if (data.isError) {
                console.error('Error updating cart:', data.errors);
            } else {
                console.log('Cart updated successfully:', data.value);
            }
        } catch (error) {
            console.error('Error updating cart:', error);
        }
    }
}

async function getCartItem(eventId, userId) {
    const endpoint = `https://localhost:7075/cart`;

    try {
        const response = await fetch(endpoint);
        const cartItems = await response.json();
        console.log(cartItems);
        let finalCart = cartItems.filter(item => parseInt(item.idEvent) == parseInt(eventId) && parseInt(item.idUser) === parseInt(userId));
        if (finalCart.length > 0) {
            console.log(finalCart);
            return finalCart[0];
        } else {
            return false;
        }
    } catch (error) {
        console.error('Error fetching cart item:', error);
        return false;
    }
}



