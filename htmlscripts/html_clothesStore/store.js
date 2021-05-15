$.noConflict();
jQuery(document).ready(function ($) {

    var currentCharacter = JSON.parse('{\
        "head":0,\
        "masks":0, \
        "hair":0, \
        "torso":0, \
        "legs":0, \
        "bags":0, \
        "shoes":0, \
        "accessories":0, \
        "undershirts":0, \
        "bodyArmor":0, \
        "decals":0, \
        "tops":0, \
        "head_variation":0,\
        "masks_variation":0, \
        "hair_variation":0, \
        "torso_variation":0, \
        "legs_variation":0, \
        "bags_variation":0, \
        "shoes_variation":0, \
        "accessories_variation":0, \
        "undershirts_variation":0, \
        "bodyArmor_variation":0, \
        "decals_variation":0, \
        "tops_variation":0 \
    }');
    var currentGender = "f";
    var maxClothesvalues = JSON.parse('{ \
        "head":{"m" : 7, "f":9}, \
        "masks":{"m" : 189, "f":189}, \
        "hair":{"m" : 74, "f":76}, \
        "torso":{"m" : 194, "f":239}, \
        "legs":{"m" : 132, "f":139}, \
        "bags":{"m" : 88, "f":88}, \
        "shoes":{"m" : 97, "f":101}, \
        "accessories":{"m" : 150, "f":119}, \
        "undershirts":{"m" : 177, "f":215}, \
        "bodyArmor":{"m" : 55, "f":55}, \
        "decals":{"m" : 79, "f":88}, \
        "tops":{"m" : 361, "f":380} \
        }');
    /*standard json hosting way:
    $.getJSON("./clothesMaxValues.json", function (json) {
        maxClothesvalues = json;
        console.log(json); // this will show the info it in firebug console
    });*/


    function display(bool) {
        if (bool) {
            $("#container").show();
        } else {
            $("#container").hide();
        }
    }

    function displayCheckout(bool) {
        if (bool) {
            $("#container2").show();
        } else {
            $("#container2").hide();
        }
    }

    //display(false)
    //displayCheckout(false)

    //$("#etc").hide();
    //$("#FaceCreation").hide();

    window.addEventListener('message', function (event) {
        var item = event.data;
        //display(true)
        if (item.toggle === true) {
            //set current values
            //get max values
            
            if (item.characreation === true){
                $("#etc").show();
                $("#FaceCreation").show();
            }else{
                $("#etc").hide();
                $("#FaceCreation").hide();
            }
            currentCharacter = JSON.parse(item);

            display(true);
        } else {
            display(false);
        }

    })

    $("#FaceCreation").click(function (evts) {
        var i, tabcontent, tablinks;
        tabcontent = document.getElementsByClassName("tabcontent");
        for (i = 0; i < tabcontent.length; i++) {
            tabcontent[i].style.display = "none";
        }
        tablinks = document.getElementsByClassName("tablinks");
        for (i = 0; i < tablinks.length; i++) {
            tablinks[i].className = tablinks[i].className.replace(" active", "");
        }
        //console.log(evts.currentTarget.id+ "TAB");
        document.getElementById(evts.currentTarget.id + "TAB").style.display = "block";
        document.getElementById(evts.currentTarget.id).className += " active";
    })

    $("#Clothes").click(function (evts) {
        var i, tabcontent, tablinks;
        tabcontent = document.getElementsByClassName("tabcontent");
        for (i = 0; i < tabcontent.length; i++) {
            tabcontent[i].style.display = "none";
        }
        tablinks = document.getElementsByClassName("tablinks");
        for (i = 0; i < tablinks.length; i++) {
            tablinks[i].className = tablinks[i].className.replace(" active", "");
        }
        //console.log(evts.currentTarget.id + "TAB");
        document.getElementById(evts.currentTarget.id + "TAB").style.display = "block";
        document.getElementById(evts.currentTarget.id).className += " active";
    })

    $("#Specials").click(function (evts) {
        var i, tabcontent, tablinks;
        tabcontent = document.getElementsByClassName("tabcontent");
        for (i = 0; i < tabcontent.length; i++) {
            tabcontent[i].style.display = "none";
        }
        tablinks = document.getElementsByClassName("tablinks");
        for (i = 0; i < tablinks.length; i++) {
            tablinks[i].className = tablinks[i].className.replace(" active", "");
        }
        //console.log(evts.currentTarget.id + "TAB");
        document.getElementById(evts.currentTarget.id + "TAB").style.display = "block";
        document.getElementById(evts.currentTarget.id).className += " active";
    })

    $("#etc").click(function (evts) {
        var i, tabcontent, tablinks;
        tabcontent = document.getElementsByClassName("tabcontent");
        for (i = 0; i < tabcontent.length; i++) {
            tabcontent[i].style.display = "none";
        }
        tablinks = document.getElementsByClassName("tablinks");
        for (i = 0; i < tablinks.length; i++) {
            tablinks[i].className = tablinks[i].className.replace(" active", "");
        }
        //console.log(evts.currentTarget.id + "TAB");
        document.getElementById(evts.currentTarget.id + "TAB").style.display = "block";
        document.getElementById(evts.currentTarget.id).className += " active";
    })

    $("#viewFace").click(function (evts) {
        console.log("sending to NUI to change view");
    })

    $("#viewBody").click(function (evts) {
        console.log("sending to NUI to change view");
    })

    function sendCharaChanged() {
        console.log("sending chara change");
    }
    /*
    * Generated code segment for changing value
    */

    $("#head_right").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == "f") {
                if (parseInt(label.value) + 1 > maxClothesvalues.head.f) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            } else if (currentGender == "m") {
                if (parseInt(label.value) + 1 > maxClothesvalues.head.m) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == "f") {

                if (parseInt(label.value) - 1 < 0) {
                    label.value = maxClothesvalues.head.f;
                }
                else {
                    label.value = parseInt(label.value) - 1;
                }
            }
            else if (currentGender == "m") {

                if (parseInt(label.value) - 1 < 0) {
                    label.value = maxClothesvalues.head.m;
                }
                else {
                    label.value = parseInt(label.value) - 1;
                }
            }

        }
        document.getElementById(splittedstring[0] + "variationid").value = 0;
        currentCharacter.head = parseInt(label.value);
        sendCharaChanged();
    })

    $("#head_left").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == "f") {
                if (parseInt(label.value) + 1 > maxClothesvalues.head.f)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == "m") {
                if (parseInt(label.value) + 1 > maxClothesvalues.head.m)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == "f") {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesvalues.head.f;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == "m") {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesvalues.head.m;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        document.getElementById(splittedstring[0] + "variationid").value = 0;
        currentCharacter.head = parseInt(label.value);
        sendCharaChanged();
    })

    $("#headvariation_right").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            label.value = parseInt(label.value) + 1;
        } else if (splittedstring[1] == "left") {
            if (parseInt(label.value) - 1 < 0)
                label.value = 0;
            else
                label.value = parseInt(label.value) - 1;
        }
        currentCharacter.head = parseInt(label.value);
        sendCharaChanged();
    })

    $("#headvariation_left").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            label.value = parseInt(label.value) + 1;
        } else if (splittedstring[1] == "left") {
            if (parseInt(label.value) - 1 < 0)
                label.value = 0;
            else
                label.value = parseInt(label.value) - 1;
        }
        currentCharacter.head = parseInt(label.value);
        sendCharaChanged();
    })

    $("#headid").on('input', function (evts) {
        currentCharacter.head = parseInt(document.getElementById(evts.currentTarget.id).value);
        sendCharaChanged();
    })

    $("#headvariationid").on('input', function (evts) {
        currentCharacter.head_variation = parseInt(document.getElementById(evts.currentTarget.id).value);
        sendCharaChanged();
    })

