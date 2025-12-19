CREATE DATABASE IF NOT EXISTS onlineshop CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE onlineshop;


create table if not exists admins
(
    uuid            binary(16)           not null
        primary key,
    account         int auto_increment,
    phone           varchar(20)          not null,
    salt            varchar(255)         not null,
    password_hash   varchar(255)         not null,
    last_location   varchar(255)         not null,
    last_login_time datetime             not null,
    is_deleted      tinyint(1) default 0 not null,
    constraint admin_pk
        unique (account),
    constraint admin_pk2
        unique (phone)
);

create table if not exists auditgroups
(
    uuid           binary(16) not null
        primary key,
    submitter_uuid binary(16) not null,
    created_at     datetime   not null,
    is_single      tinyint(1) not null,
    is_deleted     tinyint(1) not null
);

create table if not exists audits
(
    uuid           binary(16)                                                  not null
        primary key,
    object_uuid    binary(16)                                                  not null,
    audit_type     enum ('store', 'product', 'banner', 'comment', 'promotion') not null,
    submitter_uuid binary(16)                                                  not null,
    submiter_type  enum ('merchant', 'user')                                   not null,
    audit_status   enum ('pending', 'approval', 'rejection') default 'pending' not null,
    reason         varchar(255)                                                not null,
    auditor_uuid   binary(16)                                                  not null,
    created_at     datetime                                                    not null,
    reviewed_at    datetime                                                    not null,
    group_uuid     binary(16)                                                  not null,
    is_deleted     tinyint(1)                                                  not null,
    constraint audit_auditgroup_ag_uuid_fk
        foreign key (group_uuid) references auditgroups (uuid)
);

create table if not exists coupons
(
    id            int auto_increment
        primary key,
    title         varchar(50)                         not null,
    coupon_type   enum ('FM', 'discount')             not null,
    value         decimal(5, 2)                       not null,
    min_spent     decimal(8, 2)                       not null,
    start_date    datetime                            not null,
    end_date      datetime                            not null,
    total_count   int                                 not null,
    `limit`       int                                 not null,
    coupon_status enum ('NA', 'A', 'OT') default 'NA' not null,
    is_deleted    tinyint(1)             default 0    not null,
    is_audited    tinyint(1)             default 0    not null
);

create table if not exists localfiles
(
    uuid                  binary(16)                                                                         not null
        primary key,
    name                  varchar(255)                                                                       not null,
    path                  varchar(255)                                                                       not null,
    localfile_type        enum ('image', 'video', 'audio', 'log', 'other')                                   not null,
    created_at            datetime                                                                           not null,
    is_deleted            tinyint(1) default 0                                                               not null,
    object_uuid           binary(16)                                                                         null,
    localfile_object_type enum ('merchant', 'product_cover', 'product_detail', 'user', 'platform', 'system') not null,
    mime_type             varchar(50)                                                                        not null,
    size                  bigint                                                                             not null,
    is_audited            tinyint(1) default 0                                                               not null,
    uploader_uuid         binary(16)                                                                         not null,
    upload_ip             varchar(50)                                                                        not null,
    sort_number           int                                                                                not null
);

create table if not exists logs
(
    uuid        binary(16)                                                                                         not null
        primary key,
    log_type    enum ('bp', 'credit', 'order', 'refund', 'user', 'product', 'admin', 'file', 'merchant', 'coupon') not null,
    object_uuid binary(16)                                                                                         null,
    description varchar(255)                                                                                       not null,
    data_json   varchar(255)                                                                                       null,
    created_at  datetime                                                                                           not null,
    detail      varchar(255)                                                                                       not null
);

create table if not exists merchants
(
    uuid                    binary(16)           not null
        primary key,
    name                    varchar(50)          not null,
    province                varchar(50)          not null,
    city                    varchar(50)          not null,
    district                varchar(50)          not null,
    detail                  varchar(255)         not null,
    business_start          time                 not null,
    business_end            time                 not null,
    admin_uuid              binary(16)           not null,
    is_closed               tinyint(1) default 1 not null,
    is_deleted              tinyint(1) default 0 not null,
    is_audited              tinyint(1) default 0 not null,
    delivery_fee            decimal(8, 2)        not null,
    minimum_order_amount    decimal(8, 2)        not null,
    free_delivery_threshold decimal(8, 2)        null,
    constraint merchant_admin_admin_uuid_fk
        foreign key (admin_uuid) references admins (uuid)
);

