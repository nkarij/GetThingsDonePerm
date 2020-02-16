let multiListWrapper = document.querySelector("#multi-list-wrapper");
let mulitListOutPutList = document.querySelector(".multi-list__list");

(function getMultipleLists(){

    fetch('http://localhost:5000/api/lists')
    .then((response) => {
        return response.json();
    })
    .then((json) => {
        console.log(json);
        let data = json;
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
         })
    });
})();

createList();
function createList() {

    let createButton = document.querySelector(".create-list-button");
    let createForm = document.querySelector("#create-list-form");

    createButton.addEventListener('click', (e) => {
        e.preventDefault();
        console.log("klik");
        let listName = createForm.createlistname.value;
        console.log(listName)
        let listDescription = createForm.createlistdescript.value;
        console.log(listDescription)

        fetch('http://localhost:5000/api/lists', {
            method: 'POST',
            mode: 'cors',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                Name : listName,
                Description : listDescription
            }),
        }).then((response) => {
            // console.log(response.status);
            return response.json();
        }).then((response) => {
            console.log("response", response);
        }).catch(error => console.log(error))
    })
}