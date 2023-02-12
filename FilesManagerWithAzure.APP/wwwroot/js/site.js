// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//selecting all required elements
const dropArea = document.querySelector(".drag-area"),
    dragText = dropArea.querySelector("header"),
    button = dropArea.querySelector("button"),
    input = dropArea.querySelector("input");
let file; //this is a global variable and we'll use it inside multiple functions

button.onclick = () => {
    input.click(); //if user click on the button then the input also clicked
}

input.addEventListener("change", function () {
    //getting user select file and [0] this means if user select multiple files then we'll select only the first one
    file = this.files[0];
    dropArea.classList.add("active");
    uploadFile(); //calling function
});


//If user Drag File Over DropArea
dropArea.addEventListener("dragover", (event) => {
    event.preventDefault(); //preventing from default behaviour
    dropArea.classList.add("active");
    dragText.textContent = "Release to Upload File";
});

//If user leave dragged File from DropArea
dropArea.addEventListener("dragleave", () => {
    dropArea.classList.remove("active");
    dragText.textContent = "Drag & Drop to Upload File";
});

//If user drop File on DropArea
dropArea.addEventListener("drop", (event) => {
    event.preventDefault(); //preventing from default behaviour
    //getting user select file and [0] this means if user select multiple files then we'll select only the first one
    file = event.dataTransfer.files[0];
    uploadFile(); //calling function
});

function uploadFile() {
    let fileType = file.type; //getting selected file type
    let validExtensions = ["image/jpeg", "image/jpg", "image/png"]; //adding some valid image extensions in array
    if (validExtensions.includes(fileType)) { //if user selected file is an image file
        var modal = new bootstrap.Modal(document.getElementById('descModal'), {
            backdrop: 'static',
            focus: true
        });
        document.getElementById('btnUploadFile')
            .addEventListener('click', function (event) {
                confirmUpload();
        });
        modal.show();
    } else {
        alert("This is not an Image File!");
        dropArea.classList.remove("active");
        dragText.textContent = "Drag & Drop to Upload File";
    }
}
function confirmUpload() {
    let formData = new FormData();
    var form = document.querySelector("form");
    var description = document.getElementById('txtDescription').value;
    if (description.length > 0) {
        console.log(form['__RequestVerificationToken']);
        formData.set('File', file);
        formData.set('__RequestVerificationToken', form['__RequestVerificationToken'].value);
        formData.set('Description', description);
        submitFile(formData, form.action);
    } else {
        alert("The description must not be empty!");
    }
}

async function submitFile(formData, action) {
    try {
        const response = await fetch(action, {
            method: 'POST',
            body: formData
        });

        if (response.ok) {
            window.location.href = '/';
        }
    } catch (error) {
        console.error('Error:', error);
    }
}