package com.example.merchantapp.api.merchant;

import com.example.merchantapp.api.auth.AuthApiService;
import com.example.merchantapp.model.ApiResponse;
import com.example.merchantapp.model.auth.AuthResponse;
import com.example.merchantapp.model.merchant.AdminMerchantResponse;
import com.example.merchantapp.service.ApiClient;
import com.example.merchantapp.storage.ShopManager;

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
                               String businessStart,
                               String businessEnd,
                               BigDecimal deliveryFee,
                               BigDecimal minimumOrderAmount,
                               BigDecimal freeDeliveryThreshold,
                               Callback<ApiResponse<AdminMerchantResponse>> callback){
        merchantApi.createMerchant(
                new CreateMerchantRequest(name,
                        province,
                        city,
                        district,
                        detail,
                        businessStart,
                        businessEnd,
                        deliveryFee,
                        minimumOrderAmount,
                        freeDeliveryThreshold)
                ).enqueue(callback);
    }
    public void updateMerchant(String name,
                               String province,
                               String city,
                               String district,
                               String detail,
                               String businessStart,
                               String businessEnd,
                               BigDecimal deliveryFee,
                               BigDecimal minimumOrderAmount,
                               BigDecimal freeDeliveryThreshold,
                               Callback<ApiResponse<AdminMerchantResponse>> callback){
        merchantApi.updateMerchant(
                new UpdateMerchantRequest(ShopManager.getShop().getUuid(),
                        name,
                        province,
                        city,
                        district,
                        detail,
                        businessStart,
                        businessEnd,
                        deliveryFee,
                        minimumOrderAmount,
                        freeDeliveryThreshold)
        ).enqueue(callback);
    }

}
