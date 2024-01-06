var signUp = false;

const signupEndpoint = 'https://localhost:7075/users';

let userData = {
    name: '',
    email: '',
    password: '',
    age: 0,
};

function login(event) {
    event.preventDefault();

    console.log("login");
    var wrongCredentialsMessage = document.getElementById("wrong-credentials");

    if(!signUp)
    {
        if(loginData())
        {
            redirectToBrowse();
        }
        else
        {
            username.style.boxShadow = 'inset 0 0 10px red';
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
    var emailInput = document.getElementById("email");
    var emailLabel = document.getElementById("email-label");
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
    emailInput.classList.remove("hidden");
    emailInput.required = true;
    emailLabel.classList.remove("hidden");
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
    var emailInput = document.getElementById("email");
    var emailLabel = document.getElementById("email-label");
    var welcomeText = document.getElementById("welcome-text");
    var backLogin = document.getElementById("back-to-login");
    var ageText = document.getElementById("age-label");
    var age = document.getElementById("age");


    loginButton.textContent = "Login";
    welcomeText.classList.remove("hidden");
    backLogin.classList.add("hidden");
    signUpText.classList.remove("hidden");
    emailInput.classList.add("hidden");
    emailInput.required = false;
    emailLabel.classList.add("hidden");
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

function loginData(){
    
}



