var signUp = false;

function showSignUpForm() {
    var signUpText = document.querySelector(".sign-up-text");
    var loginButton = document.getElementById("login-button");
    var emailInput = document.getElementById("email");
    var emailLabel = document.getElementById("email-label");
    var welcomeText = document.getElementById("welcome-text");
    var backLogin = document.getElementById("back-to-login");
    var ageText = document.getElementById("age-label");
    var age = document.getElementById("age");

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
    console.log('Selected Age:', selectedAge);
});
