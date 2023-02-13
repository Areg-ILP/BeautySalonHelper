var GetUserDetails = function (id) {
    $.ajax({
        type: "GET",
        url: "UserDetails",
        data: { userId: id },
        success: function (user) {
            $('#modal-id').text(user.id)
            $('#modal-name').text(user.name)
            $('#modal-surname').text(user.surename)
            $('#modal-mobileNumber').text(user.mobileNumber)
            $('#modal-email').text(user.email)
            $('#modal-role').text(user.role.name)
        }
    });
};
