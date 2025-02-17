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
            firstName: $("#firstName").val().trim(),
            lastName: $("#lastName").val().trim(),
            mailAddress: $("#email").val().trim(),
            userName: $("#userName").val().trim()
        };

        // E-posta alanı boşsa hata ver
        if (!userDetails.mailAddress) {
            Swal.fire({
                icon: 'error',
                title: 'Eksik E-posta!',
                text: 'Lütfen geçerli bir e-posta adresi giriniz.',
                confirmButtonText: 'Tamam'
            });
            return; // İşlemi durdur
        }

        // Formdaki alanların hepsi dolu olmalı
        if (!userDetails.firstName || !userDetails.lastName || !userDetails.userName) {
            Swal.fire({
                icon: 'warning',
                title: 'Eksik Bilgi!',
                text: 'Lütfen tüm alanları doldurduğunuzdan emin olun.',
                confirmButtonText: 'Tamam',
            });
            return;
        }

        $.ajax({
            url: '/User/UpdateUserDetails',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(userDetails),
            dataType: "json",
            success: function (response) {
                console.log("Sunucudan gelen yanıt:", response);
                if (response.success) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Başarılı',
                        text: response.message,
                        confirmButtonText: 'Tamam'
                    });
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Hata!',
                        text: response.message,
                        confirmButtonText: 'Tamam'
                    });
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
            Swal.fire({
                icon: 'warning',
                title: 'Eksik Bilgi!',
                text: 'Lütfen tüm alanları doldurduğunuzdan emin olun.',
                confirmButtonText: 'Tamam',
            });
            return;
        }

        if (dtoObjects.confirmNewPassword != dtoObjects.newPassword) {
            Swal.fire({
                icon: 'warning',
                title: 'Eksik Bilgi!',
                text: 'Şifreleriniz Eşleşmiyor.',
                confirmButtonText: 'Tamam',
            });
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
                    Swal.fire({
                        icon: 'success',
                        title: 'Başarılı',
                        text: response.message,
                        confirmButtonText: 'Tamam'
                    });
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Hata!',
                        text: response.message,
                        confirmButtonText: 'Tamam'
                    });
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