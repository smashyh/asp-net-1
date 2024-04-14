// ==HEADER==
let header;

let navigation;
let backdrop;
let hamburgerButton;

// ==END OF HEADER==

// ==DARK MODE==

function initializeDarkModeSlider() {
    const slider = document.getElementById('dark-mode-switch');
    slider.addEventListener('click', () => {
        const newTheme = (document.documentElement.dataset.themeMode != "dark-mode")
            ? "dark-mode"
            : "light-mode";

        fetch(`/sitesettings/theme?mode=${newTheme}`);

        document.documentElement.dataset.themeMode = newTheme;
    });
}


// ==END OF DARK MODE==

function initializeNavigation() {
    navigation = document.getElementById('header-navigation').querySelector('[class="header-subdiv"]');
    navigation.style.transition = "none";

    backdrop = document.getElementById('backdrop');

    hamburgerButton = document.getElementById('hamburger-button');
    hamburgerButton.addEventListener('click', function () {

        navigation.classList.toggle('open');
        navigation.style.transition = "transform ease-in-out 0.2s";
        backdrop.classList.toggle('active');
    });

    window.addEventListener('resize', function () {
        navigation.style.transition = "none";
        if (navigation.classList.contains('open')) {
            navigation.classList.remove('open');

            backdrop.classList.remove('active');
        }
    });
}

document.addEventListener("DOMContentLoaded", () => {

    document.body.ondragstart = (e) => {
        e.preventDefault();
    };

    initializeDarkModeSlider();

    initializeNavigation();


});