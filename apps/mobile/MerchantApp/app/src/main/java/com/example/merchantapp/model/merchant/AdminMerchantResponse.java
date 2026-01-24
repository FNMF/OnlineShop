package com.example.merchantapp.model.merchant;

import java.math.BigDecimal;

public class AdminMerchantResponse {
    private String uuid;
    private String name;
    private String province;
    private String city;
    private String district;
    private String detail;
    private String businessStart;
    private String businessEnd;
    private BigDecimal deliveryFee;
    private BigDecimal minimumOrderAmount;
    private BigDecimal freeDeliveryThreshold;
    private boolean isClosed;
    private boolean isAudited;
    public String getUuid(){
        return uuid;
    }

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
        return businessStart;
    }

    public String getBusinessEnd() {
        return businessEnd;
    }

    public BigDecimal getDeliveryFee() {
        return deliveryFee;
    }

    public BigDecimal getMinimumOrderAmount() {
        return minimumOrderAmount;
    }

    public BigDecimal getFreeDeliveryThreshold() {
        return freeDeliveryThreshold;
    }

    public boolean isClosed() {
        return isClosed;
    }

    public boolean isAudited() {
        return isAudited;
    }
}
