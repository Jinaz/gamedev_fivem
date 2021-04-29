$(function () {
    function display(bool) {
        if (bool) {
            $("#container").show();
        } else {
            $("#container").hide();
        }
    }

    //display(false)


    window.addEventListener('message', function (event) {
        var item = event.data;
        //display(true)
        if (item.toggle === true) {
            display(true);
        } else {
            display(false);
        }

    })
    // if the person uses the escape key, it will exit the resource
    document.onkeyup = function (data) {
        if (data.which == 27) {
            console.log("esc triggered")
            $.post('http://characterinfo/exit', JSON.stringify({ exit: "Success" }));
            return
        }

        if (data.which == 13) {
            data.preventDefault();
        }
    };
    $(".items").draggable();
    $(".div1").droppable({
        drop: function (event, ui) {
            //$(this).css('background', 'rgb(0,200,0)');


            console.log(event);
            var id = event.target.id;
            var slot = document.getElementById(id);
            console.log(slot.childNodes);

            if (slot.childNodes.length < 2) {
                var draggedElement = document.getElementById(event.toElement.id);
                console.log(slot);
                console.log(draggedElement);

                var parent = draggedElement.parentElement;
                console.log(parent);

                slot.appendChild(parent);
                draggedElement.style = "position: relative; width: 150px; height: 150px";
                //draggedElement.droppable();
            }
        }
        

    });
    $(".div2").droppable({
        drop: function (event, ui) {
            //$(this).css('background', 'rgb(0,200,0)');
            console.log(event);
            var id = event.target.id;
            var slot = document.getElementById(id);

            var draggedElement = document.getElementById(event.toElement.id);
            console.log(slot);
            console.log(draggedElement);

            var parent = draggedElement.parentElement;
            console.log(parent);

            slot.appendChild(parent);
            draggedElement.style = "position: relative; width: 150px; height: 150px";
            //draggedElement.droppable();

        }
        //functions might come in useful one day
        /*,
        over: function (event, ui) {
            //$(this).css('background', 'orange');
        },
        out: function (event, ui) {
            //$(this).css('background', '#000000');
        }*/

    });
    var dragcount = 1;

    $("#addObj").click(function () {
        //this will not be used in fivem code
        //just some functionality testing

        var image = document.createElement("IMG");
        image.setAttribute("id", "drag" + dragcount);
        image.setAttribute("src", "A.jpg");
        image.setAttribute("draggable", "true");
        image.setAttribute("width", "150");
        image.setAttribute("height", "150");
        image.setAttribute("class", "items");
        console.log(image.getAttribute("id"));

        var imagetext = document.createElement("DIV");
        imagetext.setAttribute("id", "imgText" + dragcount);
        imagetext.setAttribute("class", "top-right");
        imagetext.innerHTML = "Count"

        var outercontainer = document.createElement("DIV");
        outercontainer.setAttribute("id", "imgcontainer" + dragcount);
        outercontainer.setAttribute("class", "container2");
        outercontainer.appendChild(image);
        outercontainer.appendChild(imagetext);

        var element = document.getElementById("inv2");
        element.appendChild(outercontainer);
        console.log(outercontainer);
        dragcount++;
        console.log("AAA");
        $(".items").draggable();
    })
    $("#consume").droppable({
        drop: function (event, ui) {
            var draggedElement = document.getElementById(event.toElement.id);
            var parent = draggedElement.parentElement;

            for (i = 0; i < parent.childNodes.length; i++) {
                parent.childNodes[i].remove();
            }
            parent.remove();

            console.log("deleted an item");
            dragcount--;
            //send message to delete an item
        }
    });


    $("#printsth").click(function () {
        console.log("close triggered");
        $.post('http://characterinfo/exit', JSON.stringify({ exit: "Success" }));
        return
    })

    //when the user clicks on the submit button, it will run
    $("#submit").click(function () {
        let inputValue = $("#input").val();
        if (inputValue.length >= 100) {
            console.log("submit triggered")
            $.post("http://characterinfo/error", JSON.stringify({
                error: "Input was greater than 100"
            }));
            return
        } else if (!inputValue) {
            console.log("submit triggered")
            $.post("http://characterinfo/error", JSON.stringify({
                error: "There was no value in the input field"
            }));
            return
        }
        // if there are no errors from above, we can send the data back to the original callback and hanndle it from there
        $.post('http://characterinfo/carchoice', JSON.stringify({
            text: inputValue,
        }));
        console.log("value sent back triggered")
        return;
    });


})