from sqlalchemy.ext.asyncio import async_sessionmaker, AsyncSession

from .schemas import create_db

__all__ = [
    "create_connection"
]


async def create_connection(engine):
    await create_db(engine)
    # Создаем асинхронную сессию
    async_session = async_sessionmaker(bind=engine, class_=AsyncSession, expire_on_commit=False)
    return async_session
