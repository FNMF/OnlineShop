package com.example.merchantapp.api.merchant;

import com.example.merchantapp.api.auth.AuthApiService;
import com.example.merchantapp.model.ApiResponse;
import com.example.merchantapp.model.auth.AuthResponse;
import com.example.merchantapp.model.merchant.AdminMerchantResponse;
import com.example.merchantapp.service.ApiClient;

import java.math.BigDecimal;

import retrofit2.Callback;

public class MerchantRepository {
    private final MerchantApiService merchantApi;
    public MerchantRepository(){
        this.merchantApi = ApiClient.getMerchantService();
    }
    public void getMerchant(Callback<ApiResponse<AdminMerchantResponse>> callback){
        merchantApi.getMerchant().enqueue(callback);
    }
    public void createMerchant(String name,
                               String province,
                               String city,
                               String district,
                               String detail,
                               String business_start,
                               String business_end,
                               BigDecimal delivery_fee,
                               BigDecimal minimum_order_amount,
                               BigDecimal free_delivery_threshold,
                               Callback<ApiResponse<AdminMerchantResponse>> callback){
        merchantApi.createMerchant(
                new CreateMerchantRequest(name,
                        province,
                        city,
                        district,
                        detail,
                        business_start,
                        business_end,
                        delivery_fee,
                        minimum_order_amount,
                        free_delivery_threshold)
                ).enqueue(callback);
    }
    public void updateMerchant(String name,
                               String province,
                               String city,
                               String district,
                               String detail,
                               String business_start,
                               String business_end,
                               BigDecimal delivery_fee,
                               BigDecimal minimum_order_amount,
                               BigDecimal free_delivery_threshold,
                               Callback<ApiResponse<AdminMerchantResponse>> callback){
        merchantApi.updateMerchant(
                new UpdateMerchantRequest(name,
                        province,
                        city,
                        district,
                        detail,
                        business_start,
                        business_end,
                        delivery_fee,
                        minimum_order_amount,
                        free_delivery_threshold)
        ).enqueue(callback);
    }

}
