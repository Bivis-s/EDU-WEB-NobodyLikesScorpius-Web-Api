async function getTimeIntervals() {
    let apiResponse = await fetch("http://127.0.0.1:3505/GetTimeIntervals");
    if (apiResponse.ok) {
        return await apiResponse.json();
    } else {
        console.log("Cannot get is zodiacs from db, status code: " + apiResponse.status);
    }
}