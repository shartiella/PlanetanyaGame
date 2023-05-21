mergeInto(LibraryManager.library, {
  LocateTheDevice: function () {
    
    var success=function(position){
      const latitude = position.coords.latitude;
      const longitude = position.coords.longitude;
	    window.alert(`lat: ${latitude} , long: ${longitude} `);
      unityInstance.SendMessage('Map','coor',`lat: ${latitude} , long: ${longitude} `);
    }

    var error=function(position){
      window.alert("ERROR");
    }
    
    if (!navigator.geolocation) {
        window.alert("NOT SUPPORTED");
    } else {
        window.alert("LOOKING");
        navigator.geolocation.getCurrentPosition(success, error);
    }
  },
});