$("#masks_right").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == "f") {
                if (parseInt(label.value) + 1 > maxClothesvalues.masks.f) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            } else if (currentGender == "m") {
                if (parseInt(label.value) + 1 > maxClothesvalues.masks.m) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == "f") {

                if (parseInt(label.value) - 1 < 0) {
                    label.value = maxClothesvalues.masks.f;
                }
                else {
                    label.value = parseInt(label.value) - 1;
                }
            }
            else if (currentGender == "m") {

                if (parseInt(label.value) - 1 < 0) {
                    label.value = maxClothesvalues.masks.m;
                }
                else {
                    label.value = parseInt(label.value) - 1;
                }
            }

        }
        document.getElementById(splittedstring[0] + "variationid").value = 0;
        currentCharacter.masks = parseInt(label.value);
        sendCharaChanged();
    })

    $("#masks_left").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == "f") {
                if (parseInt(label.value) + 1 > maxClothesvalues.masks.f)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == "m") {
                if (parseInt(label.value) + 1 > maxClothesvalues.masks.m)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == "f") {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesvalues.masks.f;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == "m") {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesvalues.masks.m;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        document.getElementById(splittedstring[0] + "variationid").value = 0;
        currentCharacter.masks = parseInt(label.value);
        sendCharaChanged();
    })

    $("#masksvariation_right").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            label.value = parseInt(label.value) + 1;
        } else if (splittedstring[1] == "left") {
            if (parseInt(label.value) - 1 < 0)
                label.value = 0;
            else
                label.value = parseInt(label.value) - 1;
        }
        currentCharacter.masks = parseInt(label.value);
        sendCharaChanged();
    })

    $("#masksvariation_left").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            label.value = parseInt(label.value) + 1;
        } else if (splittedstring[1] == "left") {
            if (parseInt(label.value) - 1 < 0)
                label.value = 0;
            else
                label.value = parseInt(label.value) - 1;
        }
        currentCharacter.masks = parseInt(label.value);
        sendCharaChanged();
    })

    $("#masksid").on('input', function (evts) {
        currentCharacter.masks = parseInt(document.getElementById(evts.currentTarget.id).value);
        sendCharaChanged();
    })

    $("#masksvariationid").on('input', function (evts) {
        currentCharacter.masks_variation = parseInt(document.getElementById(evts.currentTarget.id).value);
        sendCharaChanged();
    })

