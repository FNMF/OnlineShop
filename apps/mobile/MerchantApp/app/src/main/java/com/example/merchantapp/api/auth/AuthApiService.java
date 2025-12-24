package com.example.merchantapp.api.auth;

import com.example.merchantapp.model.ApiResponse;
import com.example.merchantapp.model.auth.AuthResponse;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.POST;

public interface AuthApiService {
    // 账号密码登录
    @POST("/api/auth/login/merchant/account")
    Call<ApiResponse<AuthResponse>> loginByAccount(@Body LoginByAccountRequest body);

    // 验证码登录或注册
    @POST("/api/auth/login/merchant/phone")
    Call<ApiResponse<AuthResponse>> loginByValidationCode(@Body LoginByValidationCodeRequest body);

    // Token刷新
    @POST("/api/auth/login/merchant/token")
    Call<ApiResponse<AuthResponse>> refreshToken(@Body RefreshTokenRequest body);

    // 申请验证码
    @POST("/api/auth/code/apply")
    Call<ApiResponse<Object>> sendCode(@Body SendCodeRequest body);

    // 验证码TempToken注册
    @POST("/api/auth/register/merchant/temp")
    Call<ApiResponse<AuthResponse>> registerByTempToken(@Body RegisterByTempTokenRequest body);
}
