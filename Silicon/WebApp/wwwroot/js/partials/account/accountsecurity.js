import { setFieldValidity, validateElement } from "../../lib/validity.js";

function onFormFocusUp(event) {

    validateElement(event.target);
}

function validatePassword(event) {

    //if (!validateElement(document.getElementById("CurrentPassword"))) {
    //    event.preventDefault();
    //}

    if (!validateElement(document.getElementById("NewPassword"))) {
        event.preventDefault();
    }

    if (!validateElement(document.getElementById("ConfirmPassword"))) {
        event.preventDefault();
    }
}

function validateDeleteAccount(event) {
    
    if (!validateElement(document.getElementById("DeleteAccountConfirm"))) {
        event.preventDefault();
    }
}

document.addEventListener("DOMContentLoaded", () => {

    const basicInfoForm = document.getElementById("form-password");
    basicInfoForm.addEventListener("focusout", onFormFocusUp);
    basicInfoForm.addEventListener("submit", validatePassword);

    const addressInfoForm = document.getElementById("form-delete-account");
    addressInfoForm.addEventListener("focusout", onFormFocusUp);
    addressInfoForm.addEventListener("submit", validateDeleteAccount);
});