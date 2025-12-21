package com.example.merchantapp.service;

import android.content.Context;

import com.example.merchantapp.api.auth.AuthApiService;
import com.example.merchantapp.api.auth.LoginByTokenRequest;
import com.example.merchantapp.model.ApiResponse;
import com.example.merchantapp.model.auth.AuthResponse;
import com.example.merchantapp.model.auth.LoginResponse;
import com.example.merchantapp.storage.TokenManager;

import java.io.IOException;

import okhttp3.Authenticator;
import okhttp3.Request;
import okhttp3.Response;
import okhttp3.Route;

public class TokenAuthenticator implements Authenticator {

    private final Context context;
    private final AuthApiService rawAuthApi;

    public TokenAuthenticator(Context context) {
        this.context = context.getApplicationContext();
        // 用裸AuthService
        this.rawAuthApi = ApiClient.getRawAuthService();
    }

    @Override
    public Request authenticate(Route route, Response response) throws IOException {

        if (responseCount(response) >= 2) {
            TokenManager.clearAll(context);
            return null;
        }

        String refreshToken = TokenManager.getRefreshToken(context);
        if (refreshToken == null) {
            TokenManager.clearAll(context);
            return null;
        }

        retrofit2.Response<ApiResponse<AuthResponse>> refreshResp =
                rawAuthApi.loginByToken(new LoginByTokenRequest(refreshToken))
                        .execute();

        if (!refreshResp.isSuccessful() || refreshResp.body() == null) {
            TokenManager.clearAll(context);
            return null;
        }

        LoginResponse login = refreshResp.body()
                .getData()
                .getLoginResponse();

        if (login == null) {
            TokenManager.clearAll(context);
            return null;
        }

        TokenManager.saveLogin(
                context,
                login.getAccessToken(),
                login.getRefreshToken(),
                login.getMerchant()
        );

        return response.request()
                .newBuilder()
                .header("Authorization", "Bearer " + login.getAccessToken())
                .build();
    }

    private int responseCount(Response response) {
        int count = 1;
        while ((response = response.priorResponse()) != null) count++;
        return count;
    }
}


