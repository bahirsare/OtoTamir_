document.addEventListener("DOMContentLoaded", function () {
    const confirmButtons = document.querySelectorAll(".confirm-button");
    const modal = new bootstrap.Modal(document.getElementById("genericConfirmModal"));
    const modalMessage = document.getElementById("confirmModalMessage");
    const confirmForm = document.getElementById("confirmModalForm");

    let targetForm = null;

    confirmButtons.forEach(button => {
        button.addEventListener("click", function () {
            const message = button.getAttribute("data-confirm-message");
            const formId = button.getAttribute("data-form-id");

            modalMessage.textContent = message;

            targetForm = document.getElementById(formId);
            modal.show();
        });
    });

    confirmForm.addEventListener("submit", function (e) {
        e.preventDefault();
        if (targetForm) {
            targetForm.submit();
        }
    });
});
