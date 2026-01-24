package com.example.merchantapp.ui.shopsettings;

import java.math.BigDecimal;

public class MerchantFormState {
    public String uuid;

    public String name;

    public Integer provinceId;
    public String provinceName;

    public Integer cityId;
    public String cityName;

    public Integer districtId;
    public String districtName;

    public String businessStart; // HH:mm
    public String businessEnd;

    public String detailAddress;

    public BigDecimal deliveryFee;
    public BigDecimal minimumOrderAmount;
    public BigDecimal freeDeliveryThreshold;
}

