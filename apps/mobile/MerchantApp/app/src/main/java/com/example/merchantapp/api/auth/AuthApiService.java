package com.example.merchantapp.api.auth;

import com.example.merchantapp.model.auth.LoginResponse;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.POST;

public interface AuthApiService {
    // 账号密码登录
    @POST("/api/auth/login/merchant/account")
    Call<LoginResponse> loginByAccount(@Body LoginByAccountRequest body);

    // Token登录
    @POST("api/auth/login/merchant/token")
    Call<LoginResponse> loginByToken(@Body LoginByTokenRequest body);
}
