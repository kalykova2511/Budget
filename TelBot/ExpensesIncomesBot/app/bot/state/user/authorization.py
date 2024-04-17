from aiogram.fsm.state import StatesGroup, State


class AuthorizationState(StatesGroup):
    login = State()
    password = State()
