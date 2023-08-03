let btnDeatil = document.querySelector("#btnDetail");

btnDeatil.addEventListener("click", (e) => {

    alert(e.type);

    const httpRequest = new XMLHttpRequest();

    httpRequest.onload = (response) => {
        console.log(JSON.parse(response.target.response));
    }

    let data = {
        "id": "4853cdc4-d0ff-40fd-9c0e-24fb3350e851"
    }

    httpRequest.open("POST", "Home/GetInformationJob/",
        false);

    httpRequest.send(JSON.stringify(data));

});
