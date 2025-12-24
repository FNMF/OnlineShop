package com.example.merchantapp.api.merchant;

import com.example.merchantapp.model.ApiResponse;
import com.example.merchantapp.model.auth.AuthResponse;

import retrofit2.Call;
import retrofit2.http.GET;

public interface MerchantApiService {
    @GET("/api/merchant/profile")
    Call<ApiResponse<AuthResponse>> GetMerchantProfile();
}
