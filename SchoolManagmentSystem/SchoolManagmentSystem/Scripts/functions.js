function showPassword() {
    document.getElementById("passwordBox").type = (document.getElementById("passwordCheckBox").value = (document.getElementById("passwordCheckBox").value == 1) ? '-1' : '1') == '1' ? 'text' : 'password';
}