$("#hair_right").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == "f") {
                if (parseInt(label.value) + 1 > maxClothesvalues.hair.f) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            } else if (currentGender == "m") {
                if (parseInt(label.value) + 1 > maxClothesvalues.hair.m) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == "f") {

                if (parseInt(label.value) - 1 < 0) {
                    label.value = maxClothesvalues.hair.f;
                }
                else {
                    label.value = parseInt(label.value) - 1;
                }
            }
            else if (currentGender == "m") {

                if (parseInt(label.value) - 1 < 0) {
                    label.value = maxClothesvalues.hair.m;
                }
                else {
                    label.value = parseInt(label.value) - 1;
                }
            }

        }
        document.getElementById(splittedstring[0] + "variationid").value = 0;
        currentCharacter.hair = parseInt(label.value);
        sendCharaChanged();
    })

    $("#hair_left").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == "f") {
                if (parseInt(label.value) + 1 > maxClothesvalues.hair.f)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == "m") {
                if (parseInt(label.value) + 1 > maxClothesvalues.hair.m)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == "f") {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesvalues.hair.f;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == "m") {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesvalues.hair.m;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        document.getElementById(splittedstring[0] + "variationid").value = 0;
        currentCharacter.hair = parseInt(label.value);
        sendCharaChanged();
    })

    $("#hairvariation_right").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            label.value = parseInt(label.value) + 1;
        } else if (splittedstring[1] == "left") {
            if (parseInt(label.value) - 1 < 0)
                label.value = 0;
            else
                label.value = parseInt(label.value) - 1;
        }
        currentCharacter.hair = parseInt(label.value);
        sendCharaChanged();
    })

    $("#hairvariation_left").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            label.value = parseInt(label.value) + 1;
        } else if (splittedstring[1] == "left") {
            if (parseInt(label.value) - 1 < 0)
                label.value = 0;
            else
                label.value = parseInt(label.value) - 1;
        }
        currentCharacter.hair = parseInt(label.value);
        sendCharaChanged();
    })

    $("#hairid").on('input', function (evts) {
        currentCharacter.hair = parseInt(document.getElementById(evts.currentTarget.id).value);
        sendCharaChanged();
    })

    $("#hairvariationid").on('input', function (evts) {
        currentCharacter.hair_variation = parseInt(document.getElementById(evts.currentTarget.id).value);
        sendCharaChanged();
    })

