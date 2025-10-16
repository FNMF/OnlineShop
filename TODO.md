#项目TODO备忘录

##TODO
    验证码那一块还没有解决发送短信，有部分是Test。

##TODO
    商户Product类的Controller里面的Product为改成聚合，暂时用DTO存储其详情图。

##TODO
    LocalFileFactory中压缩图片的逻辑未正确处理，后续要考虑到文件大小和文件质量等。

##TODO
    部分地方的命名不规范，后续有时间再重新修改。

##TODO
    API层的状态码返回目前是只有OK或BadRequest，后续可以考虑添加中间件，实现读取Result中的Code在API层返回正确的状态码。

##TODO
    Address类的Detail字段通过Factory创建或更新时使用了AES加密，后续使用的时候再通过AES解密。

##TODO
    微信支付方面回调方面没做完，暂时搁置。

##TODO
    将手机号等信息也AES加密