$.noConflict();
jQuery(document).ready(function ( $ ) {

    function display(bool) {
        if (bool) {
            $("#container").show();
        } else {
            $("#container").hide();
        }
    }

    display(false)

    window.addEventListener('message',function(event){
        var item = event.data;

        if (item.visible === true){
            display(true);
        var currentfuel = parseFloat(item.crfuel);
        

        //console.log(currentfuel);
        var element = document.getElementById("myprogressBar");


        var width =Math.round( 100*currentfuel);
        element.style.width = width + '%'; 
        element.innerHTML = width * 1  + '%';
        }else{
            display(false);
        }

    })


})

