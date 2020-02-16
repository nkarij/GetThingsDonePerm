let urlParams = (new URL(document.location)).searchParams;
console.log(urlParams)
let tagId = urlParams.get("id");

(function outPutTaskList() {

    let heading = document.querySelector(".single-tag__heading");
    let tagList = document.querySelector(".single-list__tag-list");

    fetch('http://localhost:5000/api/tags/' + tagId)
    .then((response) => {
        return response.json();
    })
    .then((json) => {
        console.log(json);
        let data = json;
        heading.textContent = data.name;
    });
})()


// kalde functionerne, ligesom i Task-list.

function updateItem(){
    let updateButton = document.querySelector(".update-tag-button");
    let updateForm = document.querySelector("#update-tag-form");
    let intid = parseInt(taskId);

    updateButton.addEventListener('click', (e) => {
        let newName = updateForm.updatetagname.value;

        fetch('http://localhost:5000/api/tags/' + tagId, {
            method: 'PUT',
            mode: 'cors',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                id: intid,
                name: newName,
            }),
        }).then((response) => {
            return response.json();
        }).then((response) => {
            console.log("data", response);
        }).catch(error => console.log(error))

    })
}


function removeItem() {
    let removeButton = document.querySelector(".single-tag__delete");
    console.log(removeButton);
    removeButton.addEventListener('click', (e) => {
        e.preventDefault();
        fetch('http://localhost:5000/api/tags/' + tagId, {
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

