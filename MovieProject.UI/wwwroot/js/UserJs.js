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
                $("#userimg").attr("src", userData.image);
            } else {
                console.error("Hata:", response.message);
            }
        },
        error: function (xhr, status, error) {
            console.error("Sunucu hatası:", error);
        }
    });
});

$(document).ready(function () {
    // Form submit işlemi
    $("#profileDetails").submit(function (e) {
        e.preventDefault();


        // Kullanıcı bilgilerini almak
        var userDetails = {
            firstName: $("#firstName").val(),
            lastName: $("#lastName").val(),
            mailAddress: $("#email").val(),
            userName: $("#userName").val()
        };



        // Formdaki alanların hepsi dolu olmalı
        if (!userDetails.firstName || !userDetails.lastName || !userDetails.userName || !userDetails.mailAddress) {
            toastr.error("Lütfen Tüm Alanları Doldurun!");
            return;
        }

        $.ajax({
            url: '/User/UpdateUserDetails',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(userDetails),
            dataType: "json",
            success: function (response) {
                if (response.success) {
                    toastr.success("Başarılı : " + response.message);

                } else {
                    toastr.error("Hata : " + response.message);

                }
            },
            error: function (xhr, status, error) {
                console.log("AJAX Hata:", xhr.responseText);
                Swal.fire({
                    icon: 'error',
                    title: 'Bir hata oluştu!',
                    text: 'Lütfen tekrar deneyin.',
                    confirmButtonText: 'Tamam'
                });
            }
        });
    });
});

$(document).ready(function () {
    $("#passwordForm").submit(function (e) {
        e.preventDefault();

        var dtoObjects = {
            oldPassword: $("#oldPassword").val().trim(),
            newPassword: $("#newPassword").val().trim(),
            confirmNewPassword: $("#confirmNewPassword").val().trim(),
        };

        if (!dtoObjects.oldPassword || !dtoObjects.newPassword) {
            toastr.error("Lütfen Tüm Alanları Doldurun!");
            return;
        }

        if (dtoObjects.confirmNewPassword != dtoObjects.newPassword) {
            toastr.error("Şifreleriniz Eşleşmiyor!");

            return;
        }
        $.ajax({
            url: '/User/ChangePassword',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(dtoObjects),
            dataType: "json",
            success: function (response) {
                if (response.success) {
                    toastr.success("Başarılı : " + response.message);
                    setTimeout(function () {
                        location.reload();
                    }, 2000); // 2 saniye bekleyip sayfayı yenile
                } else {
                    toastr.error("Hata : " + response.message);

                }
            },
            error: function (xhr, status, error) {
                console.log("AJAX Hata:", xhr.responseText);
                Swal.fire({
                    icon: 'error',
                    title: 'Bir hata oluştu!',
                    text: 'Lütfen tekrar deneyin.',
                    confirmButtonText: 'Tamam'
                });
            }
        });

    });
});