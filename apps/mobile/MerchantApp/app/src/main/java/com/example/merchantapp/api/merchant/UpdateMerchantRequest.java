package com.example.merchantapp.api.merchant;

import java.math.BigDecimal;

public class UpdateMerchantRequest {
    public String name;
    public String province;
    public String city;
    public String district;
    public String detail;
    public String business_start;
    public String business_end;
    public BigDecimal delivery_fee;
    public BigDecimal minimum_order_amount;
    public BigDecimal free_delivery_threshold;
    public UpdateMerchantRequest(String name,
                                 String province,
                                 String city,
                                 String district,
                                 String detail,
                                 String business_start,
                                 String business_end,
                                 BigDecimal delivery_fee,
                                 BigDecimal minimum_order_amount,
                                 BigDecimal free_delivery_threshold){
        this.name = name;
        this.province =province;
        this.city = city;
        this.district =district;
        this.detail = detail;
        this.business_start = business_start;
        this.business_end = business_end;
        this.delivery_fee = delivery_fee;
        this.minimum_order_amount = minimum_order_amount;
        this.free_delivery_threshold = free_delivery_threshold;
    }
}
