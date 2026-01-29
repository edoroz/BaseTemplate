

$('#frmLogin').submit(function (e) {
    e.preventDefault();
    e.stopPropagation();

    $('#loginButton').html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...');
    $('#loginButton').prop('disabled', true);

    let data = Object.fromEntries(new FormData(e.target));
    fetch(`/api/account/login`, {
        method: "POST",
        body: JSON.stringify(data),
        headers: { "Content-type": "application/json; charset=UTF-8" }
    }).then(response => response)
        .then(response => {
            switch (response.status) {
                case 200:
                    window.location.href = "/home/index";
                    break;
                case 401:
                    $('#login-error').show();
                    $('#loginButton').html('Log In');
                    $('#loginButton').prop('disabled', false);
                    break;
            }
        }).catch((err) => {
            console.log(err);
        });
});

$(function () {
    $('#togglePassword').on('click', function () {
        const passwordInput = $('#password');
        const icon = $(this);
        const type = passwordInput.attr('type') === 'password' ? 'text' : 'password';
        passwordInput.attr('type', type);

        // Alternar icono
        icon.toggleClass('bi-eye');
        icon.toggleClass('bi-eye-slash');
    });
});