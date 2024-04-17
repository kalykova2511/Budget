from aiogram import Router, F
from aiogram.filters import CommandStart
from aiogram.fsm.context import FSMContext
from aiogram.types import Message

from ExpensesIncomesBot.app.bot.keyboards import kb_menu
from ExpensesIncomesBot.app.bot.state import AuthorizationState
from ExpensesIncomesBot.app.controller import Controller
from ExpensesIncomesBot.app.enums import Messages
from ExpensesIncomesBot.app.models.database.schemas.schemas import TgToUsers

authorization_router = Router()


@authorization_router.message(CommandStart())
async def start_authorization_or_menu(message: Message, state: FSMContext, controller: Controller):
    user = await controller.user_crud.check_auth(user_id=message.from_user.id)
    if user:
        if isinstance(message, Message):
            await message.answer(Messages.menu.value, reply_markup=kb_menu())
        return
    await message.answer(Messages.input_login.value)
    await state.set_state(AuthorizationState.login)


@authorization_router.message(AuthorizationState.login, F.text)
async def authorization_login(message: Message, state: FSMContext, controller: Controller):
    user_id = await controller.user_crud.authenticate_login(login=message.text)
    if user_id:
        await state.update_data(user_id=user_id)
        await state.set_state(AuthorizationState.password)
        await message.answer(Messages.input_password.value)
        return
    await message.answer(Messages.error_login.value)
    await state.set_state(AuthorizationState.login)


@authorization_router.message(AuthorizationState.password, F.text)
async def authorization_password(message: Message, state: FSMContext, controller: Controller):
    user_id = await controller.user_crud.authenticate_password(password=message.text)
    if user_id:
        data = await state.get_data()
        if data.get("user_id") == user_id:
            await controller.user_crud.finish_auth(TgToUsers(user_id=user_id, tg_id=message.from_user.id))
            await message.answer(Messages.menu.value, reply_markup=kb_menu())
            await state.clear()
            return
    await message.answer(Messages.error_login_password.value)
    await state.set_state(AuthorizationState.login)
