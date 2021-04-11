# BureaucraticLibrary
![Image of Testing Results](/screenshot.png)

Реализованная часть API решает поставленную задачу, хотя есть несколько фич, которые я реализовать не успел.

Основные классы и интерфейсы с которыми можно взаимодействовать:  
`DepartmentBuilder()` - этот класс позволяет создавать отделы без лишних действий, а так же проверяет все данные на корректность. 
* `DepartmentBuilder(int numberOfDepartments, int numberOfStamps)` - конструктор класса требует общее число отделов и число печатей.
* `.GetDepartment(int stampIndex, int eraseIndex, int nextDepartmentIndex)` - метод для создания безусловного отдела
* `.GetDepartment(int conditionIndex, int firstStampIndex, int firstEraseIndex, int firstNextDepartmentIndex, int secondStampIndex, int secondEraseIndex, int secondNextDepartmentIndex)` - метод для получения условного отдела.
Для большей безопасности запрещено создавать отделы без использования билдера.  

`IDepartment` - интерфейс, все отделы его реализуют и именно его возвращают методы `.GetDepartment`   

`OrganizationConfig` - класс, в котором устанавливается начальная конфигурация, в т.ч. подразумевалась возможность выбирать между разными способами реализации решения и хранения данных.
```
var organizationConfig = new OrganizationConfig()
{
    StartDepartment = 1,
    EndDepartment = n,
    NumberOfDepartments = n,
    NumberOfStamps = m,
    SolutionType = SolutionTypes.PreCalculatingSolution,  \\опционально
    ContainerType = DataContainerType.InMemoryDataStorage \\опционально
    Departments = new List<IDepartment>()                 \\тут необходимо указать лист со всеми созданными департаментами 
                                                          \\или добавить их позже
};
```
`Organization(OrganizationConfig config)` - Основной класс, в конструкторе принимает `OrganizationConfig`, после чего изменения в конфигурации влиять на работу не будут.
Этот класс предоставляет доступ к методу `.GetResult(int departmentIndex)`, тот в свою очередь возвращает ещё класс `DepartmentResult`.

`DepartmentResult` - класс, в котором лежат результаты запроса
* `DepartmentStatus Status` - Статус отдела, возможные варианты `{ NotVisited, Visited, InCycle, Inaccessible }`, на самом деле в результатах запроса можно получить только последние 3 статуса.
* `List<Checklist> Checklists ` - Лист сохраненных листков с печатями

`Checklist` - Последний важный для пользователя API класс, в нем храниться состояние листка, его безопасно можно изменять, это копия оригинала, как и поле выше :D.
* `bool InCycle` - Отвечает на вопрос, правда ли, что именно это состояние есть в цикле.
* `List<int> GetAllUncheckedStamps()` - Возвращает список неотмеченых печатей.

Пример использования: 
```
public void Example()
{
    var departmentBuilder = new DepartmentBuilder(5, 5);
    var organizationConfig = new OrganizationConfig()
    {
        StartDepartment = 1,
        EndDepartment = 5,
        NumberOfDepartments = 5,
        NumberOfStamps = 5,
    };
    for (int i = 1; i <= 5; i++)
    {
        organizationConfig.Departments.Add
        (
            departmentBuilder.GetDepartment(i, i % 5 + 1, 5 - i + 1)
        );
    }
    var organization = new Organization(organizationConfig);
    var result = organization.GetResult(3);
    var status = result.Status;
    var checklists = result.Checklists;
}
```
 

