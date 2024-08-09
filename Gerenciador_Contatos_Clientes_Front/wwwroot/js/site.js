$(document).ready(function () {
    applyPhoneMask();
    applyCNPJMask();
    setMaxDateToToday();
});

function applyPhoneMask() {
    $(".mask-telephone").inputmask({
        mask: "(99) 99999-9999",
        placeholder: "_",
        showMaskOnHover: false,
        showMaskOnFocus: true
    });
}

function applyCNPJMask() {
    $(".mask-cnpj").inputmask({
        mask: "99.999.999/9999-99",
        placeholder: "_",
        showMaskOnHover: false,
        showMaskOnFocus: true
    });
}

function applyInputMask(elementId, maskPattern) {
    $("#" + elementId).inputmask(maskPattern);
}

function getUnmaskedValue(elementId) {
    return $("#" + elementId).inputmask('unmaskedvalue');
}

function setMaxDateToToday() {
    const today = new Date().toISOString().split('T')[0];
    document.getElementsByClassName('date-max-today').max = today;
}

function confirmDelete(message) {
    return confirm(message);
}

function goBack() {
    window.history.back();
}