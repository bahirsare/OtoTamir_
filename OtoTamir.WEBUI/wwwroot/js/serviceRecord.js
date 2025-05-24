const symptomIndices = {}; // containerId => current index

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

    const html = `
        <div class="symptom-group border rounded p-3 mb-3">
            <div class="mb-2">
                <label class="form-label">Semptom Adı</label>
                <input name="Symptoms[${index}].Name" class="form-control" />
            </div>
            <div class="mb-2">
                <label class="form-label">Açıklama</label>
                <textarea name="Symptoms[${index}].Description" class="form-control"></textarea>
            </div>
            <div class="mb-2">
                <label class="form-label">Tahmini Fiyat</label>
                <input name="Symptoms[${index}].EstimatedCost" class="form-control" type="number" step="0.01" />
            </div>
            <div class="mb-2">
                <label class="form-label">Olası Çözüm</label>
                <input name="Symptoms[${index}].PossibleSolution" class="form-control" />
            </div>
            <div class="mb-2">
                <label class="form-label">Gün Sayısı</label>
                <input name="Symptoms[${index}].EstimatedDaysToFix" class="form-control" type="number" />
            </div>
        </div>
    `;

    container.insertAdjacentHTML("beforeend", html);
    symptomIndices[containerId]++;
}
