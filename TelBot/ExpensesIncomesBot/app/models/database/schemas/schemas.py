from sqlalchemy import Column, Integer, ForeignKey, BIGINT
from sqlalchemy.dialects.mysql import VARCHAR, INTEGER, DATETIME
from sqlalchemy.ext.declarative import declarative_base


Base = declarative_base()


class Users(Base):
    __tablename__ = 'users'
    user_id = Column(INTEGER, primary_key=True)
    login = Column(VARCHAR(50), unique=True)


class Passwords(Base):
    __tablename__ = 'passwords'
    user_id = Column(INTEGER, ForeignKey('users.user_id'), primary_key=True)
    password = Column(VARCHAR(50))


class IncomesCategoies(Base):
    __tablename__ = 'income_categories'
    id = Column(INTEGER, primary_key=True, nullable=False, autoincrement=True, unique=True)
    user_id = Column(INTEGER, ForeignKey('users.user_id'), nullable=False)
    name = Column(VARCHAR(50), nullable=False)


class ExpensesCategoies(Base):
    __tablename__ = 'expenses_categories'
    id = Column(INTEGER, primary_key=True, autoincrement=True, nullable=False, unique=True)
    user_id = Column(INTEGER, ForeignKey('users.user_id'), nullable=False)
    name = Column(VARCHAR(50), nullable=False)


class RealIncomes(Base):
    __tablename__ = 'real_incomes'
    id = Column(INTEGER, primary_key=True, nullable=False, autoincrement=True, unique=True)
    bill = Column(INTEGER)
    date = Column(DATETIME)
    category_id = Column(INTEGER, ForeignKey('income_categories.id'))


class RealExpenses(Base):
    __tablename__ = 'real_expenses'
    id = Column(INTEGER, primary_key=True, nullable=False, autoincrement=True, unique=True)
    bill = Column(INTEGER)
    date = Column(DATETIME)
    category_id = Column(Integer, ForeignKey('expenses_categories.id'))


class TgToUsers(Base):
    __tablename__ = 'tg_to_user'
    tg_id = Column(BIGINT, primary_key=True, unique=True, nullable=False)
    user_id = Column(INTEGER, ForeignKey('users.user_id'), unique=False)
