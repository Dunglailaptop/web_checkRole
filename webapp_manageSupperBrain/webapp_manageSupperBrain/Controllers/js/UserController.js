

  
// Function to load data dynamically
function loadData() {
    $.ajax({
        type: 'GET',
        url: '/Home/getdata',
        dataType: 'json',
        success: function (data) {
            var html = '';
            $('#tableBody').empty();
            $.each(data, function (index, item) {
                var status = (item.status_working == 0) ? '<div class="progress"><div class="progress-bar bg-success" style="width:100%">ĐANG HOẠT ĐỘNG</div></div>' : item.status_working;
                html += '<tr>';
                html += '<td>' + (index + 1) + '</td>';
                html += '<td>' + item.Name + '</td>';
                html += '<td>' + item.FullName + '</td>';
                html += '<td>' + item.DateUpdate + '</td>';
                html += '<td>' + status + '</td>';
                html += '<td>' + new Date(item.term).toLocaleDateString('vi-VN') + '</td>';
                html += '<td>';
                html += '<input class="idInput" value="' + item.idUserAccount + '" hidden />';
                html += '<button class="confirmButton btn btn-warning" data-toggle="modal" data-target="#exampleModal">Phân quyền</button>';
                html += '<button type="button" class="btn btn-success"><span class="glyphicon glyphicon-edit"></span></button>';
                html += '<button type="button" class="btn btn-danger"><span class="glyphicon glyphicon-remove"></span></button>';
                html += '</td>';
                html += '</tr>';
            });
            $('#tableBody').html(html);
        },
        error: function (xhr, status, error) {
            console.error(error);
        }
    });
}


    // Call the loadData function when the document is ready
    loadData();


    $("#submitButton").click(function (e) {
        e.preventDefault(); // Prevent form submission

        // Create a new instance of the Vehicle class and populate its properties

        // Create an array to hold permissions
        var Permissions = [];

        // Populate the permissions array with data from form inputs
        $(".permission").each(function () {
            var permission = {
                IdPermission: $(this).find(".IdPermission").val(),
                IsRead: $(this).find(".IsRead").val(),
                IsCreate: $(this).find(".IsCreate").val(),
                IsEdit: $(this).find(".IsEdit").val(),
                IsDelete: $(this).find(".IsDelete").val()
            };
            Permissions.push(permission);
        });



        // AJAX request to submit the data
        $.ajax({
            type: "POST",
            url: "/Home/SaveChange", // Replace with your controller's action method URL
            data: JSON.stringify(Permissions),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                // Handle the response from the controller if needed
                console.log("Data submitted successfully to the controller.");
                console.log("Response from the controller:", response);

            },
            error: function (xhr, status, error) {
                // Handle errors here
                console.error("Error submitting data to the controller:", status, error);
            }
        });

    });




    // Send ID to controller when confirm button is clicked
