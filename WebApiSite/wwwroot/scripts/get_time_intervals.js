async function getTimeIntervals() {
    let apiResponse = await fetch("http://10.0.0.4:3505/GetTimeIntervals");
    if (apiResponse.ok) {
        return await apiResponse.json();
    } else {
        console.log("Cannot get is zodiacs from db, status code: " + apiResponse.status);
    }
}