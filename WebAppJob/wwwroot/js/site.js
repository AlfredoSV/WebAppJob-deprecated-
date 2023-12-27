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

    let options = {
        method: 'POST',
        headers: {
            "Content-Type": "application/json"
        },
    }

    let url = window.location.origin + '/Job/CreateJob';
    let createJobForm = document.getElementById("createJobForm");
    let nameInput = document.getElementById("name");
    let lastName = document.getElementById("description");
    let vacancyNumber = document.getElementById("vacancyNumber");
    let salaryMax = document.getElementById("salaryMax");
    let salaryMin = document.getElementById("salaryMin");

    try {


        let response = await fetch(url, options);
        let text = await response.text();

        document.querySelector("#formJob").innerHTML = text;

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
        $('#createJobModal').modal('show');
        await loadCatalogs();

    } catch (error) {

        alertify.error(error);
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

            let response = await fetch(api, options);
            let json = await response.json();
            $('#applyJobModal').modal('hide');
            alertify.success(json.message);

        }

    } catch (error) {

        alertify.error(error);
    }

}

const getDetailJob = async (obj) => {

    let urlhtml = window.location.origin + "/Job/GetDetailPartial";
    let id = obj.hash.replace('#', '');
    const url = window.location.origin + "Job/DetailJob/" + id
    const options = {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
    }

    try {

        let reponseView = await fetch(urlhtml);
        let text = await res.text();
        document.querySelector("#jobDetail").innerHTML = text;

        await loadCatalogs();
        $('#detailJobModal').modal('show');

        let responseData = await fetch(url, options);
        let json = await responseData.json();
      
        document.querySelector("#idArea").value = json.idArea.toString();
        document.querySelector("#idCompany").value = json.idCompany;    
        document.querySelector("#isActive").checked = json.isActive;     
        document.querySelector("#name").value = json.nameJob;       
        document.querySelector("#salaryMax").value = json.salaryMax;        
        document.querySelector("#salaryMin").value = json.salaryMin;
        document.querySelector("#vacancyNumber").value = json.vacancyNumbers;       
        document.querySelector("#description").value = json.descriptionJob;


    } catch (error) {

        alertify.error(error);

    }

}

const getEdit = async (obj) => {


    let urlhtml = window.location.origin + "/Job/EditJob";
    let id = obj.hash.replace('#', '');
    const url = "Job/DetailJob/" + id
    const options = {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
    }

    try {

        let responseHtml = await fetch(urlhtml);
        let text = await responseHtml.text();
        document.querySelector("#formJobedit").innerHTML = text;

        await loadCatalogs();
        $('#editJobModal').modal('show');

        let responseData = await fetch(url, options);
        let json = await responseData.json();

        document.querySelector("#id").value = json.id.toString();
        document.querySelector("#idArea").value = json.idArea.toString();
        document.querySelector("#idCompany").value = json.idCompany;
        document.querySelector("#isActive").checked = json.isActive;
        document.querySelector("#name").value = json.nameJob;
        document.querySelector("#salaryMax").value = json.salaryMax;
        document.querySelector("#salaryMin").value = json.salaryMin;
        document.querySelector("#vacancyNumber").value = json.vacancyNumbers;
        document.querySelector("#description").value = json.descriptionJob;
        document.getElementById("editJobForm").addEventListener("submit", eventSubmit, true);

    } catch (error) {

        alertify.error(error);

    }


  

   
}

const deleteJob = async (obj) => {

    let id = obj.hash.replace('#', '');

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
        alertify.success(json.message);
        setTimeout(() => { window.location.reload(); }, 2000);
    } catch (error) {
        alertify.error(error);
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
