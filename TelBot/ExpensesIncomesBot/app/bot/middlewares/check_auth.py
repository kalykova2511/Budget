from typing import Callable, Dict, Any, Awaitable, Union

from aiogram import BaseMiddleware
from aiogram.types import TelegramObject, CallbackQuery, Message

from ExpensesIncomesBot.app.controller import Controller


class CheckAuthorizationMiddleware(BaseMiddleware):
    def __init__(self, controller: Controller):
        self.controller = controller

    async def __call__(
            self,
            handler: Callable[[TelegramObject, Dict[str, Any]], Awaitable[Any]],
            event: Union[CallbackQuery, Message],
            data: Dict[str, Any],
    ):
        user = await self.controller.user_crud.check_auth(event.from_user.id)
        if not user:
            if isinstance(event, CallbackQuery):
                await event.message.answer("Вы не авторизованы, для авторизации нажмите /start")
                await event.message.delete()
            else:
                await event.answer("Вы не авторизованы, для авторизации нажмите /start")
            await data.get('state').clear()
            return
        return await handler(event, data)
