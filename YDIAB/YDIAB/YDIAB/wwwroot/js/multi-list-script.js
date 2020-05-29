let multiListWrapper = document.querySelector("#multi-list-wrapper");
let mulitListOutPutList = document.querySelector(".multi-list__list");

// works fine
async function getMultipleLists(){
    let data = await simpleGet('http://localhost:5000/api/lists');
    //console.log(data);
    mulitListOutPutList.innerHTML = "";
    data.forEach(item => {
        let listElement = document.createElement("li");
        let titleElement = document.createElement("p");
        let pElement = document.createElement("p");
        titleElement.textContent = item.name;
        pElement.textContent = item.Description;
        let linkElement = document.createElement("a");
        linkElement.innerText = "Manage List";
        linkElement.href = `/listview.html?id=${item.id}`;;
        linkElement.classList.add("row");
        listElement.appendChild(titleElement);
        listElement.appendChild(pElement);
        listElement.appendChild(linkElement);
        mulitListOutPutList.appendChild(listElement);
    });
}
getMultipleLists();


// works fine
function createList() {

    let createButton = document.querySelector(".create-list-button");
    let createForm = document.querySelector("#create-list-form");

    createButton.addEventListener('click', (e) => {
        e.preventDefault();
        //console.log("klik");
        let listName = createForm.createlistname.value;
        let listDescription = createForm.createlistdescript.value;
        let result = getSession();

        fetch('http://localhost:5000/api/lists', {
            method: 'POST',
            mode: 'cors',
            headers: {
                'Authorization': 'Bearer ' + result.token,
                'Accept': 'application/json, text/plain',
                'Content-Type': 'application/json;charset=UTF-8'
            },
            body: JSON.stringify({
                Name : listName,
                Description : listDescription
            }),
        }).then((response) => {
            //console.log(response.status);
            return response.json();
        }).then((data) => {
            //console.log("response", data);
            getMultipleLists();
        }).catch(error => console.log(error))

    })
}
createList();

function logoutUser() {
    let logoutButton = document.querySelector(".logout-button");
    console.log("button 1", logoutButton);
    logoutButton.addEventListener('click', async (e) => {
        console.log("button", logoutButton);
        let result = await logOutUser('/api/account/logout');
        console.log("logout", result);
        if (result == 200) {
            location.href = "../index.html";
            deleteSession();
        }
    })
}

logoutUser();