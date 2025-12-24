package com.example.merchantapp.api.merchant;

import com.example.merchantapp.api.auth.AuthApiService;
import com.example.merchantapp.model.ApiResponse;
import com.example.merchantapp.model.auth.AuthResponse;
import com.example.merchantapp.service.ApiClient;

import retrofit2.Callback;

public class MerchantRepository {
    private final MerchantApiService authApi;
    public MerchantRepository(){
        this.authApi = ApiClient.getMerchantService();
    }

    public void getMerchantProfile(Callback<ApiResponse<AuthResponse>> callback){
        authApi.GetMerchantProfile().enqueue(callback);
    }
}