$("#torso_right").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == "f") {
                if (parseInt(label.value) + 1 > maxClothesvalues.torso.f) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            } else if (currentGender == "m") {
                if (parseInt(label.value) + 1 > maxClothesvalues.torso.m) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == "f") {

                if (parseInt(label.value) - 1 < 0) {
                    label.value = maxClothesvalues.torso.f;
                }
                else {
                    label.value = parseInt(label.value) - 1;
                }
            }
            else if (currentGender == "m") {

                if (parseInt(label.value) - 1 < 0) {
                    label.value = maxClothesvalues.torso.m;
                }
                else {
                    label.value = parseInt(label.value) - 1;
                }
            }

        }
        document.getElementById(splittedstring[0] + "variationid").value = 0;
        currentCharacter.torso = parseInt(label.value);
        sendCharaChanged();
    })

    $("#torso_left").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == "f") {
                if (parseInt(label.value) + 1 > maxClothesvalues.torso.f)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == "m") {
                if (parseInt(label.value) + 1 > maxClothesvalues.torso.m)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == "f") {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesvalues.torso.f;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == "m") {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesvalues.torso.m;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        document.getElementById(splittedstring[0] + "variationid").value = 0;
        currentCharacter.torso = parseInt(label.value);
        sendCharaChanged();
    })

    $("#torsovariation_right").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            label.value = parseInt(label.value) + 1;
        } else if (splittedstring[1] == "left") {
            if (parseInt(label.value) - 1 < 0)
                label.value = 0;
            else
                label.value = parseInt(label.value) - 1;
        }
        currentCharacter.torso = parseInt(label.value);
        sendCharaChanged();
    })

    $("#torsovariation_left").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            label.value = parseInt(label.value) + 1;
        } else if (splittedstring[1] == "left") {
            if (parseInt(label.value) - 1 < 0)
                label.value = 0;
            else
                label.value = parseInt(label.value) - 1;
        }
        currentCharacter.torso = parseInt(label.value);
        sendCharaChanged();
    })

    $("#torsoid").on('input', function (evts) {
        currentCharacter.torso = parseInt(document.getElementById(evts.currentTarget.id).value);
        sendCharaChanged();
    })

    $("#torsovariationid").on('input', function (evts) {
        currentCharacter.torso_variation = parseInt(document.getElementById(evts.currentTarget.id).value);
        sendCharaChanged();
    })

$("#legs_right").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == "f") {
                if (parseInt(label.value) + 1 > maxClothesvalues.legs.f) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            } else if (currentGender == "m") {
                if (parseInt(label.value) + 1 > maxClothesvalues.legs.m) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == "f") {

                if (parseInt(label.value) - 1 < 0) {
                    label.value = maxClothesvalues.legs.f;
                }
                else {
                    label.value = parseInt(label.value) - 1;
                }
            }
            else if (currentGender == "m") {

                if (parseInt(label.value) - 1 < 0) {
                    label.value = maxClothesvalues.legs.m;
                }
                else {
                    label.value = parseInt(label.value) - 1;
                }
            }

        }
        document.getElementById(splittedstring[0] + "variationid").value = 0;
        currentCharacter.legs = parseInt(label.value);
        sendCharaChanged();
    })

    $("#legs_left").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == "f") {
                if (parseInt(label.value) + 1 > maxClothesvalues.legs.f)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == "m") {
                if (parseInt(label.value) + 1 > maxClothesvalues.legs.m)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == "f") {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesvalues.legs.f;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == "m") {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesvalues.legs.m;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        document.getElementById(splittedstring[0] + "variationid").value = 0;
        currentCharacter.legs = parseInt(label.value);
        sendCharaChanged();
    })

    $("#legsvariation_right").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            label.value = parseInt(label.value) + 1;
        } else if (splittedstring[1] == "left") {
            if (parseInt(label.value) - 1 < 0)
                label.value = 0;
            else
                label.value = parseInt(label.value) - 1;
        }
        currentCharacter.legs = parseInt(label.value);
        sendCharaChanged();
    })

    $("#legsvariation_left").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            label.value = parseInt(label.value) + 1;
        } else if (splittedstring[1] == "left") {
            if (parseInt(label.value) - 1 < 0)
                label.value = 0;
            else
                label.value = parseInt(label.value) - 1;
        }
        currentCharacter.legs = parseInt(label.value);
        sendCharaChanged();
    })

    $("#legsid").on('input', function (evts) {
        currentCharacter.legs = parseInt(document.getElementById(evts.currentTarget.id).value);
        sendCharaChanged();
    })

    $("#legsvariationid").on('input', function (evts) {
        currentCharacter.legs_variation = parseInt(document.getElementById(evts.currentTarget.id).value);
        sendCharaChanged();
    })

