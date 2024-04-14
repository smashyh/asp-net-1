import { setFieldValidity, validateElement } from "../../lib/validity.js";

function onFormFocusUp(event) {

    validateElement(event.target);
}

function validateBasicInfo(event) {

    if (!validateElement(document.getElementById("FirstName"))) {
        event.preventDefault();
    }

    if (!validateElement(document.getElementById("LastName"))) {
        event.preventDefault();
    }

    if (!validateElement(document.getElementById("Email"))) {
        event.preventDefault();
    }
}

function validateAddress(event) {

    if (!validateElement(document.getElementById("Address_1"))) {
        event.preventDefault();
    }

    if (!validateElement(document.getElementById("PostalCode"))) {
        event.preventDefault();
    }
    
    if (!validateElement(document.getElementById("City"))) {
        event.preventDefault();
    }
}

document.addEventListener("DOMContentLoaded", () => {

    const basicInfoForm = document.getElementById("form-basic-info");
    basicInfoForm.addEventListener("focusout", onFormFocusUp);
    basicInfoForm.addEventListener("submit", validateBasicInfo);

    const addressInfoForm = document.getElementById("form-address");
    addressInfoForm.addEventListener("focusout", onFormFocusUp);
    addressInfoForm.addEventListener("submit", validateAddress);
});