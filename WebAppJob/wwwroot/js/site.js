const btnNewJob = document.querySelector("#newJob");
const btnSearch = document.querySelector("#btnSearch");

const loadJobs = async () => {

    let url = window.location.origin + '/Job/ListJobs?';

    let dataVal = {
        page: 1,
        pageSize: 10,
        searchText: '',
        citySearch: '',
    }

    let options = {
        method: "POST",
        headers: {
            "Content-Type": " text/html;charset=UTF-8"
        }
    }

    url += new URLSearchParams(dataVal);

    try {

        let response = await fetch(url, options);
        let text = await response.text();
        let divListJobs = document.querySelector("#listJobs");
        divListJobs.classList = '';
        divListJobs.innerHTML = text;

    } catch (error) {

        alertify.error(error);
    }

}

window.onload = loadJobs;

btnSearch.addEventListener('click', async (e) => {

    e.preventDefault();

    let url = window.location.origin + '/Job/ListJobs?';

    let dataVal = {
        page: 1,
        pageSize: 10,
        searchText: document.querySelector("#searchText").value,
        citySearch: document.querySelector("#citySearch").value,
    }

    let options = {
        method: "POST",
        headers: {
            "Content-Type": " text/html;charset=UTF-8"
        }
    }

     url += new URLSearchParams(dataVal);

    try {

        let response = await fetch(url, options);
        let text = await response.text();
        let divListJobs = document.querySelector("#listJobs");
        divListJobs.classList = '';
        divListJobs.innerHTML = text;

    } catch (error) {

        alertify.error(error);
    }

});

btnNewJob.addEventListener("click", async (e) => {

    e.preventDefault();

    await loadCatalogs();

    let url = window.location.origin + '/Job/CreateJob';

    fetch(url)
        .then(response => response.text())
        .then(text => {

            document.querySelector("#formJob").innerHTML = text;
            $('#createJobModal').modal('show');

            let createJobForm = document.getElementById("createJobForm");

            createJobForm.addEventListener("submit", eventSubmit, true);


            let nameInput = document.getElementById("name");
            nameInput.addEventListener("change", () => {

                validateStr("name", "nameValidationMessage", "This name is not valid", 6, 10, true)

            }, true);

            let lastName = document.getElementById("description");
            lastName.addEventListener("change", () => {

                validateStr("description", "descriptionValidationMessage", "The description is not valid", 2, 80, true)

            }, true);

            let vacancyNumber = document.getElementById("vacancyNumber");
            vacancyNumber.addEventListener("change", () => {

                validateIntegerNumber("vacancyNumber", "vacancyNumberValidationMessage", "The number is not valid", true)

            }, true);

            let salaryMax = document.getElementById("salaryMax");
            salaryMax.addEventListener("change", () => {

                validateFloatingNumber("salaryMax", "salaryMaxValidationMessage", "The number is not valid", true)

            }, true);

            let salaryMin = document.getElementById("salaryMin");
            salaryMin.addEventListener("change", () => {

                validateFloatingNumber("salaryMin", "salaryMinValidationMessage", "The number is not valid", true)

            }, true);

        });

    $(".loader").hide();

});

const eventSubmit = (e) => {

    let api = '';
    let createJobForm = document.getElementById("createJobForm");
    let editJobForm = document.getElementById("editJobForm");

    if (createJobForm == undefined)
        api = 'Job/EditJob';
    else
        api = 'Job/CreateJob';

    let data = {
        'nameJob': document.querySelector("#name").value,
        'idCompany': document.querySelector("#idCompany").value,
        'salaryMax': document.querySelector("#salaryMax").value,
        'salaryMin': document.querySelector("#salaryMin").value,
        'vacancyNumbers': document.querySelector("#vacancyNumber").value,
        'idArea': document.querySelector("#idArea").value,
        'descriptionJob': document.querySelector("#description").value,
        'isActive': document.querySelector("#isActive").value === 'on'
    }

    if (createJobForm == undefined) {
        data["id"] = document.querySelector("#id").value;
    }

    let options = {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(data),
    }

    e.preventDefault()

    let isValidName = validateStr("name", "nameValidationMessage", "This name is not valid", 6, 10, true);
    let isValidLastName = validateStr("description", "descriptionValidationMessage", "The description is not valid", 2, 80, true);
    let isValidIdCompany = validateSelect("idCompany", "idCompanyValidationMessage", "The company is not valid", true);
    let isValidIdArea = validateSelect("idArea", "idAreaValidationMessage", "The area is not valid", true);
    let isValidVacancyNumber = validateIntegerNumber("vacancyNumber", "vacancyNumberValidationMessage", "The number is not valid", true);
    let isValidMaxSalary = validateFloatingNumber("salaryMax", "salaryMaxValidationMessage", "The number is not valid", true);
    let isValidMinSalary = validateFloatingNumber("salaryMin", "salaryMinValidationMessage", "The number is not valid", true);


    if (isValidLastName && isValidName && isValidIdCompany
        && isValidIdArea && isValidVacancyNumber &&
        isValidMaxSalary && isValidMinSalary) {

        fetch(api, options)
            .then(response => response.json())
            .then(json => {
                $('#applyJobModal').modal('hide');
                alertify.success(json.message);

                setTimeout(() => {
                }, 2000);

            })
            .catch(err => alertify.error(json.error));

    }

}

