let urlParams = (new URL(document.location)).searchParams;
let taskId = urlParams.get("id");
let intid = parseInt(taskId);
let tagList = document.querySelector(".single-task__tag-list");
let taskUpdateSection = document.querySelector(".update-task-section");
let activateUpdateTaskButton = document.querySelector(".single-task__update");

// works fine
outPutTaskList()
async function outPutTaskList() {

    let heading = document.querySelector(".single-task__heading");

    let data = await simpleGet('http://localhost:5000/api/items/' + taskId);
    console.log("tasks", data);
    //let data = json;
    heading.textContent = data.title;
    let checkboxElement = document.createElement("input");
    checkboxElement.type = "checkbox";
    checkboxElement.className = "task-list__checkbox";
    checkboxElement.checked = data.isDone;
    checkboxElement.id = data.id;
    heading.appendChild(checkboxElement);

    tagList.innerHTML = "";
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

    updateTaskDone();
}

createTag();
updateTask();
removeTask();

// virker
function createTag() {

    let createButton = document.querySelector(".create-tag-button");
    let createForm = document.querySelector("#create-tag-form");

    createButton.addEventListener('click', async (e) => {
        e.preventDefault();

        let tagName = createForm.createtagname.value;
        //console.log(tagName);
        let newTag = {
            ItemId: intid,
            Name: tagName            
        }

        let data = await postBodyToUrl('http://localhost:5000/api/tags', newTag);
        outPutTaskList();
    })
}


activateUpdateTaskButton.addEventListener('click', (e) => {
    taskUpdateSection.classList.toggle("update-task-section--active");
})

// works fine
function updateTask(){
    let updateButton = document.querySelector(".update-task-button");
    let updateForm = document.querySelector("#update-task-form");

    updateButton.addEventListener('click', async (e) => {
        let newTitle = updateForm.updatetaskname.value;

        let newTask = {
            Id: intid,
            Title: newTitle,
        }

        let data = await putBodyToUrl('http://localhost:5000/api/items/', newTask);
        //console.log("task update", data);
        outPutTaskList();

    })
}

// works fine
function removeTask() {

    let removeButton = document.querySelector(".single-task__delete");
    //console.log(removeButton);
    removeButton.addEventListener('click', async (e) => {
        e.preventDefault();

        let listId = await deleteItemById('http://localhost:5000/api/items/' + taskId)
        console.log("remove task", listId);
        location.href = `../listview.html?id=${listId}`;
    })
}


function updateTaskDone() {
    let taskCheckbox = document.querySelector(".task-list__checkbox");
    console.log(taskCheckbox);

    taskCheckbox.addEventListener('click', async (e) => {
        let taskId = parseInt(taskCheckbox.id);
        console.log(taskId);
        let selectedChecbox = taskCheckbox.checked;

        let newTask = {
            Id: taskId,
            IsDone: selectedChecbox,
        }
        console.log(newTask);

        let result = await putBodyToUrl('/api/items/putselecteditem', newTask);
        //console.log("checked", result);
    })
}