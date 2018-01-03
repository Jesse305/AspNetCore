// Write your JavaScript code.
$(document).ready(function(){

});

function alertaToast(tipo, msg){
    $.toast({
        icon: tipo, 
        text : msg,
        loader: false,            
        position : 'top-right',
        showHideTransition: 'slide',
        hideAfter: 4000       
    })
}