$("#bags_right").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == "f") {
                if (parseInt(label.value) + 1 > maxClothesvalues.bags.f) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            } else if (currentGender == "m") {
                if (parseInt(label.value) + 1 > maxClothesvalues.bags.m) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == "f") {

                if (parseInt(label.value) - 1 < 0) {
                    label.value = maxClothesvalues.bags.f;
                }
                else {
                    label.value = parseInt(label.value) - 1;
                }
            }
            else if (currentGender == "m") {

                if (parseInt(label.value) - 1 < 0) {
                    label.value = maxClothesvalues.bags.m;
                }
                else {
                    label.value = parseInt(label.value) - 1;
                }
            }

        }
        document.getElementById(splittedstring[0] + "variationid").value = 0;
        currentCharacter.bags = parseInt(label.value);
        sendCharaChanged();
    })

    $("#bags_left").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == "f") {
                if (parseInt(label.value) + 1 > maxClothesvalues.bags.f)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == "m") {
                if (parseInt(label.value) + 1 > maxClothesvalues.bags.m)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == "f") {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesvalues.bags.f;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == "m") {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesvalues.bags.m;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        document.getElementById(splittedstring[0] + "variationid").value = 0;
        currentCharacter.bags = parseInt(label.value);
        sendCharaChanged();
    })

    $("#bagsvariation_right").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            label.value = parseInt(label.value) + 1;
        } else if (splittedstring[1] == "left") {
            if (parseInt(label.value) - 1 < 0)
                label.value = 0;
            else
                label.value = parseInt(label.value) - 1;
        }
        currentCharacter.bags = parseInt(label.value);
        sendCharaChanged();
    })

    $("#bagsvariation_left").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            label.value = parseInt(label.value) + 1;
        } else if (splittedstring[1] == "left") {
            if (parseInt(label.value) - 1 < 0)
                label.value = 0;
            else
                label.value = parseInt(label.value) - 1;
        }
        currentCharacter.bags = parseInt(label.value);
        sendCharaChanged();
    })

    $("#bagsid").on('input', function (evts) {
        currentCharacter.bags = parseInt(document.getElementById(evts.currentTarget.id).value);
        sendCharaChanged();
    })

    $("#bagsvariationid").on('input', function (evts) {
        currentCharacter.bags_variation = parseInt(document.getElementById(evts.currentTarget.id).value);
        sendCharaChanged();
    })

$("#shoes_right").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == "f") {
                if (parseInt(label.value) + 1 > maxClothesvalues.shoes.f) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            } else if (currentGender == "m") {
                if (parseInt(label.value) + 1 > maxClothesvalues.shoes.m) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == "f") {

                if (parseInt(label.value) - 1 < 0) {
                    label.value = maxClothesvalues.shoes.f;
                }
                else {
                    label.value = parseInt(label.value) - 1;
                }
            }
            else if (currentGender == "m") {

                if (parseInt(label.value) - 1 < 0) {
                    label.value = maxClothesvalues.shoes.m;
                }
                else {
                    label.value = parseInt(label.value) - 1;
                }
            }

        }
        document.getElementById(splittedstring[0] + "variationid").value = 0;
        currentCharacter.shoes = parseInt(label.value);
        sendCharaChanged();
    })

    $("#shoes_left").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == "f") {
                if (parseInt(label.value) + 1 > maxClothesvalues.shoes.f)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == "m") {
                if (parseInt(label.value) + 1 > maxClothesvalues.shoes.m)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == "f") {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesvalues.shoes.f;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == "m") {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesvalues.shoes.m;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        document.getElementById(splittedstring[0] + "variationid").value = 0;
        currentCharacter.shoes = parseInt(label.value);
        sendCharaChanged();
    })

    $("#shoesvariation_right").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            label.value = parseInt(label.value) + 1;
        } else if (splittedstring[1] == "left") {
            if (parseInt(label.value) - 1 < 0)
                label.value = 0;
            else
                label.value = parseInt(label.value) - 1;
        }
        currentCharacter.shoes = parseInt(label.value);
        sendCharaChanged();
    })

    $("#shoesvariation_left").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            label.value = parseInt(label.value) + 1;
        } else if (splittedstring[1] == "left") {
            if (parseInt(label.value) - 1 < 0)
                label.value = 0;
            else
                label.value = parseInt(label.value) - 1;
        }
        currentCharacter.shoes = parseInt(label.value);
        sendCharaChanged();
    })

    $("#shoesid").on('input', function (evts) {
        currentCharacter.shoes = parseInt(document.getElementById(evts.currentTarget.id).value);
        sendCharaChanged();
    })

    $("#shoesvariationid").on('input', function (evts) {
        currentCharacter.shoes_variation = parseInt(document.getElementById(evts.currentTarget.id).value);
        sendCharaChanged();
    })

