from enum import Enum

from ExpensesIncomesBot.app.enums import Const


class Messages(Enum):
    menu = "Меню"
    input_incomes = "Внести доход"
    input_expenses = "Внести расход"
    input_login = "Введите логин:"
    error_login = "Неверный логин, введите снова."
    error_login_password = "Неверный логин или пароль, введите логин"
    input_password = "Введите пароль:"
    input_sum = "Введите сумму:"
    input_date = "Введите дату в формате ДД.ММ.ГГГГ"
    input_category_name = "Введите название категории:"
    type_text = {
        Const.incomes.value: "Доход успешно добавлен",
        Const.expenses.value: "Расход успешно добавлен",
    }
    data_success_add = "{type_}\nОстаток по счету: {sum_bill}"
    add_category = "Добавить категорию"
    select_category = "Выберите категорию"
    success_add_category = "Категория добавлена"
