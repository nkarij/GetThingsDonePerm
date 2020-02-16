let urlParams = (new URL(document.location)).searchParams;
let taskId = urlParams.get("id");
let intid = parseInt(taskId);

(function outPutTagList() {

    let heading = document.querySelector(".single-task__heading");
    let tagList = document.querySelector(".single-task__tag-list");

    fetch('http://localhost:5000/api/items/' + intid)
    .then((response) => {
        return response.json();
    })
    .then((json) => {
        console.log("test", json);
        let data = json;
        heading.textContent = data.title;
        data.tags.forEach(tag => {
             let listElement = document.createElement("li");
             listElement.innerText = tag.name;
             let linkElement = document.createElement("a");
             linkElement.innerText = "Manage Tag";
             linkElement.href = `/tagview.html?id=${tag.id}`;;
             linkElement.classList.add("row");
             listElement.appendChild(linkElement);
             tagList.appendChild(listElement);
         })
    });
})()

createTag();
updateTask();
removeTask();


function createTag() {

    let createButton = document.querySelector(".create-tag-button");
    let createForm = document.querySelector("#create-tag-form");

    createButton.addEventListener('click', (e) => {
        e.preventDefault();

        let tagName = createForm.createtagname.value;
        console.log(tagName);
        fetch('http://localhost:5000/api/tags', {
            method: 'POST',
            mode: 'cors',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                Name: tagName,
                ItemId: intid
            }),
        }).then((response) => {
            console.log(response.status);
            return response.json();
        }).then((response) => {
            console.log("response", response);
        }).catch(error => console.log(error))
    })
}




function updateTask(){
    let updateButton = document.querySelector(".single-task__update");
    let updateForm = document.querySelector("#update-task-form");
    let intid = parseInt(taskId);

    updateButton.addEventListener('click', (e) => {
        let newTitle = updateForm.updatetaskname.value;

        fetch('http://localhost:5000/api/items/' + taskId, {
            method: 'PUT',
            mode: 'cors',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                id: intid,
                title: newTitle,
            }),
        }).then((response) => {
            return response.json();
        }).then((response) => {
            console.log("data", response);
        }).catch(error => console.log(error))

    })
}


function removeTask() {

    let removeButton = document.querySelector(".single-task__delete");
    console.log(removeButton);
    removeButton.addEventListener('click', (e) => {
        e.preventDefault();
        fetch('http://localhost:5000/api/items/' + taskId, {
            method: 'DELETE',
            mode: 'cors',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                // her mangler jeg at sende id til controller.
            }),
        }).then((response) => {
            // console.log(response.status);
            return response.json();
        }).then((response) => {
            console.log("response", response);
        }).catch(error => console.log(error))
    })
}
