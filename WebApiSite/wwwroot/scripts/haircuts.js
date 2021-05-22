getHaircuts().then((haircuts) => {
    let haircutPredictionDate = new Date();

    let haircutTableBody = document.querySelector("#haircut_table tbody");
    let haircutRows = "";
    haircuts.forEach((haircut) => {
        haircutRows += "<tr>" +
            "<td><b>" + haircutPredictionDate.getDate() + "</b> " + getMonthNameByDate(haircutPredictionDate) + "<br>" 
            + getDayOFWeekNameByDate(haircutPredictionDate) + "</td>" +
            "<td> The moon is in sign <b>" + haircut.zodiac.name + "</b></td>" +
            "<td><p>" + haircut.moonDay + "</p><p>" + haircut.moonPhase + "</p></td>" +
            "<td>" + getHaircutPositiveNegativeEmoji(haircut.isPositive) + " " + haircut.prediction + "</td>" +
            "</tr>";

        haircutPredictionDate.setDate(haircutPredictionDate.getDate() + 1);
    });
    haircutTableBody.innerHTML = haircutRows;
});

function getMonthNameByDate(date) {
    return date.toLocaleString('en', {month: 'long'});
}

function getDayOFWeekNameByDate(date) {
    return date.toLocaleString('en', {weekday: 'long'});
}

function getHaircutPositiveNegativeEmoji(isPositive) {
    if (isPositive) {
        return "🌝";
    } else {
        return "🌚"
    }
}

async function getHaircuts() {
    let apiResponse = await fetch("http://127.0.0.1:3505/GetHaircuts");
    if (apiResponse.ok) {
        return await apiResponse.json();
    } else {
        console.log("Cannot get is zodiacs from db, status code: " + apiResponse.status);
    }
}