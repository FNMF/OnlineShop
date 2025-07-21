#Changelog
这是一个开发日志，用于记录开发进度以及重要变更

##[0.0.3] - 2025-7-22
架构重构，更贴合DDD架构，运用了事件总线以及更细的解耦。
实现了特性鉴权，无需再在业务中鉴权。
新增了平台登录API。

##[0.0.2] - 2025-7-2
完善了Notification的Service。
修改了部分DTO。

###Added
 - 新增了Notification的Service中的GetNotifications、RemoveNotificationManually、RemoveNotificationAutomatically

###Change
 - 更变了Notification的QueryOptions和DeleteOptions

##[0.0.1] - 2025-6-27
修改了部分数据结构，添加了Notification的对应Delivery表。
完善了部分Notification的Service。
修改了部分JWT的Claim设置。
修改了部分AuthService。

###Added
 - 新增了NotificationService的AddNotification

###Change
 - 更变了数据库中的数据结构
 - 更变了JWT的Claim设置
 - 更变了AuthService与JWT对应的部分

##[0.0.0] - 2025-6-27
将2025-6-27日前的项目初始化，已经完成了数据库结构的初步设置，后端框架的完善，自定义简要Log以及Notification的业务已经完善。

###Added
 - 这是模板，新增功能

###Changed
 - 这是模板，变更/改进已有功能

###Deprecated
 - 这是模板，即将废弃的功能

###Removed
 - 这是模板，移除的功能

###Fixed:
 - 这是模板，Bug 修复

###Security:
 - 这是模板，安全相关修复或更新