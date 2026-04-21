import requests
import json

MCP_URL = "http://localhost:8000/mcp/call"

def mock_llm(question):
    if "Kolikrát" in question and "včera" in question and "hrnek" in question:
        return {
            "tool": "get_count",
            "params": {
                "objectName": "hrnek",
                "from": "2026-04-20T00:00:00",
                "to": "2026-04-21T00:00:00"
            }
        }

    if "nejčastější" in question and "poslední týden" in question:
        return {
            "tool": "get_most_frequent",
            "params": {             
                "from": "2026-04-14T00:00:00",
                "to": "2026-04-21T00:00:00"
            }
        }

    if "Co" in question and "pondělí" in question:
        return {
            "tool": "get_occurrences",
            "params": {             
                "from": "2026-04-20T00:00:00",
                "to": "2026-04-21T00:00:00"
            }
        }
    
    return {
        "tool": "get_occurrences",
        "params": {}
    }


while True:
    q = input("Zadej dotaz: ")

    if q == "exit":
        break

    tool_call = mock_llm(q)
    print(tool_call)

    response = requests.post(MCP_URL, json=tool_call)

    print(json.dumps(response.json(), indent=2, ensure_ascii=False))