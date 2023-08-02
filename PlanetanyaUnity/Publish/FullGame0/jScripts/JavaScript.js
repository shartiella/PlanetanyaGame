
let groupname="";
var gamepart;

document.addEventListener("DOMContentLoaded", function (event){
    gamepart = document.getElementById("unity-container");
    console.log(gamepart);
    gamepart.classList.add("hide");
});

function getgroupname(){
    const textinput = document.getElementById("groupname");
    const pregamepart = document.getElementById("pregame");
    const gamepart = document.getElementById("unity-container");

    groupname=textinput.value;
    console.log(groupname);
    unityInstance.SendMessage('JavascriptHook','getGroupName',groupname);

    pregamepart.classList.add("hide");
    gamepart.classList.remove("hide");
    //unityInstance.SetFullscreen(1);
    gamepart.requestFullscreen();
}



