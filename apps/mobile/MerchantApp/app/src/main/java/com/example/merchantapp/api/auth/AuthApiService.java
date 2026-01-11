package com.example.merchantapp.api.auth;

import com.example.merchantapp.model.ApiResponse;
import com.example.merchantapp.model.auth.AuthResponse;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.POST;

public interface AuthApiService {
    // 账号密码登录
    @POST("/api/auth/login/shop_admin/account")
    Call<ApiResponse<AuthResponse>> loginByAccount(@Body LoginByAccountRequest body);

    // 验证码登录或注册
    @POST("/api/auth/login/shop_admin/phone")
    Call<ApiResponse<AuthResponse>> loginByValidationCode(@Body LoginByValidationCodeRequest body);

    // Token刷新
    @POST("/api/auth/refresh")
    Call<ApiResponse<AuthResponse>> refreshToken(@Body RefreshTokenRequest body);

    // 申请验证码
    @POST("/api/auth/code/apply")
    Call<ApiResponse<Object>> sendCode(@Body SendCodeRequest body);

    // 验证码TempToken注册
    @POST("/api/auth/register/shop_admin/temp")
    Call<ApiResponse<AuthResponse>> registerByTempToken(@Body RegisterByTempTokenRequest body);
    // 获取管理员信息
    @GET("/api/shop_admin/profile")
    Call<ApiResponse<AuthResponse>> GetMerchantProfile();
}
