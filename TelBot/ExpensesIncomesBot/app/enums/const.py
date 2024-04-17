from enum import Enum

from ExpensesIncomesBot.app.models.database.schemas.schemas import IncomesCategoies, ExpensesCategoies, RealIncomes, \
    RealExpenses


class Const(Enum):
    incomes = "incomes"
    expenses = "expenses"
    type_category = {
        incomes: IncomesCategoies,
        expenses: ExpensesCategoies,
    }

    type_real = {
        incomes: RealIncomes,
        expenses: RealExpenses,
    }
