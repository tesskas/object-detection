from typing import Any
import httpx
from fastmcp import FastMCP

mcp = FastMCP("recognition-mcp")

WEB_API = "https://netapi<>.azurewebsites.net/recognition"

async def get_occurrences(objectName: str| None = None, variant: str| None = None, from_datetime: str | None = None, to_datetime: str | None = None)-> list[dict[str, Any]]:
    async with httpx.AsyncClient() as client:
        r = await client.get(f"{WEB_API}/occurrences", params={
            "objectName": objectName,
            "variant": variant,
            "from": from_datetime,
            "to": to_datetime
        })
        return r.json()

async def get_count(objectName: str| None = None, variant: str| None = None, from_datetime: str| None = None, to_datetime: str| None = None)-> dict[str, Any]:
    async with httpx.AsyncClient() as client:
        r = await client.get(f"{WEB_API}/stats/count", params={
            "objectName": objectName,
            "variant": variant,
            "from": from_datetime,
            "to": to_datetime
        })
        return r.json()

async def get_most_frequent(objectName: str| None = None, variant: str| None = None, from_datetime: str| None = None, to_datetime: str| None = None)-> list[dict[str, Any]]:
    async with httpx.AsyncClient() as client:
        r = await client.get(f"{WEB_API}/stats/most-frequent", params={
            "objectName": objectName,
            "variant": variant,
            "from": from_datetime,
            "to": to_datetime
        })
        return r.json()

mcp.add_tool(get_occurrences)
mcp.add_tool(get_count)
mcp.add_tool(get_most_frequent)

if __name__ == "__main__":
    mcp.run(transport="http", host="0.0.0.0", port=8000)
