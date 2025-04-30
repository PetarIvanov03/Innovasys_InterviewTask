# Innovasys_InterviewTask

-InnovasysApp

ASP.NET Core MVC приложение, което зарежда потребители от външно JSON API, позволява добавяне на бележка (Note) и отбелязване като активен (IsActive), и записва информацията в база данни чрез Dapper.

## 📦 Технологии
- ASP.NET Core MVC (.NET 8)
- SQL Server (локална база)
- Dapper
- Bootstrap 5
- EF Core (само за миграции)

## ⚙️ Стартиране
1. Клонирай/отвори проекта в Visual Studio
2. Увери се, че connection string в `appsettings.json` е коректен:
   ```json
   "DefaultConnection": "Server=ИМЕ_НА_СЪРВЪРА;Database=InnovasysAppDB;Trusted_Connection=True;"
   ```
3. Изпълни миграциите:
   ```bash
   Add-Migration InitialCreate
   Update-Database
   ```
4. Стартирай приложението (`F5` или `Ctrl+F5`)

## 🚀 Функционалности
- **Load Data** – зарежда потребители от https://jsonplaceholder.typicode.com/users
- **Save All** – записва всички потребители и техните адреси в базата
- **User/All** – визуализира записаните в базата потребители

## 📄 Структура
- Таблица **Users** – съдържа лична информация, Note, IsActive, CreatedAt
- Таблица **Addresses** – съдържа адрес и геолокация, свързана с UserId (FK)

## 🖱️ Навигация
- Начало: `/User/Index`
- Заредени потребители: бутон **Load Data**
- Записани потребители: линк в менюто към `/User/All`

## 🧑‍💻 Автор
- Петър Иванов

---

> Задачата е изцяло изпълнена по указанията. EF Core се използва само за миграции, а реалният достъп до данните е чрез Dapper.
