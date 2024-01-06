var signUp = false;
let adminIsChecked=false;

const signupEndpoint = 'https://localhost:7075/users';
const loginEndpoint = 'https://localhost:7075/login/';

let userData = {
    name: '',
    email: '',
    password: '',
    age: 0,
};

async function login(event) {
    event.preventDefault();

    console.log("login");
    var wrongCredentialsMessage = document.getElementById("wrong-credentials");

    if(!signUp)
    {
        try {
            const data = await loginData(adminIsChecked);
            // Handle successful login
            redirectToBrowse();
        } catch (error) {
            // Handle login failure
            email.style.boxShadow = 'inset 0 0 10px red';
            password.style.boxShadow = 'inset 0 0 10px red';
            wrongCredentialsMessage.classList.remove("hidden");
        }
    }
    else
    {
        signUpData();
    }
}

function showSignUpForm() {
    var signUpText = document.querySelector(".sign-up-text");
    var loginButton = document.getElementById("login-button");
    var nameInput = document.getElementById("username");
    var nameLabel = document.getElementById("label-name");
    var welcomeText = document.getElementById("welcome-text");
    var backLogin = document.getElementById("back-to-login");
    var ageText = document.getElementById("age-label");
    var age = document.getElementById("age");
    var username = document.getElementById("username");
    var password = document.getElementById("password");
    var wrongCredentialsMessage = document.getElementById("wrong-credentials");

    username.style.boxShadow = 'none';
    password.style.boxShadow = 'none';
    wrongCredentialsMessage.classList.add("hidden");

    loginButton.textContent = "Sign up";
    welcomeText.classList.add("hidden");
    backLogin.classList.remove("hidden");
    signUpText.classList.add("hidden");
    nameInput.classList.remove("hidden");
    nameInput.required = true;
    nameLabel.classList.remove("hidden");
    age.classList.remove("hidden");
    age.required = true;
    ageText.classList.remove("hidden");

    if(age.childElementCount==0)
    {
        populateAgeDropdown();
    }    

    signUp = true;
}

function showLogInForm() {
    var signUpText = document.querySelector(".sign-up-text");
    var loginButton = document.getElementById("login-button");
    var nameInput = document.getElementById("username");
    var nameLabel = document.getElementById("label-name");
    var welcomeText = document.getElementById("welcome-text");
    var backLogin = document.getElementById("back-to-login");
    var ageText = document.getElementById("age-label");
    var age = document.getElementById("age");


    loginButton.textContent = "Login";
    welcomeText.classList.remove("hidden");
    backLogin.classList.add("hidden");
    signUpText.classList.remove("hidden");
    nameInput.classList.add("hidden");
    nameInput.required = false;
    nameLabel.classList.add("hidden");
    age.classList.add("hidden");
    age.required = false;
    ageText.classList.add("hidden");

    signUp = false;
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


// Event listener for changes in age
document.getElementById('age').addEventListener('change', function() {
    const selectedAge = document.getElementById('age').value;
    userData.age = selectedAge;
    console.log('Selected Age:', selectedAge);
});

document.getElementById('admin-check').addEventListener('change', function() {
    let checkbox = document.getElementById('admin-check');
    adminIsChecked = checkbox.checked;
    console.log('Check:', adminIsChecked);
    localStorage.setItem('isAdmin', adminIsChecked);
    console.log(localStorage.getItem('isAdmin'));
});


document.getElementById('username').addEventListener('change', function() {
    let un = document.getElementById('username').value;
    userData.name = un; 
});

document.getElementById('email').addEventListener('change', function() {
    let em = document.getElementById('email').value;
    userData.email = em;
});

document.getElementById('password').addEventListener('change', function() {
    let ps = document.getElementById('password').value;
    userData.password = ps; 
});



function signUpData(){
    console.log(userData);
    fetch(signupEndpoint, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Access-Control-Allow-Origin': 'http://127.0.0.1:5500',
        },
        body: JSON.stringify(userData),
      })
      .then(response => {
        if (!response.ok) {
          throw new Error('Network response was not ok');
        }
        return response.json();
      })
      .then(data => {
        // Handle the response data...
        console.log('User successfully signed up:', data);
        location.reload();
      })
      .catch(error => {
        // Handle errors...
        console.error('Error during signup:', error);
      });
}

async function loginData(adminIsChecked) {
    console.log("adminIsChecked: ", adminIsChecked);

    const requestData = {
        email: userData.email,
        password: userData.password,
    };

    const requestOptions = {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
        },
    };

    const queryString = `?Email=${requestData.email}&Password=${requestData.password}`;

    const endpoint = adminIsChecked ? loginEndpoint + "admin" : loginEndpoint + "user";

    const fullEndpoint = endpoint + queryString;

    try {
        const response = await fetch(fullEndpoint, requestOptions);

        if (!response.ok) {
            throw new Error('Network response was not ok');
        }

        const data = await response.json();

        console.log('User successfully logged in:', data);
        return data;
    } catch (error) {
        console.error('Error during login:', error);
        throw error; // rethrow the error for the calling function to handle
    }
}




