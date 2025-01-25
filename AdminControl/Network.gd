extends Node

var http_request: HTTPRequest = HTTPRequest.new()

const SERVER_URL = "http://localhost/project100/dbmediator.php"
const SERVER_HEADERS = ["Content-Type: application/x-www-form-urlencoded", "Cache-Control: max-age=0"]

var request_queue: Array = []
var is_requesting: bool = false

var tempt_data

func _ready():
	add_child(http_request)
	http_request.connect("request_completed", Callable(self, "_http_request_completed"))

func _process(_delta):
	check_request()

func check_request():
	# Check if we are good to send a request:
	if is_requesting:
		return
		
	if request_queue.is_empty():
		return
		
	is_requesting = true
	_send_request(request_queue.pop_front())

func _send_request(request : Dictionary):
	var client = HTTPClient.new()
	var data = client.query_string_from_dict({"data" : JSON.stringify(request['data'])})
	var body = "command=" + request['command'] + "&" + data
	
	# Make request to the server:
	var err = http_request.request(SERVER_URL, SERVER_HEADERS, HTTPClient.METHOD_POST, body)
	
	# Check if there were problems:
	if err != OK:
		printerr("HTTPRequest error: " + str(err))
		return
	
	# Print out request for debugging:
	print("Requesting...\n\tCommand: " + request['command'] + "\n\tBody: " + body)

func _http_request_completed(_result, _response_code, _headers, _body):
	is_requesting = false
	if _result != HTTPRequest.RESULT_SUCCESS:
		printerr("Error w/ connection: " + str(_result))
		return
		
	var response_body = _body.get_string_from_utf8()
	# Grab our JSON and handle any errors reported by our PHP code:
	var test_json_conv = JSON.new()
	test_json_conv.parse(response_body)
	
	var response = test_json_conv.get_data()
	
	if response != null and response.has('error') and response['error'] != "none":
		printerr("We returned error: " + response['error'])
		return
			
	# If not requesting a nonce, we handle all other requests here:
	print("Response Body:\n" + response_body)

func submit_score():
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
	
	var command = "add_score"
	var data = {"score" : score, "username" : username}
	request_queue.push_back({"command" : command, "data" : data})

func get_scores():
	var command = "get_score"
	var data = {"score_offset" : 0, "score_number" : 10}
	request_queue.push_back({"command" : command, "data" : data});
