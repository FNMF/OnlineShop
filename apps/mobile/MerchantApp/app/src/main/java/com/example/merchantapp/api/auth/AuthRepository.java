package com.example.merchantapp.api.auth;

import com.example.merchantapp.service.ApiClient;
import com.example.merchantapp.model.ApiResponse;
import com.example.merchantapp.model.auth.AuthResponse;

import retrofit2.Callback;

public class AuthRepository {

    private final AuthApiService authApi;     // 带 token
    private final AuthApiService rawAuthApi;  // 不带 token

    public AuthRepository() {
        this.authApi = ApiClient.getAuthService();
        this.rawAuthApi = ApiClient.getRawAuthService();
    }

    /* ===== 账号密码登录（无 token） ===== */
    public void loginByAccount(String account,
                               String password,
                               Callback<ApiResponse<AuthResponse>> callback) {

        rawAuthApi.loginByAccount(new LoginByAccountRequest(account, password))
                .enqueue(callback);
    }

    /* ===== 验证码登录 / 注册 ===== */
    public void loginByValidationCode(String phone,
                                      String code,
                                      Callback<ApiResponse<AuthResponse>> callback) {

        rawAuthApi.loginByValidationCode(
                new LoginByValidationCodeRequest(phone, code)
        ).enqueue(callback);
    }

    /* ===== 发送验证码（无 token） ===== */
    public void sendCode(String phone, Callback<ApiResponse<Object>> callback) {
        rawAuthApi.sendCode(new SendCodeRequest(phone))
                .enqueue(callback);
    }

    /* ===== 注册设置密码（用 temp token，会自动走 AuthInterceptor） ===== */
    public void registerByTempToken(String password,
                                    Callback<ApiResponse<AuthResponse>> callback) {

        authApi.registerByTempToken(new RegisterByTempTokenRequest(password))
                .enqueue(callback);
    }

    /* ===== refresh token（不要在 Repository 用，交给 Authenticator） ===== */
}


