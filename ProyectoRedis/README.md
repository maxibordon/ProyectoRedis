## Instrucciones

### Instalar las siguientes dependencias en el proyecto por medio de Nuget

- StackExchange.Redis.Extensions.Core
- StackExchange.Redis.Extensions.AspNetCore
- StackExchange.Redis.Extensions.Newtonsoft

### Instalar Redis en local:

- Instalar docker en windows https://docs.docker.com/desktop/windows/install/
- Ejecutar desde un powershell **docker run --name redis -d -p 5003:6379 redis redis-server --requirepass "PASSWORD"** . Se deberá devolver un container id como por ejemplo **2dabade3f5888e63e94cb692faa7fe925072ddb5e1517f1d3ffdfb3faf98dc24**  
- Para ver redis en local: instalar para windows https://github.com/qishibo/AnotherRedisDesktopManager/releases


