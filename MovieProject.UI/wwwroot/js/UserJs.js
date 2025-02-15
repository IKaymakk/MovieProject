$(document).ready(function () {
    $.ajax({
        url: "/User/GetUserDetails", // Controller ismini güncelle
        type: "GET",
        success: function (response) {
            if (response.success) {
                // Input alanlarına kullanıcı bilgilerini yerleştir
                let userData = response.data;
                $("#firstName").val(userData.firstName);
                $("#lastName").val(userData.lastName);
                $("#userName").val(userData.userName);
                $("#email").val(userData.mailAddress);
            } else {
                console.error("Hata:", response.message);
            }
        },
        error: function (xhr, status, error) {
            console.error("Sunucu hatası:", error);
        }
    });
});
