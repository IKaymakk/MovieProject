// comment.js
$(document).ready(function () {
    // Modal'ı açma fonksiyonu
    window.openCommentModal = function () {
        $('#commentModal').modal('show');
    };

    // Form gönderimi
    $('#submitComment').click(function () {
        if (!validateForm()) {
            return;
        }

        var commentData = {
            commentTitle: $('#commentTitle').val().trim(),
            commentText: $('#commentText').val().trim()
        };

        $.ajax({
            url: '/Comment/AddComment',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(commentData),
            beforeSend: function () {
                $('#submitComment').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Gönderiliyor...');
            },
            success: function (response) {
                toastr.success('Yorumunuz başarıyla eklendi!');
                $('#commentModal').modal('hide');
                resetForm();
                // Yorum listesini güncelle
                if (typeof refreshComments === 'function') {
                    refreshComments();
                }
            },
            error: function (xhr) {
                if (xhr.status === 400) {
                    var errors = xhr.responseJSON;
                    Object.keys(errors).forEach(function (key) {
                        toastr.error(errors[key]);
                    });
                } else {
                    toastr.error('Yorum eklenirken bir hata oluştu.');
                }
            },
            complete: function () {
                $('#submitComment').prop('disabled', false).html('Yorum Ekle');
            }
        });
    });

    // Form validasyonu
    function validateForm() {
        var isValid = true;
        var title = $('#commentTitle').val().trim();
        var text = $('#commentText').val().trim();

        if (!title) {
            $('#commentTitle').addClass('is-invalid');
            isValid = false;
        } else {
            $('#commentTitle').removeClass('is-invalid');
        }

        if (!text) {
            $('#commentText').addClass('is-invalid');
            isValid = false;
        } else {
            $('#commentText').removeClass('is-invalid');
        }

        return isValid;
    }

    // Formu sıfırlama
    function resetForm() {
        $('#commentForm')[0].reset();
        $('.is-invalid').removeClass('is-invalid');
    }

    // Modal kapandığında formu sıfırla
    $('#commentModal').on('hidden.bs.modal', function () {
        resetForm();
    });

    // Input alanları değiştiğinde validation sınıflarını kaldır
    $('#commentTitle, #commentText').on('input', function () {
        $(this).removeClass('is-invalid');
    });
});