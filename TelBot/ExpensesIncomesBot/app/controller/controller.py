from sqlalchemy.ext.asyncio import async_sessionmaker

from ExpensesIncomesBot.app.controller.crud.categories import CategoriesCRUD
from ExpensesIncomesBot.app.controller.crud.user import UserCRUD


class Controller:
    def __init__(self, async_session: async_sessionmaker):
        self.user_crud = UserCRUD(async_session)
        self.categories_crud = CategoriesCRUD(async_session)
