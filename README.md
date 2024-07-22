## Тестовое задание
В репозитории лежит WPF проект построенный на подходе MVVM с Dependency injection
Для внедрения зависмостей в WPF формы используется `Locator` и `LocatorFactory`
Для всех сотальных зависимостей в проекте используеться обычный `Microsoft.Extensions.DependencyInjection`

Для работы с мнимым 2D пространством есть класс `ImageService` в котором есть методы инициализации координатной плоскости (`TestApp.Services.ImageService.InitCoordinateSystem`) и методы создания многоугольников (`TestApp.Services.ImageService.CreatePolygon`) и точек(`TestApp.Services.ImageService.CreatePoint`)

Использую текущий репозиторий реализуйте следующий функционал:
1)  Создание нового окна и привязку к нему ViewModel
2)  Окно содержить интерфейс для создания вершин многоугольника по координатам (X;Y) (кол-во вершин не ограничено, может быть как 3 так и 12), а также интерфейс должен содержать меню созадиня точки по координатам (X;Y) 
3)  По нажатию кнопки мы начинаем проверки:
4)  * проверяем фигигу на возможность её существования
    * находится ли внутри фигуры точка, созданая нами ранее
5) Если точка находиться внутри многоугольника создаём линии красного цвета от точки до координаты вершин многоугольника с указанием расстояние от точки до вершины

#### Всем удачного прохождения 