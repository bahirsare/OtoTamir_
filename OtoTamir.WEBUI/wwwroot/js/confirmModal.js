document.addEventListener("DOMContentLoaded", function () {
    const confirmButtons = document.querySelectorAll(".confirm-button");
    const modal = new bootstrap.Modal(document.getElementById("genericConfirmModal"));
    const modalMessage = document.getElementById("confirmModalMessage");
    const confirmForm = document.getElementById("confirmModalForm");

    confirmButtons.forEach(button => {
        button.addEventListener("click", function () {
            // Modal mesajını ayarla
            const message = button.getAttribute("data-confirm-message");
            modalMessage.textContent = message;

            // Form action ve methodunu al
            const formAction = button.getAttribute("data-form-action");
            const formMethod = button.getAttribute("data-form-method") || "post";

            // Formun action ve methodunu dinamik olarak değiştir
            confirmForm.setAttribute("action", formAction);
            confirmForm.setAttribute("method", formMethod);

            modal.show();
        });
    });
});