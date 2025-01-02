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

        alertify.error(error.message, 10);
    }

});

btnNewJob.addEventListener("click", async (e) => {

    e.preventDefault();

    let options = {
        method: 'GET',
        headers: {
            "Content-Type": "application/json"
        },
    }

    let url = window.location.origin + '/Job/CreateJobPartial';



    try {


        let response = await fetch(url, options);
        let text = await response.text();

        document.querySelector("#formJob").innerHTML = text;
        $('#createJobModal').modal('show');
        let createJobForm = document.getElementById("createJobForm");
        let nameInput = document.getElementById("name");
        let lastName = document.getElementById("description");
        let vacancyNumber = document.getElementById("vacancyNumber");
        let salaryMax = document.getElementById("salaryMax");
        let salaryMin = document.getElementById("salaryMin");
        createJobForm.addEventListener("submit", eventSubmit, true);

        nameInput.addEventListener("change", () => {

            validateStr("name", "nameValidationMessage", "This name is not valid", 6, 10, true)

        }, true);

        lastName.addEventListener("change", () => {

            validateStr("description", "descriptionValidationMessage", "The description is not valid", 2, 80, true)

        }, true);

        vacancyNumber.addEventListener("change", () => {

            validateIntegerNumber("vacancyNumber", "vacancyNumberValidationMessage", "The number is not valid", true)

        }, true);

        salaryMax.addEventListener("change", () => {

            validateFloatingNumber("salaryMax", "salaryMaxValidationMessage", "The number is not valid", true)

        }, true);

        salaryMin.addEventListener("change", () => {

            validateFloatingNumber("salaryMin", "salaryMinValidationMessage", "The number is not valid", true)

        }, true);

        $(".loader").hide();


    } catch (error) {

        alertify.error(error.message, 10);
    }

});

const eventSubmit = async (e) => {

    e.preventDefault();

    let createJobForm = document.getElementById("createJobForm");
    let editJobForm = document.getElementById("editJobForm");
    let api = createJobForm == undefined ? 'Job/EditJob' : 'Job/CreateJob';

    let data = {
        'nameJob': document.querySelector("#name").value,
        'idCompany': document.querySelector("#idCompany").value,
        'salaryMax': document.querySelector("#salaryMax").value,
        'salaryMin': document.querySelector("#salaryMin").value,
        'vacancyNumbers': document.querySelector("#vacancyNumber").value,
        'idArea': document.querySelector("#idArea").value,
        'descriptionJob': document.querySelector("#description").value,
        'isActive': document.querySelector("#isActive").checked,
        'Logo': document.querySelector("#logo").value
    }

    if (createJobForm == undefined) {
        data["id"] = document.querySelector("#id").value;
    }

    let options = {
        method: "POST",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: data
    }

    let isValidName = validateStr("name", "nameValidationMessage", "This name is not valid", 6, 10, true);
    let isValidLastName = validateStr("description", "descriptionValidationMessage", "The description is not valid", 2, 80, true);
    let isValidIdCompany = validateSelect("idCompany", "idCompanyValidationMessage", "The company is not valid", true);
    let isValidIdArea = validateSelect("idArea", "idAreaValidationMessage", "The area is not valid", true);
    let isValidVacancyNumber = validateIntegerNumber("vacancyNumber", "vacancyNumberValidationMessage", "The number is not valid", true);
    let isValidMaxSalary = validateFloatingNumber("salaryMax", "salaryMaxValidationMessage", "The number is not valid", true);
    let isValidMinSalary = validateFloatingNumber("salaryMin", "salaryMinValidationMessage", "The number is not valid", true);

    try {

        if (isValidLastName && isValidName && isValidIdCompany
            && isValidIdArea && isValidVacancyNumber &&
            isValidMaxSalary && isValidMinSalary) {

            options.body = JSON.stringify(options.body);
            let response = await fetch(api, options);
            let json = await response.json();

            if (response.status != 200) {
                alertify.error(json.err, 10);
                return;
            }

            $('#createJobModal').modal('hide');
            await loadJobs();
            alertify.success(json.message, 10);

        }

    } catch (error) {

        alertify.error(error.message, 10);
    }

}

const getDetailJob = async (obj) => {

    let id = obj;
    let urlhtml = window.location.origin + "/Job/GetDetailPartial/" + id;
    const options = {
        method: 'GET',
        headers: {
            'Content-Type': 'text/html'
        }
    };

    try {

        let reponseView = await fetch(urlhtml, options);
        let text = await reponseView.text();
        document.querySelector("#jobDetail").innerHTML = text;
        $('#detailJobModal').modal('show');


    } catch (error) {

        alertify.error(error.message, 10);
    }

}

const getEdit = async (obj) => {

    let id = obj;
    let urlhtml = window.location.origin + "/Job/EditJob/" + id;

    const options = {
        method: 'GET',
        headers: {
            'Content-Type': 'text/html'
        }
    };

    try {

        let responseHtml = await fetch(urlhtml, options);
        let responseModal = await responseHtml.text();
        document.querySelector("#formJobedit").innerHTML = responseModal;
        $('#editJobModal').modal('show');

    } catch (error) {
        alertify.error(error.message, 10);
    }

}

const deleteJob = async (obj) => {

    let id = obj;

    const url = "Job/DeleteJob/" + id

    const options = {
        method: 'DELETE',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
    }

    try {
        let response = await fetch(url, options);
        let json = await response.json();

        if (response.status != 200) {
            alertify.error(json.err, 10);
            return;
        }
        $(".loader").show();
        alertify.success(json.message);
        await loadJobs();
        $(".loader").hide();

    } catch (error) {
        alertify.error(error.message, 10);
    }

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

        alertify.error(error.message, 10);

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
