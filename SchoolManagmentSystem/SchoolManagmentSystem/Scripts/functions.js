function showPassword() {
    document.passwordDiv.passwordBox.type = (document.passwordDiv.passwordCheckBox.value = (document.passwordDiv.passwordCheckBox.value == 1) ? '-1' : '1') == '1' ? 'text' : 'password';
}