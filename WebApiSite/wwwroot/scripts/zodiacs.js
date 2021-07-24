// set type number attribute into zodiac href
let aElements = document.querySelectorAll("td[type-number]");
for (let element of aElements) {
    let innerA = element.querySelector("a");
    let zodiacTypeNumber = element.getAttribute("type-number");
    innerA.href = "prediction_date_pick.html?type=" + zodiacTypeNumber;
}

// set zodiac names into table cells on the page
let elements = document.querySelectorAll("td[type-number]");
for (let element of elements) {
    let innerDiv = element.querySelector("div");
    let zodiacTypeNumber = element.getAttribute("type-number");
    getZodiacNameFromDb(zodiacTypeNumber).then((data) => {
        innerDiv.innerHTML = data.name;
    })
}

async function getZodiacNameFromDb(zodiacTypeNumber) {
    let apiResponse = await fetch("http://168.63.68.254:3505/Zodiac/details?zodiacNumber=" + zodiacTypeNumber);
    if (apiResponse.ok) {
        return await apiResponse.json();
    } else {
        console.log("Cannot get zodiac name from db, status code: " + apiResponse.status);
    }
}