const getDetailJob = async (obj) => {

    let urlhtml = window.location.origin + "/Job/GetDetail";

    fetch(urlhtml).then(res => res.text())
        .then(text => {

            document.querySelector("#jobDetail").innerHTML = text;

        });

    await loadCatalogs();

    $('#detailJobModal').modal('show');

    let id = obj.hash.replace('#', '');
    const url = "Job/DetailJob/" + id

    const options = {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
    }

    fetch(url, options).then(response => response.json())
        .then(json => {

            let area = document.querySelector("#idArea");
            area.value = json.idArea.toString();

            let company = document.querySelector("#idCompany");
            company.value = json.idCompany;

            let isActive = document.querySelector("#isActive");
            isActive.checked = json.isActive;

            let nameJob = document.querySelector("#name");
            nameJob.value = json.nameJob;

            let salaryMax = document.querySelector("#salaryMax");
            salaryMax.value = json.salaryMax;

            let salaryMin = document.querySelector("#salaryMin");
            salaryMin.value = json.salaryMin;

            let vacancyNumber = document.querySelector("#vacancyNumber");
            vacancyNumber.value = json.vacancyNumbers;


            let description = document.querySelector("#description");
            description.value = json.descriptionJob;

        });
}

const getEdit = async (obj) => {


    let urlhtml = window.location.origin + "/Job/EditJob";

    fetch(urlhtml).then(res => res.text())
        .then(text => {

            document.querySelector("#formJobedit").innerHTML = text;

        });

    await loadCatalogs();


    $('#editJobModal').modal('show');

    let id = obj.hash.replace('#', '');
    const url = "Job/DetailJob/" + id

    const options = {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
    }

    fetch(url, options).then(response => response.json())
        .then(json => {

            let id = document.querySelector("#id");
            id.value = json.id.toString();

            let area = document.querySelector("#idArea");
            area.value = json.idArea.toString();

            let company = document.querySelector("#idCompany");
            company.value = json.idCompany;

            let isActive = document.querySelector("#isActive");
            isActive.checked = json.isActive;

            let nameJob = document.querySelector("#name");
            nameJob.value = json.nameJob;

            let salaryMax = document.querySelector("#salaryMax");
            salaryMax.value = json.salaryMax;

            let salaryMin = document.querySelector("#salaryMin");
            salaryMin.value = json.salaryMin;

            let vacancyNumber = document.querySelector("#vacancyNumber");
            vacancyNumber.value = json.vacancyNumbers;


            let description = document.querySelector("#description");
            description.value = json.descriptionJob;

        });

    let editJobForm = document.getElementById("editJobForm");

    editJobForm.addEventListener("submit", eventSubmit, true);
}

const deleteJob = (obj) => {

    let id = obj.hash.replace('#', '');

    const url = "Job/DeleteJob/" + id

    const options = {
        method: 'DELETE',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
    }

    fetch(url, options).then(response => response.json())
        .then(json => {

            alertify.success(json.message);

            setTimeout(() => { window.location.reload(); }, 2000);
        });

}

const loadCatalogs = async () => {

    let urlCatalogAreas = window.location.origin + '/GetAreas';
    let urlCatalogCompany = window.location.origin + '/GetCompanies';

    try {

        let resCat = await fetch(urlCatalogAreas);
        let jsonCat = await resCat.json();

        let resCom = await fetch(urlCatalogCompany);
        let jsonCom = await resCom.json();

        if (!resCat.ok) {
            throw new Error(jsonCat.err);
        }

        if (!resCom.ok) {
            throw new Error(jsonCom.err);
        }

        setOptionsSelect('idCompany', jsonCom.list, 'id', 'nameCompany', true);
        setOptionsSelect('idArea', jsonCat.list, 'id', 'nameArea', true);

    } catch (e) {

        alertify.error(e.message);

    }

}

const closeBtnModalCreateJob = () => {

    $('#createJobModal').modal('hide');
    document.querySelector("#formJob").innerHTML = '';

}

const closeBtnModalDetailJob = () => {

    $('#detailJobModal').modal('hide');
    document.querySelector("#jobDetail").innerHTML = '';

}

const closeBtnModalEditJob = () => {

    $('#editJobModal').modal('hide');
    document.querySelector("#formJobedit").innerHTML = '';

}

const nextPage = (num) => {
    console.log(num);
}
