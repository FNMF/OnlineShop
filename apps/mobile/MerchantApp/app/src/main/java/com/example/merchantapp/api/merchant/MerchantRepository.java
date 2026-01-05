package com.example.merchantapp.api.merchant;

import com.example.merchantapp.api.auth.AuthApiService;
import com.example.merchantapp.model.ApiResponse;
import com.example.merchantapp.model.auth.AuthResponse;
import com.example.merchantapp.service.ApiClient;

import retrofit2.Callback;

public class MerchantRepository {
    private final MerchantApiService merchantApi;
    public MerchantRepository(){
        this.merchantApi = ApiClient.getMerchantService();
    }


}
