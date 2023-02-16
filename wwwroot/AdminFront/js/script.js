$(document).ready(function () {
    var $rows = $("#table-body tr");
    var pageSize = 10;

    $("#search-input").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $rows
            .hide()
            .filter(function () {
                return $(this).text().toLowerCase().indexOf(value) > -1;
            })
            .show();
    });

    $rows.slice(pageSize).hide();

    $("nav .pagination").append(function () {
        var pageCount = Math.ceil($rows.length / pageSize);
        var pagination = "";
        for (var i = 0; i < pageCount; i++) {
            pagination +=
                '<li class="page-item"><a class="page-link" href="#">' +
                (i + 1) +
                "</a></li>";
        }
        return pagination;
    });

    $("nav .pagination a").on("click", function () {
        var pageNum = $(this).text();
        var startItem = (pageNum - 1) * pageSize;
        var endItem = startItem + pageSize;
        $rows.hide().slice(startItem, endItem).show();
        $(this).parent().addClass("active").siblings().removeClass("active");
    });

    $("th").on("click", function () {
        var column = $(this).index();
        var sortOrder = $(this).data("sort-order") === "asc" ? "desc" : "asc";
        $(this).data("sort-order", sortOrder);
        var sortFunction = function (a, b) {
            var aValue = $(a).find("td").eq(column).text();
            var bValue = $(b).find("td").eq(column).text();
            if (sortOrder === "asc") {
                return aValue > bValue ? 1 : -1;
            } else {
                return aValue < bValue ? 1 : -1;
            }
        };
        $rows.sort(sortFunction).appendTo("#table-body");
    });

    $(".fa-info-circle").click(function () {
        $("#details-modal").modal("show");
    });

    $(".fa-trash-alt").click(function () {
        var userId = $(this).closest("tr").attr('id');
        $('#deleteUser').attr('onclick', `deleteUserDetails(${userId})`);
        $("#deleteModal").modal("show");
    });

    $('#editModal').on('hidden.bs.modal', function (e) {
        $('#roleSelect').empty();
    });
});

var getUserDetails = function (userId) {
    $.ajax({
        type: "GET",
        url: 'UserDetails',
        data: { userId: userId },
        success: function (user) {
            $('#modal-id').text(user.id)
            $('#modal-name').text(user.name)
            $('#modal-surname').text(user.surname)
            $('#modal-mobileNumber').text(user.mobileNumber)
            $('#modal-email').text(user.email)
            $('#modal-role').text(user.role)
            $('#modal-books-count').text(user.booksCount)
            $('#modal-creation-date').text(user.creationDate)
        },
        error: function (jqXHR, textStatus, errorThrown) {
            var errorMessage = jqXHR.responseText;
            var statusCode = jqXHR.status;
            $("#details-modal").modal("hide");
            alert('Status Code: ' + statusCode + ' Error Message: ' + errorMessage);
        }
    });
};

var deleteUserDetails = function (userId) {
    $.ajax({
        type: "DELETE",
        url: 'DeleteUser',
        data: { userId: userId },
        success: function () {
            location.reload();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            var errorMessage = jqXHR.responseText;
            var statusCode = jqXHR.status;
            $("#deleteModal").modal("hide");
            alert('Status Code: ' + statusCode + ' Error Message: ' + errorMessage);
        }
    });
};

var getRolesForEdit = function (userId, roleId) {
    $.ajax({
        type: "GET",
        url: 'GetRoles',
        success: function (roles) {
            $.each(roles, function (index, value) {
                $('#roleSelect').append($('<option>', {
                    value: value.id,
                    text: value.name,
                    'selected': roleId === value.id ? true : false
                }));
            });

            $('#saveChanges').attr('onclick', `updateUserRole(${userId}, ${roleId})`);
            $("#editModal").modal("show");
        },
        error: function (jqXHR, textStatus, errorThrown) {
            var errorMessage = jqXHR.responseText;
            var statusCode = jqXHR.status;
            $("#editModal").modal("hide");
            alert('Status Code: ' + statusCode + ' Error Message: ' + errorMessage);
        }
    });
}

var updateUserRole = function (userId, roleId) {
    var selectedRoleId = $('#roleSelect option:selected').val();
    $.ajax({
        type: "PUT",
        url: 'UpdateUserRole',
        data: { userId: userId, roleId: selectedRoleId },
        success: function () {
            location.reload();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            var errorMessage = jqXHR.responseText;
            var statusCode = jqXHR.status;
            $("#editModal").modal("hide");
            alert('Status Code: ' + statusCode + ' Error Message: ' + errorMessage);
        }
    });
}