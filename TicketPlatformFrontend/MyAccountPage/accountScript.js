
let idUser = localStorage.getItem('user_id');
let userData;
let cardData;

document.addEventListener("DOMContentLoaded", function(){

    if(localStorage.getItem('isAdmin') !== 'true') {
        getUserData().then(function() {
            populateAccPage();
            getCard().then(function (data) {
                cardData = data;
                populateCardData();
            });
        });
    } else {
        getAdminData().then(function() {
            populateAccPage();
        });
    }

    hideAdminData();
    populateAgeDropdown();
});

function hideAdminData() {
    if(localStorage.getItem('isAdmin')=="true"){
        const cardDataElements = document.getElementsByClassName('card-data');
        for (const element of cardDataElements) {
            element.classList.add('hidden');
        }
        document.getElementById('age').classList.add('hidden');
        document.getElementById('age-label').classList.add('hidden');
    }
}

function getUserData(){
    const userId = localStorage.getItem('user_id');
    const userEndpoint = `https://localhost:7075/users/${userId}`;

    return fetch(userEndpoint)
        .then(response => response.json())
        .then(data => {
            if (data.isError) {
                // Handle errors if needed
                console.error('Error fetching user data:', data.errors);
                return null;
            } else {
                // Process user data
                userData = data.value;
                return data.value;
            }
        })
        .catch(error => {
            console.error('Error fetching user data:', error);
            return null;
        });
}

function getAdminData(){
    const adminId = localStorage.getItem('user_id');
    const adminEndpoint = `https://localhost:7075/admins/${adminId}`;

    return fetch(adminEndpoint)
        .then(response => response.json())
        .then(data => {
            if (data.isError) {
                // Handle errors if needed
                console.error('Error fetching admin data:', data.errors);
                return null;
            } else {
                // Process admin data
                userData = data.value;
                return data.value;
            }
        })
        .catch(error => {
            console.error('Error fetching admin data:', error);
            return null;
        });
}

function populateAgeDropdown() {
    const ageDropdown = document.getElementById("age");

    for (let age = 0; age <= 64; age++) {
        const option = document.createElement('option');
        option.value = age;
        option.text = age;
        ageDropdown.appendChild(option);
    }
    const option = document.createElement('option');
    option.value = 65;
    option.text = "65+";
    ageDropdown.appendChild(option);
}

function populateAccPage(){
    console.log(userData);
    document.getElementById('username').value = userData.name;
    document.getElementById('email').value = userData.email;
    document.getElementById('age').value = userData.age;
    if(cardData)
    {
        
    }

}

function saveChanges(){
    if(localStorage.getItem('isAdmin') != 'true') {
        updateUserData();
        updateCardData();
    } else {
        updateAdminData();
    }
}

function updateUserData() {
    const userId = localStorage.getItem('user_id');
    const userEndpoint = `https://localhost:7075/users/${userId}`;

    const updatedUserData = {
        name: document.getElementById('username').value,
        email: document.getElementById('email').value,
        password: document.getElementById('password').value,
        age: document.getElementById('age').value,
    };

    const requestOptions = {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(updatedUserData),
    };

    return fetch(userEndpoint, requestOptions)
        .then(response => response.json())
        .then(data => {
            if (data.isError) {
                console.error('Error updating user data:', data.errors);
            } else {
                console.log('User data updated successfully:', data.value);
            }
        })
        .catch(error => {
            console.error('Error updating user data:', error);
        });
}

async function updateCardData(){

    let idCard = await getCardId();
    console.log('cardId:',idCard);
    const putEndpoint = `https://localhost:7075/card/${idCard}`;
    const postEndpoint = `https://localhost:7075/card`;


    const updatedCardData = {
        name: document.getElementById('username').value,
        cardNumber: document.getElementById('card-number').value,
        cvv: document.getElementById('cvv').value,
        expDate: document.getElementById('exp-date').value,
        idUser: localStorage.getItem('user_id')
    };

    console.log(updatedCardData);
    if(idCard!=-1)
    {
        const requestOptions = {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(updatedCardData),
        };
    
        return fetch(putEndpoint, requestOptions)
            .then(response => response.json())
            .then(data => {
                if (data.isError) {
                    console.error('Error updating card data:', data.errors);
                } else {
                    console.log('Card data updated successfully:', data.value);
                }
            })
            .catch(error => {
                console.error('Error updating card data:', error);
            });
    }
    else{
        const requestOptions = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(updatedCardData),
        };
    
        return fetch(postEndpoint, requestOptions)
            .then(response => response.json())
            .then(data => {
                if (data.isError) {
                    console.error('Error updating card data:', data.errors);
                } else {
                    console.log('Card data updated successfully:', data.value);
                }
            })
            .catch(error => {
                console.error('Error updating card data:', error);
            });
    }
    
}

function getCardId() {
    const endpoint = `https://localhost:7075/card`;
    return fetch(endpoint)
        .then(response => response.json())
        .then(cards => {
            const userCard = cards.find(card => card.idUser == idUser);
            return userCard ? userCard.id : -1;
        })
        .catch(error => {
            console.error('Error fetching card data:', error);
            return -1;
        });
}

function getCard() {
    const endpoint = `https://localhost:7075/card`;

    return fetch(endpoint)
        .then(response => response.json())
        .then(cards => {
            const userCards = cards.filter(card => card.idUser == localStorage.getItem('user_id'));
            return userCards.length > 0 ? userCards[0] : null;
        })
        .catch(error => {
            console.error('Error fetching card data:', error);
            return null;
        });
}


function populateCardData() {
    if (cardData) {
        console.log(cardData);
        document.getElementById('card-number').value = cardData.cardNumber ? cardData.cardNumber : "";
        document.getElementById('exp-date').value = cardData.expDate ? cardData.expDate : "";
        document.getElementById('cvv').value = cardData.cvv ? cardData.cvv : "";
    } else {
        console.warn('No card data available.');
    }
}

function updateAdminData() {
    const adminId = localStorage.getItem('admin_id');
    const adminEndpoint = `https://localhost:7075/admins/${adminId}`;

    const updatedAdminData = {
        name: document.getElementById('username').value,
        email: document.getElementById('email').value,
        password: document.getElementById('password').value,
        age: document.getElementById('age').value,
    };

    const requestOptions = {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(updatedAdminData),
    };

    return fetch(adminEndpoint, requestOptions)
        .then(response => response.json())
        .then(data => {
            if (data.isError) {
                console.error('Error updating admin data:', data.errors);
            } else {
                console.log('Admin data updated successfully:', data.value);
            }
        })
        .catch(error => {
            console.error('Error updating admin data:', error);
        });
}

function logout(){
    localStorage.clear();
    window.location.href = "../Login/index.html";
}
