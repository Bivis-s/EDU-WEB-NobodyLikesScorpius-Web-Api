async function getPredictionFromDb(zodiacNumber, timeInterval) {
    let apiResponse = await fetch("http://168.63.68.254:3505/Prediction/details?zodiacNumber=" + zodiacNumber + "&timeInterval=" + timeInterval);
    if (apiResponse.ok) {
        return await apiResponse.json();
    } else {
        console.log("Cannot get prediction from db, status code: " + apiResponse.status);
    }
}