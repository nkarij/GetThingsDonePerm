

function setSession(token) {
    if (token != undefined) {
        let parsedToken = JSON.stringify(token);
        sessionStorage.setItem("credentials", parsedToken);
        //window.location = '/alllists';
    }
}

function getSession() {
    if (sessionStorage.getItem("credentials")) {
        let token = sessionStorage.getItem("credentials");
        return retrievedObject = JSON.parse(token);
    }
}

function deleteSession() {
    sessionStorage.removeItem("credentials");
}