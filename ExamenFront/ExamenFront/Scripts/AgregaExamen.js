function OpenAddModal() {
    $.ajax(
        {
            type: 'GET',
            url: 'Home/AgregarExamen' ,
            contentType: 'application/json; charset=utf=8',
            success: function (result) {
                $('#modal-add-content').html(result);
                $('#modal-add').modal('show');
            },
            error: function (er) {
                alert(er);
            }
        });
}
function OpenActModal(id) {
    $.ajax(
        {
            type: 'GET',
            url: 'Home/ActualizarExamen/'+ id,
            contentType: 'application/json; charset=utf=8',
            success: function (result) {
                $('#modal-add-content').html(result);
                $('#modal-add').modal('show');
            },
            error: function (er) {
                alert(er);
            }
        });
}
function OpenDelModal(id) {
    $.ajax(
        {
            type: 'GET',
            url: 'Home/EliminarExamen/' + id,
            contentType: 'application/json; charset=utf=8',
            success: function (result) {
                $('#modal-add-content').html(result);
                $('#modal-add').modal('show');
            },
            error: function (er) {
                alert(er);
            }
        });
}