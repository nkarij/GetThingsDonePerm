

function removeUser() {
    let removeUserForm = document.querySelector("#remove-user-form");
    let nameElement = removeUserForm.authusername;
    let passwordElement = removeUserForm.authpassword;
    let buttonElement = removeUserForm.removeuserbutton;

    buttonElement.addEventListener('click', async (e) => {

        let user = {
            UserName: nameElement.value,
            Password: passwordElement.value,
        }
        console.log(user);

        let result = await deleteUser('/api/account/removeuser', user);
        console.log("delete", result);
        if (result == 200) {
            location.href = "../index.html";
            deleteSession();
        }
    })
}

removeUser();


