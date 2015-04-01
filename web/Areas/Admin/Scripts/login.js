$(document).ready(function () {
    
});

function refreshCode() {
    $("#codeImg").attr("src", "GetValidateCode?time=" + (new Date()).getTime());
}