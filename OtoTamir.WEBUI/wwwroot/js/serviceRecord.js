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
    card.className = "border rounded p-3 mb-1 /*col-lg-7*/ bg-light symptom-item";

    const formRow = document.createElement("div");
    formRow.className = "d-flex flex-wrap align-items-end gap-3";

    formRow.innerHTML = `
        <div>
            <label class="form-label fw-semibold  mb-1">Semptom Adı</label>
            <input name="Symptoms[${index}].Name" class="form-control form-control-sm" class="col-lg-5" />
        </div>

        <div>
            <label class="form-label fw-semibold  mb-1">Fiyat</label>
            <input name="Symptoms[${index}].EstimatedCost" class="form-control form-control-sm" type="number" step="100" style="width: 100px;" />
        </div>

        <div>
            <label class="form-label fw-semibold  mb-1">Gün</label>
            <input name="Symptoms[${index}].EstimatedDaysToFix" class="form-control form-control-sm" type="number" style="width: 70px;" />
        </div>

        <div>
            <label class="form-label fw-semibold  mb-1">Açıklama</label>
            <textarea name="Symptoms[${index}].Description" class="form-control form-control-sm" rows="1" style="width: 250px;"></textarea>
        </div>

        <div>
            <label class="form-label fw-semibold  mb-1">Çözüm</label>
            <input name="Symptoms[${index}].PossibleSolution" class="form-control form-control-sm" style="width: 200px;" />
        </div>

        <div>
            <button type="button" class="btn btn-sm btn-outline-danger" style="margin-top: 22px;">Sil</button>
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
