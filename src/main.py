import discord
import os
from dotenv import load_dotenv

intents = discord.Intents.default()
intents.message_content = True

load_dotenv()
class discord_client(discord.Client):
    async def on_ready(self):
        print(f"Logged in as {self.user.name} (ID: {self.user.id})")

client = discord_client(intents=intents)

client.run(os.getenv("TOKEN"))