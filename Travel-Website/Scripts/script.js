/*Home*/
let searchBtn = document.querySelector('#search-btn');
let searchBar = document.querySelector('.search-bar-container');
let formBtn = document.querySelector('#login-btn')
let loginForm = document.querySelector('.login-form-container')
let formClose = document.querySelector('#form-close')
let menu = document.querySelector('#menu-bar');
let navbar = document.querySelector('.navbarr');
let videoBtn = document.querySelectorAll('.vid-btn');
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
//let toggleMenu = document.querySelector('.menu')
/**Home-end */
/*Home*/

window.onscroll = () => {
    searchBtn.classList.remove('fa-times');
    searchBar.classList.remove('active');
    menu.classList.remove('fa-times');
    menu.classList.remove('active');
    loginForm.classList.remove('active');
}


forgotPassBtn.addEventListener('click', () => {
    forgotPassForm.classList.add('active');
});

formClose1.addEventListener('click', () => {
    forgotPassForm.classList.remove('active');
});

searchBtn.addEventListener('click', () => {
    searchBtn.classList.toggle('fa-times');
    searchBar.classList.toggle('active');
});

formBtn.addEventListener('click', () => {
    loginForm.classList.add('active');
});

formClose.addEventListener('click', () => {
    loginForm.classList.remove('active');
});

menu.addEventListener('click', () => {
    menu.classList.toggle('fa-times');
    navbar.classList.toggle('active');
});


videoBtn.forEach(btn => {

    btn.addEventListener('click', () => {
        document.querySelector('.controls .active').classList.remove('active');
        btn.classList.add('active');

        let src = btn.getAttribute('data-src');
        document.querySelector('#video-slider').src = src;
    });
});

var swiper = new Swiper(".review-slider", {
    spaceBetween: 140,
    loop: true,
    autoplay: {
        delay: 2500,
        disableOnInteraction: false,
    },
    breakpoints: {
        640: {
            slidesPerView: 1,
        },
        768: {
            slidesPerView: 2,
        },
        1024: {
            slidesPerView: 3,
        },
    },
});

var swiper = new Swiper(".brand-slider", {
    spaceBetween: 140,
    loop: true,
    autoplay: {
        delay: 2500,
        disableOnInteraction: false,
    },
    breakpoints: {
        450: {
            slidesPerView: 2,
        },
        768: {
            slidesPerView: 3,
        },
        991: {
            slidesPerView: 4,
        },
        1200: {
            slidesPerView: 5,
        },
    },
});
/**Home-End */







