using System.Collections.Generic;
using Godot;
using Godot.Collections;

public partial class HTTPManager : Node
{
	public static HTTPManager Instance {get; private set;}

	[Signal] public delegate void RequestCompletedEventHandler(Array<Dictionary> response_data);
	[Signal] public delegate void RequestErrorEventHandler(Dictionary response_data);

	private HttpRequest _HttpRequest;
	[Export] private Array<Dictionary> _RequestQueue = new();
	[Export] private bool _IsRequesting = false;
	[Export] public bool CheckError = false;

	// private const string SERVER_URL = "https://kltldev.com/project100/dbmediator.php";
	private const string SERVER_URL = "http://127.0.0.1/project100/dbmediator.php";
	private string[] SERVER_HEADERS = {"Content-Type: application/x-www-form-urlencoded"};

	public readonly System.Collections.Generic.Dictionary<string, string> Commands = new()
	{
		{"GET_USER_ACCOUNT", "get_user_account"},
        {"ADD_USER_ACCOUNT", "add_user_account"},

		{"GET_LESSON", "get_lesson"},
		{"ADD_LESSON", "add_lesson"},
		{"DELETE_LESSON", "delete_lesson"},

        {"GET_QUIZ", "get_quiz"},
		{"GET_SPECIFIC_QUIZ", "get_specific_quiz"},
		{"ADD_QUIZ", "add_quiz"},
		{"DELETE_QUIZ", "delete_quiz"},
	
		{"GET_PLAYER_DATA", "get_player_data"},
		{"ADD_PLAYER_DATA", "add_player_data"},
	};

	public override void _Ready()
	{
		Instance = this;

		_HttpRequest = new HttpRequest();
		AddChild(_HttpRequest);
		_HttpRequest.RequestCompleted += OnRequestCompleted;
		RequestError += OnRequestError;
	}

    public override void _Process(double delta)
	{
		ProcessRequestQueue();
	}

    public void QueueRequest(string command, Dictionary data = null)
	{
		data ??= new Dictionary();
        _RequestQueue.Add(new Dictionary
        {
            { "command", command },
            { "data", data }
        });
	}

	private void SendRequest(Dictionary request)
	{
		HttpClient client = new();

		var data = client.QueryStringFromDict(new Dictionary
        { 
            { "data", Json.Stringify(request.GetValueOrDefault("data", new Dictionary())) }
        });

		var body = $"command={request.GetValueOrDefault("command", "")}&{data}";

		GD.Print(body);

		Error err = _HttpRequest.Request(SERVER_URL, SERVER_HEADERS, HttpClient.Method.Post, body);

		if (err != Error.Ok)
		{
			EmitSignal(SignalName.RequestError, $"HTTP Request Error: {err}");
			_IsRequesting = false;
		}
	}

	private void ProcessRequestQueue()
	{
		if (_IsRequesting || _RequestQueue.Count == 0) 
			return;

		_IsRequesting = true;
		SendRequest(_RequestQueue[0]);
		_RequestQueue.RemoveAt(0);
	}

	private void OnRequestError(Dictionary ErrorMessage)
    {
		if (CheckError)
		{
			GD.PrintErr(ErrorMessage);
		}
    }

	private void OnRequestCompleted(long result, long responseCode, string[] headers, byte[] body)
    {
		_IsRequesting = false;

		if (result != 0)
		{
			EmitSignal(SignalName.RequestError, $"Connection Error: {result}");
			return;
		}

        string ResponseBody = System.Text.Encoding.UTF8.GetString(body);
		Json json = new();

		if (json.Parse(ResponseBody) == Error.Ok)
		{
			var ParsedResponse = json.Data;
			EmitSignal(SignalName.RequestCompleted, ParsedResponse);
		}
		else
		{
			EmitSignal(SignalName.RequestError, "JSON Parse Error");
		}
    }

	public string GenarateId()
	{
		double TimeStamp = Time.GetUnixTimeFromSystem();
		uint RandomPart = GD.Randi() % 100000;
		return $"{TimeStamp}{RandomPart}";
	}
}
