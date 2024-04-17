import math
from typing import List, Union

from aiogram.types import InlineKeyboardButton
from aiogram.utils.keyboard import InlineKeyboardBuilder

from ExpensesIncomesBot.app.enums import Const, Messages
from ExpensesIncomesBot.app.models.database.schemas.schemas import ExpensesCategoies, IncomesCategoies


def kb_menu():
    builder = InlineKeyboardBuilder()
    builder.row(
        InlineKeyboardButton(text=Messages.input_incomes.value, callback_data=f"{Const.incomes.value}/0"),
        InlineKeyboardButton(text=Messages.input_expenses.value, callback_data=f"{Const.expenses.value}/0"),
        width=1
    )
    return builder.as_markup()


def stub_buttons():
    return [InlineKeyboardButton(text="1", callback_data=f"_"), InlineKeyboardButton(text="⬅️", callback_data=f"_"),
            InlineKeyboardButton(text=f"1", callback_data=f"_"), InlineKeyboardButton(text="➡️", callback_data=f"_"),
            InlineKeyboardButton(text=f"1", callback_data=f"_")]


def add_buttons(
        categories: List[Union[ExpensesCategoies, IncomesCategoies]],
        page_size: int, type_: str, page: int, last_page_number: int
):
    arr = []
    if math.ceil(len(categories) / page_size) == 1:
        return stub_buttons()
    if page != 0:
        arr.append(InlineKeyboardButton(text="1", callback_data=f"{type_}/0"))
    else:
        arr.append(InlineKeyboardButton(text='1', callback_data=f'_'))
    arr.append(
        InlineKeyboardButton(text="⬅️", callback_data=f"{type_}/{(page - 1) % (len(categories) // page_size + 1)}")
    )
    arr.append(InlineKeyboardButton(text=f"{page + 1}", callback_data=f"_"))
    arr.append(
        InlineKeyboardButton(text="➡️", callback_data=f"{type_}/{(page + 1) % (len(categories) // page_size + 1)}")
    )
    if page != last_page_number:
        arr.append(
            InlineKeyboardButton(text=f"{last_page_number + 1}", callback_data=f"{type_}/{last_page_number}")
        )
    else:
        arr.append(InlineKeyboardButton(text=f"{last_page_number + 1}", callback_data=f"_"))
    return arr


def kb_categories(
        type_: str, categories: List[Union[ExpensesCategoies, IncomesCategoies]], page: int, page_size: int = 5
):
    builder = InlineKeyboardBuilder()
    start_index = page * page_size
    end_index = start_index + page_size
    paginated_categories = list(categories)[start_index:end_index]
    last_page_number = len(categories) // page_size
    for category in paginated_categories:
        builder.row(InlineKeyboardButton(text=category.name, callback_data=f"sel/{type_}/{category.id}"))  # select_type
    arr = add_buttons(categories, page_size, type_, page, last_page_number)
    builder.row(InlineKeyboardButton(text=Messages.add_category.value, callback_data=f"add_type/{type_}"))
    builder.row(*arr)
    builder.row(InlineKeyboardButton(text=Messages.menu.value, callback_data="menu"))
    return builder.as_markup()
