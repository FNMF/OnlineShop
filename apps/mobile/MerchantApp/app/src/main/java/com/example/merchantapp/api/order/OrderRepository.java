package com.example.merchantapp.api.order;

import com.example.merchantapp.service.ApiClient;

public class OrderRepository {
    private final OrderApiService orderApi;
    public OrderRepository(){
        this.orderApi = ApiClient.getOrderService();
    }
}
