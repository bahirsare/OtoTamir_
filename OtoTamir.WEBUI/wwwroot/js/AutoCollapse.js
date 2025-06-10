document.addEventListener("DOMContentLoaded", function () {
    const toggleButtons = document.querySelectorAll('[data-bs-toggle="collapse"]');

    toggleButtons.forEach(button => {
        button.addEventListener("click", function (e) {
            const targetId = button.getAttribute("data-bs-target") || button.getAttribute("href");

            if (!targetId) return;

            const targetSelector = targetId.startsWith("#") ? targetId : `#${targetId}`;
            const clickedCollapse = document.querySelector(targetSelector);

            if (!clickedCollapse) return;

            const allOpenCollapses = document.querySelectorAll(".collapse.show");

            // Diğer açık olanları kapat
            allOpenCollapses.forEach(el => {
                if (el !== clickedCollapse) {
                    const instance = bootstrap.Collapse.getInstance(el) || new bootstrap.Collapse(el, { toggle: false });
                    instance.hide();
                }
            });

            // Toggle işlemi (açık değilse aç, açıksa Bootstrap kendisi kapatır)
            const clickedInstance = bootstrap.Collapse.getInstance(clickedCollapse) || new bootstrap.Collapse(clickedCollapse, { toggle: false });

            if (!clickedCollapse.classList.contains("show")) {
                clickedInstance.show();
            }
        });
    });
});
