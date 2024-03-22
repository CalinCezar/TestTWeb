//////////////////////// VARIABILES //////////////////////////////////////////////////
const credential = document.getElementById('Credential');
const password = document.getElementById('Password');
const conPassword = document.getElementById('Con-Password');
const email = document.getElementById('Email');
const info = document.getElementById('Info');

////////////////// GREEN ARROW ///////////////////////////////////////////////////////
var credentialAccept = document.getElementById('credential-accept-data');
var passwordAccept = document.getElementById('password-accept-data');
var conPasswordAccept = document.getElementById('con-password-accept-data');
var emailAccept = document.getElementById('email-accept-data');
var infoAccept = document.getElementById('info-accept-data');


//////////////// WRONG INPUT ////////////////////////////////////////////////////////
var errorCredential = document.getElementById('error-credential');
var errorPassword = document.getElementById('error-password');
var errorConPassword = document.getElementById('error-con-password');
/////////////////// FORM ///////////////////////////////////////////////////////////
var register = document.getElementById('register');

//////////////////// VALIDATE GREEN ARROW /////////////////////////////////////////
function validateCredential() {

    if (credential.value.length == 0) {
        credentialAccept.innerHTML = '';
        return false;
    }
    credentialAccept.innerHTML = '<i class="fa-regular fa-circle-check"></i>';
    return true;
}
function validatePassword() {
    if (password.value.length == 0) {
        passwordAccept.innerHTML = '';
        return false;
    }
    passwordAccept.innerHTML = '<i class="fa-regular fa-circle-check"></i>';
    return true;
}
function validateConPassword() {

    if (conPassword.value.length == 0) {
        conPasswordAccept.innerHTML = '';
        return false;
    }
    conPasswordAccept.innerHTML = '<i class="fa-regular fa-circle-check"></i>';
    return true;
}
function validateEmail() {
    if (email.value.length == 0) {
        emailAccept.innerHTML = '';
        return false;
    }
    emailAccept.innerHTML = '<i class="fa-regular fa-circle-check"></i>';
    return true;
}
function validateInfo() {
    if (info.value.length == 0) {
        infoAccept.innerHTML = '';
        return false;
    }
    infoAccept.innerHTML = '<i class="fa-regular fa-circle-check"></i>';
    return true;
}
///////////////////////////////////////////////////////////////////////////////

///////////////// FORM CHEKS /////////////////////////////////////////////////
register.addEventListener('submit', (e) => {
    let messageCredential = []
    let messagePassword = []
    let messageConPassword = []

    ///////////// PASSWORD /////////////////////////////
    if (password.value.length <= 7) {
        messagePassword.push('Password must be longer than 7');
        passwordAccept.innerHTML = '';
    }
    ///////////// CON-PASSWORD /////////////////////////
    if (conPassword.value.length <= 7) {
        messageConPassword.push('Password must be longer than 7');
        conPasswordAccept.innerHTML = '';
    }
    if (password.value != conPassword.value) {
        messagePassword.push('Incorrect password')
        messageConPassword.push('Incorrect password');
        passwordAccept.innerHTML = '';
        conPasswordAccept.innerHTML = '';
    }
    //////////// MESSAGE ON SCREEN/////////////////////
    if (messageCredential.length > 0){
        e.preventDefault();
        errorCredential.innerText = messageCredential.join(', ');
    }
    if (messagePassword.length > 0) {
        e.preventDefault();
        errorPassword.innerText = messagePassword.join(', ');
    }
    if (messageConPassword.length > 0) {
        e.preventDefault();
        errorConPassword.innerText = messageConPassword.join(', ');
    }

});
