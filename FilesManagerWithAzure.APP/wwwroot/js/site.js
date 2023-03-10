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
    Swal.fire({
        title: 'Add description',
        input: 'text',
        inputAttributes: {
            autocapitalize: 'off'
        },
        showCancelButton: true,
        confirmButtonText: 'Upload file',
        showLoaderOnConfirm: true,
        preConfirm: (description) => {
            let formData = new FormData();
            var form = document.querySelector("form");
            if (description.length > 0) {
                formData.set('File', file);
                formData.set('__RequestVerificationToken', form['__RequestVerificationToken'].value);
                formData.set('Description', description);
                return fetch(form.action, {
                    method: 'POST',
                    body: formData
                }).then(response => {
                    if (!response.ok)
                        throw new Error(response.statusText);
                    return { message: 'File successfully processed!'};
                }).catch(error => {
                        Swal.showValidationMessage(`Request failed: ${error}` )
                })
            } else 
                Swal.showValidationMessage('The description must not be empty!');
        },
        allowOutsideClick: () => !Swal.isLoading()
    }).then((result) => {
        if (result.isConfirmed) {
            Swal.fire({
                title: result.value.message
            });
        }
    })
} 