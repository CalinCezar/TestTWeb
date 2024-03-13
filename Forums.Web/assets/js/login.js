//////////////// VARIABILES /////////////////////////////////////////////////////////
const credential = document.getElementById('Credential');
const password = document.getElementById('Password');

//////////////// WRONG INPUT ////////////////////////////////////////////////////////
const errorCredentialLogin = document.getElementById('error-credential-login');
const errorPasswordLogin = document.getElementById('error-password-login');

//////////////// FORM ///////////////////////////////////////////////////////////////
const login = document.getElementById('login');

//////////////// FORM CHEKS /////////////////////////////////////////////////////////
login.addEventListener('submit', (e) => {

    if (credential.value.length == 0 && password.value.length == 0) {
        e.preventDefault();
    }

});