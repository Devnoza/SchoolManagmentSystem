function showPassword() {
    document.passwordDiv.passwordBox.type = (document.passwordCheckBox.value = (document.passwordCheckBox.value == 1) ? '-1' : '1') == '1' ? 'text' : 'password';
}