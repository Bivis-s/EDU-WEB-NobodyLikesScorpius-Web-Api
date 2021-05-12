async function getPredictionFromDb(zodiacNumber, timeInterval) {
    let apiResponse = await fetch("http://127.0.0.1:3505/Prediction/details?zodiacNumber=" + zodiacNumber + "&timeInterval=" + timeInterval);
    if (apiResponse.ok) {
        return await apiResponse.json();
    } else {
        console.log("Cannot get prediction from db, status code: " + apiResponse.status);
    }
}