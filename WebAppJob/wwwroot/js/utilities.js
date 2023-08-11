
/*Functions for validation of string (min,max, undifined and required)*/
function validateStr(idValue, spanMessageIdValue, messageValue, min, max, printInDom) {
	
	let formInputs = {

		inputName: {
			id: idValue,
			spanMessageId: spanMessageIdValue,
			message: messageValue
		}

	}

	let isValid = validateString(max, min, document.getElementById(formInputs.inputName.id).value)

	if (printInDom) {

		if (isValid)
			hideMessageValidation(formInputs.inputName.id, formInputs.inputName.spanMessageId)
		else
			showMessageValidation(formInputs.inputName.id, formInputs.inputName.spanMessageId, formInputs.inputName.message)

	}

	return isValid
}


function validateString(max, min, str) {

	if (str == undefined || str.trim() == ''
		|| str.length > max || str.length < min)
		return false

	return true;

}

function showMessageValidation(idInput, id, message) {

	let spanMessage = document.querySelector("#" + id);
	let inputValue = document.querySelector("#" + idInput);
	spanMessage.innerHTML = message;
	spanMessage.style.color = "red"
	spanMessage.style.marginTop = "7px";
	inputValue.style.border = "solid red 1px";

}

function hideMessageValidation(idInput, id) {

	let inputValue = document.querySelector("#" + idInput);
	let spanMessage = document.querySelector("#" + id);
	spanMessage.innerHTML = "";
	inputValue.style.border = "";

}