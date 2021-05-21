$.noConflict();
jQuery(document).ready(function ($) {

    display(false);
    displayCheckout(false);
    $("#buybottom").hide();

    var clothesprice = 0;
    var charbefore = JSON.parse('{\
        "gender":0,\
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
        "tops_variation":0, \
        "hats":0, \
        "glasses":0, \
        "ears":0, \
        "watches":0, \
        "bracelets":0, \
        "hats_variation":0, \
        "glasses_variation":0, \
        "ears_variation":0, \
        "watches_variation":0, \
        "bracelets_variation":0, \
        "noseWidth":0.0,\
        "noseHeight":0.0,\
        "noseLength":0.0,\
        "noseBridge":0.0,\
        "noseTip":0.0,\
        "noseBridgeShift":0.0,\
        "browHeight":0.0,\
        "browWidth":0.0,\
        "cheekboneHeight":0.0,\
        "cheekboneWidth":0.0,\
        "cheeksWidth":0.0,\
        "eyes":0.0,\
        "lips_":0.0,\
        "jawWidth":0.0,\
        "jawHeight":0.0,\
        "chinLength":0.0,\
        "chinPosition":0.0,\
        "chinWidth":0.0,\
        "chinShape":0.0,\
        "neckWidth":0.0\
    }');
    var currentCharacter = JSON.parse('{\
        "gender":0,\
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
        "tops_variation":0, \
        "hats":0, \
        "glasses":0, \
        "ears":0, \
        "watches":0, \
        "bracelets":0, \
        "hats_variation":0, \
        "glasses_variation":0, \
        "ears_variation":0, \
        "watches_variation":0, \
        "bracelets_variation":0, \
        "noseWidth":0.0,\
        "noseHeight":0.0,\
        "noseLength":0.0,\
        "noseBridge":0.0,\
        "noseTip":0.0,\
        "noseBridgeShift":0.0,\
        "browHeight":0.0,\
        "browWidth":0.0,\
        "cheekboneHeight":0.0,\
        "cheekboneWidth":0.0,\
        "cheeksWidth":0.0,\
        "eyes":0.0,\
        "lips_":0.0,\
        "jawWidth":0.0,\
        "jawHeight":0.0,\
        "chinLength":0.0,\
        "chinPosition":0.0,\
        "chinWidth":0.0,\
        "chinShape":0.0,\
        "neckWidth":0.0\
    }');
    var currentGender = 0;
    var maxClothesvalues = JSON.parse('{ \
        "head":{"m" : 45, "f":45}, \
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
        "tops":{"m" : 361, "f":380}, \
        "hats":{"m" : 152, "f":151}, \
        "glasses":{"m" : 33, "f":35}, \
        "ears":{"m" : 40, "f":21}, \
        "watches":{"m" : 40, "f":29}, \
        "bracelets":{"m" : 8, "f":15} \
        }');
    var maxClothesVariations = JSON.parse('{ \
        "head_variation":45,\
         "masks_variation":0,\
         "hair_variation":0,\
         "torso_variation":0,\
         "legs_variation":0,\
         "bags_variation":0,\
         "shoes_variation":0,\
         "accessories_variation":0,\
         "undershirts_variation":0,\
         "bodyArmor_variation":0,\
         "decals_variation":0,\
         "tops_variation":0,\
         "hats_variation":0,\
         "glasses_variation":0,\
         "ears_variation":0,\
         "watches_variation":0,\
         "bracelets_variation":0\
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

    

    function sendCharaChanged() {
        console.log("sending chara change");
        console.log(currentCharacter);
        $.post('http://ClothesShop/charUpdate', JSON.stringify(currentCharacter));
    }

    //$("#etc").hide();
    //$("#FaceCreation").hide();

    window.addEventListener('message', function (event) {
        var item = event.data;

        //case 0: openUI for shop
        if (item.toggle == 0) {
            //set current values
            //get max values

            if (item.characreation === true) {
                $("#etc").show();
                $("#FaceCreation").show();
                $("#buybottom").hide();
                $("#createbottom").show();
            } else {
                $("#buybottom").show();
                $("#createbottom").hide();
                $("#etc").hide();
                //load all values and variations
                document.getElementById("gender").value = item.gender;
                //Looks
                currentCharacter.hats = item.hats;
                currentCharacter.glasses = item.glasses;
                currentCharacter.ears = item.ears;
                currentCharacter.watches = item.watches;
                currentCharacter.bracelets = item.bracelets;
                currentCharacter.head = item.head;
                currentCharacter.masks = item.masks;
                currentCharacter.hair = item.hair;
                currentCharacter.torso = item.torso;
                currentCharacter.legs = item.legs;
                currentCharacter.bags = item.bags;
                currentCharacter.shoes = item.shoes;
                currentCharacter.accessories = item.accessories;
                currentCharacter.undershirts = item.undershirts;
                currentCharacter.bodyArmor = item.bodyArmor;
                currentCharacter.decals = item.decals;
                currentCharacter.tops = item.tops;
                currentCharacter.noseWidth = item.noseWidth;
                currentCharacter.noseHeight = item.noseHeight;
                currentCharacter.noseLength = item.noseLength;
                currentCharacter.noseBridge = item.noseBridge;
                currentCharacter.noseTip = item.noseTip;
                currentCharacter.noseBridgeShift = item.noseBridgeShift;
                currentCharacter.browHeight = item.browHeight;
                currentCharacter.browWidth = item.browWidth;
                currentCharacter.cheekboneHeight = item.cheekboneHeight;
                currentCharacter.cheekboneWidth = item.cheekboneWidth;
                currentCharacter.cheeksWidth = item.cheeksWidth;
                currentCharacter.eyes = item.eyes;
                currentCharacter.lips_ = item.lips_;
                currentCharacter.jawWidth = item.jawWidth;
                currentCharacter.jawHeight = item.jawHeight;
                currentCharacter.chinLength = item.chinLength;
                currentCharacter.chinPosition = item.chinPosition;
                currentCharacter.chinWidth = item.chinWidth;
                currentCharacter.chinShape = item.chinShape;
                currentCharacter.neckWidth = item.neckWidth;
                currentCharacter.head_variation = item.head_variation;
                currentCharacter.masks_variation = item.masks_variation;
                currentCharacter.hair_variation = item.hair_variation;
                currentCharacter.torso_variation = item.torso_variation;
                currentCharacter.legs_variation = item.legs_variation;
                currentCharacter.bags_variation = item.bags_variation;
                currentCharacter.shoes_variation = item.shoes_variation;
                currentCharacter.accessories_variation = item.accessories_variation;
                currentCharacter.undershirts_variation = item.undershirts_variation;
                currentCharacter.bodyArmor_variation = item.bodyArmor_variation;
                currentCharacter.decals_variation = item.decals_variation;
                currentCharacter.tops_variation = item.tops_variation;
                currentCharacter.hats_variation = item.hats_variation;
                currentCharacter.glasses_variation = item.glasses_variation;
                currentCharacter.ears_variation = item.ears_variation;
                currentCharacter.watches_variation = item.watches_variation;
                currentCharacter.bracelets_variation = item.bracelets_variation;

                charbefore = currentCharacter;
                //update UI elements
                //Looks
                document.getElementById("headid").value = currentCharacter.head;
                document.getElementById("masksid").value = currentCharacter.masks;
                document.getElementById("hairid").value = currentCharacter.hair;
                document.getElementById("torsoid").value = currentCharacter.torso;
                document.getElementById("legsid").value = currentCharacter.legs;
                document.getElementById("bagsid").value = currentCharacter.bags;
                document.getElementById("shoesid").value = currentCharacter.shoes;
                document.getElementById("accessoriesid").value = currentCharacter.accessories;
                document.getElementById("undershirtsid").value = currentCharacter.undershirts;
                document.getElementById("bodyArmorid").value = currentCharacter.bodyArmor;
                document.getElementById("decalsid").value = currentCharacter.decals;
                document.getElementById("topsid").value = currentCharacter.tops;
                document.getElementById("headvariationid").value = currentCharacter.head_variation;
                document.getElementById("masksvariationid").value = currentCharacter.masks_variation;
                document.getElementById("hairvariationid").value = currentCharacter.hair_variation;
                document.getElementById("torsovariationid").value = currentCharacter.torso_variation;
                document.getElementById("legsvariationid").value = currentCharacter.legs_variation;
                document.getElementById("bagsvariationid").value = currentCharacter.bags_variation;
                document.getElementById("shoesvariationid").value = currentCharacter.shoes_variation;
                document.getElementById("accessoriesvariationid").value = currentCharacter.accessories_variation;
                document.getElementById("undershirtsvariationid").value = currentCharacter.undershirts_variation;
                document.getElementById("bodyArmorvariationid").value = currentCharacter.bodyArmor_variation;
                document.getElementById("decalsvariationid").value = currentCharacter.decals_variation;
                document.getElementById("topsvariationid").value = currentCharacter.tops_variation;
                //props
                document.getElementById("hatsvariationid").value = currentCharacter.hats_variation;
                document.getElementById("glassesvariationid").value = currentCharacter.glasses_variation;
                document.getElementById("earsvariationid").value = currentCharacter.ears_variation;
                document.getElementById("watchesvariationid").value = currentCharacter.watches_variation;
                document.getElementById("braceletsvariationid").value = currentCharacter.bracelets_variation;
                document.getElementById("hatsid").value = currentCharacter.hats_variation;
                document.getElementById("glassesid").value = currentCharacter.glasses_variation;
                document.getElementById("earsid").value = currentCharacter.ears_variation;
                document.getElementById("watchesid").value = currentCharacter.watches_variation;
                document.getElementById("braceletsid").value = currentCharacter.bracelets_variation;
                document.getElementById("noseWidth").value = currentCharacter.noseWidth;
                document.getElementById("noseHeight").value = currentCharacter.noseHeight;
                document.getElementById("noseLength").value = currentCharacter.noseLength;
                document.getElementById("noseBridge").value = currentCharacter.noseBridge;
                document.getElementById("noseTip").value = currentCharacter.noseTip;
                document.getElementById("noseBridgeShift").value = currentCharacter.noseBridgeShift;
                document.getElementById("browHeight").value = currentCharacter.browHeight;
                document.getElementById("browWidth").value = currentCharacter.browWidth;
                document.getElementById("cheekboneHeight").value = currentCharacter.cheekboneHeight;
                document.getElementById("cheekboneWidth").value = currentCharacter.cheekboneWidth;
                document.getElementById("cheeksWidth").value = currentCharacter.cheeksWidth;
                document.getElementById("eyes").value = currentCharacter.eyes;
                document.getElementById("lips_").value = currentCharacter.lips_;
                document.getElementById("jawWidth").value = currentCharacter.jawWidth;
                document.getElementById("jawHeight").value = currentCharacter.jawHeight;
                document.getElementById("chinLength").value = currentCharacter.chinLength;
                document.getElementById("chinPosition").value = currentCharacter.chinPosition;
                document.getElementById("chinWidth").value = currentCharacter.chinWidth;
                document.getElementById("chinShape").value = currentCharacter.chinShape;
                document.getElementById("neckWidth").value = currentCharacter.neckWidth;
            }
            //currentCharacter = JSON.parse(item);

            display(true);
        } else if (item.toggle == 1){
            display(false);
            displayCheckout(false);
        }else if (item.toggle == 2){
            //setting all variations
            //skins colors 45
            console.log("change triggered");
            maxClothesVariations.head_variation = item.head_variations;
            maxClothesVariations.masks_variation = item.masks_variations;
            maxClothesVariations.hair_variation = item.hair_variations;
            maxClothesVariations.torso_variation = item.torso_variations;
            maxClothesVariations.legs_variation = item.legs_variations;
            maxClothesVariations.bags_variation = item.bags_variations;
            maxClothesVariations.shoes_variation = item.shoes_variations;
            maxClothesVariations.accessories_variation = item.accessories_variations;
            maxClothesVariations.undershirts_variation = item.undershirts_variations;
            maxClothesVariations.bodyArmor_variation = item.bodyArmor_variations;
            maxClothesVariations.decals_variation = item.decals_variations;
            maxClothesVariations.tops_variation = item.tops_variations;
            maxClothesVariations.hats_variation = item.hats_variations;
            maxClothesVariations.glasses_variation = item.glasses_variations;
            maxClothesVariations.ears_variation = item.ears_variations;
            maxClothesVariations.watches_variation = item.watches_variations;
            maxClothesVariations.bracelets_variation = item.bracelets_variations;
        
        }

    })
    $("#confirmCreation").click(function (evts) {
        var name = document.getElementById("charaname").innerHTML;
        $.post('http://ClothesShop/creationDone', JSON.stringify({ gender: currentGender, name : name, exit: "Success" }));
        //$.post('http://ClothesShop/creationDone', JSON.stringify({ exit: "Success" }));
        return

    })
    $("#cancel").click(function (evts) {
        //console.log("close triggered");
        $.post('http://ClothesShop/cancelbuy', JSON.stringify({ cancelled: true }));
        return
    })
    $("#confirm").click(function (evts) {
        if (charbefore.head != currentCharacter.head) clothesprice += 20;
        if (charbefore.masks != currentCharacter.masks) clothesprice += 20;
        if (charbefore.hair != currentCharacter.hair) clothesprice += 20;
        if (charbefore.torso != currentCharacter.torso) clothesprice += 20;
        if (charbefore.legs != currentCharacter.legs) clothesprice += 20;
        if (charbefore.bags != currentCharacter.bags) clothesprice += 20;
        if (charbefore.shoes != currentCharacter.shoes) clothesprice += 20;
        if (charbefore.accessories != currentCharacter.accessories) clothesprice += 20;
        if (charbefore.undershirts != currentCharacter.undershirts) clothesprice += 20;
        if (charbefore.bodyArmor != currentCharacter.bodyArmor) clothesprice += 20;
        if (charbefore.decals != currentCharacter.decals) clothesprice += 20;
        if (charbefore.tops != currentCharacter.tops) clothesprice += 20;
        if (charbefore.hats != currentCharacter.hats) clothesprice += 20;
        if (charbefore.glasses != currentCharacter.glasses) clothesprice += 20;
        if (charbefore.ears != currentCharacter.ears) clothesprice += 20;
        if (charbefore.watches != currentCharacter.watches) clothesprice += 20;
        if (charbefore.bracelets != currentCharacter.bracelets) clothesprice += 20;
        document.getElementById("pricetag").innerHTML = "checkout for "+ clothesprice + "$ ?";
        
        $("#container2").show();
    })

    $("#cancelbuy").click(function (evts) {
        $("#container2").hide();
    })

    $("#pay").click(function (evts) {
        $.post('http://ClothesShop/payForClothes', JSON.stringify({ price: clothesprice }));
        return
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

    $("#gender").change(function (evts) {
        var tabcontent;
        tabcontent = document.getElementsByClassName("clotheIDField");
        for (i = 0; i < tabcontent.length; i++) {
            tabcontent[i].value = 0;
        }
        currentGender = document.getElementById(evts.currentTarget.id).value;
        currentCharacter.gender = document.getElementById(evts.currentTarget.id).value;
        sendCharaChanged();
    })

    /*
    * Generated code segment for changing value
    */

    $("#noseWidthSlider").change(function (evts) {
        currentCharacter.noseWidth = parseInt(evts.currentTarget.value) / 100.0;
        sendCharaChanged();
    });

    $("#noseHeightSlider").change(function (evts) {
        currentCharacter.noseHeight = parseInt(evts.currentTarget.value) / 100.0;
        sendCharaChanged();
    });

    $("#noseLengthSlider").change(function (evts) {
        currentCharacter.noseLength = parseInt(evts.currentTarget.value) / 100.0;
        sendCharaChanged();
    });

    $("#noseBridgeSlider").change(function (evts) {
        currentCharacter.noseBridge = parseInt(evts.currentTarget.value) / 100.0;
        sendCharaChanged();
    });

    $("#noseTipSlider").change(function (evts) {
        currentCharacter.noseTip = parseInt(evts.currentTarget.value) / 100.0;
        sendCharaChanged();
    });

    $("#noseBridgeShiftSlider").change(function (evts) {
        currentCharacter.noseBridgeShift = parseInt(evts.currentTarget.value) / 100.0;
        sendCharaChanged();
    });

    $("#browHeightSlider").change(function (evts) {
        currentCharacter.browHeight = parseInt(evts.currentTarget.value) / 100.0;
        sendCharaChanged();
    });

    $("#browWidthSlider").change(function (evts) {
        currentCharacter.browWidth = parseInt(evts.currentTarget.value) / 100.0;
        sendCharaChanged();
    });

    $("#cheekboneHeightSlider").change(function (evts) {
        currentCharacter.cheekboneHeight = parseInt(evts.currentTarget.value) / 100.0;
        sendCharaChanged();
    });

    $("#cheekboneWidthSlider").change(function (evts) {
        currentCharacter.cheekboneWidth = parseInt(evts.currentTarget.value) / 100.0;
        sendCharaChanged();
    });

    $("#cheeksWidthSlider").change(function (evts) {
        currentCharacter.cheeksWidth = parseInt(evts.currentTarget.value) / 100.0;
        sendCharaChanged();
    });

    $("#eyesSlider").change(function (evts) {
        currentCharacter.eyes = parseInt(evts.currentTarget.value) / 100.0;
        sendCharaChanged();
    });

    $("#lips_Slider").change(function (evts) {
        currentCharacter.lips_ = parseInt(evts.currentTarget.value) / 100.0;
        sendCharaChanged();
    });

    $("#jawWidthSlider").change(function (evts) {
        currentCharacter.jawWidth = parseInt(evts.currentTarget.value) / 100.0;
        sendCharaChanged();
    });

    $("#jawHeightSlider").change(function (evts) {
        currentCharacter.jawHeight = parseInt(evts.currentTarget.value) / 100.0;
        sendCharaChanged();
    });

    $("#chinLengthSlider").change(function (evts) {
        currentCharacter.chinLength = parseInt(evts.currentTarget.value) / 100.0;
        sendCharaChanged();
    });

    $("#chinPositionSlider").change(function (evts) {
        currentCharacter.chinPosition = parseInt(evts.currentTarget.value) / 100.0;
        sendCharaChanged();
    });

    $("#chinWidthSlider").change(function (evts) {
        currentCharacter.chinWidth = parseInt(evts.currentTarget.value) / 100.0;
        sendCharaChanged();
    });

    $("#chinShapeSlider").change(function (evts) {
        currentCharacter.chinShape = parseInt(evts.currentTarget.value) / 100.0;
        sendCharaChanged();
    });

    $("#neckWidthSlider").change(function (evts) {
        currentCharacter.neckWidth = parseInt(evts.currentTarget.value) / 100.0;
        sendCharaChanged();
    });







    $("#head_right").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesvalues.head.f) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesvalues.head.m) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0) {
                    label.value = maxClothesvalues.head.f;
                }
                else {
                    label.value = parseInt(label.value) - 1;
                }
            }
            else if (currentGender == 0) {

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
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesvalues.head.f)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesvalues.head.m)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesvalues.head.f;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

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
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesVariations.head_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesVariations.head_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.head_variation;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

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
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesVariations.head_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesVariations.head_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.head_variation;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

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
    $("#masks_right").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesvalues.masks.f) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesvalues.masks.m) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0) {
                    label.value = maxClothesvalues.masks.f;
                }
                else {
                    label.value = parseInt(label.value) - 1;
                }
            }
            else if (currentGender == 0) {

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
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesvalues.masks.f)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesvalues.masks.m)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesvalues.masks.f;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

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
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesVariations.masks_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesVariations.masks_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.masks_variation;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.masks_variation;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        currentCharacter.masks_variation = parseInt(label.value);
        sendCharaChanged();
    })

    $("#masksvariation_left").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesVariations.masks_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesVariations.masks_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.masks_variation;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.masks_variation;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        currentCharacter.masks_variation = parseInt(label.value);
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
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesvalues.hair.f) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesvalues.hair.m) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0) {
                    label.value = maxClothesvalues.hair.f;
                }
                else {
                    label.value = parseInt(label.value) - 1;
                }
            }
            else if (currentGender == 0) {

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
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesvalues.hair.f)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesvalues.hair.m)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesvalues.hair.f;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

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
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesVariations.hair_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesVariations.hair_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.hair_variation;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.hair_variation;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        currentCharacter.hair_variation = parseInt(label.value);
        sendCharaChanged();
    })

    $("#hairvariation_left").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesVariations.hair_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesVariations.hair_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.hair_variation;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.hair_variation;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        currentCharacter.hair_variation = parseInt(label.value);
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
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesvalues.torso.f) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesvalues.torso.m) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0) {
                    label.value = maxClothesvalues.torso.f;
                }
                else {
                    label.value = parseInt(label.value) - 1;
                }
            }
            else if (currentGender == 0) {

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
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesvalues.torso.f)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesvalues.torso.m)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesvalues.torso.f;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

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
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesVariations.torso_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesVariations.torso_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.torso_variation;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.torso_variation;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        currentCharacter.torso_variation = parseInt(label.value);
        sendCharaChanged();
    })

    $("#torsovariation_left").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesVariations.torso_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesVariations.torso_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.torso_variation;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.torso_variation;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        currentCharacter.torso_variation = parseInt(label.value);
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
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesvalues.legs.f) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesvalues.legs.m) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0) {
                    label.value = maxClothesvalues.legs.f;
                }
                else {
                    label.value = parseInt(label.value) - 1;
                }
            }
            else if (currentGender == 0) {

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
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesvalues.legs.f)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesvalues.legs.m)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesvalues.legs.f;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

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
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesVariations.legs_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesVariations.legs_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.legs_variation;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.legs_variation;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        currentCharacter.legs_variation = parseInt(label.value);
        sendCharaChanged();
    })

    $("#legsvariation_left").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesVariations.legs_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesVariations.legs_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.legs_variation;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.legs_variation;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        currentCharacter.legs_variation = parseInt(label.value);
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
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesvalues.bags.f) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesvalues.bags.m) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0) {
                    label.value = maxClothesvalues.bags.f;
                }
                else {
                    label.value = parseInt(label.value) - 1;
                }
            }
            else if (currentGender == 0) {

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
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesvalues.bags.f)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesvalues.bags.m)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesvalues.bags.f;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

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
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesVariations.bags_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesVariations.bags_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.bags_variation;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.bags_variation;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        currentCharacter.bags_variation = parseInt(label.value);
        sendCharaChanged();
    })

    $("#bagsvariation_left").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesVariations.bags_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesVariations.bags_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.bags_variation;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.bags_variation;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        currentCharacter.bags_variation = parseInt(label.value);
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
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesvalues.shoes.f) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesvalues.shoes.m) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0) {
                    label.value = maxClothesvalues.shoes.f;
                }
                else {
                    label.value = parseInt(label.value) - 1;
                }
            }
            else if (currentGender == 0) {

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
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesvalues.shoes.f)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesvalues.shoes.m)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesvalues.shoes.f;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

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
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesVariations.shoes_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesVariations.shoes_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.shoes_variation;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.shoes_variation;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        currentCharacter.shoes_variation = parseInt(label.value);
        sendCharaChanged();
    })

    $("#shoesvariation_left").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesVariations.shoes_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesVariations.shoes_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.shoes_variation;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.shoes_variation;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        currentCharacter.shoes_variation = parseInt(label.value);
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
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesvalues.accessories.f) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesvalues.accessories.m) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0) {
                    label.value = maxClothesvalues.accessories.f;
                }
                else {
                    label.value = parseInt(label.value) - 1;
                }
            }
            else if (currentGender == 0) {

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
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesvalues.accessories.f)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesvalues.accessories.m)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesvalues.accessories.f;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

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
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesVariations.accessories_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesVariations.accessories_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.accessories_variation;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.accessories_variation;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        currentCharacter.accessories_variation = parseInt(label.value);
        sendCharaChanged();
    })

    $("#accessoriesvariation_left").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesVariations.accessories_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesVariations.accessories_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.accessories_variation;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.accessories_variation;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        currentCharacter.accessories_variation = parseInt(label.value);
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
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesvalues.undershirts.f) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesvalues.undershirts.m) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0) {
                    label.value = maxClothesvalues.undershirts.f;
                }
                else {
                    label.value = parseInt(label.value) - 1;
                }
            }
            else if (currentGender == 0) {

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
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesvalues.undershirts.f)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesvalues.undershirts.m)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesvalues.undershirts.f;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

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
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesVariations.undershirts_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesVariations.undershirts_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.undershirts_variation;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.undershirts_variation;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        currentCharacter.undershirts_variation = parseInt(label.value);
        sendCharaChanged();
    })

    $("#undershirtsvariation_left").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesVariations.undershirts_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesVariations.undershirts_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.undershirts_variation;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.undershirts_variation;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        currentCharacter.undershirts_variation = parseInt(label.value);
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
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesvalues.bodyArmor.f) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesvalues.bodyArmor.m) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0) {
                    label.value = maxClothesvalues.bodyArmor.f;
                }
                else {
                    label.value = parseInt(label.value) - 1;
                }
            }
            else if (currentGender == 0) {

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
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesvalues.bodyArmor.f)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesvalues.bodyArmor.m)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesvalues.bodyArmor.f;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

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
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesVariations.bodyArmor_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesVariations.bodyArmor_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.bodyArmor_variation;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.bodyArmor_variation;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        currentCharacter.bodyArmor_variation = parseInt(label.value);
        sendCharaChanged();
    })

    $("#bodyArmorvariation_left").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesVariations.bodyArmor_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesVariations.bodyArmor_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.bodyArmor_variation;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.bodyArmor_variation;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        currentCharacter.bodyArmor_variation = parseInt(label.value);
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
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesvalues.decals.f) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesvalues.decals.m) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0) {
                    label.value = maxClothesvalues.decals.f;
                }
                else {
                    label.value = parseInt(label.value) - 1;
                }
            }
            else if (currentGender == 0) {

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
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesvalues.decals.f)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesvalues.decals.m)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesvalues.decals.f;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

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
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesVariations.decals_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesVariations.decals_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.decals_variation;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.decals_variation;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        currentCharacter.decals_variation = parseInt(label.value);
        sendCharaChanged();
    })

    $("#decalsvariation_left").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesVariations.decals_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesVariations.decals_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.decals_variation;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.decals_variation;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        currentCharacter.decals_variation = parseInt(label.value);
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
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesvalues.tops.f) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesvalues.tops.m) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0) {
                    label.value = maxClothesvalues.tops.f;
                }
                else {
                    label.value = parseInt(label.value) - 1;
                }
            }
            else if (currentGender == 0) {

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
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesvalues.tops.f)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesvalues.tops.m)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesvalues.tops.f;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

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
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesVariations.tops_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesVariations.tops_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.tops_variation;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.tops_variation;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        currentCharacter.tops_variation = parseInt(label.value);
        sendCharaChanged();
    })

    $("#topsvariation_left").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesVariations.tops_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesVariations.tops_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.tops_variation;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.tops_variation;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        currentCharacter.tops_variation = parseInt(label.value);
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
    $("#hats_right").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesvalues.hats.f) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesvalues.hats.m) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0) {
                    label.value = maxClothesvalues.hats.f;
                }
                else {
                    label.value = parseInt(label.value) - 1;
                }
            }
            else if (currentGender == 0) {

                if (parseInt(label.value) - 1 < 0) {
                    label.value = maxClothesvalues.hats.m;
                }
                else {
                    label.value = parseInt(label.value) - 1;
                }
            }

        }
        document.getElementById(splittedstring[0] + "variationid").value = 0;
        currentCharacter.hats = parseInt(label.value);
        sendCharaChanged();
    })

    $("#hats_left").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesvalues.hats.f)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesvalues.hats.m)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesvalues.hats.f;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesvalues.hats.m;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        document.getElementById(splittedstring[0] + "variationid").value = 0;
        currentCharacter.hats = parseInt(label.value);
        sendCharaChanged();
    })

    $("#hatsvariation_right").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesVariations.hats_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesVariations.hats_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.hats_variation;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.hats_variation;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        currentCharacter.hats_variation = parseInt(label.value);
        sendCharaChanged();
    })

    $("#hatsvariation_left").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesVariations.hats_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesVariations.hats_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.hats_variation;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.hats_variation;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        currentCharacter.hats_variation = parseInt(label.value);
        sendCharaChanged();
    })

    $("#hatsid").on('input', function (evts) {
        currentCharacter.hats = parseInt(document.getElementById(evts.currentTarget.id).value);
        sendCharaChanged();
    })

    $("#hatsvariationid").on('input', function (evts) {
        currentCharacter.hats_variation = parseInt(document.getElementById(evts.currentTarget.id).value);
        sendCharaChanged();
    })
    $("#glasses_right").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesvalues.glasses.f) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesvalues.glasses.m) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0) {
                    label.value = maxClothesvalues.glasses.f;
                }
                else {
                    label.value = parseInt(label.value) - 1;
                }
            }
            else if (currentGender == 0) {

                if (parseInt(label.value) - 1 < 0) {
                    label.value = maxClothesvalues.glasses.m;
                }
                else {
                    label.value = parseInt(label.value) - 1;
                }
            }

        }
        document.getElementById(splittedstring[0] + "variationid").value = 0;
        currentCharacter.glasses = parseInt(label.value);
        sendCharaChanged();
    })

    $("#glasses_left").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesvalues.glasses.f)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesvalues.glasses.m)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesvalues.glasses.f;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesvalues.glasses.m;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        document.getElementById(splittedstring[0] + "variationid").value = 0;
        currentCharacter.glasses = parseInt(label.value);
        sendCharaChanged();
    })

    $("#glassesvariation_right").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesVariations.glasses_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesVariations.glasses_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.glasses_variation;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.glasses_variation;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        currentCharacter.glasses_variation = parseInt(label.value);
        sendCharaChanged();
    })

    $("#glassesvariation_left").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesVariations.glasses_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesVariations.glasses_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.glasses_variation;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.glasses_variation;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        currentCharacter.glasses_variation = parseInt(label.value);
        sendCharaChanged();
    })

    $("#glassesid").on('input', function (evts) {
        currentCharacter.glasses = parseInt(document.getElementById(evts.currentTarget.id).value);
        sendCharaChanged();
    })

    $("#glassesvariationid").on('input', function (evts) {
        currentCharacter.glasses_variation = parseInt(document.getElementById(evts.currentTarget.id).value);
        sendCharaChanged();
    })
    $("#ears_right").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesvalues.ears.f) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesvalues.ears.m) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0) {
                    label.value = maxClothesvalues.ears.f;
                }
                else {
                    label.value = parseInt(label.value) - 1;
                }
            }
            else if (currentGender == 0) {

                if (parseInt(label.value) - 1 < 0) {
                    label.value = maxClothesvalues.ears.m;
                }
                else {
                    label.value = parseInt(label.value) - 1;
                }
            }

        }
        document.getElementById(splittedstring[0] + "variationid").value = 0;
        currentCharacter.ears = parseInt(label.value);
        sendCharaChanged();
    })

    $("#ears_left").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesvalues.ears.f)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesvalues.ears.m)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesvalues.ears.f;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesvalues.ears.m;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        document.getElementById(splittedstring[0] + "variationid").value = 0;
        currentCharacter.ears = parseInt(label.value);
        sendCharaChanged();
    })

    $("#earsvariation_right").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesVariations.ears_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesVariations.ears_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.ears_variation;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.ears_variation;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        currentCharacter.ears_variation = parseInt(label.value);
        sendCharaChanged();
    })

    $("#earsvariation_left").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesVariations.ears_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesVariations.ears_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.ears_variation;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.ears_variation;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        currentCharacter.ears_variation = parseInt(label.value);
        sendCharaChanged();
    })

    $("#earsid").on('input', function (evts) {
        currentCharacter.ears = parseInt(document.getElementById(evts.currentTarget.id).value);
        sendCharaChanged();
    })

    $("#earsvariationid").on('input', function (evts) {
        currentCharacter.ears_variation = parseInt(document.getElementById(evts.currentTarget.id).value);
        sendCharaChanged();
    })
    $("#watches_right").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesvalues.watches.f) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesvalues.watches.m) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0) {
                    label.value = maxClothesvalues.watches.f;
                }
                else {
                    label.value = parseInt(label.value) - 1;
                }
            }
            else if (currentGender == 0) {

                if (parseInt(label.value) - 1 < 0) {
                    label.value = maxClothesvalues.watches.m;
                }
                else {
                    label.value = parseInt(label.value) - 1;
                }
            }

        }
        document.getElementById(splittedstring[0] + "variationid").value = 0;
        currentCharacter.watches = parseInt(label.value);
        sendCharaChanged();
    })

    $("#watches_left").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesvalues.watches.f)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesvalues.watches.m)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesvalues.watches.f;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesvalues.watches.m;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        document.getElementById(splittedstring[0] + "variationid").value = 0;
        currentCharacter.watches = parseInt(label.value);
        sendCharaChanged();
    })

    $("#watchesvariation_right").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesVariations.watches_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesVariations.watches_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.watches_variation;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.watches_variation;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        currentCharacter.watches_variation = parseInt(label.value);
        sendCharaChanged();
    })

    $("#watchesvariation_left").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesVariations.watches_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesVariations.watches_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.watches_variation;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.watches_variation;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        currentCharacter.watches_variation = parseInt(label.value);
        sendCharaChanged();
    })

    $("#watchesid").on('input', function (evts) {
        currentCharacter.watches = parseInt(document.getElementById(evts.currentTarget.id).value);
        sendCharaChanged();
    })

    $("#watchesvariationid").on('input', function (evts) {
        currentCharacter.watches_variation = parseInt(document.getElementById(evts.currentTarget.id).value);
        sendCharaChanged();
    })
    $("#bracelets_right").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesvalues.bracelets.f) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesvalues.bracelets.m) {
                    label.value = 0;
                }
                else {
                    label.value = parseInt(label.value) + 1;
                }
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0) {
                    label.value = maxClothesvalues.bracelets.f;
                }
                else {
                    label.value = parseInt(label.value) - 1;
                }
            }
            else if (currentGender == 0) {

                if (parseInt(label.value) - 1 < 0) {
                    label.value = maxClothesvalues.bracelets.m;
                }
                else {
                    label.value = parseInt(label.value) - 1;
                }
            }

        }
        document.getElementById(splittedstring[0] + "variationid").value = 0;
        currentCharacter.bracelets = parseInt(label.value);
        sendCharaChanged();
    })

    $("#bracelets_left").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesvalues.bracelets.f)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesvalues.bracelets.m)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesvalues.bracelets.f;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesvalues.bracelets.m;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        document.getElementById(splittedstring[0] + "variationid").value = 0;
        currentCharacter.bracelets = parseInt(label.value);
        sendCharaChanged();
    })

    $("#braceletsvariation_right").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesVariations.bracelets_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesVariations.bracelets_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.bracelets_variation;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.bracelets_variation;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        currentCharacter.bracelets_variation = parseInt(label.value);
        sendCharaChanged();
    })

    $("#braceletsvariation_left").click(function (evts) {
        var splittedstring = evts.currentTarget.id.split("_");
        var itemid = splittedstring[0] + "id";

        var label = document.getElementById(itemid);

        if (splittedstring[1] == "right") {
            if (currentGender == 1) {
                if (parseInt(label.value) + 1 > maxClothesVariations.bracelets_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            } else if (currentGender == 0) {
                if (parseInt(label.value) + 1 > maxClothesVariations.bracelets_variation)
                    label.value = 0;
                else
                    label.value = parseInt(label.value) + 1;
            }
        } else if (splittedstring[1] == "left") {
            if (currentGender == 1) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.bracelets_variation;
                else
                    label.value = parseInt(label.value) - 1;
            }
            else if (currentGender == 0) {

                if (parseInt(label.value) - 1 < 0)
                    label.value = maxClothesVariations.bracelets_variation;
                else
                    label.value = parseInt(label.value) - 1;

            }
        }
        currentCharacter.bracelets_variation = parseInt(label.value);
        sendCharaChanged();
    })

    $("#braceletsid").on('input', function (evts) {
        currentCharacter.bracelets = parseInt(document.getElementById(evts.currentTarget.id).value);
        sendCharaChanged();
    })

    $("#braceletsvariationid").on('input', function (evts) {
        currentCharacter.bracelets_variation = parseInt(document.getElementById(evts.currentTarget.id).value);
        sendCharaChanged();
    })




    /*
    END of generated code
    */

})