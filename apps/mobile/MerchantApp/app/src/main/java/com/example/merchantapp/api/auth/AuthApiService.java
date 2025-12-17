package com.example.merchantapp.api.auth;

import com.example.merchantapp.model.auth.LoginResponse;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.POST;

public interface AuthApiService {
    // 账号密码登录
    @POST("/api/auth/login/merchant/account")
    Call<LoginResponse> loginByAccount(@Body LoginByAccountRequest body);

    // 验证码登录或注册
    @POST("/api/auth/login/merchant/phone")
    Call<LoginResponse> loginByValidationCode(@Body LoginByValidationCodeRequest body);

    // Token登录
    @POST("api/auth/login/merchant/token")
    Call<LoginResponse> loginByToken(@Body LoginByTokenRequest body);

    // 申请验证码
    @POST("/api/auth/code/apply")
    Call<Void> sendCode(@Body SendCodeRequest body);
}
