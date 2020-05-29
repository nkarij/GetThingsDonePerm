let urlParams = (new URL(document.location)).searchParams;
let tagId = urlParams.get("id");
let intid = parseInt(tagId);

outPutTagList();
async function outPutTagList() {

    let heading = document.querySelector(".single-tag__heading");
    
    let data = await simpleGet('http://localhost:5000/api/tags/' + tagId);
    console.log("tag output", data);
    heading.textContent = data.name;
}


updateTag();
removeTag();

function updateTag(){
    let updateButton = document.querySelector(".update-tag-button");
    let updateForm = document.querySelector("#update-tag-form");
    

    updateButton.addEventListener('click', async (e) => {
        let newName = updateForm.updatetagname.value;

        let newTag = {
            Id: intid,
            Name: newName,
        }

        let data = await putBodyToUrl('http://localhost:5000/api/tags/', newTag)
        console.log("update Tag", data);
        outPutTagList();
        //fetch('http://localhost:5000/api/tags/' + tagId, {
        //    method: 'PUT',
        //    mode: 'cors',
        //    headers: { 'Content-Type': 'application/json' },
        //    body: JSON.stringify({
        //        id: intid,
        //        name: newName,
        //    }),
        //}).then((response) => {
        //    return response.json();
        //}).then((response) => {
        //    console.log("data", response);
        //}).catch(error => console.log(error))

    })
}


function removeTag() {
    let removeButton = document.querySelector(".single-tag__delete");
    console.log(removeButton);
    removeButton.addEventListener('click', async (e) => {
        e.preventDefault();

        let taskId = await deleteItemById('http://localhost:5000/api/tags/' + tagId);
        location.href = `../taskview.html?id=${taskId}`;

        //fetch('http://localhost:5000/api/tags/' + tagId, {
        //    method: 'DELETE',
        //    mode: 'cors',
        //    headers: { 'Content-Type': 'application/json' },
        //    body: JSON.stringify(),
        //}).then((response) => {
        //    // console.log(response.status);
        //    return response.json();
        //}).then((response) => {
        //    console.log("response", response);
        //}).catch(error => console.log(error))
    })
}

