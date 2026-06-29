# Демонстрация Database First и Model First

## Database First

1. Откройте проект в Visual Studio.
2. Нажмите `Add` -> `New Item` -> `ADO.NET Entity Data Model`.
3. Выберите `EF Designer from database`.
4. Используйте строку подключения `FlowerShopDb` из `App.config`.
5. Отметьте таблицы `Products`, `Categories`, `Manufacturers`, `ProductAudit`.
6. Visual Studio создаст EDM-модель `.edmx` и классы сущностей по уже подключенной базе.

## Model First

1. Нажмите `Add` -> `New Item` -> `ADO.NET Entity Data Model`.
2. Выберите `Empty EF Designer model`.
3. На дизайнере добавьте сущности `Product`, `Category`, `Manufacturer`.
4. Создайте связи `Category 1 -> * Product` и `Manufacturer 1 -> * Product`.
5. Выполните `Generate Database from Model`.
6. Visual Studio сформирует DDL-скрипт создания БД и сущностные классы по EDM-модели.

В этом проекте основная реализация сделана через Code First: сущности находятся в папке `Models`, контекст `FlowerShopContext` и CRUD-сервис `EntityProductService` находятся в папке `Data`.
