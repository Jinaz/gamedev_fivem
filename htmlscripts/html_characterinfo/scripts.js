
var dragcount = 1;
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
    if (document.getElementById(ev.target.id) != null)
    {
        if (document.getElementById(ev.target.id).id.includes("inv")) {
            console.log("incldes inv");
            var outercontainerID = ev.dataTransfer.getData("parent");
            var outercontainer = document.getElementById(outercontainerID);
            document.getElementById(ev.target.id).appendChild(outercontainer);
    
        }
        else if (document.getElementById(ev.target.id).id.includes("ground")){
            var outercontainerID = ev.dataTransfer.getData("parent");
            var outercontainer = document.getElementById(outercontainerID);
            

            document.getElementById(ev.target.id).appendChild(outercontainer);
        }
        else if (document.getElementById(ev.target.id).id.includes("consume")){
            var outercontainerID = ev.dataTransfer.getData("parent");
            var outercontainer = document.getElementById(outercontainerID);
            

            var outercontainerchildren = outercontainer.childNodes;
            for (i=0;i<outercontainerchildren.length;i++){
                outercontainerchildren[i].remove();
            }
            outercontainer.remove()
            dragcount--;

            document.getElementById(ev.target.id).appendChild(outercontainer);
        }
    }



    //from ground to inv
    if (ev.dataTransfer.getData("parent").includes("ground")) {
        if (document.getElementById(ev.target.id).id.includes("inv")) {
            ev.target.appendChild(document.getElementById(data));
        }
    }

}

function clickButtonDebug() {
    var image = document.createElement("IMG");
    image.setAttribute("id", "drag"+dragcount);
    image.setAttribute("src", "A.jpg");
    image.setAttribute("draggable", "true");
    image.setAttribute("ondragstart", "drag(event)");
    image.setAttribute("width", "150");
    image.setAttribute("height", "150");
    image.setAttribute("itemname", "SM_Water");
    console.log(image.getAttribute("id"));
    
    var imagetext = document.createElement("DIV");
    imagetext.setAttribute("id", "imgText"+dragcount);
    imagetext.setAttribute("class","top-right");

    var outercontainer = document.createElement("DIV");
    outercontainer.setAttribute("id", "imgcontainer"+dragcount);
    outercontainer.setAttribute("class","container2");
    outercontainer.appendChild(image);
    outercontainer.appendChild(imagetext);

    var element = document.getElementById("inv2");
    element.appendChild(outercontainer);
    console.log("done");

    dragcount++;
}

function printSth() {
    console.log("AAAA");
}