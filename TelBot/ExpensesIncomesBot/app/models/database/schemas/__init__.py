from .schemas import Base

__all__ = ["Base", "create_db"]


async def create_db(engine):
    async with engine.begin() as conn:
        await conn.run_sync(Base.metadata.create_all)
