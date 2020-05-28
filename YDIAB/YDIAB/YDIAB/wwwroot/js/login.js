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
        let token = await postLogin('/api/account/createtoken', { "username": nameInput, "password": passwordInput });
        console.log("returned token", token);
    });
})();
