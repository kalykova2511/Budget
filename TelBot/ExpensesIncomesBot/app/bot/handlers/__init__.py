from aiogram import Dispatcher

from .menu import menu_router
from .user.authorization import authorization_router

__all__ = ["menu_router", "init_routers", "authorization_router"]

from ExpensesIncomesBot.app.bot.middlewares import CheckAuthorizationMiddleware
from ExpensesIncomesBot.app.controller import Controller


def init_routers(dp: Dispatcher, controller: Controller):
    menu_router.callback_query.middleware(CheckAuthorizationMiddleware(controller=controller))
    menu_router.message.middleware(CheckAuthorizationMiddleware(controller=controller))
    dp.include_routers(menu_router, authorization_router)
