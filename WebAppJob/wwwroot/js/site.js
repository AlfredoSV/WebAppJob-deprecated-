

//let btnDeatil = document.querySelector("#btnDetail");

//btnDeatil.addEventListener("click", (e) => {

//    alert(e.type);

//    const httpRequest = new XMLHttpRequest();
//    httpRequest.onload = (response) => {

//        if (httpRequest.status == 200)
//            console.log(JSON.parse(response.target.response));

//        if (httpRequest.status == 500)
//            alert("Error");

//    }

//    const body = JSON.stringify({
//        Id: "48"
//    });

//    httpRequest.open("POST", "Home/GetInformationJob/",
//        false);
//    httpRequest.setRequestHeader("Content-Type", "application/json; charset=UTF-8")

//    httpRequest.send(body);

//});


//$('#tableApplications').DataTable({
//    ajax: {
//        url: location.origin + "/Home/Jobs",
//        type: 'GET'
//    },
//    processing: true,
//    serverSide : true
//});

//$('#tableApplications').DataTable();
//console.log(location.origin + "/Jobs")

const applyJob = document.querySelector("#applyJob");

function getDetailJob(obj) {

	$(".loader").show();

	let url = window.location.origin + '/Job/DetailJob/1';

	fetch(url)
		.then(response => response.text())
		.then(text => {
			document.querySelector("#job").innerHTML = text;
			$('#myModal').modal('show');
		});

	$(".loader").hide();
}


applyJob.addEventListener("click", (e) => {

	e.preventDefault();

	loadCatalogs();
		
	let url = window.location.origin + '/Job/CreateJob';

	fetch(url)
		.then(response => response.text())
		.then(text => {
			document.querySelector("#formApply").innerHTML = text;
			$('#applyJobModal').modal('show');
			let form = document.getElementById("form");
			form.addEventListener("submit", eventSubmit, true);

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

function eventSubmit(e) {

	let api = 'Job/CreateJob';
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
		&& isValidIdArea && isValidVacancyNumber && isValidMaxSalary && isValidMinSalary) {

		fetch(api, options)
			.then(response => response.json())
			.then(json => {
				$('#applyJobModal').modal('hide');
				alertify.success(json.message);
			})
			.catch(err => alertify.error(json.error));

	}

}



//$('#tableApplications').DataTable({
//	"ordering": true,
//	"searching": true,
//	pageLength: 10,
//	"processing": true,
//	deferRender: true,
//	scrollY: 400,
//	scrollCollapse: true,
//	scroller: true,
//	async: false,
//	"serverSide": false,
//	"filter": true,
//	"ajax": {
//		"url": "/Home/GetApplicationsJobs",
//		"type": "GET",
//		dataSrc: "",
//		"datatype": "json", error: function (jqXHR, ajaxOptions, thrownError) {
//			//window.location.href = "/Citas/Error"
//		}
//	},

//	columns: [

//		{ 'data': 'company' },
//		{ 'data': 'title' },
//		{ 'data': 'status' },
//		{ 'data': 'dateApply' },
//		{
//			mRender: function (data, type, row) {
//				return '<a href="/Home/Eliminar/' + row.id + '">Eliminar</a> | <a data-bs-whatever="mdo" onclick="getDetailJob(this)" href="#'+row.id+'">Detalle</a> | <a data-bs-whatever="mdo" href="Home/Editar/' + row.id + '">Editar</a>'
//			}
//		}
//	],
//	dom: 'Bfrtip',
//	"language": {
//		"url": "//cdn.datatables.net/plug-ins/1.11.3/i18n/es_es.json",
//	}
//});

function getDetailJob(obj) {
	let id = obj.hash.replace('#', '');
	console.log(id);

	const url = "Job/DetailJob/" + id

	const options = {
		method: 'GET',
		headers: {
			'Accept': 'application/json',
			'Content-Type':'application/json'
		}
	}

	fetch(url, options).then(response => response.json())
		.then(json => console.log(json));

}

new gridjs.Grid({
	columns: ['Name', 'Date apply', 'Description', 'Vancancy Numbers',
	  {
		  name: 'Actions',
		  width : '30%',
		  formatter: (_, row) => gridjs.html(`<a class="btn btn-info " onclick="getDetailJob(this)" href=#${row.cells[0].data}>Detail</a> | <a class="btn btn-success" onclick="getDetailJob(this)" href=#${row.cells[0].data}>Edit</a> | <a class="btn btn-danger" onclick="getDetailJob(this)" href=#${row.cells[0].data}>Delete</a> | <a class="btn btn-primary " onclick="getDetailJob(this)" href=#${row.cells[0].data}>Apply</a>`)
	  }],
	sort: true,
	search: {
		server: {
			summary: true,
			url: (prev, text) => `${prev}?textSearch=${text}`
		}
	},
	pagination: {
		limit: 5,
		summary: true,
		server: {
			url: (prev, page, limit) => `${prev}?&page=${page}&limit=${limit}&`
		}
		
	},
	
	resizable: true,
	server: {
		url: `Job/GetJobs`,
		method : 'GET',
		then: data => data.results.map(obj => [ obj.nameJob, obj.createDate, obj.descriptionJob, obj.vacancyNumbers]),
		handle: (res) => {
			
			if (res.status === 404) return { data: [] };
			if (res.ok) return res.json();

			res.json().then((res) => alert(res.error));
		},
		total: data => data.count
	},
	language: {
		'search': {
			'placeholder': 'Buscar...'
		},
		'pagination': {
			'previous': '⬅️',
			'next': '➡️',
			'showing': '😃 Visualizar',
			'results': () => 'Resultado'
		}
	}
}).render(document.getElementById('tableApplications'));


function closeBtnModalCreateJob() {

	$('#applyJobModal').modal('hide');

}

async function loadCatalogs() {

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