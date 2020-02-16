let urlParams = (new URL(document.location)).searchParams;
console.log(urlParams)
let listId = urlParams.get("id");
console.log(listId);


(function outPutSingleList() {

    let heading = document.querySelector(".single-list__heading");
    // let deleteButton = document.querySelector(".single-list__delete");
    let taskList = document.querySelector(".single-list__task-list");
    let intid = parseInt(listId);

    fetch('http://localhost:5000/api/lists/' + intid)
        .then((response) => {
            return response.json();
        })
        .then((json) => {
            console.log("list", json);
            let list = json;
            heading.textContent = list.name;
            list.itemList.forEach(item => {
                 let listElement = document.createElement("li");
                 listElement.innerText = item.title;
                 let linkElement = document.createElement("a");
                 linkElement.innerText = "Manage Task";
                 linkElement.href = `/taskview.html?id=${item.id}`;;
                 linkElement.classList.add("row");
                 listElement.appendChild(linkElement);
                 taskList.appendChild(listElement);
             })
        });
})();


createTask();
updateItem();
removeItem();

function createTask() {

    let createButton = document.querySelector(".create-task-button");
    let createForm = document.querySelector("#create-task-form");
    let intid = parseInt(listId);
    createButton.addEventListener('click', (e) => {
        e.preventDefault();

        let taskTitle = createForm.createtaskname.value;
        console.log(taskTitle);
        fetch('http://localhost:5000/api/items', {
            method: 'POST',
            mode: 'cors',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                Title: taskTitle,
                ListId : intid
            }),
        }).then((response) => {
            console.log(response.status);
            return response.json();
        }).then((response) => {
            console.log("response", response);
        }).catch(error => console.log(error))
    })
}


function updateItem(){
    let updateButton = document.querySelector(".update-list-button");
    let updateForm = document.querySelector("#update-list-form");
    let intid = parseInt(listId);

    updateButton.addEventListener('click', (e) => {
        let newName = updateForm.updatelistname.value;
        let newDescription = updateForm.updatelistdescrip.value;

        fetch('http://localhost:5000/api/lists', {
            method: 'PUT',
            mode: 'cors',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                Id: intid,
                Name: newName,
                Description : newDescription,
            }),
        }).then((response) => {
            return response.json();
        }).then((response) => {
            console.log("data", response);
        }).catch(error => console.log(error))

    })
}


function removeItem() {

    let removeButton = document.querySelector(".single-list__delete");
    console.log(removeButton);
    removeButton.addEventListener('click', (e) => {
        e.preventDefault();
        fetch('http://localhost:5000/api/lists/' + listId, {
            method: 'DELETE',
            mode: 'cors',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(),
        }).then((response) => {
            // console.log(response.status);
            return response.json();
        }).then((response) => {
            console.log("response", response);
        }).catch(error => console.log(error))
    })
}