$(document).on('click', '.confirmButton', function () {
        var idInput = $(this).siblings(".idInput").val();
        $('#datatable tbody').empty();
        // AJAX request to send the ID to the controller
        $.ajax({
            type: "GET",
            url: "/Home/getid",
            data: { idInput: idInput },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                // Handle success response

                console.log(response);
                // Clear existing table rows


                // Xóa các hàng cũ khỏi bảng
                $('#datatable tbody').empty();
                var table = $('#datatable tbody');
                // Khi truy cập dữ liệu thành công, thêm dữ liệu vào bảng
                response.forEach(function (role) {


                    var row = $('<tr>');
                    row.append('<td><i class="fa fa-barcode"></i><b>' + role.Name + '</b></td>');
                    row.append('<td style="text-align: left;padding-left: -10px;"><a ><i class="fa fa-check"></i></a><input type="hidden" id="checkall1_3" value="1"></td>');
                    row.append('<td style="text-align: left;padding-left: -10px;"><a  ><i class="fa fa-check"></i></a><input type="hidden" id="checkall1_3" value="1"></td>');
                    row.append('<td style="text-align: left;padding-left: -10px;"><a ><i class="fa fa-check"></i></a><input type="hidden" id="checkall1_3" value="1"></td>');
                    row.append('<td style="text-align: left;padding-left:  -10px;"><a ><i class="fa fa-check"></i></a><input type="hidden" id="checkall1_3" value="1"></td>');

                    table.append(row);

                    role.Permissions.forEach(function (permission) {
                        /*  permissionRow.append('<td><input class="IdPermission" style="display: none;" type="number" value="' + permission.id + '"></td>');*/
                        var permissionRow = $('<tr class="permission">');
                        permissionRow.append('<td>+ ' + permission.Name + '<div class="blur_color">' + permission.code + '</div><input class="IdPermission" hidden type="number" value="' + permission.id + '"></td>');



                        var isReadCheckbox = '<td><input class="IsRead" type="checkbox" onchange="updateCheckboxValue(this) "' + (permission.IsRead ? ' checked' : '') + ' value="' + (permission.IsRead ? '1' : '0') + '"></td>';
                        var isCreateCheckbox = '<td><input class="IsCreate" type="checkbox" onchange="updateCheckboxValue(this)"' + (permission.IsCreate ? ' checked' : '') + ' value="' + (permission.IsCreate ? '1' : '0') + '"></td>';
                        var isEditCheckbox = '<td><input class="IsEdit" type="checkbox" onchange="updateCheckboxValue(this) "' + (permission.IsEdit ? ' checked' : '') + ' value="' + (permission.IsEdit ? '1' : '0') + '"></td>';
                        var isDeleteCheckbox = '<td><input class="IsDelete" type="checkbox" onchange="updateCheckboxValue(this) "' + (permission.IsDelete ? ' checked' : '') + ' value="' + (permission.IsDelete ? '1' : '0') + '"></td>';

                        // Thêm các checkbox vào hàng
                        permissionRow.append(isReadCheckbox);
                        permissionRow.append(isCreateCheckbox);
                        permissionRow.append(isEditCheckbox);
                        permissionRow.append(isDeleteCheckbox);


                        table.append(permissionRow);
                    });

                });

            },
            error: function (xhr, status, error) {
                // Handle error response
                console.error("Error sending ID:", status, error);
                $('#datatable tbody').empty();
            }
        });

        // Hide the popup after sending the ID

   


    });
$('#searchtxt').on('input', function () {
    // Perform AJAX call when the value of the input field changes
    $.ajax({
        url: '/Home/search',
        type: 'GET',
        data: {
            keyword: $(this).val() // Get the current value of the input field
        },
        dataType: 'json',
        success: function (data) {
            var html = '';
            $('#tableBody').empty();
            $.each(data, function (index, item) {
                var status = (item.status_working == 0) ? '<div class="progress"><div class="progress-bar bg-success" style="width:100%">ĐANG HOẠT ĐỘNG</div></div>' : item.status_working;
                html += '<tr>';
                html += '<td>' + (index + 1) + '</td>';
                html += '<td>' + item.Name + '</td>';
                html += '<td>' + item.FullName + '</td>';
                html += '<td>' + item.DateUpdate + '</td>';
                html += '<td>' + status + '</td>';
                html += '<td>' + new Date(item.term).toLocaleDateString('vi-VN') + '</td>';
                html += '<td>';
                html += '<input class="idInput" value="' + item.idUserAccount + '" hidden />';
                html += '<button class="confirmButton btn btn-warning" data-toggle="modal" data-target="#exampleModal">Phân quyền</button>';
                html += '<button type="button" class="btn btn-success"><span class="glyphicon glyphicon-edit"></span></button>';
                html += '<button type="button" class="btn btn-danger"><span class="glyphicon glyphicon-remove"></span></button>';
                html += '</td>';
                html += '</tr>';
            });
            $('#tableBody').html(html);
        },
        error: function () {
            console.log('Error occurred while fetching data.');
        }
    });

});
