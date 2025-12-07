const symptomIndices = {};

function addSymptomRow(containerId) {
    const container = document.getElementById(containerId);
    if (!container) {
        console.error("Container bulunamadı:", containerId);
        return;
    }

    if (!symptomIndices[containerId]) {
        symptomIndices[containerId] = 0;
    }
    const index = symptomIndices[containerId];

    const card = document.createElement("div");
    card.className = "border rounded p-3 mb-1 bg-light symptom-item";

    const formRow = document.createElement("div");
    formRow.className = "d-flex flex-wrap align-items-end gap-3";

    formRow.innerHTML = `
<div class="d-flex flex-wrap align-items-start gap-2 w-100">

    <!-- Sol grup -->
    <div class="d-flex flex-wrap gap-2">
        <div style="width: 160px;">
            <label class="form-label fw-semibold mb-1">Semptom Adı</label>
            <input name="Symptoms[${index}].Name" class="form-control form-control-sm" />
        </div>

        <div style="width: 100px;">
            <label class="form-label fw-semibold mb-1">Fiyat</label>
            <input name="Symptoms[${index}].EstimatedCost" class="form-control form-control-sm" type="number" step="100" />
        </div>

        <div style="width: 70px;">
            <label class="form-label fw-semibold mb-1">Gün</label>
            <input name="Symptoms[${index}].EstimatedDaysToFix" class="form-control form-control-sm" type="number" />
        </div>

        <div style="width: 250px;">
            <label class="form-label fw-semibold mb-1">Açıklama</label>
            <textarea name="Symptoms[${index}].Description" class="form-control form-control-sm" rows="1"></textarea>
        </div>
          <div style="width: 60px; margin-top: 24px;">
        <button type="button" class="btn btn-sm btn-outline-danger">Sil</button>
    </div>
    </div>

</div>
`;



    // Sil butonu için event
    formRow.querySelector("button").addEventListener("click", function () {
        card.remove();
    });

    card.appendChild(formRow);
    container.appendChild(card);

    symptomIndices[containerId]++;
}
