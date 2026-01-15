package com.example.merchantapp.service;

import android.content.Context;

import com.example.merchantapp.api.auth.AuthApiService;
import com.example.merchantapp.api.auth.RefreshTokenRequest;
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
        this.rawAuthApi = ApiClient.getRawAuthService();
    }

    @Override
    public Request authenticate(Route route, Response response) throws IOException {

        if (responseCount(response) >= 2) return null;

        String refresh = TokenManager.getRefreshToken();
        String access = TokenManager.getAccessToken();

        if (refresh == null || access == null) return null;
        retrofit2.Response<ApiResponse<AuthResponse>> resp =
                rawAuthApi.refreshToken("Bearer " + access, refresh).execute();

        if (!resp.isSuccessful() ||
                resp.body() == null ||
                !resp.body().isSuccess()||
                resp.body().getData() == null ||
                resp.body().getData().loginResponse == null
        ) return null;

        LoginResponse login = resp.body().getData().getLoginResponse();
        if (login == null) return null;

        TokenManager.saveAccessToken(login.getAccessToken());
        TokenManager.saveRefreshToken(login.getRefreshToken());

        return response.request().newBuilder()
                .header("Authorization", "Bearer " + login.getAccessToken())
                .build();
    }

    private int responseCount(Response r) {
        int c = 1;
        while ((r = r.priorResponse()) != null) c++;
        return c;
    }
}



