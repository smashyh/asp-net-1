import { validateElement } from "../../lib/validity.js";

var lightSlider;
var maskingArea;
var maskWrapper;

function dragStart(e) {
    maskWrapper.addEventListener('mousemove', setSliderPosition);
    maskWrapper.addEventListener('touchmove', setSliderTouchPosition);
}

function dragEnd(e) {
    maskWrapper.removeEventListener('mousemove', setSliderPosition);
    maskWrapper.removeEventListener('touchmove', setSliderTouchPosition);
}

function setlightSliderLeftPercentage(percentage) {
    lightSlider.style.left = percentage.toString() + "%";
    maskingArea.style.clipPath = `inset(-4px -4px -4px ${percentage}%)`;
}

function setSliderPosition(event) {
    if (window.innerWidth == 0 || event.clientX == 0)
        return;

    let ratio = (event.clientX / window.innerWidth) * 1.01;
    if (ratio > 1.0) ratio = 1.0;
    else if (ratio < 0.0) ratio = 0.0;
    setlightSliderLeftPercentage(ratio * 100);
}

function setSliderTouchPosition(event) {
    if (window.innerWidth == 0 || event.clientX == 0)
        return;

    let ratio = (event.touches[0].clientX / window.innerWidth) * 1.01;
    if (ratio > 1.0) ratio = 1.0;
    else if (ratio < 0.0) ratio = 0.0;
    setlightSliderLeftPercentage(ratio * 100);
}

function initializeDarkModeSlider() {
    lightSlider = document.querySelector('.light-slider');
    maskingArea = document.querySelector('.masking-area');

    maskWrapper = document.querySelector('.masking-wrapper');

    // Initialize
    setlightSliderLeftPercentage(50);
    maskWrapper.addEventListener('mousedown', dragStart);
    maskWrapper.addEventListener('touchstart', dragStart);
    maskWrapper.addEventListener('mouseup', dragEnd);
    maskWrapper.addEventListener('touchend', dragEnd);
}

function initializeNewsletterValidation() {
    const form = document.getElementById("form");
    form.addEventListener("focusout", (event) => {
        if (event.target.type != 'checkbox')
            validateElement(event.target);
    });
}

document.addEventListener("DOMContentLoaded", () => {

    initializeDarkModeSlider();
    initializeNewsletterValidation();
});