package com.example.merchantapp.api.auth;

import com.example.merchantapp.api.ApiClient;
import com.example.merchantapp.model.auth.AuthResponse;
import com.example.merchantapp.model.auth.LoginResponse;

import retrofit2.Callback;

public class AuthRepository {

    private final AuthApiService api;

    public AuthRepository() {
        api = ApiClient.getClient().create(AuthApiService.class);
    }

    public void loginByAccount(
            String account,
            String password,
            Callback<AuthResponse> callback
    ) {
        api.loginByAccount(new LoginByAccountRequest(account, password))
                .enqueue(callback);
    }

    public void loginByValidationCode(String phone,
                                      String validationCode,
                                      Callback<AuthResponse> callback){
        api.loginByValidationCode(new LoginByValidationCodeRequest(phone,validationCode))
                .enqueue(callback);
    }
    public void loginByToken(
            String refreshToken,
            Callback<AuthResponse> callback
    ) {
        api.loginByToken(new LoginByTokenRequest(refreshToken))
                .enqueue(callback);
    }
    public void sendCode(String phone,
                         Callback<Void> callback){
        api.sendCode(new SendCodeRequest(phone))
                .enqueue(callback);
    }

    public void registerByTempToken(
            String password,
            Callback<AuthResponse> callback
    ){
        api.registerByTempToken(new RegisterByTempTokenRequest(password))
                .enqueue(callback);
    }
}

