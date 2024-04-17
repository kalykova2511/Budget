import datetime
import logging
import re
from typing import Union, Match

from aiogram import Router, F
from aiogram.exceptions import TelegramBadRequest
from aiogram.fsm.context import FSMContext
from aiogram.types import Message, CallbackQuery

from ExpensesIncomesBot.app.bot.keyboards import kb_menu, kb_categories
from ExpensesIncomesBot.app.bot.state import AddCategory, AddReal
from ExpensesIncomesBot.app.controller import Controller
from ExpensesIncomesBot.app.enums import Const, Messages

menu_router = Router()


@menu_router.callback_query(F.data == "menu")
async def menu(event: Union[CallbackQuery, Message]):
    if isinstance(event, Message):
        await event.answer(Messages.menu.value, reply_markup=kb_menu())
    elif isinstance(event, CallbackQuery):
        try:
            await event.message.edit_text(text=Messages.menu.value, reply_markup=kb_menu())
        except TelegramBadRequest as bad:
            logging.error(bad)


@menu_router.callback_query(F.data.regexp(fr"sel/({Const.incomes.value}|{Const.expenses.value})/(\d+)$").as_('data'))
async def select_category(callback: CallbackQuery, state: FSMContext, data: Match[str]):
    type_, category_id = data.groups()
    await state.update_data(type_=type_, category_id=int(category_id))
    try:
        await callback.message.edit_text(Messages.input_sum.value)
    except TelegramBadRequest as bad:
        logging.error(bad)
    await state.set_state(AddReal.bill)


@menu_router.message(F.text.isdigit(), AddReal.bill)
async def add_bill(message: Message, state: FSMContext):
    await state.update_data(bill=int(message.text))
    await state.set_state(AddReal.date)
    await message.answer(Messages.input_date.value)


@menu_router.message(F.text.regexp(r"\d{2}\.\d{2}\.\d{4}$"), AddReal.date)
async def add_date(message: Message, state: FSMContext, controller: Controller):
    data = await state.get_data()
    await controller.categories_crud.add_real_data(
        Const.type_real.value.get(data.get("type_"))(
            bill=data.get('bill'), category_id=data.get('category_id'),
            date=datetime.datetime.strptime(message.text, "%d.%m.%Y")
        )
    )
    sum_bill = await controller.categories_crud.get_real(message.from_user.id)
    await message.answer(
        Messages.data_success_add.value.format(
            type_=Messages.type_text.value.get(data.get('type_')), sum_bill=sum_bill
        )
    )
    await state.clear()
    await menu(message)


@menu_router.callback_query(F.data.regexp(fr"add_type/({Const.incomes.value}|{Const.expenses.value})$").as_('data'))
async def add_category(callback: CallbackQuery, state: FSMContext, data: Match[str]):
    type_ = data.group(1)
    await state.update_data(type_=type_)
    await state.set_state(AddCategory.name)
    try:
        await callback.message.edit_text(Messages.input_category_name.value)
    except TelegramBadRequest as bad:
        logging.error(bad)


@menu_router.callback_query(F.data.regexp(fr"({Const.incomes.value}|{Const.expenses.value})/(\d+)$").as_('data'))
async def call_get_categories(event: Union[Message, CallbackQuery], controller: Controller, data: Match[str]):
    data, page = data.groups()
    result = await controller.categories_crud.get_categories(
        type_=Const.type_category.value.get(data), tg_user_id=event.from_user.id
    )
    if isinstance(event, Message):
        await event.answer(text=Messages.select_category.value, reply_markup=kb_categories(data, result, int(page)))
        return
    try:
        await event.message.edit_text(
            text=Messages.select_category.value, reply_markup=kb_categories(data, result, int(page))
        )
    except TelegramBadRequest as bad:
        logging.error(bad)


@menu_router.message(AddCategory.name, F.text)
async def add_category_state(message: Message, state: FSMContext, controller: Controller):
    data = await state.get_data()
    user_id = await controller.user_crud.check_auth(user_id=message.from_user.id)
    await controller.categories_crud.add_categories(
        Const.type_category.value.get(data.get('type_'))(name=message.text, user_id=user_id)
    )
    await message.answer(Messages.success_add_category.value)
    await state.clear()
    await call_get_categories(
        message, controller,
        re.match(fr"({Const.incomes.value}|{Const.expenses.value})/(\d+)", f"{data.get('type_')}/0")
    )
