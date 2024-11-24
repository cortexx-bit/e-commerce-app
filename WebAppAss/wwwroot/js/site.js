// site.js

document.addEventListener('DOMContentLoaded', function () {
    var logo = document.getElementById('logo');

    document.addEventListener('scroll', function () {
        if (window.scrollY > 0) {
            logo.classList.add('scrolled');
            logo.querySelector('img').src = '/images/logo-scrolled.png'; // Change to the scrolled logo
        } else {
            logo.classList.remove('scrolled');
            logo.querySelector('img').src = '/images/logo.png'; // Change back to the original logo
        }
    });
});
