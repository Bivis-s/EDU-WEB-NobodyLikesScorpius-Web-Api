getIsAdminAuthorized(getParamFromUrl("sessionToken")).then((data) => {
    if (!data) {
        window.location.href = 'admin_authorization.html';
    }
});


async function getIsAdminAuthorized(sessionToken) {
    let apiResponse = await fetch("http://127.0.0.1:3505/AdminIsAuthorized?sessionToken=" + sessionToken);
    if (apiResponse.ok) {
        return await apiResponse.json();
    } else {
        console.log("Cannot get is admin authorized from db, status code: " + apiResponse.status);
    }
}