$("#accessories_right").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == "f") {
                if (parseInt(label.value) + 1 > maxClothesvalues.accessories.f) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            } else if (currentGender == "m") {
                if (parseInt(label.value) + 1 > maxClothesvalues.accessories.m) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == "f") {

                if (parseInt(label.value) - 1 < 0) {
                    label.value = maxClothesvalues.accessories.f;
                }
                else {
                    label.value = parseInt(label.value) - 1;
                }
            }
            else if (currentGender == "m") {

                if (parseInt(label.value) - 1 < 0) {
                    label.value = maxClothesvalues.accessories.m;
                }
                else {
                    label.value = parseInt(label.value) - 1;
                }
            }

        }
        document.getElementById(splittedstring[0] + "variationid").value = 0;
        currentCharacter.accessories = parseInt(label.value);
        sendCharaChanged();
    })

    $("#accessories_left").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == "f") {
                if (parseInt(label.value) + 1 > maxClothesvalues.accessories.f)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == "m") {
                if (parseInt(label.value) + 1 > maxClothesvalues.accessories.m)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == "f") {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesvalues.accessories.f;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == "m") {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesvalues.accessories.m;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        document.getElementById(splittedstring[0] + "variationid").value = 0;
        currentCharacter.accessories = parseInt(label.value);
        sendCharaChanged();
    })

    $("#accessoriesvariation_right").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            label.value = parseInt(label.value) + 1;
        } else if (splittedstring[1] == "left") {
            if (parseInt(label.value) - 1 < 0)
                label.value = 0;
            else
                label.value = parseInt(label.value) - 1;
        }
        currentCharacter.accessories = parseInt(label.value);
        sendCharaChanged();
    })

    $("#accessoriesvariation_left").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            label.value = parseInt(label.value) + 1;
        } else if (splittedstring[1] == "left") {
            if (parseInt(label.value) - 1 < 0)
                label.value = 0;
            else
                label.value = parseInt(label.value) - 1;
        }
        currentCharacter.accessories = parseInt(label.value);
        sendCharaChanged();
    })

    $("#accessoriesid").on('input', function (evts) {
        currentCharacter.accessories = parseInt(document.getElementById(evts.currentTarget.id).value);
        sendCharaChanged();
    })

    $("#accessoriesvariationid").on('input', function (evts) {
        currentCharacter.accessories_variation = parseInt(document.getElementById(evts.currentTarget.id).value);
        sendCharaChanged();
    })

