let leftZodiacSelect = document.querySelector("#left_zodiac select");
let rightZodiacSelect = document.querySelector("#right_zodiac select");

let predictionTextarea = document.querySelector("#main_field");
let compatibilityValueLabel = document.querySelector("#compatibility_value");

//set text from db on page loaded
getCompatibilityFromDb(0, 0).then((compatibility) => {
    predictionTextarea.innerHTML = compatibility.textValue;
    compatibilityValueLabel.innerHTML = compatibility.compatibilityValue + "%"
});

//get zodiac options
getZodiacs().then((data) => {
    data.forEach((zodiac) => {
        let option = "<option value='" + zodiac.type + "'>" + zodiac.name + "</option>\n";
        leftZodiacSelect.innerHTML += option;
        rightZodiacSelect.innerHTML += option;
    })
});

//set text from db when select changes
function updateCompatibilityInPage() {
    let zodiac1TypeNumber = $("#left_zodiac option:selected").val();
    let zodiac2TypeNumber = $("#right_zodiac option:selected").val();
    getCompatibilityFromDb(zodiac1TypeNumber, zodiac2TypeNumber).then((compatibility) => {
        predictionTextarea.innerHTML = compatibility.textValue;
        compatibilityValueLabel.innerHTML = compatibility.compatibilityValue + "%"
    });
}