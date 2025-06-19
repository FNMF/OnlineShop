#项目进度备忘录
##
    权限方面暂鸽，admin和role的中间表以及role和permission的中间表需要明确需求后建立。
    对应admin的RSC暂时不能实现

##
    file类还没完善，导致从下至上导致localfile、image、product，不能完全实现

##
    所有Service层中把查询all改成分页查询，虽然不是所有业务都需要但是统一api很有必要

##
    命名规则要调整，比如业务中的Create要改成Add，符合规范

##
    所有的Repository层只保留Query，而不需要细分，把所有逻辑全部放在Service层里

##
    深刻认识到不提前把架构想明白，后续重构有多麻烦了。。。

##
    要优先完成权限方面，不然后续逻辑基本上都要涉及，必须提前完善。。