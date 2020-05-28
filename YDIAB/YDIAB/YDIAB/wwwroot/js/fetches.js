let token = null;

// TODO: this works fine, returning token, next save it to somewhere retrievable.
async function postLogin(url, jsonObject) {
    // check if the token is in LocalStorage. Overwrite token.
    //console.log("inside");
    //console.log(jsonObject);

    try {
        const response = await fetch(url, {
            method: 'POST',
            mode: 'cors',
            headers: {
                'Accept': 'application/json, text/plain',
                'Content-Type': 'application/json;charset=UTF-8'
            },
            body: JSON.stringify(jsonObject),
        })
        //return response.status;
        //console.log(await response.json());
        return await response.json();
    } catch (error) {
        console.log(error)
    }
}

 function postBodyToUrl(url, jsonObject) {
     // check if the token is in LocalStorage. Overwrite token.
    fetch(url, {
        method: 'POST',
        mode: 'cors',
        headers: {
            'Authorization': 'Bearer ' + token,
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(jsonObject),
    }).then((response) => {
        // console.log(response.status);
        return response.json();
    }).then((data) => {
        console.log("data", data);
        return data;
    }).catch(error => console.log(error))
}

function simpleGet(url) {

    // check if the token is in LocalStorage. Overwrite token.

    fetch(url, {
        method: 'GET',
        mode: 'cors',
        headers: {
            'Authorization': 'Bearer ' + token,
            'Content-Type': 'application/json'
            }
        })
        .then((response) => {
            return response.json();
        })
        .then((data) => {
            return data;
        });
}


function saveTokenToLocalStorage() {

}

function getTokenFromLocalStorage() {

}