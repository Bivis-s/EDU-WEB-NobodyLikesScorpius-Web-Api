// set zodiac name into page head
let pageTitle = document.querySelector("h1[id=zodiacTitle]");
getZodiacNameFromDb(getParamFromUrl("type")).then((data) => {
    pageTitle.innerHTML = data.name;
})

// set zodiac type number and time type number attribute into time interval href
let aElements = document.querySelectorAll("td[type-number]");
for (let element of aElements) {
    let innerA = element.querySelector("a");
    let timeIntervalTypeNumber = element.getAttribute("type-number");
    innerA.href = "prediction.html?type=" + getParamFromUrl("type") + "&time=" + timeIntervalTypeNumber;
}

// set time intervals names into the page
let elements = document.querySelectorAll("td[type-number]");
for (let element of elements) {
    let innerDiv = element.querySelector("p");
    let timeIntervalTypeNumber = element.getAttribute("type-number");
    getTimeIntervalNameFromDb(timeIntervalTypeNumber).then((data) => {
        innerDiv.innerHTML = data.name;
    })
}

async function getTimeIntervalNameFromDb(timeIntervalTypeNumber) {
    let apiResponse = await fetch("http://127.0.0.1:3505/TimeInterval/details?timeIntervalNumber=" + timeIntervalTypeNumber);
    if (apiResponse.ok) {
        return await apiResponse.json();
    } else {
        console.log("Cannot get time interval name from db, status code: " + apiResponse.status);
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