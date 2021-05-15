//update textarea when page opened
getPredictionFromDb(0, 0).then((prediction) => {
    let predictionTextarea = document.querySelector("*[name=Text]");
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
        let predictionTextarea = document.querySelector("*[name=Text]");
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

async function updatePredictionInDb() {
    let zodiacId = document.querySelector("#zodiac").value;
    let timeIntervalId = document.querySelector("#timeInterval").value;
    let textValue = document.querySelector("*[name=Text]").value;
    let sessionToken = getParamFromUrl("sessionToken");
    
    let body =  new FormData();
    body.append('Zodiac', zodiacId + "");
    body.append('TimeInterval', timeIntervalId + "");
    body.append('Text', textValue);
    body.append('SessionToken', sessionToken);
    
    console.log(zodiacId + " " + timeIntervalId + " " + textValue + " " + sessionToken + "\nBody: " + body);

    let response = await fetch("http://127.0.0.1:3505/UpdatePrediction",
        {
            method: 'POST',
            headers: {
                'Accept': 'multipart/form-data',
                'Content-Type': 'multipart/form-data'
            },
            body: body
        }
    );

    if (response.ok) {
        if (await response.json()[0] === true) {
            window.location.href = "admin_dashboard.html?sessionToken=" + sessionToken + "&success=true";
            return;
        }
        console.log("Cannot get is admin authorized from db, status code: " + response.status);
        window.location.href = "admin_dashboard.html?sessionToken=" + sessionToken + "&success=false";
    }
}