#代码规范文档

##文档简介
    这个文档用于规范代码规范，以便为项目提供统一的代码风格、结构和实践指导。
    -代码整体高度一致
    -降低学习和转移成本
    -减少错误
    -便于代码审查

    版本v0.0.3

##代码规范

###项目结构
    Project/
    |-src/
        |-Project.Api/                  //包含Controller层
        |-Project.Domain/               //包含Entities、Enums、IRepositories
        |-Project.Application/          //包含DTOs、IServices、Services
        |-Project.Infrastructure/       //包含DBContext、Repositories
        |-Project.Common/               //包含Helper类、其余对象
    |-tests/
        |-Project.Api.Tests/
        |-Project.Domain.Tests/

###命名规范
                        规范                    例子
    类                  PascalCase              UserService

    方法                PascalCase              GetLogByTimeWithPagingAsync
                        -AGUD+Object+(ByWay)+(WithPaging)+(Async)

    属性                PascalCase              AdminPhone

    本地变量            camelCase               addressUuid

    private字段         _camelCase              _dbContext

    常量                PascalCase              MaxPageNumber

    接口                IPascalCase             IUserRepository

    枚举                lowercase+              unshiped
                        abbreviated Pascal      OT        
    
    注意事项：
        对于方法类，用于Repository层的方法如果是异步则需要添加Async后缀，而对于Service层则不需要，Service层方法名越简单但能表意即可
        
        对于Service中某些方法，如果使用了查询选项这样的传入，则方法名无需添加参数后缀，例如：public async Task<List<Log>> GetLogs(LogQueryOptions queryOptions)
        
        对于枚举类是历史遗留问题，数据库字段太多了不易修改，这里强调枚举类在项目中所有的缩写用大写字母并且在其类中必须明确注释缩写的原意，比如OT表示out of time。

###语法与代码样式
    所有的非简写{}要用换行法表示，例如：
    public string name {get; set;}
    /////////////////////////

    if(condition)
    {
        DoSomethings;
    }
    /////////////////////////

    所有的方法与方法间需要换行一排，例如：
    public async GetUserByUuid(Guid uuid)
    {
        DoSomethings;
    }

    public async AddUserWithOpenId(string openId)
    {
        DoSomethings;
    }
    /////////////////////////

###Exception类
    所有Repository层的方法不使用try&catch；
    所有Service层的方法必须使用try&catch；

    对于try&catch，catch必须写入参数Exception ex，每个Service中必须对Logger类进行依赖注入，并且在catch中使用
    _logger.Log(Warning/Error/...)(ex,"comment")；
    /////////////////////////
    对于有Uuid传入的方法，还需将Uuid一并写入，如：
    try
    {
        DoSomethings；
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "解码地址时出错，UserUuid:{uuid}", useruuid);
    }
    /////////////////////////

###同步/异步类
    对于数据库操作，能使用异步就绝不使用同步。
    例如：
        对于add，使用AddAsync/AddRangeAsync；
        对于update/delete，使用Update/Remove后添加SaveChangesAsync();
        对于query或者get类，使用ToListAsync/FirstOrDefaultAsync；

###日志与依赖注入
    对于Repository层，只注入DBContext，其余的Logger以及Log(自定义日志),都不需要注入；
    对于Service层，注入对应Repository层以及Logger和Log，对于其中的每个方法必须要用Logger以及Log记录；

    自定义Log方法在Log的Service中，使用时可以参考LogService；

###注释类
    简洁注释：
    定义变量时，如果变量名不容易理解，双tab后//注释；
    方法实现类时，在方法名后双tab//注释；
    例子：
    public async Task<List<Address>> GetAllAddressByUuidAsync(Guid useruuid)        //通过uuid查询这个人所有的地址并返回一个地址List

###Git管理
    开始阶段，项目还在逐步重构，生产力不足的情况下，对Git无严格规范，仅需注释完善了什么即可；
    Git提交时：
    对于更新内容：
        "更新了+文件层级+部分名称+更新内容"
        "更新了+大更新总结"
        例如："更新了Infrastructure中Repositories的格式"、"更新了docs中代码规范的命名规范"、"更新了项目格式"
    
    对于新增内容：
        "新增了+文件层级+部分名称+新增内容"
        "新增了+大新增总结"
        例如："新增了Api中Controllers的UserController"、"新增了Jwt验证模块"

    对于删除内容：
        "删除了+文件层级+部分名称+删除内容+删除理由"
        "删除了+大删除总结+删除理由"
        例如："删除了Application中Services的UserService里GetUserByOpenId方法以及其接口因为方法无需实现"、"删除了Power表以及项目中实现因为无需实现"

    对于Bug修复内容：
        "修复了+总结"
        例如："修复了namespace与文件夹目录不匹配的错误"、"修复了部分历史遗留问题"

##变更记录
    v0.0.0
    -初始创建

    v0.0.1
    -更新Git管理的规范

    v0.0.2
    -更新Git管理的规范

    v0.0.3
    -更新了命名规范
    -更新了语法与代码样式