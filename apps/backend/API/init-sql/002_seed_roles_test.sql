SET NAMES utf8mb4;

INSERT INTO onlineshop.roles (id, name, is_build_in, display_name, role_type) VALUES (0, 'developer', 0, '开发人员', 'system');
INSERT INTO onlineshop.roles (id, name, is_build_in, display_name, role_type) VALUES (1, 'top_admin', 1, '最高管理员', 'system');
INSERT INTO onlineshop.roles (id, name, is_build_in, display_name, role_type) VALUES (2, 'system_admin', 1, '系统管理员', 'system');
INSERT INTO onlineshop.roles (id, name, is_build_in, display_name, role_type) VALUES (3, 'platform_admin', 1, '平台管理员', 'platform');
INSERT INTO onlineshop.roles (id, name, is_build_in, display_name, role_type) VALUES (4, 'platform_support', 1, '平台客服', 'platform');
INSERT INTO onlineshop.roles (id, name, is_build_in, display_name, role_type) VALUES (5, 'platform_warehouse', 1, '平台仓管', 'platform');
INSERT INTO onlineshop.roles (id, name, is_build_in, display_name, role_type) VALUES (6, 'platform_finance', 1, '平台财务', 'platform');
INSERT INTO onlineshop.roles (id, name, is_build_in, display_name, role_type) VALUES (7, 'platform_marketing', 1, '平台运营', 'platform');
INSERT INTO onlineshop.roles (id, name, is_build_in, display_name, role_type) VALUES (201, 'shop_owner', 1, '商户管理员', 'shop');
INSERT INTO onlineshop.roles (id, name, is_build_in, display_name, role_type) VALUES (202, 'shop_staff', 1, '商户员工', 'shop');
INSERT INTO onlineshop.roles (id, name, is_build_in, display_name, role_type) VALUES (203, 'shop_noservice', 1, '没服务的商户', 'shop');
INSERT INTO onlineshop.roles (id, name, is_build_in, display_name, role_type) VALUES (1000, 'pre_void', 1, '留空分割', 'system');
