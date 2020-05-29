

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
        //console.log(await response.staus);
        return await response.json();
    } catch (error) {
        console.log(error)
    }
}

async function registerUserData(url, jsonObject) {

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
        return response.status;
        //console.log(await response.status);
        //return await response.json();
    } catch (error) {
        console.log(error)
    }
}

async function logOutUser(url) {

    // check if the token is in LocalStorage. Overwrite token.
    let result = getSession();
    console.log(result);
    try {
        const response = await fetch(url, {
            method: 'GET',
            mode: 'cors',
            headers: {
                'Authorization': 'Bearer ' + result.token,
                //'Accept': 'application/json, text/plain',
                'Content-Type': 'application/json;charset=UTF-8'
            },
        })
        return response.status;
        //console.log(await response.staus);
        //return await response.json();
    } catch (error) {
        console.log(error)
    }
}


async function simpleGet(url) {

    // check if the token is in LocalStorage. Overwrite token.
    let result = getSession();
    console.log(result);
    try {
        const response = await fetch(url, {
            method: 'GET',
            mode: 'cors',
            headers: {
                'Authorization': 'Bearer ' + result.token,
                //'Accept': 'application/json, text/plain',
                'Content-Type': 'application/json;charset=UTF-8'
            },
        })
        //return response.status;
        //console.log(await response.staus);
        return await response.json();
    } catch (error) {
        console.log(error)
    }
}


async function postBodyToUrl(url, jsonObject) {
    // check if the token is in LocalStorage. Overwrite token.
    // check if the token is in LocalStorage. Overwrite token.
    let result = getSession();
    console.log(result);

    try {
        const response = await fetch(url, {
            method: 'POST',
            mode: 'cors',
            headers: {
                'Authorization': 'Bearer ' + result.token,
                //'Accept': 'application/json, text/plain',
                'Content-Type': 'application/json;charset=UTF-8'
            },
            body: JSON.stringify(jsonObject),
        })
        return response.status;
        //console.log(await response.staus);
        //return await response.json();
    } catch (error) {
        console.log(error)
    }
}


async function putBodyToUrl(url, jsonObject) {
    // check if the token is in LocalStorage. Overwrite token.
    // check if the token is in LocalStorage. Overwrite token.
    let result = getSession();
    console.log(result);

    try {
        const response = await fetch(url, {
            method: 'PUT',
            mode: 'cors',
            headers: {
                'Authorization': 'Bearer ' + result.token,
                //'Accept': 'application/json, text/plain',
                'Content-Type': 'application/json;charset=UTF-8'
            },
            body: JSON.stringify(jsonObject),
        })
        //return response.status;
        //console.log(await response.staus);
        return await response.json();
    } catch (error) {
        console.log(error)
    }
}

async function deleteItemById(url) {
    // check if the token is in LocalStorage. Overwrite token.
    // check if the token is in LocalStorage. Overwrite token.

    try {
        const response = await fetch(url, {
            method: 'DELETE',
            mode: 'cors',
            headers: {
                //'Accept': 'application/json, text/plain',
                'Content-Type': 'application/json;charset=UTF-8'
            },
        })
        return response.status;
        //console.log(await response.staus);
        //return await response.json();
    } catch (error) {
        console.log(error)
    }
}

async function deleteUser(url, jsonObject) {
    let result = getSession();

    try {
        const response = await fetch(url, {
            method: 'POST',
            mode: 'cors',
            headers: {
                'Authorization': 'Bearer ' + result.token,
                //'Accept': 'application/json, text/plain',
                'Content-Type': 'application/json;charset=UTF-8'
            },
            body: JSON.stringify(jsonObject),
        })
        return response.status;
        //console.log(await response.staus);
        //return await response.json();
    } catch (error) {
        console.log(error)
    }
}