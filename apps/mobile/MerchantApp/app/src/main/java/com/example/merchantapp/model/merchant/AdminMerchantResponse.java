package com.example.merchantapp.model.merchant;

import java.math.BigDecimal;

public class AdminMerchantResponse {
    private String name;
    private String province;
    private String city;
    private String district;
    private String detail;
    private String business_start;
    private String business_end;
    private BigDecimal delivery_fee;
    private BigDecimal minimum_order_amount;
    private BigDecimal free_delivery_threshold;
    private boolean is_closed;
    private boolean is_audited;

    public String getName() {
        return name;
    }

    public String getProvince() {
        return province;
    }

    public String getCity() {
        return city;
    }

    public String getDistrict() {
        return district;
    }

    public String getDetail() {
        return detail;
    }

    public String getBusinessStart() {
        return business_start;
    }

    public String getBusinessEnd() {
        return business_end;
    }

    public BigDecimal getDeliveryFee() {
        return delivery_fee;
    }

    public BigDecimal getMinimumOrderAmount() {
        return minimum_order_amount;
    }

    public BigDecimal getFreeDeliveryThreshold() {
        return free_delivery_threshold;
    }

    public boolean isClosed() {
        return is_closed;
    }

    public boolean isAudited() {
        return is_audited;
    }
}
