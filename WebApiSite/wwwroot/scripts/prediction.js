let timeParam = getParamFromUrl("time");

// set zodiac name into page head
let pageTitle = document.querySelector("h1[id=zodiacTitle]");
getZodiacNameFromDb(getParamFromUrl("type")).then((data) => {
    pageTitle.innerHTML = data.name;
})

// set time intervals name into page head
let timeIntervalTitle = document.querySelector("h2[id=timeIntervalTitle]");
getTimeIntervalNameFromDb(timeParam).then((data) => {
    timeIntervalTitle.innerHTML = data.name;
})

// set prediction
let predictionText = document.querySelector("#main-text");
getPredictionFromDb(getParamFromUrl("type"), getParamFromUrl("time")).then((data) => {
    predictionText.innerHTML = data.text;
})

// set date into the page
let dateOnPage = document.querySelector("#date p");
if (timeParam === "0") {
    let dateForSet = new Date();
    dateOnPage.innerHTML = getMonthNameByDate(dateForSet) + ' ' + dateForSet.getDate() + '<br>' + getDayOFWeekNameByDate(dateForSet);
} else if (timeParam === "1") {
    let dateForSet = new Date();
    dateForSet.setDate(dateForSet.getDate() + 1);
    dateOnPage.innerHTML = getMonthNameByDate(dateForSet) + ' ' + dateForSet.getDate() + '<br>' + getDayOFWeekNameByDate(dateForSet);
} else if (timeParam === "2") {
    let dateForSet1 = new Date();
    let dateForSet2 = new Date();
    dateForSet2.setDate(dateForSet2.getDate() + 7);
    dateOnPage.innerHTML = getMonthNameByDate(dateForSet1) + '<br>' + dateForSet1.getDate() + ' – ' + dateForSet2.getDate();
} else if (timeParam === "3") {
    dateOnPage.innerHTML = getMonthNameByDate(new Date());
}

async function getTimeIntervalNameFromDb(timeIntervalTypeNumber) {
    let apiResponse = await fetch("http://168.63.68.254:3505/TimeInterval/details?timeIntervalNumber=" + timeIntervalTypeNumber);
    if (apiResponse.ok) {
        return await apiResponse.json();
    } else {
        console.log("Cannot get zodiac name from db, status code: " + apiResponse.status);
    }
}

async function getZodiacNameFromDb(zodiacTypeNumber) {
    let apiResponse = await fetch("http://168.63.68.254:3505/Zodiac/details?zodiacNumber=" + zodiacTypeNumber);
    if (apiResponse.ok) {
        return await apiResponse.json();
    } else {
        console.log("Cannot get zodiac name from db, status code: " + apiResponse.status);
    }
}

function getMonthNameByDate(date) {
    return date.toLocaleString('ru', {month: 'long'});
}

function getDayOFWeekNameByDate(date) {
    return date.toLocaleString('ru', {weekday: 'long'});
}