$("#undershirts_right").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == "f") {
                if (parseInt(label.value) + 1 > maxClothesvalues.undershirts.f) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            } else if (currentGender == "m") {
                if (parseInt(label.value) + 1 > maxClothesvalues.undershirts.m) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == "f") {

                if (parseInt(label.value) - 1 < 0) {
                    label.value = maxClothesvalues.undershirts.f;
                }
                else {
                    label.value = parseInt(label.value) - 1;
                }
            }
            else if (currentGender == "m") {

                if (parseInt(label.value) - 1 < 0) {
                    label.value = maxClothesvalues.undershirts.m;
                }
                else {
                    label.value = parseInt(label.value) - 1;
                }
            }

        }
        document.getElementById(splittedstring[0] + "variationid").value = 0;
        currentCharacter.undershirts = parseInt(label.value);
        sendCharaChanged();
    })

    $("#undershirts_left").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == "f") {
                if (parseInt(label.value) + 1 > maxClothesvalues.undershirts.f)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == "m") {
                if (parseInt(label.value) + 1 > maxClothesvalues.undershirts.m)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == "f") {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesvalues.undershirts.f;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == "m") {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesvalues.undershirts.m;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        document.getElementById(splittedstring[0] + "variationid").value = 0;
        currentCharacter.undershirts = parseInt(label.value);
        sendCharaChanged();
    })

    $("#undershirtsvariation_right").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            label.value = parseInt(label.value) + 1;
        } else if (splittedstring[1] == "left") {
            if (parseInt(label.value) - 1 < 0)
                label.value = 0;
            else
                label.value = parseInt(label.value) - 1;
        }
        currentCharacter.undershirts = parseInt(label.value);
        sendCharaChanged();
    })

    $("#undershirtsvariation_left").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            label.value = parseInt(label.value) + 1;
        } else if (splittedstring[1] == "left") {
            if (parseInt(label.value) - 1 < 0)
                label.value = 0;
            else
                label.value = parseInt(label.value) - 1;
        }
        currentCharacter.undershirts = parseInt(label.value);
        sendCharaChanged();
    })

    $("#undershirtsid").on('input', function (evts) {
        currentCharacter.undershirts = parseInt(document.getElementById(evts.currentTarget.id).value);
        sendCharaChanged();
    })

    $("#undershirtsvariationid").on('input', function (evts) {
        currentCharacter.undershirts_variation = parseInt(document.getElementById(evts.currentTarget.id).value);
        sendCharaChanged();
    })

$("#bodyArmor_right").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == "f") {
                if (parseInt(label.value) + 1 > maxClothesvalues.bodyArmor.f) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            } else if (currentGender == "m") {
                if (parseInt(label.value) + 1 > maxClothesvalues.bodyArmor.m) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == "f") {

                if (parseInt(label.value) - 1 < 0) {
                    label.value = maxClothesvalues.bodyArmor.f;
                }
                else {
                    label.value = parseInt(label.value) - 1;
                }
            }
            else if (currentGender == "m") {

                if (parseInt(label.value) - 1 < 0) {
                    label.value = maxClothesvalues.bodyArmor.m;
                }
                else {
                    label.value = parseInt(label.value) - 1;
                }
            }

        }
        document.getElementById(splittedstring[0] + "variationid").value = 0;
        currentCharacter.bodyArmor = parseInt(label.value);
        sendCharaChanged();
    })

    $("#bodyArmor_left").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == "f") {
                if (parseInt(label.value) + 1 > maxClothesvalues.bodyArmor.f)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == "m") {
                if (parseInt(label.value) + 1 > maxClothesvalues.bodyArmor.m)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == "f") {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesvalues.bodyArmor.f;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == "m") {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesvalues.bodyArmor.m;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        document.getElementById(splittedstring[0] + "variationid").value = 0;
        currentCharacter.bodyArmor = parseInt(label.value);
        sendCharaChanged();
    })

    $("#bodyArmorvariation_right").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            label.value = parseInt(label.value) + 1;
        } else if (splittedstring[1] == "left") {
            if (parseInt(label.value) - 1 < 0)
                label.value = 0;
            else
                label.value = parseInt(label.value) - 1;
        }
        currentCharacter.bodyArmor = parseInt(label.value);
        sendCharaChanged();
    })

    $("#bodyArmorvariation_left").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            label.value = parseInt(label.value) + 1;
        } else if (splittedstring[1] == "left") {
            if (parseInt(label.value) - 1 < 0)
                label.value = 0;
            else
                label.value = parseInt(label.value) - 1;
        }
        currentCharacter.bodyArmor = parseInt(label.value);
        sendCharaChanged();
    })

    $("#bodyArmorid").on('input', function (evts) {
        currentCharacter.bodyArmor = parseInt(document.getElementById(evts.currentTarget.id).value);
        sendCharaChanged();
    })

    $("#bodyArmorvariationid").on('input', function (evts) {
        currentCharacter.bodyArmor_variation = parseInt(document.getElementById(evts.currentTarget.id).value);
        sendCharaChanged();
    })

