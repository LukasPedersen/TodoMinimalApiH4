<h3>API Overview</h3>


|    Type   |            Path            |    Response    | Secure |
|:---------:|:--------------------------:|:--------------:|:------:|
| Debug Get |             "/"            | "Hello World!" |  False |
|    Get    | "/GetTodoItems/Uncomplete" |   List< ToDo > |  False |
|    Get    |  "/GetTodoItems/Complete"  |   List< ToDo > |  False |
|    Get    |     "/GetTodoItem/{id}"    |      ToDo      |  False |
|    Post   |      "/CreateTodoItem"     |   Status code  |  False |
|    Put    |   "/UpdateTodoitem/{id}"   |   Status code  |  False |
|   Delete  |   "/DeleteTodoitem/{id}"   |   Status code  |  False |

<h3>Dependencies</h3>

| Project |                       Name                       |  Ver. |
|:-------:|:------------------------------------------------:|:-----:|
| TodoApi | Microsoft.AspNetCore.Diagnostics.EntityFramework | 6.0.8 |
| TodoApi |      Microsoft.EntityFrameworkCore.InMemory      | 6.0.8 |
