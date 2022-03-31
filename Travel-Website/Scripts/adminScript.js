/**admin-page */
let list = document.querySelectorAll('.navigation li');
/**admin-page-end */
/**menu-admin-page */
let toggle = document.querySelector('.toggle');
let navigation = document.querySelector('.navigation');
let main = document.querySelector('.main');
/**menu-admin-page-end */
/**admin-login */
const inputs = document.querySelectorAll('.input');
function focusFunc() {
    let parent = this.parentNode.parentNode;
    parent.classList.add('focus');
}
function blurFunc() {
    let parent = this.parentNode.parentNode;
    if (this.value == "") {
        parent.classList.remove('focus');
    }
}
inputs.forEach(input => {
    input.addEventListener('focus', focusFunc);
    input.addEventListener('blur', blurFunc);
});
/*admin-login-end*/


/**admin-page */
function activeLink() {
    list.forEach((item) =>
        item.classList.remove('hovered'));
    this.classList.add('hovered');
}
list.forEach((item) =>
    item.addEventListener('click', activeLink));
/**admin-page-end */

/**menu-admin-page */
toggle.onclick = function () {
    navigation.classList.toggle('active')
    main.classList.toggle('active')
}
/**menu-admin-page-end */