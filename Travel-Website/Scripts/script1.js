let formBtn = document.querySelector('#login-btn')
let loginForm = document.querySelector('.login-form-container')
let formClose = document.querySelector('#form-close')
let forgotPassBtn = document.querySelector('.forgotPassword')
let forgotPassForm = document.querySelector('.forgotPass-form-container')
let formClose1 = document.querySelector('#form-close1')
let userMenu = document.querySelector('.profile');
let toggleMenu = document.querySelector('.menu');
if (userMenu != null) {
    userMenu.addEventListener('click', () => {
        toggleMenu.classList.toggle('active');
    });
}
else {
    userMenu = null;
}

formBtn.addEventListener('click', () => {
    loginForm.classList.add('active');
});

formClose.addEventListener('click', () => {
    loginForm.classList.remove('active');
});
forgotPassBtn.addEventListener('click', () => {
    forgotPassForm.classList.add('active');
});

formClose1.addEventListener('click', () => {
    forgotPassForm.classList.remove('active');
});
