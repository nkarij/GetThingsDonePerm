let urlParams = (new URL(document.location)).searchParams;
//console.log(urlParams)
let listId = urlParams.get("id");
//console.log(listId);
let intid = parseInt(listId);
let activateUpdateListButton = document.querySelector(".single-list__update");
let updateListSection = document.querySelector(".update-list-section");
let updateForm = document.querySelector("#update-list-form");

// this works fine
outPutSingleList();
async function outPutSingleList() {

    let heading = document.querySelector(".single-list__heading");
    // let deleteButton = document.querySelector(".single-list__delete");
    let taskList = document.querySelector(".single-list__task-list");
    
    
    let data = await simpleGet('http://localhost:5000/api/lists/' + intid);
    console.log(data);
    let list = data;

    taskList.innerHTML = "";
    heading.textContent = list.name;
    list.itemList.forEach(item => {
        let listElement = document.createElement("li");
        listElement.innerText = item.title;
        let linkElement = document.createElement("a");
        linkElement.innerText = "Manage Task";
        linkElement.href = `/taskview.html?id=${item.id}`;;
        linkElement.classList.add("row");
        let checkboxElement = document.createElement("input");
        checkboxElement.type = "checkbox";
        checkboxElement.className = "task__checkbox";
        checkboxElement.checked = item.isDone;
        listElement.appendChild(linkElement);
        listElement.appendChild(checkboxElement);
        taskList.appendChild(listElement);
    })
}

// UPDATE: 

activateUpdateListButton.addEventListener('click', () => {
    // show or hide update section.
    updateListSection.classList.toggle("update-list-section--active");
});

// virker
function updateList(){
    let updateButton = document.querySelector(".update-list-button");

    updateButton.addEventListener('click', async (e) => {
        let newName = updateForm.updatelistname.value;
        let newDescription = updateForm.updatelistdescrip.value;
        let newList = {
            Id: intid,
            Name: newName,
            Description: newDescription,
        }

        let data = await putBodyToUrl('http://localhost:5000/api/lists', newList);
        console.log(data);
    })
}


// virker
function createTask() {

    let createButton = document.querySelector(".create-task-button");
    let createForm = document.querySelector("#create-task-form");

    createButton.addEventListener('click', async (e) => {
        e.preventDefault();

        let taskTitle = createForm.createtaskname.value;
        console.log(taskTitle);
        let newTask = {
            ListId: intid,
            Title: taskTitle           
        }

        let data = await postBodyToUrl('http://localhost:5000/api/items', newTask);
        outPutSingleList();
    })
}

// virker
function removeList() {

    let removeButton = document.querySelector(".single-list__delete");
    console.log(removeButton);
    let intid = parseInt(listId);
    removeButton.addEventListener('click', async (e) => {
        e.preventDefault();

        let data = await deleteItemById('http://localhost:5000/api/lists/' + intid);
        console.log("list remove item", data);
        location.href = "../alllists.html";
    })
}
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
createTask();
updateList();
removeList();

