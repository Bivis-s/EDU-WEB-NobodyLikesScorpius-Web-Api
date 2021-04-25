async function getSome() {
    let response = await fetch("http://127.0.0.1:3505/Article/ddd");
    if(response.ok) {
        return await response.json();
    } else {
        return "From HTTP: " + response.status;
    }
}

getSome().then(r => alert(r[0]["title"]));