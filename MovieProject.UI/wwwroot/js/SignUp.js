$(document).ready(function () {
    $("#signupForm").submit(function (e) {
        e.preventDefault(); // ❗ Formun normal submit işlemini engelle

        var button = $("#kayit");
        button.text("Kayıt Yapılıyor...").attr("disabled", true);
        button.css({
            "background-color": "#ffc107",
            "color": "white",
            "border": "1px solid #ffc107",
            "pointer-events": "none",
            "transition": "background-color 1s ease"
        });

        $.ajax({
            url: "/Login/SignUp",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({
                Username: $("#kadi").val(),
                Password: $("#sifre").val(),
                FirstName: $("#firstname").val(),
                LastName: $("#lastname").val(),
                Email: $("#madres").val(),
                Image: "boş"
            }),
            success: function (response) {
                if (response.success) {
                    button.text("Kayıt Başarılı, Hoşgeldiniz");
                    button.css({
                        "background-color": "#28a745",
                        "color": "white",
                        "border": "1px solid #28a745"
                    });
                    setTimeout(() => window.location.href = "/User", 2000);
                } else {
                    button.text(response.errorMessage || "Lütfen bilgilerinizi kontrol edin.");
                    button.css({
                        "background-color": "#dc3545",
                        "color": "white",
                        "border": "1px solid #dc3545"
                    });
                    button.attr("disabled", false).css("pointer-events", "auto");

                    // 🔎 Hata detaylarını logla
                    console.log("Sunucu Hatası:", response);
                }
            },
            error: function (xhr) {
                button.text("Sunucu hatası! Lütfen tekrar deneyin.");
                console.log("❌ Sunucu Hatası:", xhr.responseText);
                console.log("❌ Durum Kodu:", xhr.status);
                console.log("❌ Durum Açıklaması:", xhr.statusText);
                button.css({
                    "background-color": "#dc3545",
                    "color": "white",
                    "border": "1px solid #dc3545"
                });
                button.attr("disabled", false);
            }
        });
    });
});
