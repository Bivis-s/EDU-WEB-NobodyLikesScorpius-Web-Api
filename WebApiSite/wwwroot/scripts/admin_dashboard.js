//update textarea when page opened
getPredictionFromDb(0, 0).then((prediction) => {
    let predictionTextarea = document.querySelector("*[name=textArea]");
    predictionTextarea.innerHTML = prediction.text;
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

function updateTextarea() {
    let zodiacNumber = document.querySelector("#zodiac").value;
    let timeIntervalNumber = document.querySelector("#timeInterval").value;
    getPredictionFromDb(zodiacNumber, timeIntervalNumber).then((prediction) => {
        let predictionTextarea = document.querySelector("*[name=textArea]");
        predictionTextarea.innerHTML = prediction.text;
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