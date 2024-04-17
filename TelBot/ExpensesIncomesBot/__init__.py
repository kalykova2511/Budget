import asyncio
import os
import sys
from pathlib import Path

from aiogram import Dispatcher, Bot
from aiogram.fsm.storage.memory import MemoryStorage
from dotenv import load_dotenv
from sqlalchemy.ext.asyncio import create_async_engine

from ExpensesIncomesBot.app.bot.handlers import init_routers
from ExpensesIncomesBot.app.bot.utils import get_commands
from ExpensesIncomesBot.app.controller import base_settings
from ExpensesIncomesBot.app.models.database import create_connection
from ExpensesIncomesBot.logs import logs


async def bot_start(engine):
    async_session = await create_connection(engine)
    bot = Bot(token=os.getenv('BOT_TOKEN'))
    storage = MemoryStorage()
    dp = Dispatcher(storage=storage)
    controller = base_settings(async_session)
    dp.workflow_data['controller'] = controller
    init_routers(dp, controller)
    return bot, dp


async def on_startup(base_dir, engine):
    _ = logs(base_dir)
    bot, dp = await bot_start(engine)
    await get_commands(bot)
    try:
        await dp.start_polling(bot)
    finally:
        await bot.session.close()


def start_app():
    base_dir: Path = Path(__file__).resolve().parent.parent
    load_dotenv(base_dir / '.env')

    engine = create_async_engine(
        f"mysql+aiomysql://"
        f"{os.getenv('PG_USERNAME')}:"
        f"{os.getenv('PG_PASSWORD')}@"
        f"{os.getenv('PG_HOST')}:"
        f"{os.getenv('PG_PORT')}/"
        f"{os.getenv('PG_DATABASE')}",
        pool_recycle=3600, pool_pre_ping=True, echo=False
    )
    if sys.platform in ["linux", "darwin"]:
        import uvloop
        asyncio.set_event_loop_policy(uvloop.EventLoopPolicy())
    if sys.platform in ["linux", "darwin"] and "uvloop" in sys.modules:
        uvloop.install()
        uvloop.run(on_startup(base_dir, engine))
    elif sys.platform in ["win32"]:
        asyncio.run(on_startup(base_dir, engine))
