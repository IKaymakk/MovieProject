﻿$(document).ready(function () {
    $("#giris").click(function (e) {
        e.preventDefault(); // Sayfa yenilenmesinin önüne geç

        var username = $("#username").val();
        var password = $("#password").val();

        var button = $("#giris");
        button.text("Giriş Yapılıyor...").attr("disabled", true); // Buton metnini "Giriş Yapılıyor..." yap
        button.css({
            "background-color": "#ffc107", // Sarı renk (warning)
            "color": "white",
            "border": "1px solid #ffc107",
            "pointer-events": "none", // Disable tıklamayı engelle
            "transition": "background-color 1s ease" // Yumuşak geçiş
        });

        // 2 saniye bekledikten sonra AJAX çağrısı yapılacak
        setTimeout(function () {
            $.ajax({
                url: "/Login/Login",
                type: "POST",
                data: { username: username, password: password },
                success: function (response) {
                    if (response.success) {
                        button.text(response.username + " Hoşgeldiniz"); // Başarı mesajı
                        button.css({
                            "background-color": "#28a745", // Yeşil renk (success)
                            "color": "white",
                            "border": "1px solid #28a745"
                        });
                        setTimeout(function () { // 2 saniye bekle
                            window.location.href = "/User/Index"; // Index'e yönlendir
                        }, 2000);
                    } else {
                        button.text("Hatalı Giriş Bilgileri"); // Hata mesajı
                        button.css({
                            "background-color": "#dc3545", // Kırmızı renk (danger)
                            "color": "white",
                            "border": "1px solid #dc3545"
                        });
                        button.attr("disabled", false); // Butonu tekrar aktif yap
                        button.css({
                            "pointer-events": "auto" // Buton tıklanabilir yap
                        });
                    }
                },
                error: function () {
                    button.text("Sunucu hatası! Lütfen tekrar deneyin."); // Sunucu hatası mesajı
                    button.css({
                        "background-color": "#dc3545", // Kırmızı renk (danger)
                        "color": "white",
                        "border": "1px solid #dc3545"
                    });
                    button.attr("disabled", false); // Butonu tekrar aktif yap
                    button.css({
                        "pointer-events": "auto" // Buton tıklanabilir yap
                    });
                }
            });
        }, 1000); // 2 saniye bekle
    });



});

