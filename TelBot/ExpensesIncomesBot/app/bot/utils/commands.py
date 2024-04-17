from aiogram import Bot
from aiogram.types import BotCommand


async def get_commands(bot: Bot):
    commands = [
        BotCommand(
            command="start",
            description="start"
        )
    ]
    await bot.set_my_commands(commands=commands)
