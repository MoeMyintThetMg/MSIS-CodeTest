function CheckUploadFile() {
    var selectedFile = $("#UploadedFile")[0].files[0];
    if (selectedFile) {
        var fileName = selectedFile.name;
        var fileType = fileName.split(".").pop();
        if (fileType != 'csv' && fileType != 'xml') {
            alert("Unkown Format!");
            $("#UploadedFile").val('');
            return false;
        }

        var maxfilesize = 1024 * 1024;  // 1 Mb
        var filesize = selectedFile.size;

        if (filesize > maxfilesize) {
            alert("File too large: " + (filesize / maxfilesize) + "MB" + ". Maximum size: " +" 1MB");
            return false;
        }

    }
};
function ClearData() {
    $("#UploadedFile").val('');
}

//function Add(form) {
//    //var input = document.getElementById('UploadedFile');
//    var formData = new FormData(form);
//    $.ajax({
//        type: "POST",
//        url: form.action,
//        data: formData,
//        processData: false,
//        contentType: false,
//        cache: false,
//        //timeout: 800000,
//        success: function (data, status, xhr) {
//            if (data.success) {
//                $('#addModal').modal('hide');
//                $('#listing').DataTable().ajax.url(ReloadLink);
//                $('#listing').DataTable().ajax.reload();
//                $('.modal-backdrop').remove();
//                clearAddForm();
//            }
//            AlertMessage(data.message_status, data.message);
//            $('.submit').removeAttr('disabled');
//        }
//    });

//    return false;
//}