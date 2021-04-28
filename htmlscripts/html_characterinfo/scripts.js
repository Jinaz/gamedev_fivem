
function allowDrop(ev) {
    ev.preventDefault();
}

function drag(ev) {
    ev.dataTransfer.setData("text", ev.target.id);
    ev.dataTransfer.setData("parent", document.getElementById(ev.target.id).parentElement.id);
    ev.dataTransfer.setData("slotnumber", document.getElementById(ev.target.id).parentElement.slotnum);
    ev.dataTransfer.setData("itemname", document.getElementById(ev.target.id).itemname);

    console.log(ev.target.itemname);
}
/* Generate such a thing if we change any position
<div class="container2">
<img id="drag2" src="A.jpg" draggable="true" ondragstart="drag(event)" width="150" height="150">
<div class="top-right">Top Right</div>
</div>
*/


function drop(ev) {
    ev.preventDefault();
    var data = ev.dataTransfer.getData("text");



    console.log(document.getElementById(ev.target.id));
    //allow from inv to ground & inv to inv
    if (ev.dataTransfer.getData("parent").includes("inv")) {
        if (document.getElementById(ev.target.id).id != null) {

            if (document.getElementById(ev.target.id).id.includes("inv")) {
                console.log(document.getElementById(ev.target.id));
                document.getElementById(ev.target.id).appendChild(document.getElementById(data));
            }
            else if (document.getElementById(ev.target.id).id.includes("ground")) {
                ev.target.appendChild(document.getElementById(data));
            } else if (document.getElementById(ev.target.id).id.includes("consume")) {

                var consumedItem = ev.dataTransfer.getData("itemname");
                var slotnumberItem = ev.dataTransfer.getData("slotnumber");
                console.log("try delete");
                console.log(document.getElementById(data));
                document.getElementById(data).remove();

                console.log(consumedItem);
                console.log(slotnumberItem);


            }

        }
    }
    //from ground to inv
    if (ev.dataTransfer.getData("parent").includes("ground")) {
        if (document.getElementById(ev.target.id).id.includes("inv")) {
            ev.target.appendChild(document.getElementById(data));
        }
    }

}

function generateID(){

}

function clickButtonDebug() {
    var tag = document.createElement("IMG");
    tag.setAttribute("id", "drag6");
    tag.setAttribute("src", "A.jpg");
    tag.setAttribute("draggable", "true");
    tag.setAttribute("ondragstart", "drag(event)");
    tag.setAttribute("width", "150");
    tag.setAttribute("height", "150");
    tag.setAttribute("itemname", "SM_Water");
    console.log(tag.getAttribute("id"));
    var element = document.getElementById("ground1");
    element.appendChild(tag);
    console.log("done");

}

function printSth() {
    console.log("AAAA");
}