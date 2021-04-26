$(function () {
    function display(bool) {
        if (bool) {
            $("#container").show();
        } else {
            $("#container").hide();
        }
    }

    display(false)

    window.addEventListener('message', function(event) {
        var item = event.data;
        //display(true)
        if (item.type === "ui") {
            document.getElementById('aAAAA').innerHTML = 'ui true';
            if (item.status == true) {
                document.getElementById('aAAAA').innerHTML = 'status true';
                display(true)
            } else {
                display(false)
            }
        }
    })
    // if the person uses the escape key, it will exit the resource
    document.onkeyup = function (data) {
        if (data.which == 27) {
            console.log("esc triggered")
            $.post('http://fuels/exit', JSON.stringify({exit: "Success"}));
            return
        }


    };


    $("#close").click(function () {
        console.log("close triggered")
        $.post('http://fuels/exit', JSON.stringify({exit: "Success"}));
        return
    });

    //when the user clicks on the submit button, it will run
    $("#submit").click(function () {
        let inputValue = $("#input").val()
        if (inputValue.length >= 100) {
            console.log("submit triggered")
            $.post("http://fuels/error", JSON.stringify({
                error: "Input was greater than 100"
            }));
            return
        } else if (!inputValue) {
            console.log("submit triggered")
            $.post("http://fuels/error", JSON.stringify({
                error: "There was no value in the input field"
            }));
            return
        }
        // if there are no errors from above, we can send the data back to the original callback and hanndle it from there
        $.post('http://fuels/carchoice', JSON.stringify({
            text: inputValue,
        }));
        console.log("value sent back triggered")
        return;
    });
})