create table if not exists notifications
(
    uuid                       binary(16)                                               not null
        primary key,
    title                      varchar(255)                                             not null,
    content                    varchar(255)                                             not null,
    notification_type          enum ('order', 'system', 'activity')                     not null,
    start_time                 datetime                                                 not null,
    is_deleted                 tinyint(1) default 0                                     not null,
    notification_receiver_type enum ('user', 'merchant', 'alluser', 'allmerchant')      not null,
    notification_sender_type   enum ('user', 'merchant', 'platform', 'system', 'other') not null,
    sender_uuid                binary(16)                                               null,
    end_time                   datetime                                                 not null,
    is_audited                 tinyint(1) default 0                                     not null,
    created_at                 datetime                                                 not null,
    object_uuid                binary(16)                                               null
);

create table if not exists deliveries
(
    uuid              binary(16)           not null
        primary key,
    notification_uuid binary(16)           not null,
    receiver_uuid     binary(16)           not null,
    is_read           tinyint(1) default 0 not null,
    created_at        datetime             not null,
    read_at           datetime             null,
    constraint delivery_notification_notification_uuid_fk
        foreign key (notification_uuid) references notifications (uuid)
);

create table if not exists permissions
(
    id               int auto_increment
        primary key,
    name             varchar(50)                                                                                                  not null,
    display_name     varchar(50)                                                                                                  not null,
    permission_group enum ('user', 'product', 'order', 'shop', 'warehouse', 'marketing', 'support', 'finance', 'system', 'other') not null
);

create table if not exists privileges
(
    id           int auto_increment
        primary key,
    name         varchar(50) not null,
    display_name varchar(50) not null
);

create table if not exists products
(
    uuid          binary(16)           not null
        primary key,
    name          varchar(30)          not null,
    price         decimal(8, 2)        not null,
    stock         int                  not null,
    description   varchar(255)         null,
    is_available  tinyint(1) default 0 not null,
    ingredient    varchar(255)         null,
    weight        varchar(255)         not null,
    is_listed     tinyint(1) default 0 not null,
    merchant_uuid binary(16)           not null,
    cover_url     varchar(255)         null,
    created_at    datetime             not null,
    is_deleted    tinyint(1) default 0 not null,
    is_audited    tinyint(1) default 0 not null,
    packing_fee   decimal(8, 2)        not null,
    id            int                  null,
    constraint product_merchant_merchant_uuid_fk
        foreign key (merchant_uuid) references merchants (uuid)
);

create definer = root@localhost trigger before_insert_product
    before insert
    on products
    for each row
BEGIN
    DECLARE last_no INT;

    -- 查找该商户当前最大编号
    SELECT COALESCE(MAX(id), 0) INTO last_no
    FROM products
    WHERE merchant_uuid = NEW.merchant_uuid;

    -- 新记录编号 = 当前最大 + 1
    SET NEW.id = last_no + 1;
END;

create table if not exists refresh_tokens
(
    uuid        binary(16)   not null
        primary key,
    target_uuid binary(16)   not null,
    expires_at  datetime     not null,
    token       varchar(255) not null,
    is_revoked  tinyint(1)   not null
);

create table if not exists roles
(
    id           int auto_increment
        primary key,
    name         varchar(20)                         not null,
    is_build_in  tinyint(1) default 0                not null,
    display_name varchar(20)                         not null,
    role_type    enum ('system', 'platform', 'shop') not null
);

create table if not exists admin_role
(
    ar_id        int auto_increment
        primary key,
    ar_adminuuid binary(16) not null,
    ar_roleid    int        not null,
    constraint admin_role_admin_admin_uuid_fk
        foreign key (ar_adminuuid) references admins (uuid),
    constraint admin_role_role_role_id_fk
        foreign key (ar_roleid) references roles (id)
);

create table if not exists role_permission
(
    rp_id           int auto_increment
        primary key,
    rp_roleid       int not null,
    rp_permissionid int not null,
    constraint role_permission_permission_permission_id_fk
        foreign key (rp_permissionid) references permissions (id),
    constraint role_permission_role_role_id_fk
        foreign key (rp_roleid) references roles (id)
);

