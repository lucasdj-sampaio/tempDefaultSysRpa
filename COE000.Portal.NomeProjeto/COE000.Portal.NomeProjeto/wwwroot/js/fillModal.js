function fillRpaCredentialModal(divId, modalId) {
    const mainElement = document.getElementById(divId);

    const idModal = document.getElementById('ModalUserId');
    const modalName = document.getElementById('ModalUserName');
    const modalUrl = document.getElementById('ModalUserUrl');

    idModal.setAttribute('value', '');
    idModal.setAttribute('value', divId);
    
    modalName.setAttribute('value', '');
    modalName.setAttribute('value', mainElement.querySelector('#UserName').textContent);

    document.getElementById('ModalPassword').setAttribute('value', '')

    modalUrl.setAttribute('value', '');
    modalUrl.setAttribute('value', mainElement.querySelector('#Url').textContent);

    openModal(modalId);
};