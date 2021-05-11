// set zodiac name into page head
let pageTitle = document.querySelector("h1[id=zodiacTitle]");
getZodiacNameFromDb(getParamFromUrl("type")).then((data) => {
    pageTitle.innerHTML = data.name;
})

// set time intervals name into page head
let timeIntervalTitle = document.querySelector("h2[id=timeIntervalTitle]");
getTimeIntervalNameFromDb(getParamFromUrl("time")).then((data) => {
    timeIntervalTitle.innerHTML = data.name;
})

// set prediction
let predictionText = document.querySelector("#main-text");
getPredictionFromDb(getParamFromUrl("type"), getParamFromUrl("time")).then((data) => {
    predictionText.innerHTML = data.text;
})

// set date into the page
let monthName = new Date().toLocaleString('en', { month: 'long' });
let dayOfWeekName = new Date().toLocaleString('en', { weekday: 'long' });
let day = new Date().toLocaleString('en', {day: 'numeric'})
let dateOnPage = document.querySelector("#date p");
dateOnPage.innerHTML = monthName + ' ' + day + '<br>' + dayOfWeekName;

async function getTimeIntervalNameFromDb(timeIntervalTypeNumber) {
    let apiResponse = await fetch("http://127.0.0.1:3505/TimeInterval/details?timeIntervalNumber=" + timeIntervalTypeNumber);
    if (apiResponse.ok) {
        return await apiResponse.json();
    } else {
        console.log("Cannot get zodiac name from db, status code: " + apiResponse.status);
    }
}

async function getZodiacNameFromDb(zodiacTypeNumber) {
    let apiResponse = await fetch("http://127.0.0.1:3505/Zodiac/details?zodiacNumber=" + zodiacTypeNumber);
    if (apiResponse.ok) {
        return await apiResponse.json();
    } else {
        console.log("Cannot get zodiac name from db, status code: " + apiResponse.status);
    }
}

async function getPredictionFromDb(zodiacNumber, timeInterval) {
    let apiResponse = await fetch("http://127.0.0.1:3505/Prediction/details?zodiacNumber=" + zodiacNumber + "&timeInterval=" + timeInterval);
    if (apiResponse.ok) {
        return await apiResponse.json();
    } else {
        console.log("Cannot get prediction from db, status code: " + apiResponse.status);
    }
}