//get zodiac options
let zodiacOptions = "";
getZodiacs().then((data) => {
    data.forEach((zodiac) => {
        zodiacOptions += "<option value='" + zodiac.id + "'>" + zodiac.name + "</option>\n";
    })
});

//update textwhen page opened
getPredictionFromDb(0, 0).then((prediction) => {
    let predictionTextarea = document.querySelector("#prediction_text");
    predictionTextarea.innerHTML = prediction.text;
    updateGoToHoroscopeButton(0, 0)
});

getIsAdminAuthorized(getParamFromUrl("sessionToken")).then((data) => {
    if (!data) {
        window.location.href = 'admin_authorization.html';
    }
});

getZodiacs().then((data) => {
    data.forEach((zodiac) => {
        let selectZodiac = document.querySelector("#zodiac");
        selectZodiac.innerHTML = selectZodiac.innerHTML + "<option value='" + zodiac.type + "'>" + zodiac.name + "</option>\n";
    })
});

getTimeIntervals().then((data) => {
    data.forEach((timeInterval) => {
        let selectTimeInterval = document.querySelector("#timeInterval");
        selectTimeInterval.innerHTML = selectTimeInterval.innerHTML + "<option value='" + timeInterval.type + "'>" + timeInterval.name + "</option>\n";
    });
});

function updatePredictionText() {
    let zodiacNumber = $("#zodiac option:selected").val();
    let timeIntervalNumber = $("#timeInterval option:selected").val();
    getPredictionFromDb(zodiacNumber, timeIntervalNumber).then((prediction) => {
        let predictionTextarea = document.querySelector("#prediction_text");
        predictionTextarea.innerHTML = prediction.text;
        updateGoToHoroscopeButton(zodiacNumber, timeIntervalNumber)
    });
}

async function getIsAdminAuthorized(sessionToken) {
    let apiResponse = await fetch("http://127.0.0.1:3505/IsAdminAuthorized?sessionToken=" + sessionToken);
    if (apiResponse.ok) {
        return await apiResponse.json();
    } else {
        console.log("Cannot get is admin authorized from db, status code: " + apiResponse.status);
    }
}

async function updatePredictionInDb() {
    let zodiac = document.querySelector("#zodiac").value;
    let timeInterval = document.querySelector("#timeInterval").value;
    let textValue = document.querySelector("*[name=Text]").value;
    let sessionToken = getParamFromUrl("sessionToken");

    let selectedZodiacOption = $("#zodiac option:selected");
    let selectedTimeIntervalOption = $("#timeInterval option:selected");

    let body = JSON.stringify({
        Zodiac: zodiac,
        TimeInterval: timeInterval,
        Text: textValue,
        SessionToken: sessionToken
    });

    console.log(zodiac + " " + timeInterval + " " + textValue + " " + sessionToken + "\nBody: " + body);

    await fetch("http://127.0.0.1:3505/UpdatePrediction",
        {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: body
        }
    ).then((response) => {
        if (response.ok) {
            showSuccessMessage(selectedZodiacOption.text(), selectedTimeIntervalOption.text());
        } else {
            console.log("Cannot get is admin authorized from db, status code: " + response.status);
            window.location.href = "admin_dashboard.html?sessionToken=" + sessionToken + "&success=false";
        }
    });
    updatePredictionText();
}

function showSuccessMessage(zodiacName, timeIntervalName) {
    let predictionTextarea = document.querySelector("#successMessage");
    predictionTextarea.style.display = "block";
    predictionTextarea.innerHTML = "Prediction successfully updated for <br>Zodiac: <strong>" + zodiacName +
        "</strong> and Time Interval: <strong>" + timeIntervalName + "</strong>";
}

function updateGoToHoroscopeButton(zodiacNumber, timeIntervalNumber) {
    $("#goToHoroscopeButton").attr("href", "http://127.0.0.1:3505/pages/prediction.html?type=" + zodiacNumber + "&time=" + timeIntervalNumber);
}

// HAIRCUTS

async function getHaircuts() {
    let apiResponse = await fetch("http://127.0.0.1:3505/GetHaircuts");
    if (apiResponse.ok) {
        return await apiResponse.json();
    } else {
        console.log("Cannot get is zodiacs from db, status code: " + apiResponse.status);
    }
}

getHaircuts().then((haircuts) => {
    let haircutPredictionDate = new Date();

    let haircutTableBody = document.querySelector("#haircut_table tbody");
    haircuts.forEach((haircut) => {
        let formId = "zodiac_number_" + haircut.id;
        haircutTableBody.innerHTML +=
            "<tr>" +

            // moon sign column
            "<td> The moon is in sign <br><select name='ZodiacId' form='" + formId + "'><option value='" + haircut.zodiac.id + "'>" +
            haircut.zodiac.name + "</option>" + zodiacOptions + "</select></td>" +

            // moon day and moon phase column
            "<td><input name='MoonDay' form='" + formId + "' title='Moon day' type='text' value='" + haircut.moonDay + "'/>" +
            "<p><input name='MoonPhase' form='" + formId + "' title='Moon phase' type='text' value='" + haircut.moonPhase + "'/></p></td>" +

            // haircut prediction column
            "<td><textarea title='Haircut prediction' name='Prediction' form='" + formId + "'>" + haircut.prediction + "</textarea></td>" +

            // is positive checkbox column
            "<td><p>Is positive</p>" + getCheckbox(haircut.isPositive, "IsPositive", formId) + "</td>" +

            // submit button checkbox
            "<td><button id ='" + "sumbit_button" + haircut.id + "' class='submitHaircutButton' " +
            "onclick='subForm(\"" + "#" + formId + "\",\"" + "#sumbit_button" + haircut.id + "\")'> " + "Sumbit" + "</button> " +
            "</td>" +

            "</tr>" +

            // hidden inputs with haircut id and session token
            "<input form='" + formId + "' name='Id' value='" + haircut.id + "' hidden/>" +
            "<input form='" + formId + "' name='SessionToken' value='" + getParamFromUrl("sessionToken") + "' hidden/>" +

            // form for sending POST request to the server
            "<form id='" + formId + "'></form>";

        haircutPredictionDate.setDate(haircutPredictionDate.getDate() + 1);
    });
});

function getCheckbox(isShouldBeChecked, name, formId) {
    let checked = ""
    if (isShouldBeChecked) {
        checked = "checked";
    }
    return "<input name='" + name + "' type='checkbox' " + checked + " form='" + formId + "'>";
}

function subForm(formSelector, buttonSelector) {
    $.ajax({
        url: '/UpdateHaircut',
        type: 'post',
        data: $(formSelector).serialize(),
    });
    document.querySelector(buttonSelector).style.color = "green";
}