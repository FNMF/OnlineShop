package com.example.merchantapp.api.merchant;

import java.math.BigDecimal;

public class CreateMerchantRequest {
    public String name;
    public String province;
    public String city;
    public String district;
    public String detail;
    public String businessStart;
    public String businessEnd;
    public BigDecimal deliveryFee;
    public BigDecimal minimumOrderAmount;
    public BigDecimal freeDeliveryThreshold;
    public CreateMerchantRequest(String name,
                                 String province,
                                 String city,
                                 String district,
                                 String detail,
                                 String businessStart,
                                 String businessEnd,
                                 BigDecimal deliveryFee,
                                 BigDecimal minimumOrderAmount,
                                 BigDecimal freeDeliveryThreshold){
        this.name = name;
        this.province =province;
        this.city = city;
        this.district =district;
        this.detail = detail;
        this.businessStart = businessStart;
        this.businessEnd = businessEnd;
        this.deliveryFee = deliveryFee;
        this.minimumOrderAmount = minimumOrderAmount;
        this.freeDeliveryThreshold = freeDeliveryThreshold;
    }
}
