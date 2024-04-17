from .controller import Controller

__all__ = [
    'Controller',
    'base_settings'
]


def base_settings(async_session):
    controller_ = Controller(async_session=async_session)
    return controller_
