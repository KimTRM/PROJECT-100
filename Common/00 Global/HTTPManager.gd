extends Node

signal request_completed(response_data)
signal request_error(error_message)

var http_request: HTTPRequest
var request_queue: Array = []
var is_requesting: bool = false
var check_error: bool = false

const SERVER_URL = "https://kltldev.com/project100/dbmediator.php"
#const SERVER_URL = "http://localhost/project100/dbmediator.php"
const SERVER_HEADERS = ["Content-Type: application/x-www-form-urlencoded", "Cache-Control: max-age=0"]

const COMMANDS: Dictionary = {
	"GET_USER_ACCOUNT" : "get_user_account",
	"ADD_USER_ACCOUNT" : "add_user_account",
	
	"GET_QUIZ" : "get_quiz",
	"ADD_QUIZ" : "add_quiz",
	"DELETE_QUIZ" : "delete_quiz",
	
	"GET_PLAYER_DATA" : "get_player_data",
	"ADD_PLAYER_DATA" : "add_player_data",
	}

func _ready():
	http_request = HTTPRequest.new()
	add_child(http_request)
	http_request.connect("request_completed", Callable(self, "_on_request_completed"))
	connect("request_error", Callable(self, "_on_request_error"))

func _on_request_error(error_message):
	if check_error:
		printerr(error_message)

func _process(_delta):
	process_request_queue()

func process_request_queue():
	if is_requesting or request_queue.is_empty():
		return
	
	is_requesting = true
	send_request(request_queue.pop_front())

func send_request(request: Dictionary):
	var client = HTTPClient.new()
	var data = client.query_string_from_dict({"data": JSON.stringify(request.get('data', {}))})
	var body = "command=" + request.get('command', '') + "&" + data
	
	var err = http_request.request(SERVER_URL, SERVER_HEADERS, HTTPClient.METHOD_POST, body)
	
	if err != OK:
		emit_signal("request_error", "HTTP Request Error: " + str(err))
		is_requesting = false

func queue_request(command: String, data: Dictionary = {}):
	request_queue.push_back({
		"command": command, 
		"data": data
	})

func _on_request_completed(_result, _response_code, _headers, body):
	is_requesting = false
	
	if _result != HTTPRequest.RESULT_SUCCESS:
		emit_signal("request_error", "Connection Error: " + str(_result))
		return
	
	var response_body: String = body.get_string_from_utf8()
	var json = JSON.new()
	
	if json.parse(response_body) == OK:
		var parsed_response = json.get_data()
		emit_signal("request_completed", parsed_response)
	else:
		emit_signal("request_error", "JSON Parse Error")