create table if not exists users
(
    name        varchar(30)           not null,
    uuid        binary(16)            not null
        primary key,
    open_id     char(28)              not null,
    bonus_point int        default 0  not null,
    credit      int        default 50 not null,
    created_at  datetime              not null,
    is_deleted  tinyint(1) default 0  not null,
    phone       varchar(20)           not null
);

create table if not exists addresses
(
    uuid       binary(16)           not null
        primary key,
    user_uuid  binary(16)           not null,
    name       varchar(50)          not null,
    phone      varchar(255)         not null,
    province   varchar(50)          not null,
    city       varchar(50)          not null,
    district   varchar(50)          not null,
    detail     varchar(255)         not null,
    updated_at datetime             not null,
    is_deleted tinyint(1) default 0 not null,
    constraint address_user_user_uuid_fk
        foreign key (user_uuid) references users (uuid)
);

create table if not exists carts
(
    uuid          binary(16)           not null
        primary key,
    user_uuid     binary(16)           not null,
    updated_at    datetime             not null,
    merchant_uuid binary(16)           not null,
    is_deleted    tinyint(1) default 0 not null,
    constraint cart_user_user_uuid_fk
        foreign key (user_uuid) references users (uuid)
);

create table if not exists cartitems
(
    uuid          binary(16)           not null
        primary key,
    cart_uuid     binary(16)           not null,
    product_uuid  binary(16)           not null,
    quantity      int                  not null,
    product_name  varchar(255)         not null,
    product_price decimal(8, 2)        not null,
    product_cover varchar(255)         not null,
    created_at    datetime             not null,
    is_deleted    tinyint(1) default 0 not null,
    packing_fee   decimal(8, 2)        not null,
    constraint cartitem_cart_cart_uuid_fk
        foreign key (cart_uuid) references carts (uuid)
);

create table if not exists user_coupons
(
    uuid               binary(16)                                                   not null
        primary key,
    coupon_id          int                                                          not null,
    user_uuid          binary(16)                                                   not null,
    received_at        datetime                                                     not null,
    used_at            datetime                                                     not null,
    user_coupon_status enum ('unused', 'used', 'OT', 'invalidity') default 'unused' not null,
    discount_value     decimal(8, 2)                               default 0.00     null,
    is_deleted         tinyint(1)                                  default 0        not null,
    constraint usercoupon_coupon_coupon_id_fk
        foreign key (coupon_id) references coupons (id),
    constraint usercoupon_user_user_uuid_fk
        foreign key (user_uuid) references users (uuid)
);

create table if not exists orders
(
    uuid                binary(16)                                                                                                                        not null
        primary key,
    user_uuid           binary(16)                                                                                                                        not null,
    total               decimal(8, 2)                                                                                                                     not null,
    order_status        enum ('created', 'paid', 'accepted', 'prepared', 'shipped', 'completed', 'cancelled', 'rejected', 'exception') default 'created'  not null invisible,
    short_id            varchar(10)                                                                                                                       null,
    created_at          datetime                                                                                                                          not null,
    merchant_address    varchar(255)                                                                                                                      not null,
    user_address        varchar(255)                                                                                                                      not null,
    user_coupon_uuid    binary(16)                                                                                                                        null,
    order_rider_service enum ('scheduled', 'immediate', 'preorder', 'pickup')                                                                             not null,
    rider               varchar(255)                                                                                                                      not null,
    note                text                                                                                                                              null,
    list_cost           decimal(8, 2)                                                                                                                     not null,
    packing_cost        decimal(8, 2)                                                                                                                     not null,
    rider_cost          decimal(8, 2)                                                                                                                     not null,
    expected_time       varchar(255)                                                                                                   default '顺序配送' not null,
    channel             enum ('alipay', 'wechat', 'bank', 'instore', 'other')                                                          default 'wechat'   not null,
    is_deleted          tinyint(1)                                                                                                     default 0          not null,
    payment_uuid        binary(16)                                                                                                                        null,
    constraint order_user_user_uuid_fk
        foreign key (user_uuid) references users (uuid),
    constraint order_usercoupon_up_uuid_fk
        foreign key (user_coupon_uuid) references user_coupons (uuid)
);

