import { setFieldValidity } from "../../lib/validity.js";

function onFormFocusUp(event) {

    let boolExpression = true;
    let message = "";

    switch (event.target.type) {
        // E-mail
        case 'email': {
            const regex = new RegExp(event.target.dataset.valRegexPattern);
            if (!regex.test(event.target.value)) {
                boolExpression = false;
                message = event.target.dataset.valRequired;
            }

            break;
        }

        // Password
        case 'password': {
            
            let isFalse = true;

            if (event.target.value.length < event.target.dataset.valMinlengthMin) {
                const regex = new RegExp(event.target.dataset.valRegexPattern);
                if (!regex.test(event.target.value)) {
                    isFalse = false;
                }
            }

            if (isFalse) {
                boolExpression = false;
                message = event.target.dataset.valRequired;
            }

            break;
        }

    }

    setFieldValidity(boolExpression, event.target, message);
}

document.addEventListener("DOMContentLoaded", () => {

    const form = document.getElementById("form");
    form.addEventListener("focusout", onFormFocusUp);
});