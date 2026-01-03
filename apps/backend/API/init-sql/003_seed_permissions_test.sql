SET NAMES utf8mb4;

INSERT INTO onlineshop.permissions (id, name, display_name, permission_group) VALUES (0, 'AddAdmin', '创建管理员', 'system');
INSERT INTO onlineshop.permissions (id, name, display_name, permission_group) VALUES (1, 'VerifyAdmin', '验证管理员身份', 'system');
INSERT INTO onlineshop.permissions (id, name, display_name, permission_group) VALUES (1000, 'AddProduct', '新增商品', 'shop');
INSERT INTO onlineshop.permissions (id, name, display_name, permission_group) VALUES (1001, 'RemoveProduct', '移除商品', 'shop');
INSERT INTO onlineshop.permissions (id, name, display_name, permission_group) VALUES (1002, 'UpdateProduct', '修改商品', 'shop');
INSERT INTO onlineshop.permissions (id, name, display_name, permission_group) VALUES (1003, 'GetProduct', '查询商品', 'shop');
INSERT INTO onlineshop.permissions (id, name, display_name, permission_group) VALUES (1004, 'GetOrder', '查询订单', 'shop');
INSERT INTO onlineshop.permissions (id, name, display_name, permission_group) VALUES (1005, 'AddMerchantShop', '创建商户', 'shop');
INSERT INTO onlineshop.permissions (id, name, display_name, permission_group) VALUES (1006, 'UpdateMerchantShop', '更新商户', 'shop');
INSERT INTO onlineshop.permissions (id, name, display_name, permission_group) VALUES (1007, 'GetMerchantShop', '查询商户', 'shop');