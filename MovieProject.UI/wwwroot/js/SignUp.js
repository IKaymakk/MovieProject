$(document).ready(function () {
    $("#kayit").click(function (e) {
        e.preventDefault(); // ❗ Butonun varsayılan davranışını engelle

        var button = $("#kayit");

        // 🛑 Boş alan kontrolü
        var username = $("#kadi").val().trim();
        var password = $("#sifre").val().trim();
        var firstname = $("#firstname").val().trim();
        var lastname = $("#lastname").val().trim();
        var mailAddress = $("#madres").val().trim();

        if (!username || !password || !firstname || !lastname || !mailAddress) {
            console.log("🚨 Boş alan var! AJAX gönderilmeyecek.");

            button.text("Lütfen boş alanları doldurunuz!");
            button.css({
                "background-color": "#dc3545",
                "color": "white",
                "border": "1px solid #dc3545"
            });

            setTimeout(() => {
                button.text("Kayıt Ol");
                button.css({
                    "background-color": "",
                    "border": ""
                });
            }, 3000);

            return; // ⛔ AJAX çağrısını engelle
        }

        console.log("✅ Tüm alanlar dolu, AJAX gönderiliyor...");

        // 🟡 AJAX isteği başlamadan önce buton değiştir
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
                Username: username,
                Password: password,
                FirstName: firstname,
                LastName: lastname,
                MailAddress: mailAddress,
                Image: "boş"
            }),
            beforeSend: function () {
                console.log("📡 AJAX isteği gönderiliyor...");
            },
            success: function (response) {
                console.log("✅ API Yanıtı:", response);

                if (response.success) {
                    console.log("🎉 API 'success: true' döndürdü!");
                    button.text("Kayıt Başarılı, Hoşgeldiniz");
                    button.css({
                        "background-color": "#28a745",
                        "color": "white",
                        "border": "1px solid #28a745"
                    });
                    setTimeout(() => window.location.href = "/User/Index", 2000);
                } else {
                    console.log("❌ API 'success: false' döndürdü!");
                    console.log("❌ API Hata Mesajı:", response.errorMessage);

                    button.text(response.errorMessage || "Lütfen bilgilerinizi kontrol edin.");
                    button.css({
                        "background-color": "#dc3545",
                        "color": "white",
                        "border": "1px solid #dc3545"
                    });
                    button.attr("disabled", false).css("pointer-events", "auto");
                }
            },
            error: function (xhr) {
                button.text("Sunucu hatası! Lütfen tekrar deneyin.");

                console.log("❌ Sunucu Hatası:", xhr);
                console.log("❌ responseText:", xhr.responseText);
                console.log("❌ status:", xhr.status);
                console.log("❌ statusText:", xhr.statusText);

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
