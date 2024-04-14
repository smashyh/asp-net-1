import { setFieldValidity, validateElement } from "../../lib/validity.js";



function onFormFocusUp(event) {

    if (event.target.type != 'checkbox')
        validateElement(event.target);
    //let boolExpression = true;
    //let message = "";

    //switch (event.target.type) {

    //    // First/Last Name
    //    case 'text': {

    //        let minLength = event.target.dataset.valMinlengthMin;
    //        if (event.target.value.length < minLength) {
    //            boolExpression = false;
    //            message = event.target.dataset.valMinlength;
    //        }

    //        break;
    //    }

    //    // E-mail
    //    case 'email': {
    //        const regex = new RegExp(event.target.dataset.valRegexPattern);
    //        if (!regex.test(event.target.value)) {
    //            boolExpression = false;
    //            message = event.target.dataset.valRegex;
    //        }

    //        break;
    //    }

    //    // Password
    //    case 'password': {

    //        if (event.target.name == 'Password') {
    //            let minLength = event.target.dataset.valMinlengthMin;
    //            if (event.target.value.length < minLength) {

    //                boolExpression = false;
    //                message = event.target.value.length > 0
    //                    ? event.target.dataset.valMinlength
    //                    : event.target.dataset.valRequired;
    //            }
    //            else {
    //                const regex = new RegExp(event.target.dataset.valRegexPattern);
    //                if (!regex.test(event.target.value)) {
    //                    boolExpression = false;
    //                    message = event.target.dataset.valRegex;
    //                }
    //            }
    //        }
    //        else {
    //            if (event.target.value.length == 0) {

    //                boolExpression = false;
    //                message = event.target.dataset.valRequired;
    //            }
    //            else {
    //                let password = document.querySelector('#' + event.target.dataset.valEqualtoOther.substr(2));

    //                if (password.value != event.target.value) {

    //                    boolExpression = false;
    //                    message = event.target.dataset.valEqualto;
    //                }
    //            }
    //        }

    //        break;
    //    }

    //}

    //setFieldValidity(boolExpression, event.target, message);
}

document.addEventListener("DOMContentLoaded", () => {

    const form = document.getElementById("form");
    form.addEventListener("focusout", onFormFocusUp);
});