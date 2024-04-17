from aiogram.fsm.state import StatesGroup, State


class AddCategory(StatesGroup):
    name = State()


class AddReal(StatesGroup):
    bill = State()
    date = State()
