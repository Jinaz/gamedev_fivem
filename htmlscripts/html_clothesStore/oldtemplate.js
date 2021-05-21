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
        if (currentGender == "f") {
            if (parseInt(label.value) + 1 > maxClothesVariations.head_variation)
                label.value = 0;
            else
                label.value = parseInt(label.value) + 1;
        } else if (currentGender == "m") {
            if (parseInt(label.value) + 1 > maxClothesVariations.head_variation)
                label.value = 0;
            else
                label.value = parseInt(label.value) + 1;
        }
    } else if (splittedstring[1] == "left") {
        if (currentGender == "f") {

            if (parseInt(label.value) - 1 < 0)
                label.value = maxClothesVariations.head_variation;
            else
                label.value = parseInt(label.value) - 1;
        }
        else if (currentGender == "m") {

            if (parseInt(label.value) - 1 < 0)
                label.value = maxClothesVariations.head_variation;
            else
                label.value = parseInt(label.value) - 1;

        }
    }
    currentCharacter.head_variation = parseInt(label.value);
    sendCharaChanged();
})

$("#headvariation_left").click(function (evts) {
    var splittedstring = evts.currentTarget.id.split("_");
    var itemid = splittedstring[0] + "id";

    var label = document.getElementById(itemid);

    if (splittedstring[1] == "right") {
        if (currentGender == "f") {
            if (parseInt(label.value) + 1 > maxClothesVariations.head_variation)
                label.value = 0;
            else
                label.value = parseInt(label.value) + 1;
        } else if (currentGender == "m") {
            if (parseInt(label.value) + 1 > maxClothesVariations.head_variation)
                label.value = 0;
            else
                label.value = parseInt(label.value) + 1;
        }
    } else if (splittedstring[1] == "left") {
        if (currentGender == "f") {

            if (parseInt(label.value) - 1 < 0)
                label.value = maxClothesVariations.head_variation;
            else
                label.value = parseInt(label.value) - 1;
        }
        else if (currentGender == "m") {

            if (parseInt(label.value) - 1 < 0)
                label.value = maxClothesVariations.head_variation;
            else
                label.value = parseInt(label.value) - 1;

        }
    }
    currentCharacter.head_variation = parseInt(label.value);
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