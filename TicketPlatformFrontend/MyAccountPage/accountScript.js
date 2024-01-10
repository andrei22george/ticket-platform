
let idUser = localStorage.getItem('id_user');
let userData;

document.addEventListener("DOMContentLoaded", function(){

    if(localStorage.getItem('isAdmin') !== 'true') {
        getUserData().then(function() {
            populateAccPage();
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

}

function saveChanges(){
    if(localStorage.getItem('isAdmin') != 'true') {
        updateUserData();
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
