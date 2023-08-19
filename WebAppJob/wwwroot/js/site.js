

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

const applyJob = document.querySelector("#applyJob");

applyJob.addEventListener("click", (e) => {
	e.preventDefault();
	let urlCatalog = window.location.origin + '/GetAreas';
	fetch(urlCatalog).then(res => res.json())
		.then(json => {
			const areaSle = document.getElementById('idArea');
			
			
			json.list.forEach((area) => {
				let newOption = document.createElement('option');
				newOption.value = area.id;
				newOption.text =  area.nameArea;
				areaSle.add(newOption);
				newOption = undefined;
			});
		});
				

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

			let lastName = document.getElementById("lastName");
			lastName.addEventListener("change", () => {

				validateStr("lastName", "lastNameValidationMessage", "The last name is not valid", 2, 10, true)

			}, true);

			let presentationLetter = document.getElementById("presentationLetter");
			presentationLetter.addEventListener("change", () => {

				validateStr("presentationLetter", "presentationLetterValidationMessage", "The presentation letter  is not valid", 10, 50, true)

			}, true);

			
		});
	$(".loader").hide();
	

});

function eventSubmit(e) {

	let api = 'RegisterApplyJob';
	let data = {
		name: document.querySelector("#name").value,
		lastName: document.querySelector("#lastName").value,
		contactNumber: "45667",
		birthdate: "",
		presentationLetter: document.querySelector("#presentationLetter").value,
            cv: ""
	}
	let options = {
		method: "POST",
		headers: {
			"Content-Type": "application/json",
		},
		body: JSON.stringify(data),
	}

	e.preventDefault()

	let isValidName = validateStr("name", "nameValidationMessage", "This name is not valid", 6, 10, true)
	let isValidLastName = validateStr("lastName", "lastNameValidationMessage", "The last name is not valid", 2, 10, true)
	let isValidpresentationLetter = validateStr("presentationLetter", "presentationLetterValidationMessage", "The presentation letter  is not valid", 10, 50, true)


	if (isValidLastName && isValidName && isValidpresentationLetter) {

		fetch(api, options)
			.then(response => response.json())
			.then(json => console.log(json))

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
