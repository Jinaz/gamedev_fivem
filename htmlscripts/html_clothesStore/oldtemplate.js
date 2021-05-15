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
    sendCharaChanged();
})

$("#headid").on('input', function (evts) {
    sendCharaChanged();
})

$("#headvariationid").on('input', function (evts) {
    sendCharaChanged();
})