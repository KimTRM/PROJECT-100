extends VBoxContainer

var datas: Array

func _ready():
	HttpManager.connect("request_completed", _on_scores_received)
	HttpManager.queue_request("get_score")

func _on_scores_received(response):
	datas = response

func _on_button_pressed():
	HttpManager.queue_request("get_score")
	
	var index = 0
	
	for i in datas:
		var labels: Label = Label.new()

		add_child(labels)
		labels.text = str(index) + "      Username: " + str(i["username"]) + "      Score: " + str(i["score"]) 
		index += 1
	print("retrived")

func _on_home_pressed():
	var score = 0
	var username = ""
	
	# Generate a random username
	var con = "bcdfghjklmnpqrstvwxyz"
	var vow = "aeiou"
	username = ""
	for _i in range(3 + randi() % 4):
		var string = con
		if _i % 2 == 0:
			string = vow
		username += string.substr(randi() % string.length(), 1)
		if _i == 0:
			username = username.capitalize()
	score = randi() % 1000
	
	var data = {"score" : score, "username" : username}
	HttpManager.queue_request("add_score", data)
	print("sent")