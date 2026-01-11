package com.example.merchantapp.api.order;

import com.example.merchantapp.model.ApiResponse;
import com.example.merchantapp.model.order.OrderResponse;

import retrofit2.Call;
import retrofit2.http.GET;

public interface OrderApiService {
    @GET("/api/merchant/orders")
    Call<ApiResponse<OrderResponse>> getOrders();
}
