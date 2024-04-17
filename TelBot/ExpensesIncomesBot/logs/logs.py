import logging


def logs(log_dir):
    engine_file = f'{log_dir}/static/logs/sqlalchemy.log'  # Укажите имя файла по вашему выбору
    log_file = f'{log_dir}/static/logs/application.log'  # Укажите имя файла по вашему выбору

    engine_logger = logging.getLogger('sqlalchemy.engine')
    engine_logger.setLevel(logging.INFO)
    file_handler = logging.FileHandler(engine_file, encoding='utf-8')
    file_handler.setLevel(logging.INFO)
    formatter = logging.Formatter('%(asctime)s [%(levelname)s] [%(funcName)s] %(message)s')
    file_handler.setFormatter(formatter)
    engine_logger.addHandler(file_handler)
    engine_logger.propagate = False

    logging.basicConfig(
        handlers=[
            logging.FileHandler(log_file, encoding='utf-8'),  # Обработчик для записи в файл
            logging.StreamHandler(),  # Обработчик для вывода на консоль
        ],
        encoding='utf-8',
        level=logging.INFO,
        format='%(asctime)s [%(levelname)s] [%(funcName)s] %(message)s',
    )