$("#decals_right").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == "f") {
                if (parseInt(label.value) + 1 > maxClothesvalues.decals.f) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            } else if (currentGender == "m") {
                if (parseInt(label.value) + 1 > maxClothesvalues.decals.m) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == "f") {

                if (parseInt(label.value) - 1 < 0) {
                    label.value = maxClothesvalues.decals.f;
                }
                else {
                    label.value = parseInt(label.value) - 1;
                }
            }
            else if (currentGender == "m") {

                if (parseInt(label.value) - 1 < 0) {
                    label.value = maxClothesvalues.decals.m;
                }
                else {
                    label.value = parseInt(label.value) - 1;
                }
            }

        }
        document.getElementById(splittedstring[0] + "variationid").value = 0;
        currentCharacter.decals = parseInt(label.value);
        sendCharaChanged();
    })

    $("#decals_left").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == "f") {
                if (parseInt(label.value) + 1 > maxClothesvalues.decals.f)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == "m") {
                if (parseInt(label.value) + 1 > maxClothesvalues.decals.m)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == "f") {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesvalues.decals.f;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == "m") {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesvalues.decals.m;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        document.getElementById(splittedstring[0] + "variationid").value = 0;
        currentCharacter.decals = parseInt(label.value);
        sendCharaChanged();
    })

    $("#decalsvariation_right").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            label.value = parseInt(label.value) + 1;
        } else if (splittedstring[1] == "left") {
            if (parseInt(label.value) - 1 < 0)
                label.value = 0;
            else
                label.value = parseInt(label.value) - 1;
        }
        currentCharacter.decals = parseInt(label.value);
        sendCharaChanged();
    })

    $("#decalsvariation_left").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            label.value = parseInt(label.value) + 1;
        } else if (splittedstring[1] == "left") {
            if (parseInt(label.value) - 1 < 0)
                label.value = 0;
            else
                label.value = parseInt(label.value) - 1;
        }
        currentCharacter.decals = parseInt(label.value);
        sendCharaChanged();
    })

    $("#decalsid").on('input', function (evts) {
        currentCharacter.decals = parseInt(document.getElementById(evts.currentTarget.id).value);
        sendCharaChanged();
    })

    $("#decalsvariationid").on('input', function (evts) {
        currentCharacter.decals_variation = parseInt(document.getElementById(evts.currentTarget.id).value);
        sendCharaChanged();
    })

$("#tops_right").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == "f") {
                if (parseInt(label.value) + 1 > maxClothesvalues.tops.f) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            } else if (currentGender == "m") {
                if (parseInt(label.value) + 1 > maxClothesvalues.tops.m) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == "f") {

                if (parseInt(label.value) - 1 < 0) {
                    label.value = maxClothesvalues.tops.f;
                }
                else {
                    label.value = parseInt(label.value) - 1;
                }
            }
            else if (currentGender == "m") {

                if (parseInt(label.value) - 1 < 0) {
                    label.value = maxClothesvalues.tops.m;
                }
                else {
                    label.value = parseInt(label.value) - 1;
                }
            }

        }
        document.getElementById(splittedstring[0] + "variationid").value = 0;
        currentCharacter.tops = parseInt(label.value);
        sendCharaChanged();
    })

    $("#tops_left").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == "f") {
                if (parseInt(label.value) + 1 > maxClothesvalues.tops.f)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == "m") {
                if (parseInt(label.value) + 1 > maxClothesvalues.tops.m)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == "f") {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesvalues.tops.f;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == "m") {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesvalues.tops.m;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        document.getElementById(splittedstring[0] + "variationid").value = 0;
        currentCharacter.tops = parseInt(label.value);
        sendCharaChanged();
    })

    $("#topsvariation_right").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            label.value = parseInt(label.value) + 1;
        } else if (splittedstring[1] == "left") {
            if (parseInt(label.value) - 1 < 0)
                label.value = 0;
            else
                label.value = parseInt(label.value) - 1;
        }
        currentCharacter.tops = parseInt(label.value);
        sendCharaChanged();
    })

    $("#topsvariation_left").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            label.value = parseInt(label.value) + 1;
        } else if (splittedstring[1] == "left") {
            if (parseInt(label.value) - 1 < 0)
                label.value = 0;
            else
                label.value = parseInt(label.value) - 1;
        }
        currentCharacter.tops = parseInt(label.value);
        sendCharaChanged();
    })

    $("#topsid").on('input', function (evts) {
        currentCharacter.tops = parseInt(document.getElementById(evts.currentTarget.id).value);
        sendCharaChanged();
    })

    $("#topsvariationid").on('input', function (evts) {
        currentCharacter.tops_variation = parseInt(document.getElementById(evts.currentTarget.id).value);
        sendCharaChanged();
    })

    /*
END of generated code

*/
})