let loginForm = document.querySelector("#auth-form");
let loginNameElement = loginForm.authusername;
let loginPasswordElement = loginForm.authpassword;
let loginCheckBoxElement = loginForm.rememberUser;
let loginButtonElement = loginForm.loginButton;

(function submitLoginForm() {

    loginButtonElement.addEventListener('click', async (e) => {
        e.preventDefault();
        let nameInput = loginNameElement.value;
        let passwordInput = loginPasswordElement.value;
        //let rememberUser = loginCheckBoxElement.value;
        let result = await postLogin('/api/account/createtoken', { "username": nameInput, "password": passwordInput });
        //console.log("returned token", result);
        if (result != undefined) {
            setSession(result);
            location.href = '../alllists.html';
        } else {
            return false;
        }
    });
})();


function registerUser() {
    let registerForm = document.querySelector("#register-form");
    let firstname = registerForm.registerfirstname;
    let lastName = registerForm.registerlastname;
    let username = registerForm.registerusername;
    let password = registerForm.registerpassword;
    let registerButtonElement = registerForm.registerButton;

    let userObject;

    registerButtonElement.addEventListener('click', async (e) => {
        e.preventDefault();

        userObject = {
            FirstName: firstname.value,
            LastName: lastName.value,
            UserName: username.value,
            Password: password.value,
            Email: username.value,
            NormalizedEmail: username.value,
            NormalizedUserName: username.value
        }

        
        let result = await registerUserData('/api/account/registeruser', userObject);
        if (result == 200) {
            let succesFeedback = document.querySelector(".register-succesfull");
            succesFeedback.innerHTML = "Welcome, you can log in!"
        }

    })
}

registerUser();


