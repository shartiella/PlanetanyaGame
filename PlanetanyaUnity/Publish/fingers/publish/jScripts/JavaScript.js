
let guidecode=0;
let groupname="";
var gamepart;

document.addEventListener("DOMContentLoaded", function (event){
    gamepart = document.getElementById("unity-container");
    console.log(gamepart);
    gamepart.classList.add("hide");
});

function getguidecode(){
    const textinput = document.getElementById("guidecode");
    const grouppart = document.getElementById("group");
    const guidepart = document.getElementById("guide");
    guidecode=textinput.value;
    console.log(guidecode);
    unityInstance.SendMessage('JavascriptHook','getGuideCode',guidecode);

    guidepart.classList.add("hide");
    grouppart.classList.remove("hide");
}

function getgroupname(){
    const textinput = document.getElementById("groupname");
    const pregamepart = document.getElementById("pregame");
    const gamepart = document.getElementById("unity-container");
    groupname=textinput.value;
    console.log(groupname);
    unityInstance.SendMessage('JavascriptHook','getGroupName',groupname);

    pregamepart.classList.add("hide");
    gamepart.classList.remove("hide");
}

