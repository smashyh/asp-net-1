const validClassField = "field-validation-valid";
const invalidClassField = "field-validation-error";
const validClassInput = "input-validation-valid";
const invalidClassInput = "input-validation-error";

export function validateElement(element) {
    let boolExpression = true;
    let message = "";

    switch (element.type) {

        // First/Last Name
        case 'text':
        case 'textarea': {

            let minLength = element.dataset.valMinlengthMin;
            
            if (element.value.length < minLength) {
                
                boolExpression = false;
                message = element.dataset.valMinlength;
            }
            else {
                let maxLength = element.dataset.valMaxlengthMax;
                if (element.value.length > maxLength) {
                    boolExpression = false;
                    message = element.dataset.valMaxlength;
                }
            }

            break;
        }

        // E-mail
        case 'email': {
            const regex = new RegExp(element.dataset.valRegexPattern);
            if (!regex.test(element.value)) {
                boolExpression = false;
                message = element.dataset.valRegex;
            }

            break;
        }

        // Password
        case 'password': {

            if (element.name != 'ConfirmPassword') {
                let minLength = element.dataset.valMinlengthMin;
                if (element.value.length < minLength) {

                    boolExpression = false;
                    message = element.value.length > 0
                        ? element.dataset.valMinlength
                        : element.dataset.valRequired;
                }
                else {
                    const regex = new RegExp(element.dataset.valRegexPattern);
                    if (!regex.test(element.value)) {
                        boolExpression = false;
                        message = element.dataset.valRegex;
                    }
                }
            }
            else {
                if (element.value.length == 0) {

                    boolExpression = false;
                    message = element.dataset.valRequired;
                }
                else {
                    let password = document.querySelector('#' + element.dataset.valEqualtoOther.substr(2));

                    if (password.value != element.value) {

                        boolExpression = false;
                        message = element.dataset.valEqualto;
                    }
                }
            }

            break;
        }

        case 'checkbox': {
            const minValue = element.dataset.valRangeMin.toLowerCase();
            const maxValue = element.dataset.valRangeMax.toLowerCase();

            const value = element.checked.toString();

            if (value != minValue && value != maxValue) {
                boolExpression = false;
                message = element.dataset.valRange;
            }

            break;
        }

    }

    setFieldValidity(boolExpression, element, message);

    return boolExpression;
}

export function setFieldValidity(boolExpression, element, message = "") {
    let errorText = document.querySelector(`[data-valmsg-for="${element.name}"]`);

    if (errorText == null || errorText === undefined)
        return;

    if (!boolExpression) {
        errorText.classList.remove(validClassField);
        if (!errorText.classList.contains(invalidClassField))
            errorText.classList.add(invalidClassField);

        element.classList.remove(validClassInput);
        if (!element.classList.contains(invalidClassInput))
            element.classList.add(invalidClassInput);
    }
    else {
        errorText.classList.remove(invalidClassField);
        if (!errorText.classList.contains(validClassField))
            errorText.classList.add(validClassField);

        element.classList.remove(invalidClassInput);
        if (!element.classList.contains(validClassInput))
            element.classList.add(validClassInput);
    }

    errorText.innerHTML = message;
}