let btnDeatil = document.querySelector("#btnDetail");

btnDeatil.addEventListener("click", (e) => {

    alert(e.type);

    const httpRequest = new XMLHttpRequest();
    httpRequest.onload = (response) => {

        if (httpRequest.status == 200) 
            console.log(JSON.parse(response.target.response));
        
        if (httpRequest.status == 500)
            alert("Error");
        
    }

    const body = JSON.stringify({
        Id: "48"
    });

    httpRequest.open("POST", "Home/GetInformationJob/",
        false);
    httpRequest.setRequestHeader("Content-Type", "application/json; charset=UTF-8")

    httpRequest.send(body);

});
