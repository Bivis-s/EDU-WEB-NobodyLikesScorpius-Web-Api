function makeInvalidAuthVisible() {
    let invalidAuthText = document.querySelector("#invalid_auth");
    invalidAuthText.style.display = "block";
}

if (getParamFromUrl("invalidAuth")) {
    makeInvalidAuthVisible();
}