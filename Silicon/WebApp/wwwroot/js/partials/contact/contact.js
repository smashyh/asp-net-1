import { validateElement } from "../../lib/validity.js";

let map;

async function initMap() {
    const position = { lat: 37.7294, lng: -122.4145 };
    const { Map } = await google.maps.importLibrary("maps");
    const { AdvancedMarkerElement } = await google.maps.importLibrary("marker");

    map = new Map(document.getElementById("map"), {
        zoom: 15,
        center: position,
        mapId: "DEMO_MAP_ID",
    });

    const marker = new AdvancedMarkerElement({
        map: map,
        position: position,
        title: "Office",
    });
}

function initDropdown() {
    const dropdownElement = document.getElementById('Services');

    const dropdown = dropdownElement.getElementsByClassName('dropdown')[0];

    for (const option of dropdown.getElementsByClassName('option')) {
        option.addEventListener('click', (e) => {

            const hiddenService = document.getElementById('Service');
            hiddenService.value = e.target.dataset.option;
        });
    }

    // If service parameter was invalid, color border red.
    const service = document.querySelector('[name="Service"]');
    if (service.classList.contains('input-validation-error')) {
        dropdownElement.classList.add('input-validation-error');
    }
}

function onFormFocusUp(event) {
    if (event.target.type != 'checkbox')
        validateElement(event.target);
}

function initFormValidation() {

    const form = document.getElementById("contact-form");
    form.addEventListener("focusout", onFormFocusUp);
}

document.addEventListener('DOMContentLoaded', () => {

    initDropdown();
    initFormValidation();
    initMap();
});