package com.example.merchantapp.api.auth;

import android.content.Context;

import com.example.merchantapp.model.auth.LoginResponse;
import com.example.merchantapp.model.auth.RegisterResponse;
import com.example.merchantapp.service.ApiClient;
import com.example.merchantapp.model.ApiResponse;
import com.example.merchantapp.model.auth.AuthResponse;
import com.example.merchantapp.storage.AdminManager;
import com.example.merchantapp.storage.SessionManager;
import com.example.merchantapp.storage.TokenManager;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class AuthRepository {

    private final AuthApiService authApi;     // 带 token
    private final AuthApiService rawAuthApi;  // 不带 token
    private final SessionManager session;

    public AuthRepository() {
        this.authApi = ApiClient.getAuthService();
        this.rawAuthApi = ApiClient.getRawAuthService();
        this.session = SessionManager.get();
    }

    /* ===== 账号密码登录（无 token） ===== */
    public void loginByAccount(String account,
                               String password,
                               Callback<ApiResponse<AuthResponse>> callback) {

        rawAuthApi.loginByAccount(new LoginByAccountRequest(account, password))
                .enqueue(new Callback<ApiResponse<AuthResponse>>() {
                    @Override
                    public void onResponse(
                            Call<ApiResponse<AuthResponse>> call,
                            Response<ApiResponse<AuthResponse>> response
                    ) {
                        if (response.isSuccessful()
                                && response.body() != null
                                && response.body().getData() != null
                                && response.body().getData().getLoginResponse() != null) {

                            // 统一保存登录态
                            session.onLoginSuccess(
                                    response.body().getData().getLoginResponse()
                            );
                        }

                        // 无论成功失败，回调给上层
                        callback.onResponse(call, response);
                    }

                    @Override
                    public void onFailure(Call<ApiResponse<AuthResponse>> call, Throwable t) {
                        callback.onFailure(call, t);
                    }
                });
    }

    /* ===== 验证码登录 / 注册 ===== */
    public void loginByValidationCode(String phone,
                                      String code,
                                      Callback<ApiResponse<AuthResponse>> callback) {

        rawAuthApi.loginByValidationCode(new LoginByValidationCodeRequest(phone, code))
                .enqueue(new Callback<ApiResponse<AuthResponse>>() {
                    @Override
                    public void onResponse(Call<ApiResponse<AuthResponse>> call,
                                           Response<ApiResponse<AuthResponse>> response) {

                        if (!response.isSuccessful() || response.body() == null || response.body().getData() == null) {
                            // 回调给上层
                            callback.onResponse(call, response);
                            return;
                        }

                        AuthResponse body = response.body().getData();

                        if (Boolean.TRUE.equals(body.isNewUser())) {
                            // 新用户 → 保存 TempToken
                            RegisterResponse register = body.getRegisterResponse();
                            if (register != null && register.getTempToken() != null) {
                                TokenManager.saveTempToken(register.getTempToken());
                            }
                        } else {
                            // 老用户 → 保存 AccessToken / RefreshToken / Admin
                            if (response.isSuccessful()
                                    && response.body() != null
                                    && response.body().getData() != null
                                    && response.body().getData().getLoginResponse() != null) {
                                session.onLoginSuccess(body.getLoginResponse());
                            }
                        }

                        // 无论新老用户，回调给上层
                        callback.onResponse(call, response);
                    }

                    @Override
                    public void onFailure(Call<ApiResponse<AuthResponse>> call, Throwable t) {
                        callback.onFailure(call, t);
                    }
                });
    }

    /* ===== 发送验证码（无 token） ===== */
    public void sendCode(String phone, Callback<ApiResponse<Object>> callback) {
        rawAuthApi.sendCode(new SendCodeRequest(phone))
                .enqueue(callback);
    }

    /* ===== 注册设置密码（用 temp token，会自动走 AuthInterceptor） ===== */
    public void registerByTempToken(String password, Callback<ApiResponse<AuthResponse>> callback) {
        String temp = TokenManager.getTempToken();
        authApi.registerByTempToken("Bearer " + temp ,new RegisterByTempTokenRequest(password))
                .enqueue(new Callback<ApiResponse<AuthResponse>>() {
                    @Override
                    public void onResponse(
                            Call<ApiResponse<AuthResponse>> call,
                            Response<ApiResponse<AuthResponse>> response
                    ) {
                        if (response.isSuccessful()
                                && response.body() != null
                                && response.body().getData() != null
                                && response.body().getData().getLoginResponse() != null) {

                            // 统一保存登录态
                            session.onLoginSuccess(
                                    response.body().getData().getLoginResponse()
                            );
                        }

                        // 无论成功失败，回调给上层
                        callback.onResponse(call, response);
                    }

                    @Override
                    public void onFailure(Call<ApiResponse<AuthResponse>> call, Throwable t) {
                        callback.onFailure(call, t);
                    }
                });;
    }

    /* ===== 获取当前用户信息 ===== */
    public void getMerchantProfile(Callback<ApiResponse<AuthResponse>> callback){
        authApi.GetMerchantProfile().enqueue(callback);
    }
}


