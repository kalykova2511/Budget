from typing import Union, Iterable

from sqlalchemy import select
from sqlalchemy.ext.asyncio import async_sessionmaker

from ExpensesIncomesBot.app.models.database.schemas.schemas import IncomesCategoies, ExpensesCategoies, TgToUsers, \
    Users, RealIncomes, \
    RealExpenses


class CategoriesCRUD:
    def __init__(self, async_session: async_sessionmaker) -> None:
        self.session = async_session

    async def get_real(self, tg_user_id):
        async with self.session() as session:
            real_incomes = await session.scalars(
                select(RealIncomes.bill).where(
                    RealIncomes.category_id.in_(
                        select(IncomesCategoies.id).where(
                            TgToUsers.tg_id == tg_user_id,
                            TgToUsers.user_id == Users.user_id,
                            Users.user_id == IncomesCategoies.user_id
                        )
                    )
                )
            )
            real_expenses = await session.scalars(
                select(RealExpenses.bill).where(
                    RealExpenses.category_id.in_(
                        select(ExpensesCategoies.id).where(
                            TgToUsers.tg_id == tg_user_id,
                            TgToUsers.user_id == Users.user_id,
                            Users.user_id == ExpensesCategoies.user_id
                        )
                    )
                )
            )
            return sum(real_incomes.all()) - sum(real_expenses.all())

    async def get_categories(self, type_: Union[ExpensesCategoies, IncomesCategoies], tg_user_id) -> (
            Iterable)[Union[ExpensesCategoies, IncomesCategoies]]:
        async with self.session() as session:
            category = await session.scalars(
                select(type_).where(
                    TgToUsers.tg_id == tg_user_id,
                    TgToUsers.user_id == Users.user_id,
                    Users.user_id == type_.user_id
                )
            )
            return category.all()

    async def add_categories(self, type_: Union[ExpensesCategoies, IncomesCategoies]):
        async with self.session() as session:
            session.add(type_)
            await session.commit()

    async def add_real_data(self, type_: Union[RealIncomes, RealExpenses]):
        async with self.session() as session:
            session.add(type_)
            await session.commit()
