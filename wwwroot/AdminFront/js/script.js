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
});

$(".fa-trash-alt").click(function () {
    var index = $(this).closest("tr").index();
    $("#deleteModal").modal("show");
});

$(document).on("click", ".edit-icon", function () {
    let row = $(this).closest("tr");
    let cells = row.find("td");
    let data = {};
    cells.each(function () {
        let key = $(this).data("col");
        let value = $(this).text();
        data[key] = value;
    });

    $("#editModal").modal("show");
    $.each(data, function (key, value) {
        $('#editModal [name="' + key + '"]').val(value);
    });

    $("#editModal form").submit(function (e) {
        e.preventDefault();
        let formData = $(this).serializeArray();
        $.each(formData, function (index, field) {
            row.find('td[data-col="' + field.name + '"]').text(field.value);
        });
        $("#editModal").modal("hide");
    });
});