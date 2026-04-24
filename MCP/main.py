from mcp.server import Server
from mcp.server.stdio import stdio_server
from mcp import types
import httpx
import asyncio
import json
import logging
import sys

app = Server("recognition-mcp")

WEB_API = "http://localhost:5299/recognition"

with open("<complete-path>\\MCP\\tools.json", "r") as f:
    TOOLS = json.load(f)["tools"]

logging.basicConfig(
    level=logging.INFO,
    stream=sys.stderr,
    format="%(asctime)s [%(levelname)s] %(message)s"
)
logger = logging.getLogger(__name__)


@app.list_tools()
async def list_tools() -> list[types.Tool]:
    tools = []
    for tool in TOOLS:
        tools.append(
            types.Tool(
                name=tool["name"],
                description=tool["description"],
                inputSchema=tool["inputSchema"]
            )
        )
    return tools


@app.call_tool()
async def call_tool(name: str, arguments: dict):
    async with httpx.AsyncClient(verify=False) as client:
        endpoint_map = {
            "get_occurrences": "/occurrences",
            "get_count": "/stats/count",
            "get_most_frequent": "/stats/most-frequent"
        }
        
        if name not in endpoint_map:
            raise ValueError(f"Neznámý nástroj: {name}")
        

        logger.info(f"URL: {WEB_API}{endpoint_map[name]}")
        logger.info(f"ARGUMENTS: {str(arguments)}")

        response = await client.get(
            f"{WEB_API}{endpoint_map[name]}",
            params=arguments
        )
        
        logger.info(f"RESPONSE STATUS: {str(response.status_code)}")
        logger.info(f"RESPONSE TEXT: {str(response.text)}")
        
        return [types.TextContent(type="text", text=str(response.json()))]

async def main():
    async with stdio_server() as (read_stream, write_stream):
        await app.run(read_stream, write_stream, app.create_initialization_options())

if __name__ == "__main__":
    asyncio.run(main())