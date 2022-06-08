function openModal(modal) {
    $(modal).modal('show');
};

function closeModal(modal) {
    $(modal).modal('toggle');
}

function closeAndOpenNewModal(toClose, toOpen) {
    closeModal(toClose);
    openModal(toOpen);
}