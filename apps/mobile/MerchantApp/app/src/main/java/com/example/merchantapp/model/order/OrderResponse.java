package com.example.merchantapp.model.order;

import java.math.BigDecimal;
import java.util.List;

public class OrderResponse {
    private String orderUuid;
    private String userUuid;
    private String paymentUuid;
    private BigDecimal total;
    private String status;
    private String shortId;
    private String orderAt; // datetime
    private String merchantAddress;
    private String userAddress;
    private BigDecimal cost;
    private BigDecimal packingCharge;
    private BigDecimal riderCost;
    private String note;
    private String expectedTime;
    private List<OrderItem> items;
    private String riderService;
}