create table if not exists orderitems
(
    uuid          binary(16)    not null
        primary key,
    order_uuid    binary(16)    not null,
    product_uuid  binary(16)    not null,
    quantity      int           not null,
    price         decimal(8, 2) not null,
    name          varchar(50)   not null,
    packing_fee   decimal(8, 2) not null,
    merchant_uuid binary(16)    not null,
    constraint orderitem_order_order_uuid_fk
        foreign key (order_uuid) references orders (uuid),
    constraint orderitem_product_product_uuid_fk
        foreign key (product_uuid) references products (uuid)
);

create table if not exists payments
(
    uuid               binary(16)                                                              not null
        primary key,
    order_uuid         binary(16)                                                              not null,
    amout              decimal(8, 2)                                                           not null,
    currency           varchar(20)                                                             null,
    payment_status     enum ('pending', 'accepted', 'rejected', 'exception') default 'pending' not null,
    created_at         datetime                                                                not null,
    app_id             varchar(255)                                                            not null,
    mch_id             varchar(255)                                                            not null,
    out_trade_no       varchar(255)                                                            not null,
    transaction_id     varchar(255)                                                            null,
    trade_type         varchar(255)                                                            null,
    bank_type          varchar(255)                                                            null,
    open_id            varchar(255)                                                            not null,
    attach             varchar(255)                                                            null,
    success_time       datetime                                                                null,
    raw_call_back_data varchar(255)                                                            null,
    constraint payment_order_order_uuid_fk
        foreign key (order_uuid) references orders (uuid)
);

create table if not exists refunds
(
    uuid               binary(16)                                                   not null
        primary key,
    user_uuid          binary(16)                                                   not null,
    order_uuid         binary(16)                                                   not null,
    refund_type        enum ('returnof', 'refund', 'discount')                      not null,
    reason             varchar(300)                                                 null,
    refund_status      enum ('create', 'review', 'pass', 'refuse') default 'create' not null,
    amount             decimal(8, 2)                               default 0.00     not null,
    update_user_credit int                                                          not null,
    created_at         datetime                                                     not null,
    is_deleted         tinyint(1)                                  default 0        not null,
    constraint refund_order_order_uuid_fk
        foreign key (order_uuid) references orders (uuid),
    constraint refund_user_user_uuid_fk
        foreign key (user_uuid) references users (uuid)
);

create table if not exists user_privilege
(
    up_uuid        binary(16) not null
        primary key,
    up_useruuid    binary(16) not null,
    up_privilegeid int        not null,
    constraint user_privilege_privilege_privilege_id_fk
        foreign key (up_privilegeid) references privileges (id),
    constraint user_privilege_user_user_uuid_fk
        foreign key (up_useruuid) references users (uuid)
);

create table if not exists wallet_accounts
(
    uuid           binary(16)     not null
        primary key,
    merchant_uuid  binary(16)     not null,
    available      decimal(10, 2) not null,
    frozen         decimal(10, 2) not null,
    total_income   decimal(10, 2) not null,
    total_withdraw decimal(10, 2) not null,
    updated_at     datetime       not null,
    constraint wallet_account_merchant_merchant_uuid_fk
        foreign key (merchant_uuid) references merchants (uuid)
);

create table if not exists wallet_requests
(
    uuid                  binary(16)                                       not null
        primary key,
    merchant_uuid         binary(16)                                       not null,
    amount                decimal(10, 2)                                   not null,
    wallet_request_status enum ('pending', 'approved', 'rejected', 'paid') not null,
    reason                varchar(255)                                     null,
    audited_at            datetime                                         null,
    transfer_time         datetime                                         null,
    created_at            datetime                                         not null,
    constraint walletrequest_merchant_merchant_uuid_fk
        foreign key (merchant_uuid) references merchants (uuid)
);

create table if not exists wallet_transactions
(
    uuid                    binary(16)                                      not null
        primary key,
    merchant_uuid           binary(16)                                      not null,
    wallet_transaction_type enum ('income', 'withdraw', 'refund', 'charge') not null,
    amount                  decimal(10, 2)                                  not null,
    `before`                decimal(10, 2)                                  not null,
    after                   decimal(10, 2)                                  not null,
    object_uuid             binary(16)                                      null,
    remark                  varchar(255)                                    null,
    created_at              datetime                                        not null,
    constraint wallettransaction_merchant_merchant_uuid_fk
        foreign key (merchant_uuid) references merchants (uuid)
);



