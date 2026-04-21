from flask import Flask, request, jsonify
import json
import requests

app = Flask(__name__)

with open("./MCP/tools.json", "r") as f:
    TOOLS = json.load(f)["tools"]

WEB_API = "http://localhost:5299/recognition"

@app.route("/mcp/call", methods=["POST"])
def mcp_call():
    data = request.json

    tool_name = data.get("tool")
    params = data.get("params", {})

    tool = next((t for t in TOOLS if t["name"] == tool_name), None)

    if not tool:
        return jsonify({"error": "Unknown tool."}), 400

    response = requests.post(f"http://localhost:8000{tool['endpoint']}", json=params)

    return jsonify(response.json())


@app.route("/tools/get_occurrences", methods=["POST"])
def get_occurrences():
    data = request.json

    response = requests.get(
        f"{WEB_API}/occurrences",
        params={
            "objectName": data.get("objectName"),
            "variant": data.get("variant"),
            "from": data.get("from"),
            "to": data.get("to")
        },
        verify=False
    )

    return jsonify(response.json())

@app.route("/tools/get_count", methods=["POST"])
def get_count():
    data = request.json

    response = requests.get(
        f"{WEB_API}/stats/count",
        params={
            "objectName": data.get("objectName"),
            "variant": data.get("variant"),
            "from": data.get("from"),
            "to": data.get("to")
        },
        verify=False
    )
    print(response)
    return jsonify(response.json())

@app.route("/tools/get_most_frequent", methods=["POST"])
def get_most_frequent():
    data = request.json

    response = requests.get(
        f"{WEB_API}/stats/most-frequent",
        params={
            "objectName": data.get("objectName"),
            "variant": data.get("variant"),
            "from": data.get("from"),
            "to": data.get("to")
        },
        verify=False
    )

    return jsonify(response.json())

if __name__ == "__main__":
    app.run(port=8000, debug=True)