#pragma strict

function OnGUI(){
	if(GUI.Button(Rect(0, 0, Screen.width, Screen.height/10), "Event 1")){
		Debug.Log("event 1");
		FlurryManager.logEvent("Event 1");
	}
	
	if(GUI.Button(Rect(0, Screen.height*2/10, Screen.width, Screen.height/10), "Event 2 - A")){
		Debug.Log("event 2 - A");
		FlurryManager.logEvent("Event 2", {"type" : "A", "level" : "3"});
	}
	
	if(GUI.Button(Rect(0, Screen.height*3/10, Screen.width, Screen.height/10), "Event 2 - B")){
		Debug.Log("event 2 - B");
		FlurryManager.logEvent("Event 2", {"type" : "B", "level" : "1"});
	}
	
	if(GUI.Button(Rect(0, Screen.height*5/10, Screen.width, Screen.height/10), "Error")){
		Debug.Log("sample error");
		FlurryManager.onError("error", "sample error", "Sample Exception");
	}
}