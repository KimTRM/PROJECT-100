extends Node

var http_request: HTTPRequest = HTTPRequest.new()

var request_queue: Array = []
var is_requesting: bool = false

var tdata: Array = []

const SERVER_URL = "http://localhost/project100/dbmediator.php"
const SERVER_HEADERS = ["Content-Type: application/x-www-form-urlencoded", "Cache-Control: max-age=0"]

func _ready():
	add_child(http_request)
	http_request.connect("request_completed", Callable(self, "_http_request_completed"))

func _process(_delta):
	process_request_queue()

func process_request_queue():
	if is_requesting or request_queue.is_empty():
		return
	
	is_requesting = true
	_send_request(request_queue.pop_front())

func _send_request(request : Dictionary):
	var client = HTTPClient.new()
	var data = client.query_string_from_dict({"data" : JSON.stringify(request['data'])})
	var body = "command=" + request['command'] + "&" + data
	
	var err = http_request.request(SERVER_URL, SERVER_HEADERS, HTTPClient.METHOD_POST, body)
	
	if err != OK:
		printerr("HTTPRequest error: " + str(err))
		is_requesting = false
		return
	
	print("Requesting...\n\tCommand: " + request['command'] + "\n\tBody: " + body)

func _http_request_completed(_result, _response_code, _headers, _body):
	is_requesting = false
	if _result != HTTPRequest.RESULT_SUCCESS:
		printerr("Error w/ connection: " + str(_result))
		return
		
	var response_body: String = _body.get_string_from_utf8()
	var json: JSON = JSON.new()
	var parse_result = json.parse(response_body)
	
	if parse_result != OK:
		return
	
	var response = json.parse_string(response_body)
	tdata = response
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
