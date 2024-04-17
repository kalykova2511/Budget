from sqlalchemy import select
from sqlalchemy.ext.asyncio import async_sessionmaker

from ExpensesIncomesBot.app.models.database.schemas.schemas import Users, TgToUsers, Passwords


class UserCRUD:
    def __init__(self, async_session: async_sessionmaker) -> None:
        self.session = async_session

    async def authenticate_login(self, login: str) -> int:
        async with self.session() as session:
            user = await session.scalar(select(Users.user_id).filter_by(login=login))
            return user

    async def authenticate_password(self, password: str) -> int:
        async with self.session() as session:
            user = await session.scalar(select(Passwords.user_id).filter_by(password=password))
            return user

    async def check_auth(self, user_id: int) -> int:
        async with self.session() as session:
            user = await session.scalar(select(TgToUsers.user_id).filter_by(tg_id=user_id))
            return user

    async def finish_auth(self, user: TgToUsers):
        async with self.session() as session:
            session.add(user)
            await session.commit()
