//get CompatibilityFromDb
async function getCompatibilityFromDb(zodiac1TypeNumber, zodiac2TypeNumber) {
    let apiResponse = await fetch("http://127.0.0.1:3505/GetCompatibility?zodiacType1=" + zodiac1TypeNumber + "&zodiacType2=" + zodiac2TypeNumber);
    if (apiResponse.ok) {
        return await apiResponse.json();
    } else {
        console.log("Cannot get compatibility from db, status code: " + apiResponse.status);
    }
}