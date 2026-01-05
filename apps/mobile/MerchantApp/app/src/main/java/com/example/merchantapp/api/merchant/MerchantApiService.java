package com.example.merchantapp.api.merchant;

import com.example.merchantapp.model.ApiResponse;
import com.example.merchantapp.model.auth.AuthResponse;
import com.example.merchantapp.model.merchant.AdminMerchantResponse;

import retrofit2.Call;
import retrofit2.Response;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.POST;

public interface MerchantApiService {
    @POST("/api/merchant")
    Call<ApiResponse<AdminMerchantResponse>> createMerchant(@Body )
}
