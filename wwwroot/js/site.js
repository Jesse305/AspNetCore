// Write your JavaScript code.
$(document).ready(function(){

    $('.delete').click(function(){
        var href = $(this).attr('data-href');
        swal({
            title: 'Tem Certeza?',
            text: "Quer realmente apagar o registro?",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Sim, apagar!',
            cancelButtonText: 'Não, cancelar'
          }).then((result) => {
            if (result.value) {
              location.href = href;
            // result.dismiss can be 'cancel', 'overlay',
            // 'close', and 'timer'
            } else if (result.dismiss === 'cancel') {
              alertaToast('info', 'Operação Cancelada!');
            }
        });
    });

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